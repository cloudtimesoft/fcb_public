using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using fcb_public.cn.com.webxml.www;

namespace fcb_public
{
    /// <summary>
    /// sub_weather.xaml 的交互逻辑
    /// </summary>
    public partial class sub_weather : UserControl
    {
        public sub_weather()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            WeatherWebService wx = new WeatherWebService();
            string[] s = new string[23];//声明string数组存放返回结果 
           // string[] s1 = new string[23];//声明string数组存放返回结果   
           // string city = this.textBox1.Text.Trim();//获得文本框录入的查询城市
           // s1 = newweather.getRegionCountry();
            //string city = txt_address.Text;
            //s = wx.getWeatherbyCityName(city);
        }

        private void show_weather_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.weatherTableAdapter publicDataSetInitializeTableAdapter = new fcb_public.publicDataSetTableAdapters.weatherTableAdapter();
            if (city_name.Text != "")
            {
                publicDataSet.weather.AddweatherRow((int)(SystemParameters.PrimaryScreenWidth - 300), 70, "");
                publicDataSetInitializeTableAdapter.Update(publicDataSet.weather);
                PublicClass.city_name = city_name.Text.Trim();
                publicDataSet.AcceptChanges();
                PublicClass.show = "showweather";
            }
        }
      
    }
}
