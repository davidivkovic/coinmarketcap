namespace CoinGecko.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using CoinGecko.Entities.Response.Derivatives;

public interface IDerivativesClient
{
    Task<IReadOnlyList<Derivatives>> GetDerivatives();
    Task<IReadOnlyList<Derivatives>> GetDerivatives(string includeTicker);
    Task<IReadOnlyList<DerivativesExchanges>> GetDerivativesExchanges();
    Task<IReadOnlyList<DerivativesExchanges>> GetDerivativesExchanges(string order, int? perPage,int? page);
    Task<DerivativesExchanges> GetDerivativesExchangesById(string id);
    Task<DerivativesExchanges> GetDerivativesExchangesById(string id,string includeTickers);
    Task<IReadOnlyList<DerivativesExchangesList>> GetDerivativesExchangesList();
}