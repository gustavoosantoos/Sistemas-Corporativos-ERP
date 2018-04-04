using System;

namespace WebERP.Utils
{
    public class BootstrapAlertBuilder
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool AllowDismiss { get; set; } = true;
        public bool PauseOnHover { get; set; } = true;
        public MessageType Type { get; set; } 
        public MessagePosition Position { get; set; } = MessagePosition.TopRight;

        public BootstrapAlertBuilder(string title, string message, MessageType type)
        {
            Title = title;
            Message = message;
            Type = type;
        }

        public string BuildScript()
        {
            var script = $@"$.notify({{
                title: '{Title}',
                message: '{Message}',
                {Type.GetIconScript()}
            }}, {{
                element: 'body',
                type: '{Type.ToString().ToLower()}',
                {Position.GetPositionTranslated()},
                showProgressbar: true,
                delay: 10000
            }})";

            return script;
        }
    }

    public enum MessageType
    {
        Success,
        Info,
        Warning,
        Danger
    }

    public enum MessagePosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public static class AlertBulderExtensions
    {
        public static string GetPositionTranslated(this MessagePosition position)
        {
            switch (position)
            {
                case MessagePosition.TopLeft:
                    return "placement: {from: 'top', align: 'left'}";
                case MessagePosition.TopCenter:
                    return "placement: {from: 'top', align: 'center'}";
                case MessagePosition.TopRight:
                    return "placement: {from: 'top', align: 'right'}";
                case MessagePosition.BottomLeft:
                    return "placement: {from: 'bottom', align: 'left'}";
                case MessagePosition.BottomCenter:
                    return "placement: {from: 'bottom', align: 'center'}";
                case MessagePosition.BottomRight:
                    return "placement: {from: 'bottom', align: 'right'}";
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetIconScript(this MessageType type)
        {
            switch (type)
            {
                case MessageType.Danger:
                    return "icon: 'fa fa-exclamation-circle'";
                case MessageType.Info:
                    return "icon: 'fa fa-info-circle'";
                case MessageType.Warning:
                    return "icon: 'fa fa-exclamation-triangle'";
                case MessageType.Success:
                    return "icon: 'fa fa-check-circle'";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

