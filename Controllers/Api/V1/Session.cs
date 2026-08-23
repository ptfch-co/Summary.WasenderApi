namespace Summary.WASenderApi.Controllers.Api.V1
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Core.Environment.Shell.Descriptor.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Workflows.Services;
    using Core.Modules;
    using Summary.WASenderApi.Services;

    [Feature(WASenderApi.Features.WASenderApi)]
    [ApiController]
    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    [Route("api/v1/wa-sender/session")]
    public class SessionController : Controller
    {
        private readonly IWorkflowManager _workflowManager;
        private readonly ILogger<SessionController> _logger;
        private readonly ISessionService _session;

        public SessionController(
            IWorkflowManager workflowManager,
            ILogger<SessionController> logger,
            ISessionService session)
        {
            _workflowManager = workflowManager;
            _logger = logger;
            _session = session;
        }

        [HttpGet, Route("keepalive/ping")]
        public async Task<IActionResult> KeepAlive()
        {
            var status = await _session.GetStatusAsync();

            return status == RecentActivity.Connected ? Ok("I'm Alive") : (IActionResult)Unauthorized(status);
        }
    }
}