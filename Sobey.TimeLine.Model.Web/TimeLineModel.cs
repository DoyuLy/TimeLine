using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobey.TimeLine.Model
{
    public class TimeLineModel
    {
        public DateTime Time { get; set; }
        public string TimeString
        {
            get
            {
                return Time.ToString("yyyy-MM-dd");
            }
        }
        public string GroupTime
        {
            get
            {
                return Time.ToString("yyyy-MM");
            }
        }
        public List<NewsModel> Items { get; set; }

        public TimeLineModel()
        {
            Items = new List<NewsModel>();
        }
    }
}