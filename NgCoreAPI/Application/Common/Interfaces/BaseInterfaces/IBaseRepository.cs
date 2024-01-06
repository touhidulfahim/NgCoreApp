using System.Linq.Expressions;

namespace Infrastructure.Common.Interfaces;

public interface IBaseRepository<T> where T : class
{
    #region find and get
    IQueryable<T> GetAll(bool disabledTracking = false);
    IQueryable<T> GetAllNoneDeleted(bool disabledTracking = false);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate,
       params Expression<Func<T, object>>[] includeProperties);
    T GetSingleNoneDeleted(Expression<Func<T, bool>> predicate);
    T GetSingle(Expression<Func<T, bool>> predicate);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool disableTracking);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
    T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
    IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
    Task<T> GetSingleNoneDeletedAsync(Expression<Func<T, bool>> predicate);
    #endregion

    #region count 
    int Count();
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    #endregion


    #region add
    void Add(T entity);
    void Add(IEnumerable<T> entities);
    #endregion

    #region update 
    void Update(T entity);
    /*void Update(Expression<Func<T, bool>> predicate);*/
    //void Update(IEnumerable<T> entities);
    #endregion

    #region delete
    void Delete(T entity, bool isHardDelete = false);
    void Delete(Expression<Func<T, bool>> predicate, bool isHardDelete = false);
    Task<bool> InstantDelete(T entity, bool isHardDelete = false);
    Task<bool> InstantDelete(Expression<Func<T, bool>> predicate, bool isHardDelete = false);
    #endregion

    #region filter       
    //IQueryable<T> Filter(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool isDisableTracking = false);
    #endregion

}
