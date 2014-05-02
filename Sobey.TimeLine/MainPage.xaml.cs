using Sobey.TimeLine.DataService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Sobey.TimeLine
{
    public partial class MainPage : UserControl
    {
        private Service1Client client = new Service1Client();
        

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            tlc_Main.RequestData += getData;
            client.GetDataAfterCompleted += client_GetDataAfterCompleted;
            client.GetDataBeforeCompleted += client_GetDataBeforeCompleted;
            client.GetDataAfterAsync(DateTime.Now);
        }

        void client_GetDataBeforeCompleted(object sender, GetDataBeforeCompletedEventArgs e)
        {
            GetDataCompleted(e.Result);
        }

        void client_GetDataAfterCompleted(object sender, GetDataAfterCompletedEventArgs e)
        {
            GetDataCompleted(e.Result);
        }

        private void GetDataCompleted(ObservableCollection<NewsModel> result)
        {
            List<Sobey.TimeLine.Model.NewsModel> models = new List<Model.NewsModel>();
            foreach (var item in result)
            {
                Sobey.TimeLine.Model.NewsModel model = new Sobey.TimeLine.Model.NewsModel();
                model.ID = item.ID;
                model.Title = item.Title;
                model.Time = item.Time;
                models.Add(model);
            }
            tlc_Main.AddItems(models);
        }
        void getData(DateTime time, bool after)
        {
            if (after)
                client.GetDataAfterAsync(time);
            else
                client.GetDataBeforeAsync(time);
        }
    }
}
