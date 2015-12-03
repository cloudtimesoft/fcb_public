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
    /// sub_new_element.xaml 的交互逻辑
    /// </summary>
    public partial class sub_new_element : UserControl
    {
        public sub_new_element()
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
            //publicDataSet.element =(publicDataSet.elementDataTable) from c in publicDataSet.element where c.ID == 2 select c;
            publicDataSetTableAdapters.Fill(publicDataSet.element);
            System.Windows.Data.CollectionViewSource elementViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("elementViewSource"));
            
            elementDataGrid.CanUserAddRows = false;
            typeComboBox.SelectedIndex = 0;
        }

        private void add_record_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.elementTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();

            if (add_record.Content.ToString() == "新增")
            {
                add_record.Content = "保存";
                titleTextBox.Text = "";
                typeComboBox.SelectedIndex = 0;
            }
            else
            {
                add_record.Content = "新增";
                publicDataSet.element.AddelementRow(titleTextBox.Text, contentTextBox.Text, typeComboBox.Text, DateTime.Parse(start_timeDatePicker.Text), DateTime.Parse(end_timeDatePicker.Text), (bool)statusCheckBox.IsChecked);
                publicDataSetTableAdapters.Update(publicDataSet.element);
                publicDataSet.element.AcceptChanges();
                publicDataSetTableAdapters.Fill(publicDataSet.element);
                
                
            }

            //publicDataSetTableAdapters.Fill(publicDataSet.element);
            //System.Windows.Data.CollectionViewSource elementViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("elementViewSource"));
            //elementDataGrid.CanUserAddRows = false;
        }

        private void delete_record_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.elementTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();
            if (publicDataSet.element.Count > 0)
            {
                publicDataSet.element.Rows[elementDataGrid.SelectedIndex].Delete();
                publicDataSetTableAdapters.Update(publicDataSet.element);
                publicDataSet.element.AcceptChanges();
                publicDataSetTableAdapters.Fill(publicDataSet.element);
            }
        }
    }
}
