namespace Summary.WASenderApi.Workflows.Task.Group.Send
{
    using System.ComponentModel.DataAnnotations;
    public class SendGroupMessageInWASenderApiViewModel
    {
        public string Token { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string Message { get; set; }
        public string File { get; set; }
    }
}