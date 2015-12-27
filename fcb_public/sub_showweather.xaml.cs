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
using fcb_public.cn.com.webxml.www;
namespace fcb_public
{
    /// <summary>
    /// sub_showweather.xaml 的交互逻辑
    /// </summary>
    public partial class sub_showweather : UserControl
    {
        public sub_showweather()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (PublicClass.city_name != "")
            {
                //WeatherWebService weather = new WeatherWebService();
                WeatherWebService wx = new WeatherWebService();
                string[] s = new string[23];//声明string数组存放返回结果 
                 //string[] s1 = new string[23];//声明string数组存放返回结果   
                string city = PublicClass.city_name;//获得文本框录入的查询城市
                // s1 = newweather.getRegionCountry();
                // string city = txt_address.Text;
                s = wx.getWeatherbyCityName(city);

                txt_address.Text = city;
                string temp = s[13];
                txtDate.Text = temp.Substring(6);
                string a=temp.Substring(6);
                txtWindAndTemperature.Text = s[12];
                string img1 = s[15];
                string img2 = s[16];
                weather_zhiliang.Text = System.DateTime.Now.ToString();
                if (img1 == img2)
                {
                    icon2.Visibility = Visibility.Hidden;
                    icon1.Source = new BitmapImage(new Uri(@"image\weather\" + img1, UriKind.Relative));
                }
                else 
                {
                    icon1.Source = new BitmapImage(new Uri(@"image\weather\" + img1, UriKind.Relative));
                    icon2.Source = new BitmapImage(new Uri(@"image\weather\" + img2, UriKind.Relative));
                }
                //int num;
                //if (a == "晴".Trim())
                //{
                //    num = 0;
                //    icon2.Visibility = Visibility.Hidden;
                //    icon1.Source = new BitmapImage(new Uri(@"image\weather\" + num + ".gif", UriKind.Relative));
                //}
                //else if (a == "多云")
                //{
                //    num = 2;
                //    icon2.Visibility = Visibility.Hidden;
                //    icon1.Source = new BitmapImage(new Uri(@"image\weather\" + num + ".gif", UriKind.Relative));
                //}
                //else if (a == "阵雨")
                //{
                //    num = 3;
                //    icon2.Visibility = Visibility.Hidden;
                //    icon1.Source = new BitmapImage(new Uri(@"image\weather\" + num + ".gif", UriKind.Relative));
                //}
                //else if(a=="")
               
            }
        }
    }
}
