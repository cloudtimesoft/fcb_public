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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ImageBrush newimage = new ImageBrush();

            newimage.ImageSource = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\image\\" + "background.jpg", UriKind.Absolute));
            main_grid.Background = newimage;
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
           
            
        }

        private void element_Click(object sender, RoutedEventArgs e)
        {
            subwindow_content.Children.Clear();
            sub_element newelement = new sub_element();
            subwindow.Width = 400;
            subwindow.Height = 550;
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
            sub_background newbackground = new sub_background();
            subwindow.Width = 300;
            subwindow.Height = 320;
            newbackground.BackGround += new RoutedPropertyChangedEventHandler<object>(newbackground_BackGround);
            newbackground.Name = "newbackground";
            subwindow.Opacity = 0.9;
            subwindow_content.Children.Add(newbackground);
            Panel.SetZIndex(submainwindow, 3000);
           // Panel.SetZIndex(subwindow, 1);
        }

        void newbackground_BackGround(object sender, RoutedPropertyChangedEventArgs<object> e)
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
           
        }

        private void element_set_Click(object sender, RoutedEventArgs e)
        {
            subwindow_content.Children.Clear();
            sub_elementset newelementset = new sub_elementset();
            subwindow.Width = 400;
            subwindow.Height = 550;
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
            newshow.HorizontalAlignment = HorizontalAlignment.Center;
            newshow.Margin = new Thickness(0, 200, 0, 0);
            DataObject data = new DataObject(typeof(sub_show), newshow);
            DragDrop.DoDragDrop(newshow, data, DragDropEffects.Copy);
            PublicClass.dragdrop_up = true;

        }



        private void show_window_Click(object sender, RoutedEventArgs e)
        {
            sub_show newshow = new sub_show();
            newshow.Width = 700;
            newshow.Height = 800;
            newshow.HorizontalAlignment = HorizontalAlignment.Center;
            newshow.Margin = new Thickness(0, 200, 0, 0);
            show.Children.Add(newshow);
            Panel.SetZIndex(show, 3000);
        }

        private void main_grid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (PublicClass.dragdrop_up)
            //{
            //   if()
            //}
        }

   









    


    }
}
