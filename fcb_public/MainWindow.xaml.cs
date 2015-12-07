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
using System.Windows.Media.Animation;
using System.IO;

namespace fcb_public
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
       
        }
        Point old_point;
        string aa;
        double weight;
        double height;
        bool can_moves=false;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ImageBrush newimage = new ImageBrush();

            //newimage.ImageSource = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\image\\" + "background.jpg", UriKind.Absolute));
            //main_grid.Background = newimage;

            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            // 将数据加载到表 elset_init 中。可以根据需要修改此代码。
            fcb_public.publicDataSetTableAdapters.elset_initTableAdapter publicDataSetelset_initTableAdapter = new fcb_public.publicDataSetTableAdapters.elset_initTableAdapter();
            publicDataSetelset_initTableAdapter.Fill(publicDataSet.elset_init);
            System.Windows.Data.CollectionViewSource elset_initViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("elset_initViewSource")));
            elset_initViewSource.View.MoveCurrentToFirst();
            // 将数据加载到表 Initialize 中。可以根据需要修改此代码。
            fcb_public.publicDataSetTableAdapters.InitializeTableAdapter publicDataSetInitializeTableAdapter = new fcb_public.publicDataSetTableAdapters.InitializeTableAdapter();
            publicDataSetInitializeTableAdapter.Fill(publicDataSet.Initialize);
            System.Windows.Data.CollectionViewSource initializeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("initializeViewSource")));
            initializeViewSource.View.MoveCurrentToFirst();
            // 将数据加载到表 background_pic 中。可以根据需要修改此代码。
            fcb_public.publicDataSetTableAdapters.background_picTableAdapter publicDataSetbackground_picTableAdapter = new fcb_public.publicDataSetTableAdapters.background_picTableAdapter();
            publicDataSetbackground_picTableAdapter.Fill(publicDataSet.background_pic);
            System.Windows.Data.CollectionViewSource background_picViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("background_picViewSource")));
            background_picViewSource.View.MoveCurrentToFirst();

            var default_background = from c in publicDataSet.background_pic where c._default == "1" select c;
            foreach (var t in default_background)
            {
                PublicClass.background_url = Directory.GetCurrentDirectory() + "\\image\\" + t.url;
                ImageBrush newimage = new ImageBrush();

                newimage.ImageSource = new BitmapImage(new Uri(PublicClass.background_url, UriKind.Absolute));
                main_grid.Background = newimage;
            }

        }

        private void main_grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            old_point = e.GetPosition(null);
            weight = subwindow1.Width;
            height = subwindow1.Height;
            if (aa == "123")
            {
                can_moves = true;
            }
           
        }
        private void main_grid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(null).Y <= 30 && main_canvas.Margin.Top==-60)
            {

                ThicknessAnimation ta = new ThicknessAnimation();
                ta.From = new Thickness(0, -60, 0, 0);             //起始值
                ta.To = new Thickness(0, 0, 0, 0);        //结束值
                ta.Duration = TimeSpan.FromSeconds(0.5);         //动画持续时间
                main_canvas.BeginAnimation(TextBlock.MarginProperty, ta);//开始动画
                             
                ta.FillBehavior = FillBehavior.Stop;
              
            }


            if (e.GetPosition(null).X > PublicClass.show_right - 3 && e.GetPosition(null).X < PublicClass.show_right + 3)
            {
                main_grid.Cursor = Cursors.SizeWE;
                aa = "123";
                
            }

          else  if (e.GetPosition(null).Y > PublicClass.show_bottom - 3 && e.GetPosition(null).Y < PublicClass.show_bottom + 3)
            {
                main_grid.Cursor = Cursors.SizeNS;
              aa="456";
            }
            else
            {
                main_grid.Cursor = Cursors.Arrow;
                aa = "";

            }


            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (can_moves)
                {
                    subwindow1.Width = e.GetPosition(null).X - old_point.X + weight;
                }
                if (aa == "456")
                {
                    //subwindow1.Height = e.GetPosition(null).Y - old_point.Y + height;
                    
                    subwindow1.Height--;
                }
                //subwindow1.Margin = new Thickness(e.GetPosition(null).X, PublicClass.show_bottom - subwindow1.Height, 0, 0);
            }
          

            
        }

        private void main_grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            PublicClass.show_left = subwindow1.Margin.Left;
            PublicClass.show_right = subwindow1.Margin.Left + subwindow1.Width;
            PublicClass.show_bottom = subwindow1.Margin.Top + subwindow1.Height-45;
            //subwindow1.Background = Brushes.White;
            aa = "";

            can_moves = false;
        }

        private void element_Click(object sender, RoutedEventArgs e)
        {
            subwindow_content.Children.Clear();
            //sub_element newelement = new sub_element();
            sub_new_element newelement = new sub_new_element();
            subwindow.Width = 500;
            subwindow.Height = 640;
            newelement.Name = "newelement";
            subwindow.Opacity = 0.9;
            subwindow_content.Children.Add(newelement);
            Panel.SetZIndex(subwindow, 1);
            Panel.SetZIndex(submainwindow, 3000);
        }

        private void main_canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            ThicknessAnimation ta = new ThicknessAnimation();
           // ta.From = new Thickness(0, 0, 0, 0);             //起始值
            ta.To = new Thickness(0, -60, 0, 0);        //结束值
            ta.Duration = TimeSpan.FromSeconds(0.5);         //动画持续时间
            main_canvas.BeginAnimation(TextBlock.MarginProperty, ta);//开始动画
        }

        private void Background_pic_Click(object sender, RoutedEventArgs e)
        {
            subwindow_content.Children.Clear();
            sub_new_background newbackground = new sub_new_background();
            subwindow.Width = 720;
            subwindow.Height = 350;
            newbackground.NewBackGround += new RoutedPropertyChangedEventHandler<object>(newbackground_NewBackGround);
            newbackground.Name = "newbackground";
            subwindow.Opacity = 0.9;
            subwindow_content.Children.Add(newbackground);
            Panel.SetZIndex(submainwindow, 3000);
           // Panel.SetZIndex(subwindow, 1);
        }

        void newbackground_NewBackGround(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ImageBrush newimage = new ImageBrush();

            newimage.ImageSource = new BitmapImage(new Uri(PublicClass.background_url, UriKind.Absolute));
            main_grid.Background = newimage;
           
        }


        Point movepoint = new Point();
        bool can_move = false;
        private void subwindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            movepoint = e.GetPosition(null);
            if (e.GetPosition(subwindow).X < subwindow.Width && e.GetPosition(subwindow).Y < 20)
            {
                can_move = true;
            }
        }

        private void subwindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && can_move)
            {
                subwindow.Margin = new Thickness(subwindow.Margin.Left + e.GetPosition(null).X - movepoint.X, subwindow.Margin.Top + e.GetPosition(null).Y - movepoint.Y, 0, 0);
                movepoint = e.GetPosition(null);
            }
        }

        private void subwindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            can_move = false;
        }




        private void sub_window_close_Click(object sender, RoutedEventArgs e)//关闭弹出窗口
        {
            Panel.SetZIndex(submainwindow, -1);
            subwindow.Opacity = 0;

            mode_show();
        }

        private void mode_show()
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.InitializeTableAdapter publicDataSetInitializeTableAdapter = new fcb_public.publicDataSetTableAdapters.InitializeTableAdapter();
            publicDataSetInitializeTableAdapter.Fill(publicDataSet.Initialize);
            foreach (var t in publicDataSet.Initialize)
            {
                sub_new_show del_show = main_grid.FindName("s" + t.in_name) as sub_new_show;
                if (del_show != null)
                {
                    main_grid.Children.Remove(del_show);
                    main_grid.UnregisterName("s" + t.in_name);
                }
                sub_new_show newshow = new sub_new_show();
                newshow.Margin = new Thickness(t.mar_left, t.mar_top, 0, 0);
                newshow.Width = t.mar_weight;
                newshow.Height = t.mar_hight;
                newshow.elset_id = int.Parse(t.in_name);
                main_grid.Children.Add(newshow);
                main_grid.RegisterName("s" + t.in_name, newshow);

            }
        }

        private void element_set_Click(object sender, RoutedEventArgs e)
        {
            subwindow_content.Children.Clear();
            sub_new_elementset newelementset = new sub_new_elementset();
            subwindow.Width = 500;
            subwindow.Height = 560;
            newelementset.Name = "newelementset";
            subwindow.Opacity = 0.9;
            subwindow_content.Children.Add(newelementset);
            Panel.SetZIndex(submainwindow, 3000);




        }

        private void module_Click(object sender, RoutedEventArgs e)
        {
            subwindow_content.Children.Clear();
            sub_initialize newinitialize = new sub_initialize();
            subwindow.Width = 400;
            subwindow.Height = 500;
            newinitialize.Name = "newinitialize";
            subwindow.Opacity = 0.9;
            subwindow_content.Children.Add(newinitialize);
            Panel.SetZIndex(submainwindow, 3000);
        }

        private void roll_Click(object sender, RoutedEventArgs e)
        {
            subwindow_content.Children.Clear();
            sub_roll newroll = new sub_roll();
            subwindow.Width = 400;
            subwindow.Height = 400;
            newroll.Name = "newroll";
            subwindow.Opacity = 0.9;
            subwindow_content.Children.Add(newroll);
            Panel.SetZIndex(submainwindow, 3000);
        }

        private void show_window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sub_show newshow = new sub_show();
            newshow.Width = 700;
            newshow.Height = 800;
            //newshow.HorizontalAlignment = HorizontalAlignment.Center;
            //newshow.Margin = new Thickness(0, 200, 0, 0);
            DataObject data = new DataObject(typeof(sub_show), newshow);
            DragDrop.DoDragDrop(newshow, data, DragDropEffects.Copy);
            sub_addelement newadd = new sub_addelement();
           
          
        }

        void newadd_AddElement(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            sub_addelement newadd = new sub_addelement();
            subwindow.Width = 400;
            subwindow.Height = 400;

            newadd.Name = "newroll";
            subwindow.Opacity = 0.9;
            subwindow_content.Children.Add(newadd);
            Panel.SetZIndex(submainwindow, 3000);
        }






        private void main_grid_Drop(object sender, DragEventArgs e)
        {
          

            IDataObject data = new DataObject();
            data = e.Data;
            sub_show newshow = new sub_show();
            subwindow1.Width = 700;
            subwindow1.Height = 800;
            //newshow.Name = "show";
            subwindow1.Opacity = 0.9;
            //newshow = data.GetData(typeof(sub_show)) as sub_show;
            subwindow1.Margin = new Thickness(e.GetPosition(main_grid).X, e.GetPosition(main_grid).Y, 0, 0);
            //subwindow1.Margin = new Thickness(100,100, 0, 0);
            newshow.PreviewMouseRightButtonDown += new MouseButtonEventHandler(newshow_PreviewMouseRightButtonDown);
            subwindow_content1.Children.Add(newshow);

            PublicClass.show_left = subwindow1.Margin.Left;
            PublicClass.show_right = subwindow1.Margin.Left + subwindow1.Width;
            PublicClass.show_bottom = subwindow1.Margin.Top + subwindow1.Height-45;

           

        }

        void newshow_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

           // UIElement uid = new UIElement();
           //uid= ContextMenuService.GetPlacementTarget( LogicalTreeHelper.GetParent(sender as MenuItem));

           //Menu newmenu = new Menu();
           MenuItem newmenuitem = new MenuItem();
           newmenuitem.Header = "添加元素";
           newmenuitem.Width = 150;
           newmenuitem.Height = 30;
           newmenuitem.Background = Brushes.White;
           newmenuitem.Click += new RoutedEventHandler(newmenuitem_Click);
          // newmenu.Items.Add(newmenuitem);
           main_grid.Children.Add(newmenuitem);
          
        }

        void newmenuitem_Click(object sender, RoutedEventArgs e)
        {
            subwindow_content.Children.Clear();
            sub_addelement newadd = new sub_addelement();
            subwindow.Width = 400;
            subwindow.Height = 400;
            newadd.Name = "newadd";
            subwindow.Opacity = 0.9;
           
            subwindow_content.Children.Add(newadd);
            Panel.SetZIndex(submainwindow, 3000);




            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            // 将数据加载到表 elset_init 中。可以根据需要修改此代码。
            fcb_public.publicDataSetTableAdapters.elset_initTableAdapter publicDataSetelset_initTableAdapter = new fcb_public.publicDataSetTableAdapters.elset_initTableAdapter();
            publicDataSetelset_initTableAdapter.Fill(publicDataSet.elset_init);
            System.Windows.Data.CollectionViewSource elset_initViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("elset_initViewSource")));
            elset_initViewSource.View.MoveCurrentToFirst();
            // 将数据加载到表 Initialize 中。可以根据需要修改此代码。
            fcb_public.publicDataSetTableAdapters.InitializeTableAdapter publicDataSetInitializeTableAdapter = new fcb_public.publicDataSetTableAdapters.InitializeTableAdapter();



            var query = from c in publicDataSet.Initialize join t in publicDataSet.elset_init on c.ID equals t.Initialize_ID select c;

            //publicDataSetInitializeTableAdapter.Fill(query);
            
            System.Windows.Data.CollectionViewSource initializeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("initializeViewSource")));
            
            initializeViewSource.View.MoveCurrentToFirst();

            
            


        }


  

        private void subwindow1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            movepoint = e.GetPosition(null);
            if (e.GetPosition(subwindow1).X < subwindow1.Width && e.GetPosition(subwindow1).Y < 20)
            {
                can_move = true;
            }
        }

        private void subwindow1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            can_move = false;
            PublicClass.show_left = subwindow1.Margin.Left;
            PublicClass.show_right = subwindow1.Margin.Left + subwindow1.Width;
            PublicClass.show_bottom = subwindow1.Margin.Top + subwindow1.Height;
        }

        private void subwindow1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && can_move)
            {
                subwindow1.Margin = new Thickness(subwindow1.Margin.Left + e.GetPosition(null).X - movepoint.X, subwindow1.Margin.Top + e.GetPosition(null).Y - movepoint.Y, 0, 0);
                movepoint = e.GetPosition(null);
            }
        }


        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void test_show_Click(object sender, RoutedEventArgs e)
        {
            //sub_new_show newshow = new sub_new_show();
            //newshow.Width = 400;
            //newshow.Height = 500;
            
            //newshow.Margin = new Thickness(100, 100, 0, 0);
            ////submainwindow.Children.Add(newshow);
            ////subwindow_content1.Children.Add(newshow);
            
            //main_grid.Children.Add(newshow);


            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            foreach (var t in publicDataSet.Initialize)
            {
                sub_new_show newshow = new sub_new_show();
                newshow.Margin = new Thickness(t.mar_left, t.mar_top, 0, 0);
                newshow.Width = t.mar_weight;
                newshow.Height = t.mar_hight;
                main_grid.Children.Add(newshow);
                main_grid.RegisterName("s" + t.in_name, newshow);

            }



        }

   


    






   









    


    }
}
