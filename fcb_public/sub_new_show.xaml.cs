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
    /// sub_new_show.xaml 的交互逻辑
    /// </summary>
    public partial class sub_new_show : UserControl
    {
        public sub_new_show()
        {
            InitializeComponent();
        }
        public int elset_id = -1;
        public int element_count = 0;
        public int step = 0;
        public bool init_publicdataset = false;
        //public var show_el;

        System.Timers.Timer newtimer = new System.Timers.Timer();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            newtimer.Elapsed += new System.Timers.ElapsedEventHandler(newtimer_Elapsed);
            newtimer.Interval = 1000;
            newtimer.Start();
            // 不要在设计时加载数据。
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//在此处加载数据并将结果指派给 CollectionViewSource。
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        void newtimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            if (elset_id > -1 && init_publicdataset==false)
            {
                var show_el = from el in publicDataSet.element join el_set in publicDataSet.el_elset on el.ID equals el_set.element_ID select el;
                element_count = show_el.Count();
                init_publicdataset = true;
                newtimer.Interval = 30000;
            }

            if (element_count > 0)
            {
                var show_el = from el in publicDataSet.element join el_set in publicDataSet.el_elset on el.ID equals el_set.element_ID where el.status == true select el;

                foreach (var t in show_el)
                {
                    int temp_step=0;
                    if (step == temp_step)
                    {
                        var show_el_set = from el_set in publicDataSet.element_set join el_elset in publicDataSet.el_elset on el_set.ID equals el_elset.element_ID select el_elset;
                        foreach (var st in show_el_set)
                        {
                            var sst = from c in publicDataSet.element_set where c.ID == st.element_set_ID select c;
                            foreach (var ssst in sst)
                            {
                                big_title.Text = ssst.elset_name;
                            }
                        }
                    }
                    small_title.Text = t.title;
                    text_content.Text = t.content;
                    temp_step++;
                }
                step++;
                if (step > element_count)
                {
                    step = 0;
                }
            }
            


        }
        
    }
}
