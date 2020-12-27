using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.IO;
using System.Text;
using System.Net.Http;

namespace TelegramBot.Models.Commands
{
    public class MusicCommand : Command
    {
        public override string Name => "music";

        private MusicModel musicModel;
        public MusicCommand()
        {
            musicModel = new MusicModel();
        }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            Random random = new Random();
            List<Music> musics = (List<Music>)musicModel.GetMusics();

            string allMusicsName = "";

            foreach (var music in musics)
            {
                allMusicsName += $"{music.Url}\n";
            }

            await client.SendTextMessageAsync(chatId, allMusicsName);

            musics.Clear();
        } 
    }
}