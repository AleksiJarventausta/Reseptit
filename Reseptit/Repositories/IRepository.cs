using System.Linq.Expressions;
using Reseptit.Models;

namespace Reseptit.Repositories;

public interface IRepository<TModel> where TModel: Model, new()
{    IQueryable<TModel> AsQueryable();
 
     IEnumerable<TModel> FilterBy(
         Expression<Func<TModel, bool>> filterExpression);
 
     IEnumerable<TProjected> FilterBy<TProjected>(
         Expression<Func<TModel, bool>> filterExpression,
         Expression<Func<TModel, TProjected>> projectionExpression);
 
     TModel FindOne(Expression<Func<TModel, bool>> filterExpression);
 
     Task<TModel> FindOneAsync(Expression<Func<TModel, bool>> filterExpression);
 
     TModel FindById(string id);
 
     Task<TModel> FindByIdAsync(string id);
 
     void InsertOne(TModel document);
 
     Task InsertOneAsync(TModel document);
 
     void InsertMany(ICollection<TModel> documents);
 
     Task InsertManyAsync(ICollection<TModel> documents);
 
     void ReplaceOne(TModel document);
 
     Task ReplaceOneAsync(TModel document);
 
     void DeleteOne(Expression<Func<TModel, bool>> filterExpression);
 
     Task DeleteOneAsync(Expression<Func<TModel, bool>> filterExpression);
 
     void DeleteById(string id);
 
     Task DeleteByIdAsync(string id);
 
     void DeleteMany(Expression<Func<TModel, bool>> filterExpression);
 
     Task DeleteManyAsync(Expression<Func<TModel, bool>> filterExpression);
    
}