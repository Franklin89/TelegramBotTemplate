namespace TelegramBotTemplate
{
    public class CallbackEventArgs
    {
        public string? Command { get; set; }
        public int UserId { get; set; }
        public long ChatId { get; set; }
        public int MessageId { get; set; }
    }
}
