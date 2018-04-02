namespace WebERP.Utils
{
    public class SweetAlertBuilder
    {
        public SweetAlertBuilder(string title, string message, MessageType type)
        {
            Title = title;
            Message = message;
            Type = type;
        }

        public string Title { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; set; }
        public bool AllowDismiss { get; set; } = true;

        public string BuildScript()
        {
            string messageType = Type == MessageType.Danger ? "error" : Type.ToString().ToLower();
            return $"swal('{Title}', '{Message}', '{messageType}');";
        }
    }
}
