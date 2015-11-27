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
using Microsoft.Win32;
using System.IO;

namespace fcb_public
{
    /// <summary>
    /// sub_background.xaml 的交互逻辑
    /// </summary>
    public partial class sub_background : UserControl
    {
        public sub_background()
        {
            InitializeComponent();
        }
        public static readonly RoutedEvent BackGroundEvent = EventManager.RegisterRoutedEvent("BackGround", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(sub_background));
        public event RoutedPropertyChangedEventHandler<object> BackGround
        {
            add { AddHandler(BackGroundEvent, value); }
            remove { RemoveHandler(BackGroundEvent, value); }
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
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.background_picTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.background_picTableAdapter();
            publicDataSetTableAdapters.Fill(publicDataSet.background_pic);
            System.Windows.Data.CollectionViewSource background_picViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("background_picViewSource"));

            background_picDataGrid.CanUserAddRows = false;


            


        }

        //private void create_background_Click(object sender, RoutedEventArgs e)
        //{

        //    fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
        //    fcb_public.publicDataSetTableAdapters.background_picTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.background_picTableAdapter();
        //    System.Windows.Data.CollectionViewSource background_picViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("background_picViewSource"));
        //    if (create_background.Content.ToString() == "add")
        //    {
        //        publicDataSet.background_pic.Addbackground_picRow(urlTextBox.Text,"");
        //        background_picViewSource.View.MoveCurrentToLast();
        //        background_picViewSource.View.MoveCurrentToNext();
        //        publicDataSetTableAdapters.Update(publicDataSet.background_pic);
        //        publicDataSet.background_pic.AcceptChanges();
        //    }
          
        //}

        private void gridDelete_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.background_picTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.background_picTableAdapter();
            publicDataSet.background_pic.Rows[background_picDataGrid.SelectedIndex].Delete();
            publicDataSetTableAdapters.Update(publicDataSet.background_pic);
            publicDataSet.background_pic.AcceptChanges();
        }

        private void url_liulan_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "jpg文件|*.jpg|RTF 文件 | *.rtf|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
            }
            
                string pic_guid = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(openFileDialog.SafeFileName);
                string aa = openFileDialog.FileName;
                 PublicClass.background_url = Directory.GetCurrentDirectory() + "\\image\\" + pic_guid;
                File.Copy(aa, PublicClass.background_url);
          
           // bbb.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
            


            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.background_picTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.background_picTableAdapter();
            System.Windows.Data.CollectionViewSource background_picViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("background_picViewSource"));
           
                publicDataSet.background_pic.Addbackground_picRow(pic_guid, "");
                background_picViewSource.View.MoveCurrentToLast();
                background_picViewSource.View.MoveCurrentToNext();
                publicDataSetTableAdapters.Update(publicDataSet.background_pic);
                publicDataSet.background_pic.AcceptChanges();



                foreach (var t in publicDataSet.background_pic)
                {
                    Image newimage = new Image();
                    newimage.Width = 50;
                    newimage.Height = 40;
                    newimage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\image\\" + t.url, UriKind.Absolute));
                    listbox.Items.Add(newimage);
                   
                   
                }
               // listbox.LostFocus += new RoutedEventHandler(listbox_LostFocus);
                listbox.MouseDown += new MouseButtonEventHandler(listbox_MouseDown);
            
        }

        void listbox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RoutedPropertyChangedEventArgs<object> args = new RoutedPropertyChangedEventArgs<object>(this, e);
            args.RoutedEvent = sub_background.BackGroundEvent;
            this.RaiseEvent(args);
        }
       


        //void listbox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    RoutedPropertyChangedEventArgs<object> args = new RoutedPropertyChangedEventArgs<object>(this, e);
        //    args.RoutedEvent = sub_background.BackGroundEvent;
        //    this.RaiseEvent(args);
        //}
    }
}
