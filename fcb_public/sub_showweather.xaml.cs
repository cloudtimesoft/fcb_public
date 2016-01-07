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
using System.Timers;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

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

        System.Timers.Timer newtime = new System.Timers.Timer();
        
        public string city_name;
        public float gmt;
        DateTime time;
        public string status;



                    float datas ;
            DateTime ss ;
            


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            newtime.Interval = 1000;
            newtime.Elapsed += new ElapsedEventHandler(newtime_Elapsed);
            newtime.Start();

            request("city="+city_name);


            //try
            //{
            //    // WeatherWs weather = new WeatherWebService();
            //    WeatherWebService wx = new WeatherWebService();
            //    string[] s = new string[23];//声明string数组存放返回结果 
            //    // string[] s1 = new string[23];//声明string数组存放返回结果   
            //    string city = city_name;//获得文本框录入的查询城市
            //    //s1 = newweather.getRegionCountry();
            //    // string city = txt_address.Text;
            //    s = wx.getWeatherbyCityName(city);

            //txt_address.Text = city_name;
            //    string temp = s[13];
            //    txtDate.Text = temp.Substring(6);
            //    string a = temp.Substring(6);
            //    txtWindAndTemperature.Text = s[12];
            //    string img1 = s[15];
            //    string img2 = s[16];
            //    weather_zhiliang.Text = System.DateTime.UtcNow.AddHours(gmt).ToString(); //System.DateTime.Now.ToShortDateString() +"  "+ System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute;
            //    if (img1 == img2)
            //    {
            //        icon2.Visibility = Visibility.Hidden;
            //        icon1.Source = new BitmapImage(new Uri(@"image\weather\" + img1, UriKind.Relative));
            //    }
            //    else
            //    {
            //        icon1.Source = new BitmapImage(new Uri(@"image\weather\" + img1, UriKind.Relative));
            //        icon2.Source = new BitmapImage(new Uri(@"image\weather\" + img2, UriKind.Relative));
            //    }
            //}
            //catch { }
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

        void newtime_Elapsed(object sender, ElapsedEventArgs e)
        {
            Thread newthread = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() =>
                {



            //weather_zhiliang.Text = System.DateTime.Now.ToShortDateString() +"  "+ System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute;
                    if (status == "ok")
                    {
                        ss = DateTime.Now.AddHours(-8);
                        time = ss.AddHours(datas);
                        weather_zhiliang.Text = time.ToShortDateString();
                        city_time.Text = time.ToShortTimeString();
                    }
                    else
                    {
 
                    }

                }));



            }));
            newthread.SetApartmentState(ApartmentState.MTA);
            newthread.IsBackground = true;
            //newthread.Priority = ThreadPriority.AboveNormal;
            newthread.Start();
        }




        private void request(string param)
        {
            string strURL = "http://apis.baidu.com/heweather/weather/free" + '?' + param;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", "b5623bb8d8bf30a044d8787a3bb84354");
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate ;
            }
            strValue = strValue.Substring(strValue.IndexOf(":")+1, strValue.Length - strValue.IndexOf(":") - 2);

            //string jsonText = "[{'a':'aaa','b':'bbb','c':'ccc'},{'a':'aaa2','b':'bbb2','c':'ccc2'}]";
            //JArray ja = (JArray)JsonConvert.DeserializeObject(jsonText);
            //JObject o = (JObject)ja[1];  


            //JObject json2 = (JObject)JsonConvert.DeserializeObject(strValue);
            JArray json2 = (JArray)JsonConvert.DeserializeObject(strValue);
            //string json3 = json2.ToString();
            //JArray json4 = (JArray)JsonConvert.DeserializeObject(json3);


            //JObject basic = (JObject)json2[0]["update"];
            JObject basic = (JObject)json2[0]["basic"];
             status = json2[0]["status"].ToString();
            if (status == "ok")
            {
                string cnty = basic["cnty"].ToString();
                txt_address.Text = basic["city"].ToString();
                JObject update = (JObject)basic["update"];
                DateTime loc = DateTime.Parse(update["loc"].ToString());
                DateTime utc = DateTime.Parse(update["utc"].ToString());
                datas = (loc - utc).Hours;
                ss = DateTime.Now.AddHours(-8);
                time = ss.AddHours(datas);
                JArray daily_forecast = (JArray)json2[0]["daily_forecast"];

                JObject cond = (JObject)daily_forecast[0]["cond"];
                string code_d = cond["code_d"].ToString();
                string code_n = cond["code_n"].ToString();
                string txt_d = cond["txt_d"].ToString();
                string txt_n = cond["txt_n"].ToString();
                JObject tmp = (JObject)daily_forecast[0]["tmp"];
                string max = tmp["max"].ToString() + " ℃";
                string min = tmp["min"].ToString() + " ℃";
                if (max == min)
                {
                    Temperature.Text = max;

                }
                else
                {
                    Temperature.Text = min + "~" + max;
                }

                if (txt_d == txt_n)
                {
                    txtWindAndTemperature.Text = txt_d;
                }
                else
                {
                    if (cnty == "中国")
                    {
                        txtWindAndTemperature.Text = txt_d + "转" + txt_n;
                    }
                    else
                    {
                        txtWindAndTemperature.Text = txt_d + " ~ " + txt_n;
                    }
                }
                if (code_d == code_n)
                {
                    icon2.Visibility = Visibility.Hidden;
                    icon1.Source = new BitmapImage(new Uri(@"image\weather\" + code_d + ".png", UriKind.Relative));
                }
                else
                {
                    icon1.Source = new BitmapImage(new Uri(@"image\weather\" + code_d + ".png", UriKind.Relative));
                    icon2.Source = new BitmapImage(new Uri(@"image\weather\" + code_n + ".png", UriKind.Relative));
                }

            }
            else
            {
                MessageBox.Show("请输入正确城市名称","错误提示");

            }
           // string txt = cond["txt"].ToString();
            //string tmp = now["max"].ToString();
             //var t =json2["HeWeather data service3.0"]["basic"]["city"];
          //   JObject st = (JObject)j[1];
           // var newcity=json2[HttpListenerBasicIdentity]
            //JsonResult(strValue);
            int a;
        }


        private void timestep()
        {
        }
    }
}





//http://apistore.baidu.com/apiworks/servicedetail/478.html
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.IO;
//using System.Net;
//using System.Runtime.Serialization.Json;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;






//namespace text
//{
//    /// <summary>
//    /// MainWindow.xaml 的交互逻辑
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void Window_Loaded(object sender, RoutedEventArgs e)
//        {
//            string url = "http://apis.baidu.com/heweather/weather/free";
//            string param = "city=玉溪";
//            request(url, param);
//        }

//        /// <summary>
//        /// 发送HTTP请求
//        /// </summary>
//        /// <param name="url">请求的URL</param>
//        /// <param name="param">请求的参数</param>
//        /// <returns>请求结果</returns>
//        string a;


//        public void JsonResult(string JsonText)
//        {
           

//            List<string> list = new List<string>();
//             var json2 = (JObject)JsonConvert.DeserializeObject(JsonText);
       
//            var vid = json2["now"];
//            //var uid = json2["uid"];
//            //var Object = json2["object"];
//        }

//    }






//}

