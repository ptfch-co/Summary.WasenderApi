namespace Summary.WASenderApi.Workflows.Task.Message.Send
{
    using System.ComponentModel.DataAnnotations;

    public class SendMessageInWASenderApiTaskViewModel
    {
        public string Token { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string Message { get; set; }
        public string File { get; set; }
    }
}