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

namespace fcb_public
{
    /// <summary>
    /// sub_roll.xaml 的交互逻辑
    /// </summary>
    public partial class sub_roll : UserControl
    {
        public sub_roll()
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

            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.rollTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.rollTableAdapter();
            publicDataSetTableAdapters.Fill(publicDataSet.roll);
            System.Windows.Data.CollectionViewSource initializeViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("rollViewSource"));


        }

        private void roll_quedin_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.rollTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.rollTableAdapter();
            
            System.Windows.Data.CollectionViewSource initializeViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("rollViewSource"));
            if (roll_quedin.Content.ToString() == "添加")
            {
                publicDataSet.roll.AddrollRow(title_textbox.Text, txt_textbox.Text);
                publicDataSetTableAdapters.Update(publicDataSet.roll);
                publicDataSet.roll.AcceptChanges();
                publicDataSetTableAdapters.Fill(publicDataSet.roll);
 
            }
        }

        private void show_roll_Click(object sender, RoutedEventArgs e)
        {
            if (iDTextBox.Text != null)
            {
                PublicClass.roll_index = int.Parse(iDTextBox.Text);
                PublicClass.show = "showroll";
            }
        }

        


    }
}
