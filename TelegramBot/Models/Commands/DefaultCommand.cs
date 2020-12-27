using System;
using System.Collections.Generic;
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

            Music music = new Music();

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Split('\\')[2].ToLower().Contains(message.Text.ToLower()))
                {
                    var tag = TagLib.File.Create(files[i]);
                    music.Name = tag.Name.Split('\\')[2].ToLower();
                    music.Duration = (int)tag.Properties.Duration.TotalSeconds;

                    using (var stream = System.IO.File.OpenRead(files[i]))
                    {
                        //if (!string.IsNullOrEmpty(music.Photo))
                        //{
                        //    await client.SendPhotoAsync(
                        //          chatId: chatId,
                        //          photo: music.Photo
                        //          );
                        //}
                        await client.SendAudioAsync(
                           chatId: chatId,
                           audio: stream,
                           title: music.Name,
                           duration: music.Duration
                        );
                    }

                    break;
                }
            }
        }
    }
}