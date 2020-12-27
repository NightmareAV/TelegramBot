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
        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            string[] files = Directory.GetFiles("E:\\music");
            List<string> musics = new List<string>();

            for (int i = 0; i < files.Length; i++)
            {
                musics.Add(files[i].Split('\\')[2].ToLower());
            }

            string allMusicsName = "";

            foreach (var music in musics)
            {
                allMusicsName += $"{music}\n";
            }

            await client.SendTextMessageAsync(chatId, allMusicsName);

            musics.Clear();
        } 
    }
}