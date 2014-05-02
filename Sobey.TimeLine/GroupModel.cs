using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Sobey.TimeLine
{
    public class GroupModel : ViewModelBase
    {
        public string Year { get; set; }
        public List<Month> Months { get; set; }

        private Visibility visibility;
        public Visibility Visibility 
        {
            get { return visibility; }
            set
            {
                visibility = value;
                RaisePropertyChanged("Visibility");
            }
        }

        public GroupModel()
        {
            Months = new List<Month>();
            Visibility = Visibility.Collapsed;
        }
    }

    public class Month
    {
        /// <summary>
        /// 年
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int M { get; set; }

        public string MonthString
        {
            get { return string.Format("{0} 月", M.ToString().PadLeft(2, '0')); }
        }
        public string DateString
        {
            get { return string.Format("{0}-{1}", Y.ToString().PadLeft(2, '0'), M.ToString().PadLeft(2, '0')); }
        }

    }
}