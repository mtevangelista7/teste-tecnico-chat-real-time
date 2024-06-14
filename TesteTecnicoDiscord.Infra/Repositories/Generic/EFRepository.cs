using Microsoft.EntityFrameworkCore;
using TesteTecnicoDiscord.Domain.Entities.Base;
using TesteTecnicoDiscord.Infra.Data.Context;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Infra.Repositories.Generic;

public class EFRepository<T> : IRepository<T> where T : EntityBase
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    // ATENÇÃO ISSO AQUI PRECISA SER PUBLIC NÃO CONFIA NO RIDER
    public EFRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<List<T>> GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Entity not found");
    }

    public async Task<T> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(Guid id)
    {
        _dbSet.Remove(await GetById(id));
        await _context.SaveChangesAsync();
    }
}