using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using Reseptit.Models;

namespace Reseptit.Repositories;

public class MongoRepository<TModel> : IRepository<TModel> where TModel : Model, new()
{
    private readonly IMongoCollection<TModel> _collection;
    private readonly ILogger<TModel> _logger;

    public MongoRepository(MongoClient client, MongoDbSettings settings, ILogger<TModel> logger)
    {
        _logger = logger;
        _collection = client.GetDatabase(settings.DatabaseName)
            .GetCollection<TModel>(GetCollectionName(typeof(TModel)));
    }

    private protected string GetCollectionName(Type documentType)
    {
        return typeof(TModel).Name;
    }

    public virtual IQueryable<TModel> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public virtual IEnumerable<TModel> FilterBy(
        Expression<Func<TModel, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).ToEnumerable();
    }

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TModel, bool>> filterExpression,
        Expression<Func<TModel, TProjected>> projectionExpression)
    {
        return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }

    public virtual TModel FindOne(Expression<Func<TModel, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).FirstOrDefault();
    }

    public virtual Task<TModel> FindOneAsync(Expression<Func<TModel, bool>> filterExpression)
    {
        return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
    }

    public virtual TModel FindById(string id)
    {
        var guid = Guid.Parse(id);
        var filter = Builders<TModel>.Filter.Eq(doc => doc.Id, guid);
        return _collection.Find(filter).SingleOrDefault();
    }

    public virtual Task<TModel> FindByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var guid = Guid.Parse(id);
            var filter = Builders<TModel>.Filter.Eq(doc => doc.Id, guid);
            return _collection.Find(filter).SingleOrDefaultAsync();
        });
    }


    public virtual void InsertOne(TModel document)
    {
        _collection.InsertOne(document);
    }

    public virtual Task InsertOneAsync(TModel document)
    {
        return Task.Run(() => _collection.InsertOneAsync(document));
    }

    public void InsertMany(ICollection<TModel> documents)
    {
        _collection.InsertMany(documents);
    }


    public virtual async Task InsertManyAsync(ICollection<TModel> documents)
    {
        await _collection.InsertManyAsync(documents);
    }

    public void ReplaceOne(TModel document)
    {
        var filter = Builders<TModel>.Filter.Eq(doc => doc.Id, document.Id);
        _collection.FindOneAndReplace(filter, document);
    }

    public virtual async Task ReplaceOneAsync(TModel document)
    {
        var filter = Builders<TModel>.Filter.Eq(doc => doc.Id, document.Id);
        await _collection.FindOneAndReplaceAsync(filter, document);
    }

    public void DeleteOne(Expression<Func<TModel, bool>> filterExpression)
    {
        _collection.FindOneAndDelete(filterExpression);
    }

    public Task DeleteOneAsync(Expression<Func<TModel, bool>> filterExpression)
    {
        return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
    }

    public void DeleteById(string id)
    {
        var guid = Guid.Parse(id);
        var filter = Builders<TModel>.Filter.Eq(doc => doc.Id, guid);
        _collection.FindOneAndDelete(filter);
    }

    public Task DeleteByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var guid = Guid.Parse(id);
            var filter = Builders<TModel>.Filter.Eq(doc => doc.Id, guid);
            _collection.FindOneAndDeleteAsync(filter);
        });
    }

    public void DeleteMany(Expression<Func<TModel, bool>> filterExpression)
    {
        _collection.DeleteMany(filterExpression);
    }

    public Task DeleteManyAsync(Expression<Func<TModel, bool>> filterExpression)
    {
        return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
    }
}