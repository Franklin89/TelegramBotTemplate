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
    }
}
