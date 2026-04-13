using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;

public class AppTicketStore : ITicketStore
{
  private readonly IDistributedCache _cache;

  public AppTicketStore(IDistributedCache cache)
  {
    _cache = cache;
  }

  public async Task RemoveAsync(string key)
  {
    await _cache.RemoveAsync(key);
  }

  public async Task RenewAsync(string key, AuthenticationTicket ticket)
  {
    var options = new DistributedCacheEntryOptions
    {
      AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2) // Set the expiration time as needed
    };

    var serializedTicket = SerializeToBytes(ticket);
    await _cache.SetAsync(key, serializedTicket, options);
  }

  public async Task<AuthenticationTicket?> RetrieveAsync(string key)
  {
    var serializedTicket = await _cache.GetAsync(key);
    if (serializedTicket == null)
    {
      return null;
    }

    return DeserializeFromBytes(serializedTicket);
  }

  public async Task<string> StoreAsync(AuthenticationTicket ticket)
  {
    var key = $"{Guid.NewGuid().ToString()}:{ticket.Principal.Identity?.Name}";
    var options = new DistributedCacheEntryOptions
    {
      AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2) // Set the expiration time as needed
    };

    var serializedTicket = SerializeToBytes(ticket);
    await _cache.SetAsync(key, serializedTicket, options);

    return key;
  }

  private byte[] SerializeToBytes(AuthenticationTicket ticket)
  {
    return TicketSerializer.Default.Serialize(ticket);
  }

  private AuthenticationTicket? DeserializeFromBytes(byte[] bytes)
  {
    return TicketSerializer.Default.Deserialize(bytes);
  }
}
