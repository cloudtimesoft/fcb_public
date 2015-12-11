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
using Microsoft.Win32;
using System.IO;

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

        string openname;

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
           // typeComboBox.SelectionChanged -= new SelectionChangedEventHandler(typeComboBox_SelectionChanged);
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


                if (typeComboBox.Text !="文档")
                {
                    string t = typeComboBox.Text;
                    File.Copy(openname, PublicClass.background_url);
                }
                publicDataSet.element.AddelementRow(titleTextBox.Text, contentTextBox.Text, typeComboBox.Text, DateTime.Parse(start_timeDatePicker.Text), DateTime.Parse(end_timeDatePicker.Text), (bool)statusCheckBox.IsChecked,int.Parse(show_timeTextBox.Text));
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

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                contentTextBox.Height = 140;
                Button newbtn = element_grid.FindName("newbutton") as Button;
                if (newbtn != null)
                {
                    element_grid.Children.Remove(newbtn);
                    element_grid.UnregisterName("newbutton");
                }

            }
            if (typeComboBox.SelectedIndex == 1 || typeComboBox.SelectedIndex == 2 )
            {
                contentTextBox.Height = 26;
                //contentTextBox.Visibility = Visibility.Hidden;
                Button newbutton = new Button();
                //newbutton.Name = "newbutton";
                newbutton.Width = 40;
                newbutton.Height = 30;
                newbutton.Content = "上传";
                newbutton.Margin = new Thickness(259, 405, 0, 0);
                element_grid.Children.Add(newbutton);
                element_grid.RegisterName("newbutton", newbutton);
                newbutton.Click += new RoutedEventHandler(newbutton_Click);
                
            }
            else
            {
                contentTextBox.Height = 140;
                Button newbtn = element_grid.FindName("newbutton") as Button;
                if (newbtn != null)
                {
                    element_grid.Children.Remove(newbtn);
                    element_grid.UnregisterName("newbutton");
                }

            }
        }

        void newbutton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "jpg文件|*.jpg|RTF 文件 | *.rtf|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
            }

            string pic_guid = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(openFileDialog.SafeFileName);
            openname = openFileDialog.FileName;
            PublicClass.background_url = Directory.GetCurrentDirectory() + "\\image\\" + pic_guid;
            contentTextBox.Text = Directory.GetCurrentDirectory() + "\\image\\" + pic_guid;
          


           

          


        }

        private void typeComboBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //typeComboBox.SelectionChanged+=new SelectionChangedEventHandler(typeComboBox_SelectionChanged);
        }
    }
}
