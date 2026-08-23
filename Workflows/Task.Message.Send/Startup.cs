namespace Summary.WASenderApi.Workflows.Task.Message.Send
{
    using Microsoft.Extensions.DependencyInjection;
    using Core.Modules;
    using Core.Workflows.Helpers;

    [Feature(WASenderApi.Features.WASenderApi)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<SendMessageInWASenderApiTask, SendMessageInWASenderApiTaskDisplay>();
        }
    }
}