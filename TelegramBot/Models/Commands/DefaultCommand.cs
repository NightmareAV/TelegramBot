using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Commands
{
    public class DefaultCommand : Command
    {
        public override string Name => "default";

        private MusicModel musicModel;
        public DefaultCommand()
        {
            musicModel = new MusicModel();
        }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            List<Music> musics = (List<Music>)musicModel.GetMusics();
            Music music = new Music();

            foreach (var itemMusic in musics)
            {
                if (itemMusic.Url.Contains(message.Text))
                {
                    music = itemMusic;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(music.Name))
            {
                using (var stream = System.IO.File.OpenRead("E:\\music\\" + music.Url))
                {
                   await client.SendPhotoAsync(
                       chatId: chatId,
                       photo: music.Photo
                   );
                   await client.SendAudioAsync(
                        chatId: chatId,
                        audio: stream,
                        performer: music.Name,
                        title: music.Performer,
                        duration: music.Duration
                  );
                }
            }            
        }
    }
}