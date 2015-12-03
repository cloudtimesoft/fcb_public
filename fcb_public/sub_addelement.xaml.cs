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
using System.IO;

namespace fcb_public
{
    /// <summary>
    /// sub_addelement.xaml 的交互逻辑
    /// </summary>
    public partial class sub_addelement : UserControl
    {
        public sub_addelement()
        {
            InitializeComponent();
        }
        public static readonly RoutedEvent AddElementEvent = EventManager.RegisterRoutedEvent("AddElement", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(sub_addelement));
        public event RoutedPropertyChangedEventHandler<object> AddElement
        {
            add { AddHandler(AddElementEvent, value); }
            remove { RemoveHandler(AddElementEvent, value); }
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

            //fcb_public.publicDataSet publicDataSet = (fcb_public.publicDataSet)(this.FindResource("publicDataSet"));
            //fcb_public.publicDataSetTableAdapters.elementTableAdapter publicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();
            //publicDataSetTableAdapters.Fill(publicDataSet.element);
            //System.Windows.Data.CollectionViewSource elementViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("elementViewSource"));

            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            // 将数据加载到表 elset_init 中。可以根据需要修改此代码。
            fcb_public.publicDataSetTableAdapters.elset_initTableAdapter publicDataSetelset_initTableAdapter = new fcb_public.publicDataSetTableAdapters.elset_initTableAdapter();
            publicDataSetelset_initTableAdapter.Fill(publicDataSet.elset_init);
            System.Windows.Data.CollectionViewSource elset_initViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("elset_initViewSource")));
            elset_initViewSource.View.MoveCurrentToFirst();
            // 将数据加载到表 Initialize 中。可以根据需要修改此代码。
            fcb_public.publicDataSetTableAdapters.InitializeTableAdapter publicDataSetInitializeTableAdapter = new fcb_public.publicDataSetTableAdapters.InitializeTableAdapter();



            var query = from c in publicDataSet.Initialize join t in publicDataSet.elset_init on c.ID equals t.Initialize_ID select c;

            foreach(var s in publicDataSet.Initialize)
            {
                CheckBox newcheckbox = new CheckBox();

            }
            //publicDataSetInitializeTableAdapter.Fill(query);

            System.Windows.Data.CollectionViewSource initializeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("initializeViewSource")));

            initializeViewSource.View.MoveCurrentToFirst();
         
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            vido.Source = new Uri(Directory.GetCurrentDirectory() + "\\image\\" + "weicheng.mp4", UriKind.Absolute);
           
        }
    }
}
