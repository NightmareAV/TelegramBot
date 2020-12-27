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
        private MusicModel musicModel;
        public MusicCommand()
        {
            musicModel = new MusicModel();
        }
        public override string Name => "music";

        public async override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            Random random = new Random();
            List<Music> musics = (List<Music>)musicModel.GetMusics();
            Music music = new Music();

            music = musics.ElementAt(random.Next(0, musics.Count));

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