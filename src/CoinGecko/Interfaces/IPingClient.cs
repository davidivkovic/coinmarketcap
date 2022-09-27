namespace CoinGecko.Interfaces;

using System.Threading.Tasks;
using CoinGecko.Entities.Response;
public interface IPingClient
{
    /// <summary>
    /// Check API server status
    /// </summary>
    /// <returns></returns>
    Task<Ping> GetPingAsync();
}