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
            rollDataGrid.CanUserAddRows = false;
         // roll_quedin.Content=  System.DateTime.UtcNow.AddHours(-5.5);

        }

        private void roll_quedin_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.rollTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.rollTableAdapter();
            
            System.Windows.Data.CollectionViewSource initializeViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("rollViewSource"));
            if (roll_quedin.Content.ToString() == "添加")
            {
                publicDataSet.roll.AddrollRow(title_textbox.Text, txt_textbox.Text, (bool)statusCheckBox.IsChecked,1);
                publicDataSetTableAdapters.Update(publicDataSet.roll);
                publicDataSet.roll.AcceptChanges();
                publicDataSetTableAdapters.Fill(publicDataSet.roll);
 
            }
        }

        private void show_roll_Click(object sender, RoutedEventArgs e)
        {
           
                if (show_roll.Content.ToString() == "展示")
                {
                    PublicClass.show = "showroll";
                    show_roll.Content = "取消展示";
                }
                else
                {
                    PublicClass.show = "";
                    show_roll.Content = "展示";
                }
        }

        private void roll_del_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.rollTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.rollTableAdapter();
            if (iDTextBox.Text != "")
            {
                int sel_id = int.Parse(iDTextBox.Text);
                publicDataSet.roll.FindByID(sel_id).Delete();
                publicDataSetTableAdapters.Update(publicDataSet.roll);
                publicDataSet.roll.AcceptChanges();
                publicDataSetTableAdapters.Fill(publicDataSet.roll);
            }
        }


        public void savechanges()
        {

            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.rollTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.rollTableAdapter();
          
            System.Windows.Data.CollectionViewSource initializeViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("rollViewSource"));

            //publicDataSetTableAdapters.Fill(publicDataSet.element);
            initializeViewSource.View.MoveCurrentToNext();
            publicDataSetTableAdapters.Update(publicDataSet.roll);
            // publicDataSet.element.AcceptChanges();


        }

        


    }
}
