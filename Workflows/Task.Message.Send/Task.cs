namespace Summary.WASenderApi.Workflows.Task.Message.Send
{
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;
    using Core.Workflows.Abstractions.Models;
    using Core.Workflows.Activities;
    using Core.Workflows.Models;
    using Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;
    using Core.Mvc.Utilities;
    using Summary.WASenderApi.Workflows.Task.Message.Send;

    public class SendMessageInWASenderApiTask : TaskActivity
    {
        private readonly IStringLocalizer<SendMessageInWASenderApiTask> T;
        private readonly IMessageService _message;
        private readonly WASenderApiSettings _options;

        public SendMessageInWASenderApiTask(
            IStringLocalizer<SendMessageInWASenderApiTask> t,
            IMessageService message,
            IOptions<WASenderApiSettings> options)
        {
            T = t;
            _message = message;
            _options = options.Value;
        }

        public override string Name => nameof(SendMessageInWASenderApiTask);

        public override LocalizedString DisplayText => T[WASenderApi.Localize.SOfSendMessage];

        public override LocalizedString Category => T[WASenderApi.Public.Category];

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            return Outcomes(
                T[WASenderApi.Workflows.Done],
                T[WASenderApi.Workflows.NotExist]
            );
        }

        public WorkflowExpression<string> Token
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public string To
        {
            get => GetProperty(() => String.Empty);
            set => SetProperty(value);
        }

        public string Message
        {
            get => GetProperty(() => String.Empty);
            set => SetProperty(value);
        }

        public WorkflowExpression<string> File
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(
            WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            try
            {
                // Initialization
                var token = workflowContext.GetInputOrDefault(Token.Expression);
                var to = workflowContext.GetInputOrDefault(To);
                var message = workflowContext.GetInputOrDefault(Message);
                var file = workflowContext.GetInputOrDefault(File.Expression);

                // Call SendMessage service
                await _message.SendMessageAsync(
                    token,
                    to,
                    message,
                    file
                );

                // Successful request
                return Outcomes(WASenderApi.Workflows.Done);
            }

            catch (AccountDoesNotExist)
            {
                // The number does not have WhatsApp account
                return Outcomes(WASenderApi.Workflows.NotExist);
            }

            catch
            {
                throw;
            }
        }
    }
}