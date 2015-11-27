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
            left_line.Width = bottom_line.ActualWidth / 3;
            right_line.Width = bottom_line.ActualWidth / 3;
        }

     
    }
}
