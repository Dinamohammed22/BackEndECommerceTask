using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace KOG.ECommerce.Data;

public class Repository<T> where T : BaseModel, new()
{
    readonly DbSet<T> dbSet;
    readonly DbContext _context;
    readonly UserState _userState;

    protected string[] defaultExcludedEditProperties = new string[]
        { nameof(BaseModel.ID), nameof(BaseModel.CreatedBy), nameof(BaseModel.CreatedDate), nameof(BaseModel.Deleted) };

    protected string[] defaultExcludedDeleteProperties = new string[]
        { nameof(BaseModel.ID), nameof(BaseModel.CreatedBy), nameof(BaseModel.CreatedDate) };

    public Repository(Entities context, UserState userState)
    {
        _context = context;
        _userState = userState;
        dbSet = _context.Set<T>();
    }
    public T Add(T entity)
    {
      // entity.ID = Guid.NewGuid().ToString();
        entity.CreatedBy = _userState.UserID;
        entity.CreatedDate = DateTime.Now;
        entity.UpdatedBy = _userState.UserID;
        entity.UpdatedDate = DateTime.Now;

        dbSet.Add(entity);

        return entity;
    }
    public async Task<T> AddAsync(T entity)
    {
        entity.ID = Guid.NewGuid().ToString();
        entity.CreatedBy = _userState.UserID;
        entity.CreatedDate = DateTime.Now;
        entity.UpdatedBy = _userState.UserID;
        entity.UpdatedDate = DateTime.Now;

        await dbSet.AddAsync(entity).ConfigureAwait(false);

        return entity;
    }
    public IEnumerable<T> AddRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            entity.ID = Guid.NewGuid().ToString();
            entity.CreatedBy = _userState.UserID;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = _userState.UserID;
            entity.UpdatedDate = DateTime.Now;

            dbSet.Add(entity);
        }

        return entities;
    }

    public bool IsDeleted(T entity)
    {
        return GetWithDeleted().FirstOrDefault(e => e.ID == entity.ID)?.Deleted ?? true;
    }
    public void Delete(T entity)
    {
        entity.Deleted = true;
        SaveIncluded(entity, nameof(entity.Deleted), nameof(entity.UpdatedBy), nameof(entity.UpdatedDate));
    }
    public void Delete(string uID)
    {
        T entity = new() { ID = uID };
        Delete(entity);
    }

    public IQueryable<T> Get()
    {
        return dbSet.Where(entity => !entity.Deleted);
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
    {
        return Get().Where(predicate);
    }
    public IQueryable<T> GetAsSplit(Expression<Func<T, bool>> predicate)
    {
        return Get().Where(predicate).AsSplitQuery();
    }
    public int BulkHardDelete(Expression<Func<T, bool>> predicate)
    {
        return GetWithDeleted().Where(predicate).ExecuteDelete();
    }
    public void BulkSoftDelete(List<T> entities)
    {
        foreach (var entity in entities)
        {
            Delete(entity);
        }
    }
    public void BulkSoftDelete(List<String> IDs)
    {
        foreach (var ID in IDs)
        {
            Delete(ID);
        }
    }
    public int BulkUpdate(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
    {
        return Get().ExecuteUpdate(setPropertyCalls);
    }

    public T GetByID(string ID)
    {
        return Get().FirstOrDefault(item => item.ID == ID);
    }

    public async Task<T> GetByIDAsync(string ID)
    {
        return await Get().FirstOrDefaultAsync(item => item.ID == ID);
    }
    public IQueryable<T> GetWithDeleted()
    {
        return dbSet;
    }
    public bool ExistsLocal(T entity)
    {
        return dbSet.Local.Any(e => e == entity);
    }
    public T FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        return Get().Where(predicate).FirstOrDefault();
    }
    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await Get().Where(predicate).FirstOrDefaultAsync();
    }

    public bool Any(Expression<Func<T, bool>> predicate)
    {
        return Get().Any(predicate);
    }
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await Get().AnyAsync(predicate);
    }

    public void Update(T entity)
    {
        if (string.IsNullOrEmpty(entity.ID))
            return;

        SaveExcluded(entity, defaultExcludedEditProperties);
    }

    public virtual void SaveIncluded(T entity, params string[] properties)
    {
        var local = dbSet.Local.FirstOrDefault(entry => entry.ID == entity.ID);

        EntityEntry entry;

        if (local is null)
            entry = _context.Entry(entity);
        else
            entry = _context.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity.ID == entity.ID);

        foreach (var prop in entry.Properties)
        {
            if (properties.Contains(prop.Metadata.Name))
            {
                prop.CurrentValue = entity.GetType().GetProperty(prop.Metadata.Name).GetValue(entity);
                prop.IsModified = true;
            }
        }

        entity.UpdatedBy = _userState.UserID;
        entity.UpdatedDate = DateTime.Now;
    }

    public virtual async Task SaveIncludedAsync(T entity, params string[] properties)
    {
        var local = dbSet.Local
            .FirstOrDefault(entry => entry.ID == entity.ID);

        EntityEntry entry;

        if (local is null)
            entry = _context.Entry(entity);
        else
            entry = _context.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity.ID == entity.ID);

        foreach (var prop in entry.Properties)
        {
            if (properties.Contains(prop.Metadata.Name))
            {
                prop.CurrentValue = entity.GetType().GetProperty(prop.Metadata.Name).GetValue(entity);
                prop.IsModified = true;
            }
        }

        entity.UpdatedBy = _userState.UserID;
        entity.UpdatedDate = DateTime.Now;

        await Task.CompletedTask;
    }
    public virtual void SaveExcluded(T entity, params string[] properties)
    {
        if (string.IsNullOrEmpty(entity.ID))
            return;

        List<string> excludedProperties = properties.ToList();
        excludedProperties.Add(nameof(BaseModel.ID));
        excludedProperties.Add(nameof(BaseModel.CreatedBy));
        excludedProperties.Add(nameof(BaseModel.CreatedDate));
        excludedProperties.Add(nameof(BaseModel.Deleted));

        RemoveIfAttachedToContext(entity);

        entity.UpdatedBy = _userState.UserID;
        entity.UpdatedDate = DateTime.Now;

        var entry = _context.Entry(entity);
        foreach (var prop in entry.Properties)
        {
            if (excludedProperties.Contains(prop.Metadata.Name))
                prop.IsModified = false;
            else
                prop.IsModified = true;
        }
    }

    public IQueryable<T> GetWithTracking(Expression<Func<T, bool>> predicate)
    {
        return Get().Where(predicate).AsTracking();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    private void RemoveIfAttachedToContext(T entity)
    {
        var local = dbSet.Local
            .FirstOrDefault(entry => entry.ID.Equals(entity.ID));

        if (local != null)
        {
            _context.Entry(local).State = EntityState.Detached;
        }
    }
}