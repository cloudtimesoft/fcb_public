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
using System.Threading;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Markup;


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
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//在此处加载数据并将结果指派给 CollectionViewSource。
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }


            //MediaElement newmedia = new MediaElement();
          
            //newmedia.Source = new Uri(Directory.GetCurrentDirectory() + "\\image\\" + "weicheng.mp4", UriKind.Absolute);
        
            //abc.Children.Add(newmedia);



        }

        void newtimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
                Thread newthread = new Thread(new ThreadStart(() =>
        {
            Dispatcher.Invoke(new Action(() =>
                {



            fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
            fcb_public.publicDataSetTableAdapters.elementTableAdapter elpublicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();
            fcb_public.publicDataSetTableAdapters.element_setTableAdapter elsetpublicDataSetTableAdapters = new fcb_public.publicDataSetTableAdapters.element_setTableAdapter();
            fcb_public.publicDataSetTableAdapters.InitializeTableAdapter initpublicDataSetTableAdapters = new fcb_public.publicDataSetTableAdapters.InitializeTableAdapter();
            fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter publicDataSetel_elsetTableAdapter = new fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter();
            publicDataSetel_elsetTableAdapter.Fill(publicDataSet.el_elset);

            elpublicDataSetTableAdapters.Fill(publicDataSet.element);
            elsetpublicDataSetTableAdapters.Fill(publicDataSet.element_set);
            initpublicDataSetTableAdapters.Fill(publicDataSet.Initialize);
           
            
            if (elset_id > -1 && init_publicdataset==false)
            {
                var show_el = from el in publicDataSet.element join el_set in publicDataSet.el_elset on el.ID equals el_set.element_ID where elset_id == el_set.element_set_ID  select el;
                element_count = show_el.Count();
                init_publicdataset = true;
               // newtimer.Interval = 10000;
            }
    
            if (element_count > 0)
            {
                var show_el = from el in publicDataSet.element join el_set in publicDataSet.el_elset on el.ID equals el_set.element_ID where el.status == true where el_set.element_set_ID==elset_id select el;
                int temp_step = 0;
                foreach (var t in show_el)
                {
                    
                    if (step == temp_step)
                    {
                        var show_el_set = from el_set in publicDataSet.element_set join el_elset in publicDataSet.el_elset on el_set.ID equals el_elset.element_ID where el_elset.element_ID==t.ID select el_elset;
                        foreach (var st in show_el_set)
                        {
                            var sst = from c in publicDataSet.element_set where c.ID == st.element_set_ID select c;
                            foreach (var ssst in sst)
                            {
                                big_title.Text = ssst.elset_name;
                            }
                        }
                    
                    small_title.Text = t.title;
                   
                   //10000;
                    if (t.type == "文档")
                    {

                        //FlowDocument newfld = new FlowDocument();
                        ////FlowDocumentReader a = new FlowDocumentReader();
                        //TextRange a = new TextRange(newfld.ContentStart, newfld.ContentEnd);

                        //MemoryStream stream = new MemoryStream();
                        //StreamWriter sw = new StreamWriter(stream);
                        //sw.Write(t.content);
                        ////StreamReader r = new StreamReader(stream);
                       
                        ////TextBox newtextbox = new TextBox();
                        ////newtextbox.Text = r.ReadToEnd();
                        //sw.Flush();
                        //stream.Seek(0, SeekOrigin.Begin);
                        //a.Load(stream, DataFormats.Xaml);
                        //Paragraph para = new Paragraph();
                        //para.Inlines.Add(a);
                        //newfld.Blocks.Add(para);
                        //var a = new TextRange(newfld.ContentStart, newfld.ContentEnd);
                        //string content = t.content;
                        //StringReader sr = new StringReader(content);
                        ////TextBox newtextbox = new TextBox();
                        ////newtextbox.Text = t.content;
                        RichTextBox newRTB =new RichTextBox();
                        TextRange documentTextRange = new TextRange(newRTB.Document.ContentStart, newRTB.Document.ContentEnd);
                        MemoryStream stream = new MemoryStream();
                        StreamWriter sw = new StreamWriter(stream);
                        sw.Write(t.content);
                        sw.Flush();
                        stream.Seek(0, SeekOrigin.Begin);
                        documentTextRange.Load(stream, DataFormats.Xaml);
                        string xw = XamlWriter.Save(newRTB.Document);
                       
                        StringReader sr = new StringReader(xw);
                        System.Xml.XmlReader xr = System.Xml.XmlReader.Create(sr);
                        FlowDocument doc = XamlReader.Load(xr) as FlowDocument;

                       
                          
                    
                        show_video.Width = 0;
                        show_video.Height = 0;
                        show_image.Width = 0;
                        show_image.Height = 0;
                        text_content.Document = doc;
                        newtimer.Interval = t.show_time * 1000;
                    }
                   else if (t.type == "图片")
                    {
                        text_content.Width = 0;
                        text_content.Height = 0;
                        show_video.Width = 0;
                        show_video.Height = 0;
                        show_image.Source = new BitmapImage(new Uri(t.content, UriKind.Absolute));
                    }
                   else if (t.type== "视频")
                    {
                        text_content.Height = 0;
                        text_content.Width = 0;
                        show_image.Width = 0;
                        show_image.Height = 0;
                        show_video.Source = new Uri( t.content, UriKind.Absolute);
                        newtimer.Interval = t.show_time * 1000;
                    }

                    }
                    temp_step++;
                }
                step++;
                if (step > element_count)
                {
                    step = 0;
                }
            }

            


                }));



        }));
                newthread.SetApartmentState(ApartmentState.MTA);
                newthread.IsBackground = true;
                //newthread.Priority = ThreadPriority.AboveNormal;
                newthread.Start();




        }

        private void text_content_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ThicknessAnimation txt_margin_animation = new ThicknessAnimation();
            txt_margin_animation.From = new Thickness(0, 0, 0, 0);
            txt_margin_animation.To = new Thickness(0, SystemParameters.PrimaryScreenHeight - text_content.ActualHeight - 110, 0, 0);
            txt_margin_animation.Duration = TimeSpan.FromSeconds(500);
            text_content.BeginAnimation(TextBlock.MarginProperty, txt_margin_animation);


            text_content.FontSize=36;
        
        }


 
 


   




        
    }
}
