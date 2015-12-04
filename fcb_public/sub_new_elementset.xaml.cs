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
    /// sub_new_elementset.xaml 的交互逻辑
    /// </summary>
    public partial class sub_new_elementset : UserControl
    {
        public sub_new_elementset()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            // 将数据加载到表 element_set 中。可以根据需要修改此代码。
            fcb_public.publicDataSetTableAdapters.element_setTableAdapter publicDataSetelement_setTableAdapter = new fcb_public.publicDataSetTableAdapters.element_setTableAdapter();
            publicDataSetelement_setTableAdapter.Fill(publicDataSet.element_set);
            System.Windows.Data.CollectionViewSource element_setViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("element_setViewSource")));
            element_setViewSource.View.MoveCurrentToFirst();
            // 将数据加载到表 element 中。可以根据需要修改此代码。
            fcb_public.publicDataSetTableAdapters.elementTableAdapter publicDataSetelementTableAdapter = new fcb_public.publicDataSetTableAdapters.elementTableAdapter();
            publicDataSetelementTableAdapter.Fill(publicDataSet.element);
            System.Windows.Data.CollectionViewSource elementViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("elementViewSource")));
            elementViewSource.View.MoveCurrentToFirst();
                        // 将数据加载到表 el_elset 中。可以根据需要修改此代码。
            fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter publicDataSetel_elsetTableAdapter = new fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter();
            publicDataSetel_elsetTableAdapter.Fill(publicDataSet.el_elset);
            System.Windows.Data.CollectionViewSource el_elsetViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("el_elsetViewSource")));
            el_elsetViewSource.View.MoveCurrentToFirst();
            element_setDataGrid.CanUserAddRows = false;
        }

        private void add_elset_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.element_setTableAdapter publicDataSetelement_setTableAdapter = new fcb_public.publicDataSetTableAdapters.element_setTableAdapter();
            System.Windows.Data.CollectionViewSource element_setViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("element_setViewSource")));

            if (add_elset.Content.ToString() == "新增")
            {
                element_setViewSource.View.MoveCurrentToLast();
                element_setViewSource.View.MoveCurrentToNext();
                add_elset.Content = "保存";
                elset_nameTextBox.Text = "";
                statusCheckBox.IsChecked = true;
 
            }
            else
            {
                add_elset.Content = "新增";
                publicDataSet.element_set.Addelement_setRow(elset_nameTextBox.Text, DateTime.Parse(start_timeDatePicker.Text), DateTime.Parse(end_timeDatePicker.Text), (bool)statusCheckBox.IsChecked);
                publicDataSetelement_setTableAdapter.Update(publicDataSet.element_set);
                publicDataSet.AcceptChanges();
                publicDataSetelement_setTableAdapter.Fill(publicDataSet.element_set);
                element_setViewSource.View.MoveCurrentToLast();
                add_el_elset(int.Parse(iDTextBox.Text));
            }
        }

        private void add_el_elset(int cur_id)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            
            List<int> el_set_ids = (from c in publicDataSet.el_elset where c.element_set_ID == cur_id select c.element_ID).ToList();
            List<int> element_ids = (from c in publicDataSet.element where !el_set_ids.Contains(c.ID) select c.ID).ToList();

            var element_list = from c in publicDataSet.element where element_ids.Contains(c.ID) select c;
            foreach (var t in element_list)
            {
                TextBlock idtxt = new TextBlock();
                idtxt.Text = t.ID.ToString();
                idtxt.Width = 1;
                idtxt.Opacity = 0;
                TextBlock nametxt = new TextBlock();
                nametxt.Text = t.title;
                StackPanel newstack = new StackPanel();
                newstack.Orientation = Orientation.Horizontal;
                newstack.Children.Add(idtxt);
                idtxt.RegisterName("idtxt1", idtxt);
                newstack.Children.Add(nametxt);
                slist.Items.Add(newstack);
            }

            var Delement_list = from c in publicDataSet.element where !element_ids.Contains(c.ID) select c;
            foreach (var t in Delement_list)
            {
                TextBlock idtxt = new TextBlock();
                idtxt.Text = t.ID.ToString();
                idtxt.Width = 1;
                idtxt.Opacity = 0;
                TextBlock nametxt = new TextBlock();
                nametxt.Text = t.title;
                StackPanel newstack = new StackPanel();
                newstack.Orientation = Orientation.Horizontal;
                newstack.Children.Add(idtxt);
                newstack.RegisterName("idtxt", idtxt);
                newstack.Children.Add(nametxt);
                newstack.RegisterName("nametxt", nametxt);
                dlist.Items.Add(newstack);
            }

        }

        private void move_to_d_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            StackPanel newstack = (StackPanel)slist.SelectedItem;
            if (newstack != null)
            {
                TextBlock newblock = newstack.FindName("idtxt") as TextBlock;
                int element_id = int.Parse(newblock.Text);
                publicDataSet.el_elset.Addel_elsetRow(element_id, int.Parse(iDTextBox.Text));
                add_el_elset(int.Parse(iDTextBox.Text));
            }
        }
    }
}
