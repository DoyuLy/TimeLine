using System;
using System.Net;

namespace Sobey.TimeLine.Model
{
    public class NewsModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public DateTime Time { get; set; }
    }
}