using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TheLastHotel.Repository.Database.Interfaces;

namespace TheLastHotel.Repository.Database
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoContext context;
        protected readonly IMongoCollection<TEntity> DbSet;
        protected readonly int SleepTime = 1000;
        protected readonly int MaxAttempts  = 3;

        protected BaseRepository(IMongoContext context)
        {
            this.context = context;
            DbSet = this.context.GetCollection<TEntity>(typeof(TEntity).Name);
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SleepTimeRepository")))
                SleepTime = int.Parse(Environment.GetEnvironmentVariable("SleepTimeRepository"));

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MaxAttemptsRepository")))
                MaxAttempts = int.Parse(Environment.GetEnvironmentVariable("MaxAttemptsRepository"));

        }

        public async Task Add(TEntity obj)
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                await DbSet.InsertOneAsync(obj);
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                return (await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id))).SingleOrDefault();
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<TEntity> GetById(string id)
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                return (await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id)))).SingleOrDefault();
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<TProjection> GetById<TProjection>(string id, ProjectionDefinition<TEntity, TProjection> projection) where TProjection : class
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                if (projection != null)
                {
                    var options = new FindOptions<TEntity, TProjection> { Projection = projection };
                    return (await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id)), options: options)).SingleOrDefault();
                }
                else
                    return default;
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<TEntity> GetById(ObjectId id)
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                return (await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id))).SingleOrDefault();
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                return await (await DbSet.FindAsync(Builders<TEntity>.Filter.Empty)).ToListAsync();
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<IEnumerable<TProjection>> GetAll<TProjection>(ProjectionDefinition<TEntity, TProjection> projection) where TProjection : class
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                if (projection != null)
                {
                    var options = new FindOptions<TEntity, TProjection> { Projection = projection };
                    return await (await DbSet.FindAsync(Builders<TEntity>.Filter.Empty, options: options)).ToListAsync();
                }
                else
                    return default;
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<long> Count(List<Expression<Func<TEntity, bool>>> filter)
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                if (filter.Any())
                    return await DbSet.CountDocumentsAsync(GetCombinedFilters(filter));

                return await DbSet.CountDocumentsAsync(Builders<TEntity>.Filter.Empty);
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TEntity>> GetByFilters(List<Expression<Func<TEntity, bool>>> filter, SortDefinition<TEntity> orderBy = null)
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                FindOptions<TEntity> options = null;
                if (orderBy != null)
                    options = new FindOptions<TEntity> { Sort = orderBy };
                else
                    options = new FindOptions<TEntity> { };


                if (filter.Any())
                    return await (await DbSet.FindAsync(GetCombinedFilters(filter), options: options)).ToListAsync();

                return await (await DbSet.FindAsync(Builders<TEntity>.Filter.Empty, options: options)).ToListAsync();
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TProjection>> GetByFilters<TProjection>(List<Expression<Func<TEntity, bool>>> filter, ProjectionDefinition<TEntity, TProjection> projection, SortDefinition<TEntity> orderBy = null) where TProjection : class
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                FindOptions<TEntity, TProjection> options = null;
                if (orderBy != null)
                    options = new FindOptions<TEntity, TProjection> { Projection = projection, Sort = orderBy };
                else
                    options = new FindOptions<TEntity, TProjection> { Projection = projection };


                if (filter.Any())
                    return await (await DbSet.FindAsync(GetCombinedFilters(filter), options: options)).ToListAsync();

                return await (await DbSet.FindAsync(Builders<TEntity>.Filter.Empty, options: options)).ToListAsync();
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Expression<Func<TEntity, bool>> GetCombinedFilters(IEnumerable<Expression<Func<TEntity, bool>>> filters)
        {
            Expression<Func<TEntity, bool>> combinedFilter = filters.First();
            var filtersArray = filters.ToArray();
            for (int i = 1; i < filtersArray.Length; i++)
                combinedFilter = combinedFilter.And(filtersArray[i]);

            return combinedFilter;

        }
        public async Task Update(TEntity obj)
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                var builder = Builders<TEntity>.Filter;
                var filtro = builder.Eq("_id", ObjectId.Parse(obj.GetId().ToString()));

                await DbSet.ReplaceOneAsync(filtro, obj);
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Dictionary<Expression<Func<TEntity, object>>, object> fieldsUpdate, List<Expression<Func<TEntity, bool>>> filter)
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                var update = Builders<TEntity>.Update;
                var updates = new List<UpdateDefinition<TEntity>>();
                foreach (var item in fieldsUpdate)
                {
                    updates.Add(update.Set(item.Key, item.Value));
                }

                await DbSet.UpdateOneAsync(GetCombinedFilters(filter), update.Combine(updates));
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateManyAsync(IEnumerable<TEntity> obj)
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                var updates = new List<WriteModel<TEntity>>();
                var builder = Builders<TEntity>.Filter;

                foreach (var doc in obj)
                {
                    var filtro = builder.Eq("_id", ObjectId.Parse(obj.GetId().ToString()));
                    updates.Add(new ReplaceOneModel<TEntity>(filtro, doc));
                }

                await DbSet.BulkWriteAsync(updates);
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Paged<TEntity>> GetByFiltersPaged(List<Expression<Func<TEntity, bool>>> filter, int Paged, int take, object orderBy = null)
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                IFindFluent<TEntity, TEntity> query;
                var dataResult = new Paged<TEntity>
                {
                    Take = take,
                    Skip = 0
                };

                if (Paged > 1)
                    dataResult.Skip = (Paged - 1) * take;


                if (filter.Any())
                {
                    if (orderBy == null)
                        query = DbSet.Find(GetCombinedFilters(filter)).Skip(dataResult.Skip).Limit(take);
                    else
                        query = DbSet.Find(GetCombinedFilters(filter)).Skip(dataResult.Skip).Limit(take).Sort(JsonConvert.SerializeObject(orderBy));
                }
                else
                {
                    if (orderBy == null)
                        query = DbSet.Find(x => true).Skip(dataResult.Skip).Limit(take);
                    else
                        query = DbSet.Find(x => true).Skip(dataResult.Skip).Limit(take).Sort(JsonConvert.SerializeObject(orderBy));
                }

                if (filter.Any())
                    dataResult.TotalItemCount = await DbSet.CountDocumentsAsync(GetCombinedFilters(filter));
                else
                    dataResult.TotalItemCount = await DbSet.CountDocumentsAsync(Builders<TEntity>.Filter.Empty);

                dataResult.Content = await query.ToListAsync();

                return dataResult;
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Paged<TProjection>> GetByFiltersPaged<TProjection>(List<Expression<Func<TEntity, bool>>> filter, int Paged, int take, ProjectionDefinition<TEntity, TProjection> projection, object orderBy = null) where TProjection : class
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                IFindFluent<TEntity, TProjection> query;
                var dataResult = new Paged<TProjection>
                {
                    Take = take,
                    Skip = 0
                };

                if (Paged > 1)
                    dataResult.Skip = (Paged - 1) * take;


                if (filter.Any())
                {
                    if (orderBy == null)
                        query = DbSet.Find(GetCombinedFilters(filter)).Project(projection).Skip(dataResult.Skip).Limit(take);
                    else
                        query = DbSet.Find(GetCombinedFilters(filter)).Project(projection).Skip(dataResult.Skip).Limit(take).Sort(JsonConvert.SerializeObject(orderBy));
                }
                else
                {
                    if (orderBy == null)
                        query = DbSet.Find(x => true).Project(projection).Skip(dataResult.Skip).Limit(take);
                    else
                        query = DbSet.Find(x => true).Project(projection).Skip(dataResult.Skip).Limit(take).Sort(JsonConvert.SerializeObject(orderBy));
                }

                if (filter.Any())
                    dataResult.TotalItemCount = await DbSet.CountDocumentsAsync(GetCombinedFilters(filter));
                else
                    dataResult.TotalItemCount = await DbSet.CountDocumentsAsync(Builders<TEntity>.Filter.Empty);

                dataResult.Content = await query.ToListAsync();

                return dataResult;
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Remove(string id)
        {
            int currentAttempt = 0;
        TryAgain:
            try
            {
                currentAttempt++;
                var builder = Builders<TEntity>.Filter;
                var filtro = builder.Eq("_id", ObjectId.Parse(id));

                await DbSet.DeleteOneAsync(filtro);
            }
            catch (MongoCommandException ex)
            {
                if (IsPossibleRetry(ex.Code, currentAttempt))
                {
                    Thread.Sleep(SleepTime);
                    goto TryAgain;
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsPossibleRetry(int errorCode, int currentAttempt)
        {
            return Enum.IsDefined(typeof(EnumMongoErrorCode), errorCode)
                && (currentAttempt != MaxAttempts);

        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
