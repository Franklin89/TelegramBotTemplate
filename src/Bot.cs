using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TelegramBotTemplate.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TelegramBotTemplate
{
    public class Bot : IHostedService
    {
        private readonly IChatService _chatService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Bot> _logger;

        public const string UnknownCommandMessage = "Unknown command. Try /help for a list of available commands.";

        public Bot(IChatService chatService, IServiceProvider serviceProvider, ILogger<Bot> logger)
        {
            _chatService = chatService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _chatService.ChatMessage += OnChatMessage;
            _chatService.Callback += OnCallback;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _chatService.ChatMessage -= OnChatMessage;
            return Task.CompletedTask;
        }

        private async void OnChatMessage(object? sender, ChatMessageEventArgs chatMessageArgs)
        {
            try
            {
                await ProcessChatMessage(sender, chatMessageArgs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Time}: OnChatMessage - Error {Exception}", DateTime.UtcNow, ex.Message);
            }
        }

        private async void OnCallback(object? sender, CallbackEventArgs callbackEventArgs)
        {
            try
            {
                await ProcessCallback(sender, callbackEventArgs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Time}: OnChatMessage - Error {Exception}", DateTime.UtcNow, ex.Message);
            }
        }

        private async Task ProcessChatMessage(object? sender, ChatMessageEventArgs chatMessageArgs)
        {
            if (sender is IChatService chatService)
            {
                var command = _serviceProvider.GetServices<IBotCommand>().SingleOrDefault(x => $"/{x.Command}".Equals(chatMessageArgs.Command, StringComparison.InvariantCultureIgnoreCase));
                if (command != null)
                {
                    await command.Execute(chatService,
                        chatMessageArgs.ChatId,
                        chatMessageArgs.UserId,
                        chatMessageArgs.MessageId,
                        chatMessageArgs.Text);
                }
                else
                {
                    _logger.LogTrace("Unknown command was sent");
                    await chatService.SendMessage(chatMessageArgs.ChatId, UnknownCommandMessage);
                }
            }
        }

        private async Task ProcessCallback(object? sender, CallbackEventArgs callbackEventArgs)
        {
            if (sender is IChatService chatService)
            {
                var commandText = callbackEventArgs.Command?.Split(' ').First();
                var command = _serviceProvider.GetServices<IBotCommand>().SingleOrDefault(x => $"/{x.Command}".Equals(commandText, StringComparison.InvariantCultureIgnoreCase));

                if (command != null && !string.IsNullOrEmpty(commandText))
                {
                    await command.Execute(chatService,
                        callbackEventArgs.ChatId,
                        callbackEventArgs.UserId,
                        callbackEventArgs.MessageId,
                        callbackEventArgs.Command?.Replace(commandText, string.Empty).Trim());
                }
                else
                {
                    _logger.LogCritical("Invalid callback data was provided: {CallbackData}", callbackEventArgs);
                }
            }
        }
    }
}
