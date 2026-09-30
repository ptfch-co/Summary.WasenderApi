namespace Summary.WASenderApi.Workflows.Task.Group.Send
{
    using Core.Workflows.Display;
    using Core.Workflows.Models;

    public class SendGroupMessageInWASenderApiDisplay : ActivityDisplayDriver<SendGroupMessageInWASenderApiTask,
        SendGroupMessageInWASenderApiViewModel>
    {
        protected override void EditActivity(SendGroupMessageInWASenderApiTask activity,
            SendGroupMessageInWASenderApiViewModel model)
        {
            model.Token = activity.Token.Expression;
            model.To = activity.To;
            model.Message = activity.Message;
            model.File = activity.File.Expression;
        }

        protected override void UpdateActivity(SendGroupMessageInWASenderApiViewModel model,
            SendGroupMessageInWASenderApiTask activity)
        {
            activity.Token = new WorkflowExpression<string>(model.Token);
            activity.To = model.To;
            activity.Message = model.Message;
            activity.File = new WorkflowExpression<string>(model.File);
        }
    }
}