using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using AI3.Common.Core.Utility.UIUility;
using Sobey.TimeLine.Model;
using System.Windows.Threading;

namespace Sobey.TimeLine.Controls
{
    public partial class TimeLineControl : UserControl
    {
        private TimeLineViewModel viewModel;

        public event Action<DateTime, bool> RequestData;
        private event EventHandler verticalScrollChanged;

        private bool requestIsJump = false;
        private bool isTop = false;
        private bool loading = false;
        private DateTime? requestTime = null;

        public TimeLineControl()
        {
            InitializeComponent();

            viewModel = new TimeLineViewModel();
            DataContext = viewModel;

            #region 注册依赖属性
            this.SetBinding(
                DependencyProperty.RegisterAttached("VerticalOffset",
                typeof(double),
                this.GetType(),
                new PropertyMetadata((d, e) =>
                {
                    if (verticalScrollChanged != null)
                        verticalScrollChanged(this, EventArgs.Empty);
                })
            ),
            new Binding("VerticalOffset") { Source = this.sv_Main });
            #endregion

            Loaded += TimeLineControl_Loaded;
        }

        void TimeLineControl_Loaded(object sender, RoutedEventArgs e)
        {
            verticalScrollChanged += TimeLineControl_VerticalScrollChanged;
        }

        #region 滚动条事件
        void TimeLineControl_VerticalScrollChanged(object sender, EventArgs e)
        {
            if (loading)
                return;

            #region 滚动到底
            if (ScrollableIsBottom())
            {
                RequestDataByButton();
                return;
            }
            #endregion

            #region 滚动到顶
            if (ScrollableIsTop())
            {
                RequestDataByTop();
                return;
            }
            #endregion
        }
        #endregion

        #region 列表项点击
        private void Item_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            MessageBox.Show(btn.CommandParameter.ToString());
        }
        #endregion

        #region 时间导航 - 年份
        private void ToYear_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string year = btn.CommandParameter.ToString();
            GroupModel group = viewModel.Groups.FirstOrDefault(n => n.Year == year);
            if (group != null)
            {
                foreach (var item in viewModel.Groups)
                    item.Visibility = Visibility.Collapsed;
                group.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region 时间导航 - 月份
        private void ToMonth_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string time = btn.CommandParameter.ToString();

            if (RequestData != null)
            {
                requestTime = DateTime.Parse(time).AddMonths(1);
                requestIsJump = true;
                loading = true;
                RequestData(requestTime.Value, true);
            }
        }
        #endregion


        #region 添加项
        public void AddItems(List<NewsModel> models)
        {
            bool first = viewModel.Items.Count == 0;
            if (requestIsJump)
                viewModel.Items.Clear();

            foreach (var item in models)
                viewModel.AddItem(requestTime, item);
            requestTime = null;
            if (first)
                TopMore.Visibility = Visibility.Collapsed;

            if (requestIsJump || isTop)
            {
                if (models != null && models.Count > 0)
                    sv_Main.ScrollToVerticalOffset(20);
                else
                    TopMore.Visibility = Visibility.Collapsed;
            }

            loading = false;

            DetectIsBottom(models);
        }
        #endregion


        #region 滚动条位置

        private bool ScrollableIsTop()
        {
            return sv_Main.VerticalOffset == 0;
        }

        private bool ScrollableIsBottom()
        {
            return (sv_Main.ScrollableHeight - sv_Main.VerticalOffset) == 0;
        }

        #endregion

        #region 请求数据

        private void RequestDataByTop()
        {
            if (RequestData != null)
            {
                TopMore.Visibility = Visibility.Visible;
                int index = 0;
                var f = viewModel.Items[index];
                requestIsJump = false;
                loading = isTop = true;
                while (f.Childs.Count == 0)
                {
                    index++;
                    if (index >= viewModel.Items.Count - 1)
                    {
                        MessageBox.Show("没有任何信息");
                        return;
                    }
                    f = viewModel.Items[index];
                }
                RequestData(f.Childs[0].Time, false);
            }
        }

        private void RequestDataByButton()
        {
            if (RequestData != null)
            {
                var f = viewModel.Items[viewModel.Items.Count - 1];
                isTop = requestIsJump = false;
                loading = true;
                RequestData(f.Childs[f.Childs.Count - 1].Time, true);
            }
        }

        #endregion

        #region 延迟检测页面是否被撑满
        private void DetectIsBottom(List<NewsModel> models)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                if (ScrollableIsBottom() && models != null && models.Count > 0)
                {
                    RequestDataByButton();
                }
            };
            timer.Start();
        }
        #endregion

    }
}