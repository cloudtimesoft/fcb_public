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

using System.IO;
using System.Data;



namespace fcb_public
{
    /// <summary>
    /// test.xaml 的交互逻辑
    /// </summary>
    public partial class test : UserControl
    {
        public test()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // 不要在设计时加载数据。
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//在此处加载数据并将结果指派给 CollectionViewSource。
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }


           
            FlowDocument newdocument = new FlowDocument();

            Paragraph para = new Paragraph();
            para.Inlines.Add("等哈看速度好快送达客户 速度来看哈登录看好速度看哈登录可阿萨德as阿萨德 大声道撒旦啊飒飒大大大阿达都是大大声的");
            newdocument.Blocks.Add(para);


            TimeZoneInfo Paris = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, Paris);
           // Paris.StandardName
            
            


        }
    }
}
