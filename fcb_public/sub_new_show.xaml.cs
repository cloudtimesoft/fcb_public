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

        //System.Timers.Timer newtimer = new System.Timers.Timer();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


            //newtimer.Elapsed += new System.Timers.ElapsedEventHandler(newtimer_Elapsed);
            //newtimer.Interval = 1000;
            //newtimer.Start();
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//在此处加载数据并将结果指派给 CollectionViewSource。
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }


            //MediaElement newmedia = new MediaElement();
          
            //newmedia.Source = new Uri(Directory.GetCurrentDirectory() + "\\image\\" + "weicheng.mp4", UriKind.Absolute);
        
            //abc.Children.Add(newmedia);



            //Thread newthread = new Thread(new ThreadStart(() =>
            //{
            //    Dispatcher.Invoke(new Action(() =>
            //    {



                    fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
                    fcb_public.publicDataSetTableAdapters.elementTableAdapter elpublicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();
                    fcb_public.publicDataSetTableAdapters.element_setTableAdapter elsetpublicDataSetTableAdapters = new fcb_public.publicDataSetTableAdapters.element_setTableAdapter();
                    fcb_public.publicDataSetTableAdapters.InitializeTableAdapter initpublicDataSetTableAdapters = new fcb_public.publicDataSetTableAdapters.InitializeTableAdapter();
                    fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter publicDataSetel_elsetTableAdapter = new fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter();
                    publicDataSetel_elsetTableAdapter.Fill(publicDataSet.el_elset);

                    elpublicDataSetTableAdapters.Fill(publicDataSet.element);
                    elsetpublicDataSetTableAdapters.Fill(publicDataSet.element_set);
                    initpublicDataSetTableAdapters.Fill(publicDataSet.Initialize);


                    if (elset_id > -1 && init_publicdataset == false)
                    {
                        var show_el = from el in publicDataSet.element join el_set in publicDataSet.el_elset on el.ID equals el_set.element_ID where elset_id == el_set.element_set_ID select el;
                        element_count = show_el.Count();
                        init_publicdataset = true;
                        //newtimer.Interval = 1000;
                    }

                    if (element_count > 0)
                    {
                        var show_el = from el in publicDataSet.element join el_set in publicDataSet.el_elset on el.ID equals el_set.element_ID where el.status == true where el_set.element_set_ID == elset_id select el;
                       // int temp_step = 0;
                        foreach (var t in show_el)
                        {

                            //if (step == temp_step)
                            //{
                                var show_el_set = from el_set in publicDataSet.element_set join el_elset in publicDataSet.el_elset on el_set.ID equals el_elset.element_ID where el_elset.element_ID == t.ID select el_elset;
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


                                    RichTextBox newRTB = new RichTextBox();
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

                                    //var showel = from el in publicDataSet.element join el_set in publicDataSet.el_elset on el.ID equals el_set.element_ID where el.status == true where el_set.element_set_ID == elset_id select el;
                                    //foreach (var s in showel)
                                    //{
                                        FlowDocumentScrollViewer deldoc = content_stackpanel.FindName("newdoc" + t.ID) as FlowDocumentScrollViewer;
                                        if (deldoc != null)
                                        {
                                            content_stackpanel.Children.Remove(deldoc);
                                            content_stackpanel.UnregisterName("newdoc" + t.ID);
                                        }
                                        System.Windows.Media.Effects.DropShadowEffect da = new System.Windows.Media.Effects.DropShadowEffect();
                                        da.Opacity = 0.65;
                                        da.ShadowDepth = 3;
                                        da.Color = Colors.Black;
                                        FlowDocumentScrollViewer newdoc = new FlowDocumentScrollViewer();
                                        newdoc.Document = doc;
                                       // newdoc.Effect = da;
                                        newdoc.Padding = new Thickness(10);
                                        newdoc.IsEnabled = false;
                                        newdoc.Foreground = Brushes.White;
                                       // newdoc.Visibility = Visibility.Hidden;
                                        newdoc.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                                        newdoc.Margin = new Thickness(0, 0, 0, 0);
                                        content_stackpanel.Children.Add(newdoc);
                                        content_stackpanel.RegisterName("newdoc" + t.ID, newdoc);
                                       // Storyboard newstoryboard = new Storyboard();
                                       // ThicknessAnimation txt_animation = new ThicknessAnimation();
                                       // txt_animation.From = new Thickness(0, 0, 0, 0);
                                       // txt_animation.To = new Thickness(0, -newdoc.ActualHeight, 0, 0);
                                       //// newdoc.Visibility = Visibility.Visible;
                                       // txt_animation.Duration = TimeSpan.FromSeconds(t.show_time);
                                       // Storyboard.SetTargetName(txt_animation, newdoc.Name);
                                       // Storyboard.SetTargetProperty(txt_animation, new PropertyPath(Grid.OpacityProperty));
                                       // newstoryboard.Children.Add(txt_animation);
                                    
                                        //}





                                    //show_video.Width = 0;
                                    //show_video.Height = 0;
                                    //show_image.Width = 0;
                                    //show_image.Height = 0;
                                    //text_content.Document = doc;
                                    //newtimer.Interval = t.show_time * 1000;
                                   // newtimer.Stop();
                                    //newtimer.Start();

                                    // PublicClass.show_hight = text_content.Height;
                                    //text_content.UpdateLayout();
                                    //content_stackpanel.UpdateLayout();

                                    //ThicknessAnimation wait_animation = new ThicknessAnimation();
                                    //wait_animation.From = new Thickness(0, 0, 0, 0);
                                    //wait_animation.To = new Thickness(0, 0, 0, 0);
                                    //wait_animation.Duration = TimeSpan.FromSeconds(t.show_time/2);

                                    //text_content.BeginAnimation(TextBlock.MarginProperty, wait_animation);

                                   // newtimer.Start();
                                    //ThicknessAnimation txt_margin_animation = new ThicknessAnimation();
                                    //txt_margin_animation.From = new Thickness(0, 0, 0, 0);
                                    //txt_margin_animation.To = new Thickness(0, -(text_content.ActualHeight), 0, 0);
                                    //txt_margin_animation.Duration = TimeSpan.FromSeconds(t.show_time);

                                    //text_content.BeginAnimation(FlowDocumentScrollViewer.MarginProperty, txt_margin_animation);
                                    // txt_margin_animation.RepeatBehavior = RepeatBehavior.Forever;

                                   // newtimer.Interval = t.show_time * 1000;



                                }
                                else if (t.type == "图片")
                                {
                                    //text_content.Width = 0;
                                    //text_content.Height = 0;
                                    //show_video.Width = 0;
                                    //show_video.Height = 0;
                                    //show_image.Source = new BitmapImage(new Uri(t.content, UriKind.Absolute));
                                }
                                else if (t.type == "视频")
                                {
                                    //text_content.Height = 0;
                                    //text_content.Width = 0;
                                    //show_image.Width = 0;
                                    //show_image.Height = 0;
                                    //show_video.Source = new Uri(t.content, UriKind.Absolute);
                                   // newtimer.Interval = t.show_time * 1000;
                                }

                            //}
                            //temp_step++;
                        }

                        //var showel = from el in publicDataSet.element join el_set in publicDataSet.el_elset on el.ID equals el_set.element_ID where el.status == true where el_set.element_set_ID == elset_id select el;
                        foreach(var s in show_el)
                        {
                        Storyboard newstoryboard = new Storyboard();
                        ThicknessAnimation txt_animation = new ThicknessAnimation();
                        FlowDocumentScrollViewer new_fld = content_stackpanel.FindName("newdoc" + s.ID) as FlowDocumentScrollViewer;
                        new_fld.Name = "newdoc" + s.ID;
                        txt_animation.From = new Thickness(0, 0, 0, 0);
                        txt_animation.To = new Thickness(0, -new_fld.ActualHeight, 0, 0);
                        // newdoc.Visibility = Visibility.Visible;
                        txt_animation.Duration = TimeSpan.FromSeconds(s.show_time);
                        Storyboard.SetTargetName(txt_animation, new_fld.Name);
                        Storyboard.SetTargetProperty(txt_animation, new PropertyPath(StackPanel.MarginProperty));
                        newstoryboard.Children.Add(txt_animation);
                        newstoryboard.Begin(content_stackpanel);

                        }

                        //step++;
                        //if (step > element_count)
                        //{
                        //    step = 0;
                        //}
                    }




            //    }));



            //}));
            //newthread.SetApartmentState(ApartmentState.MTA);
            //newthread.IsBackground = true;
            ////newthread.Priority = ThreadPriority.AboveNormal;
            //newthread.Start();







        }

        void newtimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
        //        Thread newthread = new Thread(new ThreadStart(() =>
        //{
        //    Dispatcher.Invoke(new Action(() =>
        //        {



        //    fcb_public.publicDataSet publicDataSet = ((fcb_public.publicDataSet)(this.FindResource("publicDataSet")));
        //    fcb_public.publicDataSetTableAdapters.elementTableAdapter elpublicDataSetTableAdapters = new publicDataSetTableAdapters.elementTableAdapter();
        //    fcb_public.publicDataSetTableAdapters.element_setTableAdapter elsetpublicDataSetTableAdapters = new fcb_public.publicDataSetTableAdapters.element_setTableAdapter();
        //    fcb_public.publicDataSetTableAdapters.InitializeTableAdapter initpublicDataSetTableAdapters = new fcb_public.publicDataSetTableAdapters.InitializeTableAdapter();
        //    fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter publicDataSetel_elsetTableAdapter = new fcb_public.publicDataSetTableAdapters.el_elsetTableAdapter();
        //    publicDataSetel_elsetTableAdapter.Fill(publicDataSet.el_elset);

        //    elpublicDataSetTableAdapters.Fill(publicDataSet.element);
        //    elsetpublicDataSetTableAdapters.Fill(publicDataSet.element_set);
        //    initpublicDataSetTableAdapters.Fill(publicDataSet.Initialize);
           
            
        //    if (elset_id > -1 && init_publicdataset==false)
        //    {
        //        var show_el = from el in publicDataSet.element join el_set in publicDataSet.el_elset on el.ID equals el_set.element_ID where elset_id == el_set.element_set_ID  select el;
        //        element_count = show_el.Count();
        //        init_publicdataset = true;
        //        //newtimer.Interval = 1000;
        //    }
    
        //    if (element_count > 0)
        //    {
        //        var show_el = from el in publicDataSet.element join el_set in publicDataSet.el_elset on el.ID equals el_set.element_ID where el.status == true where el_set.element_set_ID==elset_id select el;
        //        int temp_step = 0;
        //        foreach (var t in show_el)
        //        {
                    
        //            if (step == temp_step)
        //            {
        //                var show_el_set = from el_set in publicDataSet.element_set join el_elset in publicDataSet.el_elset on el_set.ID equals el_elset.element_ID where el_elset.element_ID==t.ID select el_elset;
        //                foreach (var st in show_el_set)
        //                {
        //                    var sst = from c in publicDataSet.element_set where c.ID == st.element_set_ID select c;
        //                    foreach (var ssst in sst)
        //                    {
        //                        big_title.Text = ssst.elset_name;
        //                    }
        //                }
                    
        //            small_title.Text = t.title;
                   


        //           //10000;
        //            if (t.type == "文档")
        //            {

                      
        //                RichTextBox newRTB =new RichTextBox();
        //                TextRange documentTextRange = new TextRange(newRTB.Document.ContentStart, newRTB.Document.ContentEnd);
        //                MemoryStream stream = new MemoryStream();
        //                StreamWriter sw = new StreamWriter(stream);
        //                sw.Write(t.content);
        //                sw.Flush();
        //                stream.Seek(0, SeekOrigin.Begin);
        //                documentTextRange.Load(stream, DataFormats.Xaml);
        //                string xw = XamlWriter.Save(newRTB.Document);
                       
        //                StringReader sr = new StringReader(xw);
        //                System.Xml.XmlReader xr = System.Xml.XmlReader.Create(sr);
        //                FlowDocument doc = XamlReader.Load(xr) as FlowDocument;
                                              
                    



        //                show_video.Width = 0;
        //                show_video.Height = 0;
        //                show_image.Width = 0;
        //                show_image.Height = 0;
        //                text_content.Document =doc;
        //                //newtimer.Interval = t.show_time * 1000;
        //                newtimer.Stop();
        //                //newtimer.Start();

        //               // PublicClass.show_hight = text_content.Height;
        //                text_content.UpdateLayout();
        //                content_stackpanel.UpdateLayout();

        //                //ThicknessAnimation wait_animation = new ThicknessAnimation();
        //                //wait_animation.From = new Thickness(0, 0, 0, 0);
        //                //wait_animation.To = new Thickness(0, 0, 0, 0);
        //                //wait_animation.Duration = TimeSpan.FromSeconds(t.show_time/2);

        //                //text_content.BeginAnimation(TextBlock.MarginProperty, wait_animation);

        //                newtimer.Start();
        //                ThicknessAnimation txt_margin_animation = new ThicknessAnimation();
        //                txt_margin_animation.From = new Thickness(0, 0, 0, 0);
        //                txt_margin_animation.To = new Thickness(0, -(text_content.ActualHeight), 0, 0);
        //                txt_margin_animation.Duration = TimeSpan.FromSeconds(t.show_time);

        //                text_content.BeginAnimation(FlowDocumentScrollViewer.MarginProperty, txt_margin_animation);
        //               // txt_margin_animation.RepeatBehavior = RepeatBehavior.Forever;
                      
        //                newtimer.Interval = t.show_time * 1000;
                      
                       
                        
        //            }
        //           else if (t.type == "图片")
        //            {
        //                text_content.Width = 0;
        //                text_content.Height = 0;
        //                show_video.Width = 0;
        //                show_video.Height = 0;
        //                show_image.Source = new BitmapImage(new Uri(t.content, UriKind.Absolute));
        //            }
        //           else if (t.type== "视频")
        //            {
        //                text_content.Height = 0;
        //                text_content.Width = 0;
        //                show_image.Width = 0;
        //                show_image.Height = 0;
        //                show_video.Source = new Uri( t.content, UriKind.Absolute);
        //                newtimer.Interval = t.show_time * 1000;
        //            }

        //            }
        //            temp_step++;
        //        }
        //        step++;
        //        if (step > element_count)
        //        {
        //            step = 0;
        //        }
        //    }

            


        //        }));



        //}));
        //        newthread.SetApartmentState(ApartmentState.MTA);
        //        newthread.IsBackground = true;
        //        //newthread.Priority = ThreadPriority.AboveNormal;
        //        newthread.Start();




        }

 


 
 


   




        
    }
}
