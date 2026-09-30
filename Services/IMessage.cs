namespace Summary.WASenderApi.Services
{
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Core.Data;
    using Core.Mvc.Utilities;
    using Core.Workflows;
    using Summary.WASenderApi;
    using Summary.WASenderApi.Workflows.Task.Message.Send;
    using Summary.WASenderAPI.Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task SendMessageAsync(
            string token,
            string to,
            string message,
            string file
        );
    }

    public class MessageService : IMessageService
    {
        private readonly HttpRequestClient _client;
        private readonly WASenderApiSettings _options;
        private readonly IAccountService _account;
        private readonly IGroupService _group;
        private readonly ISessionService _session;

        public MessageService(HttpRequestClient client,
            IOptions<WASenderApiSettings> options,
            IAccountService account,
            IGroupService group,
            ISessionService session)
        {
            _client = client;
            _options = options.Value;
            _account = account;
            _group = group;
            _session = session;
        }

        public async Task SendMessageAsync(
            string token,
            string to,
            string message,
            string file)
        {
            if (_options.ApiIsAlive is false) throw new AccountDoesNotExist();

            token = String.IsNullOrWhiteSpace(token) ? _options.Token : token;

            ThrowExceptionIf.TokenIsEmpty(token);

            if (to.IsMobileNo()) to = to.RemoveMobilePrefixNo("98");

            message = message.ConvertHtmlToWhatsappFormat();

            var data = new Dictionary<string, object>
            {
                { "text", message },
                { "to", $"{to}"}
            };

            if (String.IsNullOrWhiteSpace(file) is false)
            {
                var fileExtension = Path.GetExtension(new Uri(file).AbsolutePath);

                string fileType;

                switch (fileExtension.ToLower())
                {
                    case ".png":
                    case ".jpeg":
                    case ".jpg": fileType = "imageUrl"; break;
                    case ".mp4": fileType = "videoUrl"; break;
                    default: fileType = "documentUrl"; break;
                }

                data.Add(fileType, file);
            }

            var response = await _client.SendPostRequestAsync<BaseResponseModel>(
                $"{WASenderApi.Public.API_Address}/send-message",
                data,
                new KeyValuePair<string, string>("Authorization", $"Bearer {token}"),
                "application/json",
                true
            );

            if (response.Success is false) ThrowExceptionIf.ResponseIsNotValid(
                response.Message,
                JsonConvert.SerializeObject(data),
                to
            );
        }
    }
}