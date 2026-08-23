namespace Summary.WASenderApi
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Core.DisplayManagement.Handlers;
    using Core.Modules;
    using Core.Navigation;
    using Core.Security.Permissions;
    using Core.Settings;
    using Summary.WASenderApi.Services;
    using Summary.WASenderAPI.Services;

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

            services.AddTransient<IConfigureOptions<WASenderApiSettings>, WASenderApiSettingsConfiguration>();
        }
    }
}