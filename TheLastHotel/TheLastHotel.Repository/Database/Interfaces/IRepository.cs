using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TheLastHotel.Repository.Database.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Add(TEntity obj);
        Task Update(TEntity obj);
        Task Update(Dictionary<Expression<Func<TEntity, object>>, object> fieldsUpdate, List<Expression<Func<TEntity, bool>>> filter);
        Task UpdateManyAsync(IEnumerable<TEntity> obj);
        Task Remove(string id);

        Task<TEntity> GetById(string id);
        Task<TEntity> GetById(Guid id);
        Task<TProjection> GetById<TProjection>(string id, ProjectionDefinition<TEntity, TProjection> projection) where TProjection : class;
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TProjection>> GetAll<TProjection>(ProjectionDefinition<TEntity, TProjection> projection) where TProjection : class;
        Task<long> Count(List<Expression<Func<TEntity, bool>>> filter);

        Task<List<TEntity>> GetByFilters(List<Expression<Func<TEntity, bool>>> filter, SortDefinition<TEntity> orderBy = null);
        Task<List<TProjection>> GetByFilters<TProjection>(List<Expression<Func<TEntity, bool>>> filter, ProjectionDefinition<TEntity, TProjection> projection, SortDefinition<TEntity> orderBy = null) where TProjection : class;
        
        Task<Paged<TEntity>> GetByFiltersPaged(List<Expression<Func<TEntity, bool>>> filter, int page, int take, object orderBy = null);
        Task<Paged<TProjection>> GetByFiltersPaged<TProjection>(List<Expression<Func<TEntity, bool>>> filter, int page, int take, ProjectionDefinition<TEntity, TProjection> projection, object orderBy = null) where TProjection : class;
    }
}
