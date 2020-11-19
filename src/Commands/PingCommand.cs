using System.Threading.Tasks;

namespace TelegramBotTemplate.Commands
{
    public class PingCommand : IBotCommand
    {
        public string Command => "ping";

        public string Description => "This is a simple command that can be used to test if the bot is online";

        public bool InternalCommand => false;

        public async Task Execute(IChatService chatService, long chatId, int userId, int messageId, string? commandText)
        {
            await chatService.SendMessage(chatId, "pong");
        }
    }
}