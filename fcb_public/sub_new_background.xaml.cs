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
using Microsoft.Win32;

namespace fcb_public
{
    /// <summary>
    /// sub_new_background.xaml 的交互逻辑
    /// </summary>
    public partial class sub_new_background : UserControl
    {
        public sub_new_background()
        {
            InitializeComponent();
        }
        public static readonly RoutedEvent NewBackGroundEvent = EventManager.RegisterRoutedEvent("NewBackGround", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(sub_background));
        public event RoutedPropertyChangedEventHandler<object> NewBackGround
        {
            add { AddHandler(NewBackGroundEvent, value); }
            remove { RemoveHandler(NewBackGroundEvent, value); }
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


            load_img();

        }

        private void load_img()
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.background_picTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.background_picTableAdapter();
            publicDataSetTableAdapters.Fill(publicDataSet.background_pic);
            System.Windows.Data.CollectionViewSource background_picViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("background_picViewSource"));
            int local_step = 0;
            background.Children.Clear();
            foreach (var t in publicDataSet.background_pic)
            {
                StackPanel newstackpanel = new StackPanel();

                Button newbtn = new Button();
                Image newimg = new Image();
                Image imagebru = new Image();
                imagebru.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\image\\" + "close.png", UriKind.Absolute));
                newimg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\image\\" + t.url, UriKind.Absolute));
                newimg.Width = 100;
                newimg.Height = 80;
                newstackpanel.Children.Add(newimg);
                Button del_btn = new Button();
                del_btn.Width = 20;
                del_btn.Height = 20;
                del_btn.Content = imagebru;
                del_btn.Opacity = 0;
                del_btn.Name = "d" + t.ID.ToString();
                del_btn.MouseEnter += new MouseEventHandler(del_btn_MouseEnter);
                del_btn.MouseLeave += new MouseEventHandler(del_btn_MouseLeave);
                del_btn.Margin = new Thickness(75, -140, 0, 0);
                newstackpanel.Children.Add(del_btn);
                newbtn.Content = newstackpanel;
                newbtn.Name ="s"+ t.ID.ToString();
                newbtn.Click += new RoutedEventHandler(newbtn_Click);
                del_btn.Click += new RoutedEventHandler(del_btn_Click);
                //newbtn.Content = newimg;
                
                
                newbtn.Margin = new Thickness(15+(local_step % 5) * 120, 10+local_step/5*95, 0, 0);
                background.Children.Add(newbtn);
               // canvas_scrollv.ScrollToBottom();
                local_step++;
            }
        }

        void del_btn_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.background_picTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.background_picTableAdapter();
            Button newbtn = sender as Button;
            int s_id = int.Parse(newbtn.Name.ToString().Substring(1, newbtn.Name.ToString().Length - 1));
            publicDataSet.background_pic.FindByID(s_id).Delete();
            publicDataSetTableAdapters.Update(publicDataSet.background_pic);
            publicDataSet.AcceptChanges();
            load_img();
        }

        void newbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
                fcb_public.publicDataSetTableAdapters.background_picTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.background_picTableAdapter();
                foreach (var t in publicDataSet.background_pic)
                {
                    t._default = "0";
                }
                Button newbtn = sender as Button;
                int s_id = int.Parse(newbtn.Name.ToString().Substring(1, newbtn.Name.ToString().Length - 1));
                publicDataSet.background_pic.FindByID(s_id)._default = "1";
                publicDataSetTableAdapters.Update(publicDataSet.background_pic);
                publicDataSet.AcceptChanges();
                PublicClass.background_url = Directory.GetCurrentDirectory() + "\\image\\" + publicDataSet.background_pic.FindByID(s_id).url;



                RoutedPropertyChangedEventArgs<object> args = new RoutedPropertyChangedEventArgs<object>(this, e);
                args.RoutedEvent = sub_new_background.NewBackGroundEvent;
                this.RaiseEvent(args);
            }
            catch { }
        }

        void del_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button del_btn = sender as Button;
            del_btn.Opacity = 0;
        }

        void del_btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button del_btn = sender as Button;
            del_btn.Opacity = 1;
        }

        private void upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "jpg文件|*.jpg|RTF 文件 | *.rtf|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {

                string pic_guid = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(openFileDialog.SafeFileName);
                string aa = openFileDialog.FileName;
                PublicClass.background_url = Directory.GetCurrentDirectory() + "\\image\\" + pic_guid;
                File.Copy(aa, PublicClass.background_url);
                fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
                fcb_public.publicDataSetTableAdapters.background_picTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.background_picTableAdapter();
                publicDataSet.background_pic.Addbackground_picRow(pic_guid,"0");
                publicDataSetTableAdapters.Update(publicDataSet.background_pic);
                publicDataSet.AcceptChanges();
                load_img();


                int s = publicDataSet.background_pic.Count;
                int a;
            }


            // bbb.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));


        }

        private void GroupBox_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
