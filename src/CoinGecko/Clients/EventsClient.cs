namespace CoinGecko.Clients;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinGecko.ApiEndPoints;
using CoinGecko.Entities.Response.Events;
using CoinGecko.Interfaces;
using CoinGecko.Services;

public class EventsClient : BaseApiClient, IEventsClient
{
    public EventsClient(HttpClient httpClient) : base(httpClient) { }
    public async Task<Events> GetEvents()
    {
        return await GetEvents(System.Array.Empty<string>(), System.Array.Empty<string>(), null, null, null, null).ConfigureAwait(false);
    }
    
    public async Task<Events> GetEvents(string[] countryCode, string[] type, string page, string upcommingEventsOnly,
        string fromDate, string toDate)
    {
        return await GetAsync<Events>(QueryStringService.AppendQueryString(EventsApiEndPoints.Events,
            new Dictionary<string, object>
            {
                {"country_code",string.Join(",",countryCode)},
                {"type",string.Join(",",type)},
                {"page",page},
                {"upcoming_events_only",upcommingEventsOnly},
                {"from_date",fromDate},
                {"to_date",toDate}
            })).ConfigureAwait(false);
    }
    
    public async Task<EventCountry> GetEventCountry()
    {
        return await GetAsync<EventCountry>(
            QueryStringService.AppendQueryString(EventsApiEndPoints.EventsCountries)).ConfigureAwait(false);
    }
    
    public async Task<EventTypes> GetEventTypes()
    {
        return await GetAsync<EventTypes>(QueryStringService.AppendQueryString(EventsApiEndPoints.EventsTypes)).ConfigureAwait(false);
    }
}