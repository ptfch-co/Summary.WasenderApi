namespace Summary.WASenderApi.Workflows.Task.Channel.Send
{
    using Core.Workflows.Display;
    using Core.Workflows.Models;

    public class SendChannelMessageInWASenderApiDisplay : ActivityDisplayDriver<SendChannelMessageInWASenderApiTask,
        SendChannelMessageInWASenderApiViewModel>
    {
        protected override void EditActivity(SendChannelMessageInWASenderApiTask activity,
            SendChannelMessageInWASenderApiViewModel model)
        {
            model.Token = activity.Token.Expression;
            model.To = activity.To;
            model.Message = activity.Message;
            model.File = activity.File.Expression;
        }

        protected override void UpdateActivity(SendChannelMessageInWASenderApiViewModel model,
            SendChannelMessageInWASenderApiTask activity)
        {
            activity.Token = new WorkflowExpression<string>(model.Token);
            activity.To = model.To;
            activity.Message = model.Message;
            activity.File = new WorkflowExpression<string>(model.File);
        }
    }
}