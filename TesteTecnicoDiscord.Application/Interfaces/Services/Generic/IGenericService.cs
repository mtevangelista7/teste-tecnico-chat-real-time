namespace TesteTecnicoDiscord.Application.Interfaces.Services.Generic;

public interface IGenericService<T>
{
    Task<List<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task Delete(Guid id);
}