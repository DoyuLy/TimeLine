using System;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using Sobey.TimeLine.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows;
using System.Linq;

namespace Sobey.TimeLine.Controls
{
    public class TimeLineViewModel : ViewModelBase
    {
        private ObservableCollection<TimeLineModel> items;
        public ObservableCollection<TimeLineModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged("Items");
            }
        }
        private ObservableCollection<GroupModel> groups;
        public ObservableCollection<GroupModel> Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                RaisePropertyChanged("Groups");
            }
        }

        public TimeLineViewModel()
        {
            Items = new ObservableCollection<TimeLineModel>();
            Groups = new ObservableCollection<GroupModel>();
            InitGroup();
        }

        #region 初始化分组
        private void InitGroup()
        {
            DateTime now = DateTime.Now;
            DateTime end = DateTime.Parse("2008-1-1");

            GroupModel year = null;
            while (now >= end)
            {
                if (year == null)
                {
                    year = new GroupModel();
                    year.Year = now.Year.ToString();
                    Groups.Add(year);
                }
                Month m = new Month();
                m.Y = now.Year;
                m.M = now.Month;
                year.Months.Add(m);
                if (now.Month == 1)
                    year = null;
                now = now.AddMonths(-1);
            }
            Groups[0].Visibility = Visibility.Visible;
        }
        #endregion

        public void AddItem(DateTime? requestTime, NewsModel model)
        {
            string time = model.Time.ToString("yyyy-MM-dd");
            TimeLineModel item = null;
            item = Items.FirstOrDefault(n => n.TimeString == time);
            if (item == null)
                item = AddTimeLine(requestTime, time);
            var temp = item.Childs.FirstOrDefault(n => n.ID == model.ID);
            if (temp == null)
            {
                int index = 0;
                if (item.Childs.Count > 0)
                    index = model.Time > item.Childs[0].Time ? 0 : item.Childs.Count;
                item.Childs.Insert(index, model);
            }
        }


        private TimeLineModel AddTimeLine(DateTime? requestTime, string dateTime)
        {
            var item = new TimeLineModel();
            item.Time = DateTime.Parse(dateTime);
            int index = 0;
            if (Items.Count > 0)
                index = item.Time > Items[0].Time ? 0 : Items.Count;
            DateTime big = new DateTime();
            DateTime small = new DateTime();
            if (requestTime.HasValue)
            {
                big = requestTime.Value > item.Time ? requestTime.Value : item.Time;
                small = requestTime.Value < item.Time ? requestTime.Value : item.Time;
            }
            if (Items.Count >= 1)
            {
                big = index == 0 ? DateTime.Parse(item.GroupTime) : DateTime.Parse(Items[index - 1].GroupTime);
                small = index == 0 ? DateTime.Parse(Items[0].GroupTime) : DateTime.Parse(item.GroupTime);
            }

            int gap = (big.Year - small.Year) * 12 + (big.Month - small.Month) - 1;
            if (gap > 0)
            {
                for (int i = gap; i > 0; i--)
                {
                    big = big.AddMonths(-1);
                    var blank = new TimeLineModel();
                    blank.Time = big;
                    blank.Blank = true;
                    Items.Insert(index, blank);
                    index++;
                }
            }

            Items.Insert(index, item);
            return item;
        }

    }
}