using CyprusAirportTransfer.App.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CyprusAirportTransfer.Infrastructure.Services
{
    public class TelegramBotService: IChatbotService
    {
        public ITelegramBotClient _telegramBotClient { get; set; }
        public ILogger<TelegramBotService> _logger { get; set; }

        public TelegramBotService(ITelegramBotClient telegramBotClient, ILogger<TelegramBotService> logger)
        {
            _telegramBotClient = telegramBotClient;
            _logger = logger;
        }

        public void StartReceivingUpdates()
        {
            _telegramBotClient.SetMyCommandsAsync(new List<BotCommand> { new BotCommand { Command = "vacancy", Description = "Get Vacancy list" } });
            _telegramBotClient.StartReceiving(UpdateHandler, ErrorHandler);
        }

        private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if (update.Message != null)
            {
                if (update.Message.Entities != null)
                {
                    // process the commands
                    foreach (var entity in update.Message.Entities)
                    {
                        if (entity.Type == MessageEntityType.BotCommand)
                        {
                            await ProcessCommand(client, update);
                        }
                    }
                } 
                // process the message
                StringBuilder sb = new StringBuilder();
                sb.Append($"{update.Message.From.FirstName} sent message {update.Message.MessageId} ");
                sb.Append($"to chat {update.Message.Chat.Id} at {update.Message.Date}. ");
                if (update.Message.ReplyToMessage != null)
                {
                    sb.Append($"It is a reply to message {update.Message.ReplyToMessage.MessageId} ");
                }
                if (update.Message.Entities != null)
                {
                    sb.Append($"and has {update.Message.Entities.Length} message entities.");
                }
                _logger.LogInformation(sb.ToString());
                client.SendTextMessageAsync(update.Message.Chat.Id, "This is my answer");
            }
        }

        private async Task ProcessCommand(ITelegramBotClient client, Update update)
        {
            //command
            switch (update.Message.Text)
            {
                case "/vacancy":
                    client.SendTextMessageAsync(update.Message.Chat.Id, "Vacancy list");
                    break;
                default:
                    client.SendTextMessageAsync(update.Message.Chat.Id, "Command is not supported");
                    break;
            }
        }

        private Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
