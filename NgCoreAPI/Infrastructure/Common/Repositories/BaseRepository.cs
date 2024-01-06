using Infrastructure.Common.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Common.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly NgCoreAppContext _context;
    private readonly DbSet<T> _dbset;
    private readonly IHttpContextAccessor _httpContext;

    //private IAuthHelper _authHelper;
    //public IUnitOfWork UnitOfWork => throw new NotImplementedException();
    IHttpContextAccessor _httpContextAccessor;
    public BaseRepository(NgCoreAppContext context)
    {
        _context = context;
        _dbset = context.Set<T>();
        //_httpContext = httpContext;
        //_httpContextAccessor = new HttpContextAccessor();
        // _authHelper = authHelper;
    }
    public Guid _currentUserId { get { return Guid.NewGuid(); } }
    /*public Guid _currentUserId { get { return CurrentUserId; } }*/
    //public Guid CurrentUserId
    //{
    //    get
    //    {
    //        string accessToken = string.Empty;
    //        var authHeader = _httpContextAccessor.HttpContext.Request.Headers.ToList().Find(x => x.Key == "Authorization");

    //        if (!StringValues.IsNullOrEmpty(authHeader.Value))
    //        {
    //            accessToken = authHeader.Value.ToString().Replace("Bearer ", "");
    //        }
    //        var data = _httpContextAccessor.HttpContext.User.Claims.ToList();
    //        var headers = _httpContextAccessor.HttpContext.Request.Headers.ToList().Find(x => x.Key == "Authorization");
    //        var jwt = headers.Value.ToString().Replace("Bearer ", "");
    //        var handler = new JwtSecurityTokenHandler();
    //        var token = handler.ReadJwtToken(accessToken);
    //        //var email = token.Claims.FirstOrDefault(x => x.Type == "upn").Value.;
    //        var email = "tonmoy.chowdhury@petronas.com";

    //        var userInfo = _context.UserProfiles.Where(x => x.IsDeleted == false && x.Email.Replace(".my", "") == email.Replace(".my", "")).AsNoTracking().FirstOrDefault();


    //        return userInfo.Id;
    //    }
    //}
    #region filter
    public IQueryable<T> Filter(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool isDisableTracking = false)
    {
        IQueryable<T> query = _context.Set<T>();

        if (isDisableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return query;
    }
    #endregion

    #region find and get
    public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
    {
        return _dbset.Where(predicate);
    }

    public IQueryable<T> GetAll(bool disabledTracking)
    {
        IQueryable<T> query = _dbset;
        if (disabledTracking) query = query.AsNoTracking();
        return query.AsQueryable();
    }
    public IQueryable<T> GetAllNoneDeleted(bool disabledTracking = false)
    {
        IQueryable<T> query = _dbset.Where(x => x.IsDeleted == false);
        if (disabledTracking) query = query.AsNoTracking();
        return query.AsQueryable();
    }


    public T GetSingle(Expression<Func<T, bool>> predicate)
    {
        return _dbset.FirstOrDefault(predicate);
    }

    public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbset;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return query.Where(predicate).FirstOrDefault();
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {

        IQueryable<T> query = _dbset;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbset.FirstOrDefaultAsync(predicate);
    }
    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool disableTracking)
    {
        //var query = _dbset.Where(x => x.Id == x.Id);
        var query = _dbset.AsQueryable();
        if (disableTracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(predicate);
    }

    public T GetSingleNoneDeleted(Expression<Func<T, bool>> predicate)
    {
        var query = _dbset.Where(x => x.IsDeleted == false);
        return query.Where(predicate).FirstOrDefault();
    }

    public async Task<T> GetSingleNoneDeletedAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbset.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(predicate);
    }
    #endregion

    #region add
    public void Add(T entity)
    {
        //entity.Id = Guid.NewGuid();
        /*
        entity.CreatedById = _currentUserId;
        entity.CreatedDate = DateTime.Now;*/
        _context.Add(entity);
    }
    public void Add(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    #endregion


    #region count
    public int Count()
    {
        return _dbset.Count();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbset.CountAsync(predicate);
    }
    #endregion


    #region delete
    public void Delete(T entity, bool isHardDelete = false)
    {
        if (isHardDelete)
        {
            _context.Entry<T>(entity).State = EntityState.Deleted;
            return;
        }
        entity.IsDeleted = true;
        entity.DeletedById = _currentUserId;// entity.DeletedByUserId.HasValue == false || entity.DeletedByUserId.Value.ToString().Length <= 0 ? this.CurrentUserId : entity.DeletedByUserId;
        entity.DeletedDate = DateTime.Now;
        _context.Entry<T>(entity).State = EntityState.Modified;
    }

    public void Delete(Expression<Func<T, bool>> predicate, bool isHardDelete = false)
    {
        IList<T> entities = _dbset.Where(predicate).ToList();
        if (entities.Any())
        {
            if (isHardDelete)
            {
                foreach (var entity in entities)
                {
                    _context.Entry<T>(entity).State = EntityState.Deleted;
                }
                return;
            }
            var now = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletedById = _currentUserId;
                entity.DeletedDate = now;
                _context.Entry<T>(entity).State = EntityState.Modified;
            }
        }
    }
    public async Task<bool> InstantDelete(T entity, bool isHardDelete = false)
    {
        try
        {
            if (isHardDelete)
            {
                await _dbset.Where(x => x.Id == entity.Id).ExecuteDeleteAsync();
                return true;
            }
            var now = DateTime.Now;
            await _dbset.Where(x => x.Id == entity.Id).ExecuteUpdateAsync<T>(x => x
                                                            .SetProperty(p => p.IsDeleted, p => true)
                                                            .SetProperty(p => p.DeletedById, p => _currentUserId)
                                                            .SetProperty(p => p.DeletedDate, p => now));
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> InstantDelete(Expression<Func<T, bool>> predicate, bool isHardDelete = false)
    {
        try
        {
            if (isHardDelete)
            {
                await _dbset.Where(predicate).ExecuteDeleteAsync();
                return true;
            }
            var now = DateTime.Now;
            await _dbset.Where(predicate).ExecuteUpdateAsync<T>(x => x
                                                            .SetProperty(p => p.IsDeleted, p => true)
                                                            .SetProperty(p => p.DeletedById, p => _currentUserId)
                                                            .SetProperty(p => p.DeletedDate, p => now));
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    #endregion


    #region update

    public void Update(T entity)
    {
        entity.ModifiedById = _currentUserId;
        entity.ModifiedDate = DateTime.Now;

        _context.Entry<T>(entity).State = EntityState.Modified;

    }

    /*public async Task<bool> Update(Expression<Func<T, bool>> predicate, Dictionary<string, object> paramValues)
    {
        var paramValuesList = paramValues.ToList();
        await _dbset.Where(predicate).ExecuteUpdateAsync(x => x
                                                                .SetProperty(p => p.GetType().GetProperty(paramValuesList[0].Key), p => paramValuesList[0].Value)
                                                                .SetProperty(p => p.DeletedById, p => _currentUserId)
                                                                .SetProperty(p => p.DeletedDate, p => now));
        return true;
    }*/
    #endregion
}
