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
    /// sub_element.xaml 的交互逻辑
    /// </summary>
    public partial class sub_element : UserControl
    {
        public sub_element()
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
            fcb_public.publicDataSetTableAdapters.elementTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();
            publicDataSetTableAdapters.Fill(publicDataSet.element);
            System.Windows.Data.CollectionViewSource elementViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("elementViewSource"));
            elementDataGrid.CanUserAddRows = false;
           // background_picDataGrid.CanUserAddRows = false;




        }

        private void element_quedin_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.elementTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();
            System.Windows.Data.CollectionViewSource elementViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("elementViewSource"));
            if (element_quedin.Content.ToString() == "添加")
            {
                publicDataSet.element.AddelementRow(title_textbox.Text, content_textbox.Text, type_textbox.Text, DateTime.Parse(starttime_textbox.Text), DateTime.Parse(endtime_textbox.Text), true, 1);
                elementViewSource.View.MoveCurrentToLast();
                
                publicDataSetTableAdapters.Update(publicDataSet.element);
                publicDataSet.element.AcceptChanges();
                elementViewSource.View.MoveCurrentToNext();
            }
        }

        private void gridDelete_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.elementTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();
            System.Windows.Data.CollectionViewSource elementViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("elementViewSource"));
            publicDataSet.element.Rows[elementDataGrid.SelectedIndex].Delete();
           //publicDataSet.element.AcceptChanges();
            publicDataSetTableAdapters.Update(publicDataSet.element);
            publicDataSet.element.AcceptChanges();
        }
    }
}
