using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Duyu.Timeline.Model
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

        public ObservableCollection<ContentModel> Childs { get; set; }

        public TimeLineModel()
        {
            Childs = new ObservableCollection<ContentModel>();
        }
    }
}
