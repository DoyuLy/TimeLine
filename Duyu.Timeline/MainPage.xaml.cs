using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Duyu.Timeline
{
    public partial class MainPage : UserControl
    {
        
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //tlc_Main.RequestData += GetData;
        }

        //void GetData(DateTime time, bool after)
        //{
        //    //if(after)
        //}
    }
}
