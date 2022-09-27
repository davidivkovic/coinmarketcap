namespace CoinGecko.Interfaces;

using System;
using System.Threading.Tasks;

public interface IAsyncApiRepository
{
    /// <summary>
    ///     Sends an API request async using GET Method
    /// </summary>
    /// <param name="resourceUri">The resouce uri path</param>
    /// <returns>Asyncronous result turns by TApiResouce</returns>
    Task<T> GetAsync<T>(Uri resourceUri);
}