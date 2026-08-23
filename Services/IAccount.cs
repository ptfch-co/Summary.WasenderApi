namespace Summary.WASenderApi.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Core.Mvc.Utilities;
    using Core.Workflows;

    public interface IAccountService
    {
        Task<bool> CheckAccountExist(string token, string phoneNumber);
    }

    public class AccountService : IAccountService
    {
        private readonly HttpRequestClient _client;
        private readonly WASenderApiSettings _options;

        public AccountService(HttpRequestClient client,
            IOptions<WASenderApiSettings> options)
        {
            _client = client;
            _options = options.Value;
        }

        public async Task<bool> CheckAccountExist(string token, string phoneNumber)
        {
            var url = $"{WASenderApi.Public.API_Address}/contacts/{phoneNumber}";

            var response = await _client.SendGetRequestAsync<BaseResponseModel>(
                url,
                new KeyValuePair<string, string>("Authorization", $"Bearer {token}"),
                true
            );

            if (response.Success) return response.Success;

            if (response.Message.ToLower().Contains("contact not found")) return false;

            else throw new WorkflowException(
                response.Message,
                null,
                url
            );
        }
    }
}