namespace MyTelegramBot.Dtos.Telegram
{
    public class MessageForCreationDto
    {
        public long update_id { get; set; }
        public MessageDto message { get; set; }

    }
}