namespace Summary.WASenderApi
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Core.DisplayManagement.Entities;
    using Core.DisplayManagement.Handlers;
    using Core.DisplayManagement.Views;
    using Core.Entities;
    using Core.Environment.Shell;
    using Core.Settings;

    public class WASenderApiSettings
    {
        public string Token { get; set; }
        public bool ApiIsAlive { get; set; } = true;
    }

    public class WASenderApiSettingsDisplayDriver : SectionDisplayDriver<ISite,
        WASenderApiSettings>
    {
        private readonly IShellHost _host;
        private readonly ShellSettings _shell;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IAuthorizationService _authorize;

        public WASenderApiSettingsDisplayDriver(IShellHost host,
            ShellSettings settings,
            IHttpContextAccessor httpContext,
            IAuthorizationService authorize)
        {
            _host = host;
            _shell = settings;
            _httpAccessor = httpContext;
            _authorize = authorize;
        }

        public override async Task<IDisplayResult> EditAsync(WASenderApiSettings settings,
            BuildEditorContext context)
        {
            var user = _httpAccessor.HttpContext?.User;
            if (user is null || !await _authorize.AuthorizeAsync(user, Permissions.ManageWASenderApiSettings))
            {
                return null;
            }

            var init = Initialize<WASenderApiSettings>("WASenderApiSettings_Edit", model =>
            {
                model.Token = settings.Token;
                model.ApiIsAlive = settings.ApiIsAlive;
            });

            return init.Location("Content:5").OnGroup("WASenderApi");
        }

        public override async Task<IDisplayResult> UpdateAsync(WASenderApiSettings settings,
            BuildEditorContext context)
        {
            var user = _httpAccessor.HttpContext?.User;
            if (user is null || !await _authorize.AuthorizeAsync(user, Permissions.ManageWASenderApiSettings))
            {
                return null;
            }

            if (context.GroupId == "WASenderApi")
            {
                await context.Updater.TryUpdateModelAsync(settings, Prefix);
                await _host.ReloadShellContextAsync(_shell);
            }

            return await EditAsync(settings, context);
        }
    }

    public class WASenderApiSettingsConfiguration : IConfigureOptions<WASenderApiSettings>
    {
        private readonly ISiteService _site;
        private readonly ILogger<WASenderApiSettingsConfiguration> _logger;

        public WASenderApiSettingsConfiguration(ISiteService site,
            ILogger<WASenderApiSettingsConfiguration> logger)
        {
            _site = site;
            _logger = logger;
        }

        public void Configure(WASenderApiSettings options)
        {
            var settings = _site.GetSiteSettingsAsync().GetAwaiter().GetResult().As<WASenderApiSettings>();
            options.Token = settings.Token;
            options.ApiIsAlive = settings.ApiIsAlive;
        }
    }
}