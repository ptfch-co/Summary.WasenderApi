namespace Summary.WASenderApi
{
    using Core.DisplayManagement.Handlers;
    using Core.Modules;
    using Core.Navigation;
    using Core.Security.Permissions;
    using Core.Settings;
    using Core.Workflows.Helpers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Summary.WASenderApi.Services;
    using Summary.WASenderAPI.Services;
    using Summary.WASenderApi.Workflows.Task.Group.Send;
    using Summary.WASenderApi.Workflows.Task.Channel.Send;
    using Summary.WASenderApi.Workflows.Task.Message.Send;

    [Feature(WASenderApi.Features.WASenderApi)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<INavigationProvider, Menu>();
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IDisplayDriver<ISite>, WASenderApiSettingsDisplayDriver>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ISessionService, SessionService>();

            services.AddActivity<SendMessageInWASenderApiTask, SendMessageInWASenderApiTaskDisplay>();
            services.AddActivity<SendGroupMessageInWASenderApiTask, SendGroupMessageInWASenderApiDisplay>();
            services.AddActivity<SendChannelMessageInWASenderApiTask, SendChannelMessageInWASenderApiDisplay>();

            services.AddTransient<IConfigureOptions<WASenderApiSettings>, WASenderApiSettingsConfiguration>();
        }
    }
}