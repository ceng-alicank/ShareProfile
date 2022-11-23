namespace Application.Interfaces.Caching
{
    public interface ICacheService
    {
        Task SetCache(string key, byte[] serializeData, CancellationToken CancellationToken);
        Task RemoveCache(string key , CancellationToken cancellationToken);
        Task<byte[]> GetCache(string key , CancellationToken cancellationToken);
    }
}
