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

        string ctrl_name = "";
        string process_type = "";
        Thickness old_margin = new Thickness();
        double old_width;
        double old_height;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ImageBrush newimages = new ImageBrush();

            //newimages.ImageSource = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\image\\" + "background.jpg", UriKind.Absolute));
            //main_grid.Background = newimages;

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
            main_canvas.Width = main_grid.ActualWidth;

            //Color color = new Color();

            //color.A = 0;

            //color.R = 0;

            //color.G = 0;

            //color.B = 0;        
            //toolbarpanel.Background = new SolidColorBrush(color);
            //toolbarpanel.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
            main_canvas.Background = new SolidColorBrush(Color.FromArgb(20, 255, 255, 255));


        }

        private void main_grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            old_point = e.GetPosition(main_grid);
            weight = subwindow1.Width;
            height = subwindow1.Height;
            if (aa == "123")
            {
                can_moves = true;
            }
          

            foreach (var s in main_grid.Children)
            {
                sub_new_show newshow = s as sub_new_show;
                if (newshow != null)
                {
                    if (e.GetPosition(main_grid).X > newshow.Margin.Left && e.GetPosition(main_grid).X < newshow.Margin.Left + newshow.Width &&e.GetPosition(main_grid).Y>newshow.Margin.Top&&e.GetPosition(main_grid).Y<newshow.Margin.Top+44)
                    {
                        ctrl_name = newshow.Name;
                        process_type = "move";
                        old_margin = newshow.Margin;
                        main_grid.Cursor = Cursors.Hand;
                    }
                    else if (e.GetPosition(main_grid).X > newshow.Margin.Left - 5 && e.GetPosition(main_grid).X < newshow.Margin.Left + 5 && e.GetPosition(main_grid).Y > newshow.Margin.Top + 54 && e.GetPosition(main_grid).Y < newshow.Margin.Top + newshow.Height)
                    {
                        //main_grid.Cursor = Cursors.ScrollSE;
                        ctrl_name = newshow.Name;
                        process_type = "leftmove";
                        old_margin = newshow.Margin;
                        old_width = newshow.Width;
                    }
                    else if (e.GetPosition(main_grid).X > newshow.Margin.Left + newshow.Width - 5 && e.GetPosition(main_grid).X < newshow.Margin.Left + newshow.Width + 5 && e.GetPosition(main_grid).Y > newshow.Margin.Top + 54 && e.GetPosition(main_grid).Y < newshow.Margin.Top + newshow.Height)
                    {
                        ctrl_name = newshow.Name;
                        process_type = "rightmove";
                        old_margin = newshow.Margin;
                        old_width = newshow.Width;
                    }
                    else if (e.GetPosition(main_grid).X > newshow.Margin.Left && e.GetPosition(main_grid).X < newshow.Margin.Left + newshow.Width && e.GetPosition(main_grid).Y > newshow.Margin.Top +newshow.Height-5 && e.GetPosition(main_grid).Y < newshow.Margin.Top +newshow.Height+5)
                    {
                        ctrl_name = newshow.Name;
                        process_type = "bottommove";
                        old_margin = newshow.Margin;
                        old_height = newshow.Height;
                        main_grid.Cursor = Cursors.Hand;
                    }
                }
                sub_showweather newweather = s as sub_showweather;
                if (newweather != null)
                {
                    if (e.GetPosition(main_grid).X > newweather.Margin.Left && e.GetPosition(main_grid).X < newweather.Margin.Left + newweather.Width && e.GetPosition(main_grid).Y > newweather.Margin.Top && e.GetPosition(main_grid).Y < newweather.Margin.Top + 200)
                    {
                        ctrl_name = newweather.Name;
                        process_type = "move";
                        old_margin = newweather.Margin;
                        //main_grid.Cursor = Cursors.Hand;
                    }
                   
                }
                StackPanel newpanel = FindName("rollstackpanel") as StackPanel;
                if (newpanel != null)
                {
                    if (e.GetPosition(big_grid).Y > newpanel.Margin.Top)
                    {
                        ctrl_name = newpanel.Name;
                        process_type = "move";
                        old_margin = newpanel.Margin;
                        big_grid.Cursor = Cursors.Hand;
                    }
                }
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
            //fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            //fcb_public.publicDataSetTableAdapters.InitializeTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.InitializeTableAdapter();
            //foreach (var s in publicDataSet.Initialize)
            //{
            //    PublicClass.show_left = s.mar_hight;
            //    PublicClass
            //}

            if ((e.GetPosition(null).Y > PublicClass.show_bottom - 3 && e.GetPosition(null).Y < PublicClass.show_bottom + 3))
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


                sub_new_show newshow = main_grid.FindName(ctrl_name) as sub_new_show;
                if (newshow != null && process_type=="move")
                {
                    newshow.Margin = new Thickness(old_margin.Left + e.GetPosition(main_grid).X - old_point.X, old_margin.Top + e.GetPosition(main_grid).Y - old_point.Y, 0, 0);
                }
                else if (newshow != null && process_type == "leftmove")
                {
                    newshow.Width = old_width - (e.GetPosition(main_grid).X - old_margin.Left);
                    newshow.Margin = new Thickness(old_margin.Left - (old_margin.Left-e.GetPosition(main_grid).X  ), old_margin.Top, 0, 0);
                }
                else if (newshow != null && process_type == "rightmove")
                {
                    newshow.Width = old_width + (e.GetPosition(main_grid).X-old_point.X);
                    //newshow.Margin = new Thickness(old_margin.Left - (old_margin.Left - e.GetPosition(main_grid).X), old_margin.Top, 0, 0);
                }
                else if (newshow != null && process_type == "bottommove")
                {
                    newshow.Height = old_height + (e.GetPosition(main_grid).Y - old_point.Y);
                    //newshow.Margin = new Thickness(old_margin.Left - (old_margin.Left - e.GetPosition(main_grid).X), old_margin.Top, 0, 0);
                }
                sub_showweather newweather = main_grid.FindName(ctrl_name) as sub_showweather;
                if (newweather != null && process_type == "move")
                {
                    newweather.Margin = new Thickness(old_margin.Left + e.GetPosition(main_grid).X - old_point.X, old_margin.Top + e.GetPosition(main_grid).Y - old_point.Y, 0, 0);
                }
                StackPanel newpanel = big_grid.FindName("rollstackpanel") as StackPanel;
                if (newpanel != null && process_type == "move")
                {
                    newpanel.Margin = new Thickness(old_margin.Left, old_margin.Top + e.GetPosition(big_grid).Y - old_point.Y, 0, 0);
                }


            }
          

            
        }

        private void main_grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.InitializeTableAdapter publicDataSetInitializeTableAdapter = new fcb_public.publicDataSetTableAdapters.InitializeTableAdapter();
            fcb_public.publicDataSetTableAdapters.weatherTableAdapter publicDataSetweatherTableAdapter = new fcb_public.publicDataSetTableAdapters.weatherTableAdapter();
            publicDataSetweatherTableAdapter.Fill(publicDataSet.weather);
            publicDataSetInitializeTableAdapter.Fill(publicDataSet.Initialize);

            sub_new_show newshow = main_grid.FindName(ctrl_name) as sub_new_show;
            sub_showweather newweather = main_grid.FindName(ctrl_name) as sub_showweather;
            if (newshow != null)
            {
                var s = from c in publicDataSet.Initialize where c.in_name == ctrl_name.Substring(1,ctrl_name.Length-1) select c;
                foreach (var t in s)
                {
                    t.mar_left = (int)newshow.Margin.Left;
                    t.mar_top = (int)newshow.Margin.Top;
                    t.mar_weight = (int)newshow.Width;
                    t.mar_hight = (int)newshow.Height;
                }
                publicDataSetInitializeTableAdapter.Update(publicDataSet.Initialize);
                publicDataSet.AcceptChanges();
                newshow.init_show();



                PublicClass.show_hight = newshow.Height - 70.0;
                mode_show();
            }
            if (newweather != null)
            {
                var s = from c in publicDataSet.weather where c.in_name == ctrl_name.Substring(1, ctrl_name.Length - 1) select c;
                foreach (var t in s)
                {
                    t.mar_left = (int)newweather.Margin.Left;
                    t.mar_top = (int)newweather.Margin.Top;
                   
                }
                publicDataSetweatherTableAdapter.Update(publicDataSet.weather);
                publicDataSet.AcceptChanges();
            }

            ctrl_name = "";
            process_type = "";
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
            subwindow.Width = 620;
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
            subwindow.Width = 0;
            subwindow.Height = 0;

            mode_show();
        }

        private void mode_show()
        {
            if (PublicClass.show == "showelementset")
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
                    newshow.Name = "s" + t.in_name;

                    newshow.elset_id = int.Parse(t.in_name);
                    main_grid.Children.Add(newshow);
                    //Panel.SetZIndex(submainwindow1, 3000);
                    main_grid.RegisterName("s" + t.in_name, newshow);

                }
            }

            if (PublicClass.show == "showroll")
            {


                TextBlock newtextblock = new TextBlock();

                fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
                fcb_public.publicDataSetTableAdapters.rollTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.rollTableAdapter();
                publicDataSetTableAdapters.Fill(publicDataSet.roll);

                //newtextblock.Text = publicDataSet.roll.FindByID(PublicClass.roll_index).txt;
                var select = from c in publicDataSet.roll where c.status == true select c;
                //int s1 = select.Count();
                //int step = 0;
                //int temp_step = 0;
                //if (s1 > 0)
                //{
                foreach (var s in select)
                {
                    

                        newtextblock.Text +="      "+ s.txt;

                    
                }
                //    step++;
                //}
                //if (step > s1)
                //{
                //    step = 0;
                //}

                newtextblock.FontSize = 30;
                newtextblock.Foreground = Brushes.White;
               // newtextblock.Background = Brushes.AliceBlue;
                newtextblock.TextWrapping = TextWrapping.NoWrap;
                StackPanel delpanel = main_grid.FindName("rollstackpanel") as StackPanel;
                if (delpanel != null)
                {
                    main_grid.Children.Remove(delpanel);
                    main_grid.UnregisterName("rollstackpanel");
                }

                StackPanel newstackpanel = new StackPanel();
                newstackpanel.Name = "rollstackpanel";
                newstackpanel.Width = main_grid.ActualWidth;
                newstackpanel.Height = 36;
                newstackpanel.Margin = new Thickness(0, SystemParameters.PrimaryScreenHeight-36, 0, 0);
               // newstackpanel.Background = Brushes.Green;
               // newstackpanel.Background=new
               // newstackpanel.VerticalAlignment = VerticalAlignment.Bottom;
                newstackpanel.Children.Add(newtextblock);
                main_grid.Children.Add(newstackpanel);
                main_grid.RegisterName("rollstackpanel", newstackpanel);
                newtextblock.UpdateLayout();
                ThicknessAnimation txt_margin_animation = new ThicknessAnimation();
                txt_margin_animation.From = new Thickness(SystemParameters.PrimaryScreenWidth, 0, 0, 0);
                txt_margin_animation.To = new Thickness(-newtextblock.ActualWidth, 0, 0, 0);
                txt_margin_animation.Duration = TimeSpan.FromSeconds(20);
                txt_margin_animation.RepeatBehavior = RepeatBehavior.Forever;
                double t = newtextblock.ActualHeight;
                newtextblock.BeginAnimation(TextBlock.MarginProperty, txt_margin_animation);
               




            }
            if (PublicClass.show == "showweather")
            {
                fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
                fcb_public.publicDataSetTableAdapters.weatherTableAdapter publicDataSetInitializeTableAdapter = new fcb_public.publicDataSetTableAdapters.weatherTableAdapter();
                publicDataSetInitializeTableAdapter.Fill(publicDataSet.weather);
                var show_weatherconut = from c in publicDataSet.weather where c.status == true select c;
                foreach (var t in show_weatherconut)
                {
                   
                        sub_showweather del_show = main_grid.FindName("w" + t.in_name) as sub_showweather;
                        if (del_show != null)
                        {
                            main_grid.Children.Remove(del_show);
                            main_grid.UnregisterName("w" + t.in_name);
                        }

                        sub_showweather newshow = new sub_showweather();
                        newshow.Margin = new Thickness(t.mar_left, t.mar_top, 0, 0);
                        newshow.Name = "w" + t.in_name;
                        newshow.city_name = t.in_name;
                        newshow.gmt = t.gmt;
                        newshow.Width = 200;
                        newshow.Height = 184;
                        //newshow.Background = Brushes.White;
                        //newshow.Opacity = 0.2;
                        // newshow.elset_id = int.Parse(t.in_name);
                        main_grid.Children.Add(newshow);
                        //Panel.SetZIndex(submainwindow1, 3000);
                        main_grid.RegisterName("w" + t.in_name, newshow);



                    
                }
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

            //PublicClass.show_left = subwindow1.Margin.Left;
            //PublicClass.show_right = subwindow1.Margin.Left + subwindow1.Width;
            //PublicClass.show_bottom = subwindow1.Margin.Top + subwindow1.Height-45;

           

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


            //fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            //foreach (var t in publicDataSet.Initialize)
            //{
            //    sub_new_show newshow = new sub_new_show();
            //    newshow.Margin = new Thickness(t.mar_left, t.mar_top, 0, 0);
            //    newshow.Width = t.mar_weight;
            //    newshow.Height = t.mar_hight;
            //    main_grid.Children.Add(newshow);
            //    main_grid.RegisterName("s" + t.in_name, newshow);

            //}


            //test newtest = new test();
            //newtest.Margin = new Thickness(810, 300, 0, 0);
            //newtest.VerticalAlignment = VerticalAlignment.Bottom;
            //newtest.HorizontalAlignment = HorizontalAlignment.Left;
            //newtest.VerticalAlignment = VerticalAlignment.Bottom;
            
            //newtest.Width = main_grid.ActualWidth;
            //main_grid.Children.Add(newtest);


          


        }



        private void weather_Click(object sender, RoutedEventArgs e)
        {
          
            subwindow_content.Children.Clear();
            sub_weather newweather = new sub_weather();
            subwindow.Width = 450;
            subwindow.Height = 350;
            newweather.Name = "newweather";
            subwindow.Opacity = 0.9;

            subwindow_content.Children.Add(newweather);
            Panel.SetZIndex(submainwindow, 3000);

        }

   


    






   









    


    }
}
