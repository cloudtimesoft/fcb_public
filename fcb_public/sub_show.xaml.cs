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

namespace fcb_public
{
    /// <summary>
    /// sub_show.xaml 的交互逻辑
    /// </summary>
    public partial class sub_show : UserControl
    {
        
        public sub_show()
        {
            InitializeComponent();
           
        }

      


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.elementTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();
            System.Windows.Data.CollectionViewSource elementViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("elementViewSource"));
            left_line.Width = bottom_line.ActualWidth / 3;
            right_line.Width = bottom_line.ActualWidth / 3;
            title.Width = bottom_line.ActualWidth / 3;
            title.Margin = new Thickness(left_line.Width, 0, 0, 0);
            title.Content = publicDataSet.element.titleColumn.AllowDBNull;

            // 不要在设计时加载数据。
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//在此处加载数据并将结果指派给 CollectionViewSource。
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            // 不要在设计时加载数据。
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//在此处加载数据并将结果指派给 CollectionViewSource。
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            // 不要在设计时加载数据。
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//在此处加载数据并将结果指派给 CollectionViewSource。
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            // 不要在设计时加载数据。
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//在此处加载数据并将结果指派给 CollectionViewSource。
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void txt_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
             DoubleAnimation title_width_animation = new DoubleAnimation();
            ThicknessAnimation title_margin_animation = new ThicknessAnimation();
            DoubleAnimation content_width_animation = new DoubleAnimation();
            DoubleAnimation content_height_animation = new DoubleAnimation();
            ThicknessAnimation content_margin_animation = new ThicknessAnimation();
            DoubleAnimation txt_width_animation = new DoubleAnimation();
            ThicknessAnimation txt_margin_animation = new ThicknessAnimation();
            if (txt.Width < 720 || txt.ActualWidth < 720)
            {
                title_width_animation.To = SystemParameters.PrimaryScreenWidth;
                
                

                title_margin_animation.To = new Thickness(0, 10, 0, 0);
                
                

                content_height_animation.To = SystemParameters.PrimaryScreenHeight - 105;
                
                

                content_width_animation.To = SystemParameters.PrimaryScreenWidth;
                
                

                content_margin_animation.To = new Thickness(0, 10, 0, 0);
                
                

                txt_width_animation.To = SystemParameters.PrimaryScreenWidth;

                
                txt_margin_animation.From = new Thickness(0, 0, 0, 0);
                txt_margin_animation.To = new Thickness(0, SystemParameters.PrimaryScreenHeight-txt.ActualHeight - 110, 0, 0);
                

            }
            else
            {
                title_width_animation.To = 392;



                title_margin_animation.To = new Thickness(0, 200, 0, 0);



                content_height_animation.To = 533;



                content_width_animation.To = 392;



                content_margin_animation.To = new Thickness(0, 10, 0, 0);

                txt_margin_animation.From = new Thickness(0, 0, 0, 0);
                txt_margin_animation.To = new Thickness(0, SystemParameters.PrimaryScreenHeight-txt.ActualHeight -110, 0, 0);

                //txt_width_animation.To = SystemParameters.PrimaryScreenWidth;
            }
            title_width_animation.Duration = TimeSpan.FromSeconds(0.5);
            title_margin_animation.Duration = TimeSpan.FromSeconds(0.5);
            content_height_animation.Duration = TimeSpan.FromSeconds(0.5);
            content_width_animation.Duration = TimeSpan.FromSeconds(0.5);
            content_margin_animation.Duration = TimeSpan.FromSeconds(0.5);
            txt_width_animation.Duration = TimeSpan.FromSeconds(0.5);
            txt_margin_animation.Duration = TimeSpan.FromSeconds(500);

            txt.BeginAnimation(StackPanel.WidthProperty, title_width_animation);
            txt.BeginAnimation(StackPanel.MarginProperty, title_margin_animation);
            //content.BeginAnimation(StackPanel.HeightProperty, content_height_animation);
            //content.BeginAnimation(StackPanel.WidthProperty, title_width_animation);
            //content.BeginAnimation(StackPanel.MarginProperty, content_margin_animation);
            txt.BeginAnimation(TextBlock.WidthProperty, txt_width_animation);
            txt.BeginAnimation(TextBlock.MarginProperty, txt_margin_animation);
        }

     

        

     
    }
}
