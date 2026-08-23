namespace Summary.WASenderApi.Workflows.Task.Message.Send
{
    using Core.Workflows.Display;
    using Core.Workflows.Models;

    public class SendMessageInWASenderApiTaskDisplay : ActivityDisplayDriver<SendMessageInWASenderApiTask,
        SendMessageInWASenderApiTaskViewModel>
    {
        protected override void EditActivity(SendMessageInWASenderApiTask activity,
            SendMessageInWASenderApiTaskViewModel model)
        {
            model.Token = activity.Token.Expression;
            model.To = activity.To;
            model.Message = activity.Message;
            model.File = activity.File.Expression;
        }

        protected override void UpdateActivity(SendMessageInWASenderApiTaskViewModel model,
            SendMessageInWASenderApiTask activity)
        {
            activity.Token = new WorkflowExpression<string>(model.Token);
            activity.To = model.To;
            activity.Message = model.Message;
            activity.File = new WorkflowExpression<string>(model.File);
        }
    }
}