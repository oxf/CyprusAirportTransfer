using CyprusAirportTransfer.App.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

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
            _telegramBotClient.StartReceiving(UpdateHandler, ErrorHandler);
        }

        private Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if(update.Message != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{update.Message.From.FirstName} sent message {update.Message.MessageId} ");
                sb.Append($"to chat {update.Message.Chat.Id} at {update.Message.Date}. ");
                if (update.Message.ReplyToMessage != null) {
                    sb.Append($"It is a reply to message {update.Message.ReplyToMessage.MessageId} ");
                }
                if (update.Message.Entities != null)
                {
                    sb.Append($"and has {update.Message.Entities.Length} message entities.");
                }
                _logger.LogInformation(sb.ToString());
            }
            return Task.CompletedTask;
        }

        private Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
