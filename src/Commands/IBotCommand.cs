using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelegramBotTemplate.Commands
{
    public interface IBotCommand
    {
        string Command { get; }
        string Description { get; }
        bool InternalCommand { get; }

        Task Execute(IChatService chatService, long chatId, int userId, int messageId, string? commandText);

        async Task UnknownCommand(IChatService chatService, long chatId)
        {
            await SendMessage(chatService, chatId, Bot.UnknownCommandMessage);
        }

        async Task SendMessage(IChatService chatService, long chatId, string message, Dictionary<string, string>? buttons = null)
        {
            await chatService.SendMessage(chatId, message, buttons);
        }
    }
}