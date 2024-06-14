using TesteTecnicoDiscord.Application.Interfaces.Services.Generic;
using TesteTecnicoDiscord.Infra.Interfaces.Generic;

namespace TesteTecnicoDiscord.Application.Services;

public class GenericService<T>(IRepository<T> repository) : IGenericService<T>
{
    public async Task<List<T>> GetAll()
    {
        return await repository.GetAll();
    }
    public async Task<T> GetById(Guid id)
    {
        return await repository.GetById(id);
    }
    public async Task<T> Add(T entity)
    {
        return await repository.Add(entity);
    }

    public async Task<T> Update(T entity)
    {
        return await repository.Update(entity);
    }

    public async Task Delete(Guid id)
    {
        await repository.Delete(id);
    }
}