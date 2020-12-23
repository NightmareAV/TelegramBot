using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramBot.Models
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Performer { get; set; }
        public int Duration { get; set; }
        public string Url { get; set; }

    }
}