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
    /// sub_elementset.xaml 的交互逻辑
    /// </summary>
    public partial class sub_elementset : UserControl
    {
        public sub_elementset()
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
            fcb_public.publicDataSetTableAdapters.element_setTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.element_setTableAdapter();
            publicDataSetTableAdapters.Fill(publicDataSet.element_set);
            System.Windows.Data.CollectionViewSource element_setViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("element_setViewSource"));
            element_setDataGrid.CanUserAddRows = false;
        }

        private void elementset_quedin_Click(object sender, RoutedEventArgs e)
        {
            //fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            //fcb_public.publicDataSetTableAdapters.element_setTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.element_setTableAdapter();
            //System.Windows.Data.CollectionViewSource element_setViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("element_setViewSource"));
            //if (elementset_quedin.Content.ToString() == "添加")
            //{
            //    publicDataSet.element_set.Addelement_setRow(elsetname_textbox.Text, DateTime.Parse(starttime_textbox.Text), DateTime.Parse(endtime_textbox.Text), true);
            //    element_setViewSource.View.MoveCurrentToLast();
            //    element_setViewSource.View.MoveCurrentToNext();
            //    publicDataSetTableAdapters.Update(publicDataSet.element_set);
            //    publicDataSet.element_set.AcceptChanges();
            //}
        }

        private void gridDelete_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.element_setTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.element_setTableAdapter();
            System.Windows.Data.CollectionViewSource element_setViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("element_setViewSource"));
            publicDataSet.element_set.Rows[element_setDataGrid.SelectedIndex].Delete();
            publicDataSetTableAdapters.Update(publicDataSet.element_set);
            publicDataSet.element_set.AcceptChanges();
        }
    }
}
