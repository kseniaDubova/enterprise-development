namespace Polyclinic.Domain.Repositories;
/// <summary>
/// Интерфейс репозитория
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IRepository<T, TKey>
{
    /// <summary>
    /// Вернуть все элементы
    /// </summary>
    /// <returns></returns>
    public Task<List<T>> GetAll();

    /// <summary>
    /// Вернуть элемент по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<T?> Get(TKey id);

    /// <summary>
    /// Удалить элемент по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> Delete(TKey id);

    /// <summary>
    /// Добавить элемент
    /// </summary>
    /// <param name="newObj"></param>
    public Task Post(T newObj);

    /// <summary>
    /// Изменить элемент по идентификатору
    /// </summary>
    /// <param name="newObj"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> Put(T newObj, TKey id);
}
