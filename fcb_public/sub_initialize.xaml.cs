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
    /// sub_initialize.xaml 的交互逻辑
    /// </summary>
    public partial class sub_initialize : UserControl
    {
        public sub_initialize()
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
            fcb_public.publicDataSetTableAdapters.InitializeTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.InitializeTableAdapter();
            publicDataSetTableAdapters.Fill(publicDataSet.Initialize);
            System.Windows.Data.CollectionViewSource initializeViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("initializeViewSource"));
            initializeDataGrid.CanUserAddRows = false;


        }

        private void initialize_quedin_Click(object sender, RoutedEventArgs e)
        {

            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.InitializeTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.InitializeTableAdapter();
            System.Windows.Data.CollectionViewSource initializeViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("initializeViewSource"));
            if (initialize_quedin.Content.ToString() == "添加")
            {
                publicDataSet.Initialize.AddInitializeRow(inname_textbox.Text, true, int.Parse(mar_left_textbox.Text),int.Parse( mar_top_textbox.Text), int.Parse(mar_weight_textbox.Text),int.Parse( mar_height_textbox.Text));
                initializeViewSource.View.MoveCurrentToLast();
                initializeViewSource.View.MoveCurrentToNext();
                publicDataSetTableAdapters.Update(publicDataSet.Initialize);
                publicDataSet.Initialize.AcceptChanges();
            }
        }

        private void gridDelete_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.InitializeTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.InitializeTableAdapter();
            System.Windows.Data.CollectionViewSource initializeViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("initializeViewSource"));
            publicDataSet.Initialize.Rows[initializeDataGrid.SelectedIndex].Delete();
            publicDataSetTableAdapters.Update(publicDataSet.Initialize);
            publicDataSet.Initialize.AcceptChanges();
        }
    }
}
