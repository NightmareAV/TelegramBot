using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Commands
{
    public class DefaultCommand : Command
    {
        public override string Name => "default";
        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            string[] files = Directory.GetFiles("E:\\music");

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Split('\\')[2].ToLower().Contains(message.Text.ToLower()))
                {
                    var tag = TagLib.File.Create(files[i]);

                    using (var stream = System.IO.File.OpenRead(files[i]))
                    {
                        //await client.SendPhotoAsync(
                        //          chatId: chatId,
                        //          photo: tag.Tag.Album
                        //          );
                        await client.SendAudioAsync(
                           chatId: chatId,
                           audio: stream,
                           title: tag.Name.Split('\\')[2].ToLower(),
                           duration: (int)tag.Properties.Duration.TotalSeconds
                        );
                    }
                    break;
                }
            }
        }
    }
}