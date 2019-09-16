using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyTelegramBot.Data.Interface;
using MyTelegramBot.Dtos.Telegram;
using MyTelegramBot.Models.Telegram;
using Newtonsoft.Json;

namespace MyTelegramBot.Checkers.Messages
{
    public class ProductChecker : AbstractMessageChecker
    {
        private readonly string[] commands = {"/product", "/cat"};
        private readonly IDataRepository _repo;
        public ProductChecker(IServiceProvider provider,IDataRepository repo)//ILoggerFactory loggerFactory, IMyLogger filelogger, ITelegramApiRequest telegramRequest) 
            : base(provider) =>
            _repo = repo;

        public override async Task<object> Checker(MessageDto incomingMessageDto) 
        {
            if (incomingMessageDto?.Text == "/product")
            {

                var messageForSend = await CreateMessageForSend(incomingMessageDto);

                await LogInformation("RESPONSE TO USER\n" + JsonConvert.SerializeObject(messageForSend));
                
                var response = await _telegramRequest.SendMessage(messageForSend);

                return response;
            }
            return base.Checker(incomingMessageDto);
        }
        private async Task<MessageForSendDto<InlineKeyboardMarkup>> CreateMessageForSend(MessageDto message)
        {
             var messageForSend = new MessageForSendDto<InlineKeyboardMarkup>() {
                chat_id = message.Chat.Id
            };
            switch (message.Text.ToLower()) {
                case "/cat":
                    messageForSend.text = "Категории";
                    
                    var inlineKeyboard = new InlineKeyboardMarkup();
                    
                    //messageForSend.reply_markup = 
                    //    GetInlineButtons(message.chat.id);
                    break;
            }
            return messageForSend;
        }
    }
}