using Newtonsoft.Json;

namespace MyTelegramBot.Dtos.Telegram
{
    public class UpdateForCreationDto
    {
        [JsonProperty("update_id")]
        public long Id{ get; set; }
        [JsonProperty("message")]
        public MessageDto Message { get; set; }
        [JsonProperty("callback_query")]
        public CallbackQueryDto CallbackQuery { get; set; }
        public ResponseDto response { get; set ;}
    }
}