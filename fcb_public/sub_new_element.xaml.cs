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
using System.Text.RegularExpressions;


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
        int id_index;
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
            addtoolfontname();
            start_timeDatePicker.SelectedDate = System.DateTime.Now;
            end_timeDatePicker.SelectedDate = System.DateTime.Now.AddDays(7);
            show_timeTextBox.Text = "1";
           
        }

        private void add_record_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            fcb_public.publicDataSetTableAdapters.elementTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();
            System.Windows.Data.CollectionViewSource elementViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("elementViewSource"));

            if (add_record.Content.ToString() == "新增")
            {
                add_record.Content = "保存";
                titleTextBox.Text = "";
                typeComboBox.SelectedIndex = 0;
                //elementViewSource.View.MoveCurrentToLast();
            }
            else
            {
                add_record.Content = "新增";


                if (typeComboBox.Text !="文档")
                {
                    string t = typeComboBox.Text;
                    File.Copy(openname, PublicClass.background_url);
                }
                if (typeComboBox.Text == "文档")
                {
                    publicDataSet.element.AddelementRow(titleTextBox.Text, contentTextBox.Text, typeComboBox.Text, DateTime.Parse(start_timeDatePicker.Text), DateTime.Parse(end_timeDatePicker.Text), (bool)statusCheckBox.IsChecked, int.Parse(show_timeTextBox.Text));
                }
                else if (typeComboBox.Text == "图片" || typeComboBox.Text == "视频")
                {
                    TextRange TRB = new TextRange(richTextBox1.Document.ContentStart, richTextBox1.Document.ContentEnd);
                    publicDataSet.element.AddelementRow(titleTextBox.Text, TRB.Text, typeComboBox.Text, DateTime.Parse(start_timeDatePicker.Text), DateTime.Parse(end_timeDatePicker.Text), (bool)statusCheckBox.IsChecked, int.Parse(show_timeTextBox.Text));
                }
               
               
                publicDataSetTableAdapters.Update(publicDataSet.element);
                publicDataSet.element.AcceptChanges();
                publicDataSetTableAdapters.Fill(publicDataSet.element);
            }
            System.Windows.Documents.FlowDocument doc = richTextBox1.Document;
            doc.Blocks.Clear();

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

                publicDataSet.element.FindByID(id_index).Delete();
                publicDataSetTableAdapters.Update(publicDataSet.element);
                publicDataSet.element.AcceptChanges();
                publicDataSetTableAdapters.Fill(publicDataSet.element);
                System.Windows.Documents.FlowDocument doc = richTextBox1.Document;
                doc.Blocks.Clear();
            }
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            {
                richTextBox1.Height = 140;
                Button newbtn = element_grid.FindName("newbutton") as Button;
                if (newbtn != null)
                {
                    element_grid.Children.Remove(newbtn);
                    element_grid.UnregisterName("newbutton");
                }

            }
            if (typeComboBox.SelectedIndex == 1 || typeComboBox.SelectedIndex == 2 )
            {
                toolBar1.Visibility = Visibility.Hidden;
                richTextBox1.TextChanged-=new TextChangedEventHandler(richTextBox1_TextChanged);
                richTextBox1.Height = 26;
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
                toolBar1.Visibility = Visibility.Visible;
                richTextBox1.Height = 140;
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
            openFileDialog.Filter = "jpg文件|*.jpg|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
            }

            string pic_guid = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(openFileDialog.SafeFileName);
            openname = openFileDialog.FileName;
            PublicClass.background_url = Directory.GetCurrentDirectory() + "\\image\\" + pic_guid;
            //contentTextBox.Text = Directory.GetCurrentDirectory() + "\\image\\" + pic_guid;
            FlowDocument newfd=new FlowDocument();
            Paragraph p = new Paragraph();
            p.Inlines.Add(Directory.GetCurrentDirectory() + "\\image\\" + pic_guid);
            newfd.Blocks.Add(p);
            richTextBox1.Document = newfd;
            

           

          


        }



        private void elementDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            id_index = int.Parse(iDTextBox.Text);

                fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
                fcb_public.publicDataSetTableAdapters.elementTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();

                publicDataSetTableAdapters.Fill(publicDataSet.element);


                contentTextBox.Text = publicDataSet.element.FindByID(id_index).content;

                TextRange documentTextRange = new TextRange(richTextBox1.Document.ContentStart, richTextBox1.Document.ContentEnd);
                MemoryStream stream = new MemoryStream();
                StreamWriter sw = new StreamWriter(stream);
                sw.Write(contentTextBox.Text);
                sw.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                if (contentTextBox.Text != "")
                {
                    documentTextRange.Load(stream, DataFormats.Xaml);
                }
                else
                {
                    richTextBox1.Document.Blocks.Clear();
                }
                titleTextBox.Text = publicDataSet.element.FindByID(id_index).title;
                typeComboBox.Text = publicDataSet.element.FindByID(id_index).type;
                show_timeTextBox.Text = publicDataSet.element.FindByID(id_index).show_time.ToString();
                statusCheckBox.IsChecked = publicDataSet.element.FindByID(id_index).status;
                start_timeDatePicker.SelectedDate = publicDataSet.element.FindByID(id_index).start_time;
                end_timeDatePicker.SelectedDate = publicDataSet.element.FindByID(id_index).end_time;



           
        }
        private void addtoolfontname()
        {
            ICollection<FontFamily> familyCollection = Fonts.SystemFontFamilies;
            if (familyCollection != null)
            {
                foreach (FontFamily family in familyCollection)
                {
                    toolfontname.Items.Add(family);
                }
                toolfontname.SelectedIndex = 0;
            }
            for (int i = 10; i <= 36; i++)
            {
                toolfontsize.Items.Add(i.ToString());
            }
            toolfontsize.SelectedIndex = 0;
        }

        private void toolfontname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void toolfontname_DropDownClosed(object sender, EventArgs e)
        {
            if (toolfontname.SelectedValue != null)
            {
                richTextBox1.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, toolfontname.SelectedValue);
            }
        }

        private void toolfontsize_DropDownClosed(object sender, EventArgs e)
        {
            if (toolfontsize.SelectedValue != null)
            {
                richTextBox1.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, toolfontsize.SelectedValue);
            }
        }

        private void richTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            TextRange range = new TextRange(richTextBox1.Document.ContentStart, richTextBox1.Document.ContentEnd);
            range.Save(stream, DataFormats.Xaml);
            stream.Position = 0;
            StreamReader r = new StreamReader(stream);
            contentTextBox.Text = r.ReadToEnd();
        }

        private void toolfontClearStyle_Click(object sender, RoutedEventArgs e)
        {
            richTextBox1.Selection.ClearAllProperties();
        }


        private void show_timeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }

   
    }
}
