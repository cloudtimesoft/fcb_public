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
            //WeatherWebService wx = new WeatherWebService();
            //string[] s = new string[23];//声明string数组存放返回结果 
            // string[] s1 = new string[23];//声明string数组存放返回结果   
            // string city = this.textBox1.Text.Trim();//获得文本框录入的查询城市
            // s1 = newweather.getRegionCountry();
            //string city = txt_address.Text;
            //s = wx.getWeatherbyCityName(city);
            // 不要在设计时加载数据。
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//在此处加载数据并将结果指派给 CollectionViewSource。
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.weatherTableAdapter publicDataSetInitializeTableAdapter = new fcb_public.publicDataSetTableAdapters.weatherTableAdapter();
            publicDataSetInitializeTableAdapter.Fill(publicDataSet.weather);
            weatherDataGrid.CanUserAddRows = false;
        }

        private void show_weather_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.weatherTableAdapter publicDataSetInitializeTableAdapter = new fcb_public.publicDataSetTableAdapters.weatherTableAdapter();
            if (in_nameTextBox.Text != "")
            {
              ComboBoxItem item = gmt.SelectedItem as ComboBoxItem;
                float gmtime=0;
                //string str = item.Content.ToString();

                //switch (str)
                //{
                //    case "UTC":
                //        gmtime = 0;
                //        break;
                //    case"UTC+00:30":
                //        gmtime = 0.5f;
                //        break;
                //    case "UTC+01:00":
                //        gmtime = 1f;
                //        break;
                //    case "UTC+01:30":
                //        gmtime = 1.5f;
                //        break;
                //    case "UTC+02:00":
                //        gmtime = 2f;
                //        break;
                //    case "UTC+02:30":
                //        gmtime = 2.5f;
                //        break;
                //    case "UTC+03:00":
                //        gmtime = 3f;
                //        break;
                //    case "UTC+03:30":
                //        gmtime = 3.5f;
                //        break;
                //    case "UTC+04:00":
                //        gmtime = 4f;
                //        break;
                //    case "UTC+04:30":
                //        gmtime = 4.5f;
                //        break;
                //    case "UTC+05:00":
                //        gmtime = 5f;
                //        break;
                //    case "UTC+05:30":
                //        gmtime = 5.5f;
                //        break;
                //    case "UTC+06:00":
                //        gmtime = 6f;
                //        break;
                //    case "UTC+06:30":
                //        gmtime = 6.5f;
                //        break;
                //    case "UTC+07:00":
                //        gmtime = 7f;
                //        break;
                //    case "UTC+07:30":
                //        gmtime = 7.5f;
                //        break;
                //    case "UTC+08:00":
                //        gmtime = 8f;
                //        break;
                //    case "UTC+08:30":
                //        gmtime = 8.5f;
                //        break;
                //    case "UTC+09:00":
                //        gmtime = 9.5f;
                //        break;
                //    case "UTC+10:00":
                //        gmtime = 10f;
                //        break;
                //    case "UTC+10:30":
                //        gmtime = 10.5f;
                //        break;
                //    case "UTC+11:00":
                //        gmtime = 11f;
                //        break;
                //    case "UTC+11:30":
                //        gmtime = 11.5f;
                //        break;
                //    case "UTC+12:00":
                //        gmtime = 12f;
                //        break;



                //    case "UTC-00:30":
                //        gmtime = -0.5f;
                //        break;
                //    case "UTC-01:00":
                //        gmtime = -1f;
                //        break;
                //    case "UTC-01:30":
                //        gmtime = -1.5f;
                //        break;
                //    case "UTC-02:00":
                //        gmtime = -2f;
                //        break;
                //    case "UTC-02:30":
                //        gmtime = -2.5f;
                //        break;
                //    case "UTC-03:00":
                //        gmtime = -3f;
                //        break;
                //    case "UTC-03:30":
                //        gmtime = -3.5f;
                //        break;
                //    case "UTC-04:00":
                //        gmtime = -4f;
                //        break;
                //    case "UTC-04:30":
                //        gmtime = -4.5f;
                //        break;
                //    case "UTC-05:00":
                //        gmtime = -5f;
                //        break;
                //    case "UTC-05:30":
                //        gmtime = -5.5f;
                //        break;
                //    case "UTC-06:00":
                //        gmtime = -6f;
                //        break;
                //    case "UTC-06:30":
                //        gmtime = -6.5f;
                //        break;
                //    case "UTC-07:00":
                //        gmtime = -7f;
                //        break;
                //    case "UTC-07:30":
                //        gmtime = -7.5f;
                //        break;
                //    case "UTC-08:00":
                //        gmtime = -8f;
                //        break;
                //    case "UTC-08:30":
                //        gmtime = -8.5f;
                //        break;
                //    case "UTC-09:00":
                //        gmtime = -9.5f;
                //        break;
                //    case "UTC-10:00":
                //        gmtime = -10f;
                //        break;
                //    case "UTC-10:30":
                //        gmtime = -10.5f;
                //        break;
                //    case "UTC-11:00":
                //        gmtime = -11f;
                //        break;
                //    case "UTC-11:30":
                //        gmtime = -11.5f;
                //        break;
                //    case "UTC-12:00":
                //        gmtime = -12f;
                //        break;
                       
                        
                //}
                publicDataSet.weather.AddweatherRow((int)(SystemParameters.PrimaryScreenWidth - 300), 70, in_nameTextBox.Text, (bool)statusCheckBox.IsChecked,gmtime);
                publicDataSetInitializeTableAdapter.Update(publicDataSet.weather);
                publicDataSetInitializeTableAdapter.Fill(publicDataSet.weather);
               // PublicClass.city_name = in_nameTextBox.Text.Trim();
                publicDataSet.AcceptChanges();
                PublicClass.show = "showweather";
              
            }
        }

        private void gmt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void del_weather_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.weatherTableAdapter publicDataSetInitializeTableAdapter = new fcb_public.publicDataSetTableAdapters.weatherTableAdapter();
           // publicDataSetInitializeTableAdapter.Fill(publicDataSet.weather);
            if (iDTextBox.Text != "")
            {
                int index = int.Parse(iDTextBox.Text);
                publicDataSet.weather.FindByID(index).Delete();
                publicDataSetInitializeTableAdapter.Update(publicDataSet.weather);
                publicDataSet.weather.AcceptChanges();
                publicDataSetInitializeTableAdapter.Fill(publicDataSet.weather);
                
            }
        }


        public void savechanges()
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.weatherTableAdapter publicDataSetweatherTableAdapter = new fcb_public.publicDataSetTableAdapters.weatherTableAdapter();


            System.Windows.Data.CollectionViewSource weatherViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("weatherViewSource"));

            //publicDataSetTableAdapters.Fill(publicDataSet.element);
            weatherViewSource.View.MoveCurrentToNext();
            publicDataSetweatherTableAdapter.Update(publicDataSet.weather);
            // publicDataSet.element.AcceptChanges();


        }



      
    }
}
