namespace Summary.WASenderApi.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Core.Mvc.Utilities;
    using Core.Workflows;
    using System;

    public interface ISessionService
    {
        Task<RecentActivity> GetStatusAsync();
    }

    public class SessionService : ISessionService
    {
        private readonly HttpRequestClient _client;
        private readonly WASenderApiSettings _options;

        public SessionService(
            HttpRequestClient client,
            IOptions<WASenderApiSettings> options)
        {
            _client = client;
            _options = options.Value;
        }

        public async Task<RecentActivity> GetStatusAsync()
        {
            ThrowExceptionIf.TokenIsEmpty(_options.Token);

            try
            {
                var response = await _client.SendGetRequestAsync<StatusResponseModel>(
                    $"{WASenderApi.Public.API_Address}/status",
                    new KeyValuePair<string, string>("Authorization", $"Bearer {_options.Token}"),
                    true
                );

                return response.Status ?? RecentActivity.BadRequest;
            }

            catch
            {
                return RecentActivity.NotFound;
            }
        }
    }
}