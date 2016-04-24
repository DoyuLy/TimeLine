using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                if (Blank)
                    return Time.ToString("yyyy-MM");
                else
                    return Time.ToString("yyyy-MM-dd");
            }
        }

        private string groupTime = "yyyy-MM";
        public string GroupTime
        {
            set { groupTime = value; }
            get
            {
                return Time.ToString(groupTime);
            }
        }

        public string TimeDisplay
        {
            get { return Time.ToString("G"); }
        }

        public bool Blank { get; set; }

        public ObservableCollection<NewsModel> Childs { get; set; }

        public TimeLineModel()
        {
            Childs = new ObservableCollection<NewsModel>();
        }
    }
}