namespace Summary.WASenderAPI.Services
{
    using Microsoft.Extensions.Options;
    using Core.Workflows;
    using Summary.WASenderApi;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGroupService
    {
        Task<List<GroupInfo>> GetGroupsAsync(string token);
    }

    public class GroupService : IGroupService
    {
        private readonly HttpRequestClient _client;
        private readonly WASenderApiSettings _options;

        public GroupService(HttpRequestClient client,
            IOptions<WASenderApiSettings> options)
        {
            _client = client;
            _options = options.Value;
        }

        public async Task<List<GroupInfo>> GetGroupsAsync(string token)
        {
            token = String.IsNullOrWhiteSpace(token) ? _options.Token : token;

            var url = $"{WASenderApi.Public.API_Address}/groups";

            var response = await _client.SendGetRequestAsync<GetGroupsResponseModel>(
                url,
                new KeyValuePair<string, string>("Authorization", $"Bearer {token}"),
                true
            );

            if (response.Success is false) throw new WorkflowException(
                $"سرویس واکشی گروه‌های واتساپ با خطاء مواجه شد. متن خطا: {response.Message}",
                null,
                url
            );

            return response.Data;
        }
    }
}