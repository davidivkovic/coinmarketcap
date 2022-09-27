﻿namespace CoinGecko.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using CoinGecko.Entities.Response.Indexes;
public interface IIndexesClient
{
    Task<IReadOnlyList<IndexData>> GetIndexes();
    Task<IReadOnlyList<IndexData>> GetIndexes(int? perPage, string page);
    //Task<IndexData> GetIndexById(string id);
    Task<IReadOnlyList<IndexList>> GetIndexList();
}