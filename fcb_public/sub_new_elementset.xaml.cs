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

        public static readonly RoutedEvent ModeShowEvent = EventManager.RegisterRoutedEvent("ModeShow", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(sub_new_elementset));
        public event RoutedPropertyChangedEventHandler<object> BackGround
        {
            add { AddHandler(ModeShowEvent, value); }
            remove { RemoveHandler(ModeShowEvent, value); }
        }


        int index=1;
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
                index++;
            }
        }

        private void add_el_elset(int cur_id)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            
            List<int> el_set_ids = (from c in publicDataSet.el_elset where c.element_set_ID == cur_id select c.element_ID).ToList();
            List<int> element_ids = (from c in publicDataSet.element where !el_set_ids.Contains(c.ID) select c.ID).ToList();

            var element_list = from c in publicDataSet.element where element_ids.Contains(c.ID) select c;

                slist.Items.Clear();
                //slist.ItemsSource = null;

            foreach (var t in element_list)
            {

                TextBlock idtxt = new TextBlock();
                idtxt.Text = t.ID.ToString();
                idtxt.Name = "idtxt";
                idtxt.Width = 1;
                idtxt.Opacity = 0;
                TextBlock nametxt = new TextBlock();
                nametxt.Text = t.title;
                StackPanel newstack = new StackPanel();

                newstack.Orientation = Orientation.Horizontal;
                //newstack.Name = "asd";
                newstack.Children.Add(idtxt);
                
                newstack.Children.Add(nametxt);
                //newstack.RegisterName("idtxt", idtxt);
                slist.Items.Add(newstack);
            }

            var Delement_list = from c in publicDataSet.element where !element_ids.Contains(c.ID) select c;
            dlist.Items.Clear();
            foreach (var t in Delement_list)
            {
                TextBlock idtxt = new TextBlock();
                idtxt.Text = t.ID.ToString();
                idtxt.Name = "idtxt";
                idtxt.Width = 1;
                idtxt.Opacity = 0;
                TextBlock nametxt = new TextBlock();
                nametxt.Text = t.title;
                StackPanel newstack = new StackPanel();
                newstack.Orientation = Orientation.Horizontal;
                newstack.Children.Add(idtxt);
                //this.RegisterName("idtxt1", idtxt);
                newstack.Children.Add(nametxt);
                //this.RegisterName("nametxt1", nametxt);
                dlist.Items.Add(newstack);
            }

        }

        private void move_to_d_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter publicDataSetel_elsetTableAdapter = new fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter();
            StackPanel newstack = (StackPanel)slist.SelectedItem;
            if (newstack != null)
            {
                foreach (var t in newstack.Children)
                {
                    TextBlock newtxt = t as TextBlock;
                    if (newtxt.Name == "idtxt")
                    {
                        int element_id = int.Parse(newtxt.Text);
                        publicDataSet.el_elset.Addel_elsetRow(element_id, int.Parse(iDTextBox.Text));
                        publicDataSetel_elsetTableAdapter.Update(publicDataSet.el_elset);
                        publicDataSet.el_elset.AcceptChanges();
                        publicDataSetel_elsetTableAdapter.Fill(publicDataSet.el_elset);
                        add_el_elset(int.Parse(iDTextBox.Text));
                        break;
                    }




                }
                //TextBlock newblock = newstack.FindName("idtxt") as TextBlock;

                
                
            }
        }

        private void element_setDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (iDTextBox.Text != "")
            {
                add_el_elset(int.Parse(iDTextBox.Text));
            }
        }

        private void move_to_s_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter publicDataSetel_elsetTableAdapter = new fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter();
            StackPanel newstack = (StackPanel)dlist.SelectedItem;
            if (newstack != null)
            {
                foreach (var t in newstack.Children)
                {
                    TextBlock newtxt = t as TextBlock;
                    if (newtxt.Name == "idtxt")
                    {
                        int element_id = int.Parse(newtxt.Text);
                        //publicDataSet.el_elset.Addel_elsetRow(element_id, int.Parse(iDTextBox.Text));
                        //publicDataSet.el_elsetRow del_row = (publicDataSet.el_elsetRow)from c in publicDataSet.el_elset where c.element_ID == int.Parse(newtxt.Text) && c.element_set_ID == int.Parse(iDTextBox.Text) select c;
                        List<int> el_elset_id = (from c in publicDataSet.el_elset where c.element_ID == int.Parse(newtxt.Text) && c.element_set_ID == int.Parse(iDTextBox.Text) select c.ID).ToList();
                        //publicDataSet.el_elset.Rows[el_elset_id[0]].Delete();
                        publicDataSet.el_elset.FindByID(el_elset_id[0]).Delete();
                        publicDataSetel_elsetTableAdapter.Update(publicDataSet.el_elset);
                        publicDataSet.el_elset.AcceptChanges();
                        publicDataSetel_elsetTableAdapter.Fill(publicDataSet.el_elset);
                        add_el_elset(int.Parse(iDTextBox.Text));
                        break;
                    }




                }
                //TextBlock newblock = newstack.FindName("idtxt") as TextBlock;



            }
        }

        private void show_Click(object sender, RoutedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.InitializeTableAdapter publicDataSetInitializeTableAdapter = new fcb_public.publicDataSetTableAdapters.InitializeTableAdapter();

            if (show.Content.ToString() == "展示")
            {
                publicDataSet.Initialize.AddInitializeRow(iDTextBox.Text, false, (int)(SystemParameters.PrimaryScreenWidth - 300) / 2, (int)(SystemParameters.PrimaryScreenHeight - 400) / 2, 300, 400);
                publicDataSetInitializeTableAdapter.Update(publicDataSet.Initialize);
                publicDataSet.AcceptChanges();
                show.Content = "取消展示";
            }
            else
            {
                var del_row = from c in publicDataSet.Initialize where c.in_name == iDTextBox.Text select c;
                foreach (var t in del_row)
                {
                    t.Delete();
                }
                publicDataSetInitializeTableAdapter.Update(publicDataSet.Initialize);
                publicDataSet.AcceptChanges();
                show.Content = "展示";
            }


            RoutedPropertyChangedEventArgs<object> args = new RoutedPropertyChangedEventArgs<object>(this, e);
            args.RoutedEvent = sub_new_elementset.ModeShowEvent;
            this.RaiseEvent(args);
        }
    }
}
