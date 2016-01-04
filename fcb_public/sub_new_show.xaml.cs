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
using System.Collections;


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
        ArrayList alist = new ArrayList();
        int mvindex = 0;
        int anmilistep = 0;
        List<string> titlename = new List<string>();
        List<string> mvlist = new List<string>();
        //public var show_el;

        //System.Timers.Timer newtimer = new System.Timers.Timer();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


            init_show();


        }

        public void init_show()
        {
            //small_title.Width = content_stackpanel.ActualWidth;
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
                        
                        //var sst = from c in publicDataSet.element_set where c.ID == st.element_set_ID select c;
                        //var sst = from c in publicDataSet.element join el in publicDataSet.el_elset on c.ID equals el.element_ID where el.element_set_ID == elset_id select c;
                        var sst = from c in publicDataSet.element_set where c.ID == elset_id select c;
                        foreach (var ssst in sst)
                        {
                            big_title.Text = ssst.elset_name;
                           // break;
                        }
                    }

                    small_title.Text = t.title;
                    titlename.Add(t.ID + "|" + t.title);

                
                    //10000;
                    if (t.type == "文档")
                    {

                        small_title.Visibility = Visibility.Hidden;
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

                        FlowDocumentScrollViewer newdoc = new FlowDocumentScrollViewer();
                        newdoc.Document = doc;
                        // newdoc.Effect = da;
                        //newdoc.Padding = new Thickness(10);
                        newdoc.IsEnabled = false;
                        newdoc.Foreground = Brushes.White;
                        newdoc.Opacity = 0;
                        newdoc.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                        newdoc.Margin = new Thickness(0, 0, 0, 0);
                        newdoc.Width = content_stackpanel.ActualWidth;
                        newdoc.UpdateLayout();
                        content_stackpanel.Children.Add(newdoc);
                        content_stackpanel.RegisterName("newdoc" + t.ID, newdoc);
                        //newdoc.Name = "newdoc" + t.ID;
                        //Storyboard newstoryboard = new Storyboard();
                        //ThicknessAnimation txt_animation = new ThicknessAnimation();
                        //txt_animation.From = new Thickness(0, 0, 0, 0);
                        //txt_animation.To = new Thickness(0, -800, 0, 0);
                        //// newdoc.Visibility = Visibility.Visible;
                        //txt_animation.Duration = TimeSpan.FromSeconds(t.show_time);
                        //newstoryboard.Children.Add(txt_animation);
                        //Storyboard.SetTargetName(txt_animation, newdoc.Name);
                        //Storyboard.SetTargetProperty(txt_animation, new PropertyPath(MarginProperty));
                        //newstoryboard.Stop(content_stackpanel);
                        //newstoryboard.Begin(content_stackpanel);

                        //DoubleAnimation newadouble = new DoubleAnimation();
                        //newadouble.From = 1;
                        //newadouble.To = 0;
                        //newadouble.Duration = TimeSpan.FromSeconds(1);
                        //newstoryboard.Children.Add(newadouble);
                        //Storyboard.SetTargetName(newadouble, newdoc.Name);
                        //Storyboard.SetTargetProperty(newadouble, new PropertyPath(OpacityProperty));
                        //newstoryboard.Begin(content_stackpanel);

                        //DoubleAnimation newadouble = new DoubleAnimation();
                        //newadouble.From = 1;
                        //newadouble.To = 0;
                        //newadouble.Duration = TimeSpan.FromSeconds(1);
                        //newstoryboard.Children.Add(newadouble);
                        //Storyboard.SetTargetName(txt_animation, newdoc.Name);
                        //Storyboard.SetTargetProperty(newadouble, new PropertyPath(OpacityProperty));
                        //newstoryboard.Begin(content_stackpanel);







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

                        //small_title.Text = t.title;
                        Image delimg = content_stackpanel.FindName("newimage" + t.ID) as Image;
                        if (delimg != null)
                        {
                            content_stackpanel.Children.Remove(delimg);
                            content_stackpanel.UnregisterName("newimage" + t.ID);
                        }
                        content_stackpanel.UpdateLayout();
                        Image newimage = new Image();
                        newimage.Opacity = 0;
                       
                        newimage.Source = new BitmapImage(new Uri(t.content, UriKind.Absolute));
                        newimage.Width = content_stackpanel.ActualWidth;
                        newimage.Height = this.ActualHeight;
                        //newimage.Width = content_stackpanel.Width;
                        //newimage.Height = content_stackpanel.Height;
                        content_stackpanel.Children.Add(newimage);
                        content_stackpanel.RegisterName("newimage" + t.ID, newimage);
                       
                        //Label backlable = new Label();
                        //small_title.Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
                        small_title.Margin = new Thickness(0, content_back.ActualHeight - 150, 0, 0);
                        //backlable.Height = 200;
                        small_title.Width = content_stackpanel.ActualWidth;
                       
                        //Panel.SetZIndex(backlable, 2);
                        //content_stackpanel.Children.Add(backlable);
                        //TextBlock newtextblock = new TextBlock();
                        

                    }
                    else if (t.type == "视频")
                    {
                        mvlist.Add(t.content);
                        //text_content.Height = 0;
                        //text_content.Width = 0;
                        //show_image.Width = 0;
                        //show_image.Height = 0;
                        //show_video.Source = new Uri(t.content, UriKind.Absolute);
                        // newtimer.Interval = t.show_time * 1000;
                        small_title.Visibility = Visibility.Hidden;
                      
                           
                    }

                    //}
                    //temp_step++;
                }


                if (mvlist.Count > 0)
                {
                    //foreach (var v in show_el)
                    //{
                        //MediaElement delmedia = content_stackpanel.FindName("newmeida" + v.ID) as MediaElement;
                        //if (delmedia != null)
                        //{
                        //    content_stackpanel.Children.Remove(delmedia);
                        //    content_stackpanel.UnregisterName("newmeida" + v.ID);
                        //}
                 
                        MediaElement newmeida = new MediaElement();


                        content_stackpanel.Children.Add(newmeida);
                        //content_stackpanel.RegisterName("newmeida" + v.ID, newmeida);
                        newmeida.LoadedBehavior = MediaState.Manual;
                        newmeida.UnloadedBehavior = MediaState.Manual;
                     
                     
                     
                        newmeida.MediaEnded += new RoutedEventHandler(newmeida_MediaEnded);
                        newmeida.Source = new Uri(mvlist[0], UriKind.Absolute);
                        content_stackpanel.UpdateLayout();
                        newmeida.Width = content_stackpanel.ActualWidth;
                        //newmeida.Height = content_stackpanel.ActualHeight;
                       // newmeida.UpdateLayout();
                        newmeida.Play();
                        //newmeida.Loaded += new RoutedEventHandler(newmeida_Loaded);
                        //newmeida.Unloaded += new RoutedEventHandler(newmeida_Unloaded);
                        //newmeida.MediaOpened += new RoutedEventHandler(newmeida_MediaOpened);
                    //}
                }





                //var showel = from el in publicDataSet.element join el_set in publicDataSet.el_elset on el.ID equals el_set.element_ID where el.status == true where el_set.element_set_ID == elset_id select el;
                //Storyboard newstoryboard = new Storyboard();
                int startanimation = 0;
                foreach (var s in show_el)
                {
                    if (s.type == "文档")
                    {
                        //ThicknessAnimation txt_animation = new ThicknessAnimation();
                        FlowDocumentScrollViewer new_fld = content_stackpanel.FindName("newdoc" + s.ID) as FlowDocumentScrollViewer;
                        new_fld.Name = "newdoc" + s.ID;

                        new_fld.UpdateLayout();

                        Storyboard newstoryboard1 = new Storyboard();
                        newstoryboard1.Name = "t" + s.ID;
                        DoubleAnimation newadoubleop = new DoubleAnimation();
                        newadoubleop.From = 0;
                        newadoubleop.To = 1;
                        newadoubleop.Duration = TimeSpan.FromSeconds(5);
                        newstoryboard1.Children.Add(newadoubleop);
                        Storyboard.SetTargetName(newadoubleop, new_fld.Name);
                        Storyboard.SetTargetProperty(newadoubleop, new PropertyPath(OpacityProperty));
                        newstoryboard1.Completed += new EventHandler(newstoryboard_Completed);
                        alist.Add(newstoryboard1);
                        if (startanimation == 0)
                        {
                            newstoryboard1.Begin(content_stackpanel);
                            startanimation = 1;
                        }

                        Storyboard newstoryboard2 = new Storyboard();
                        newstoryboard2.Name = "m" + s.ID;
                        ThicknessAnimation txt_animation = new ThicknessAnimation();
                        txt_animation.From = new Thickness(0, 0, 0, 0);
                        if (PublicClass.show_hight - new_fld.ActualHeight >= 0)
                        {
                            txt_animation.To = new Thickness(0, 0, 0, 0);
                        }
                        else
                        {
                            txt_animation.To = new Thickness(0, (PublicClass.show_hight - new_fld.ActualHeight), 0, 0);
                        }
                        // newdoc.Visibility = Visibility.Visible;
                        txt_animation.Duration = TimeSpan.FromSeconds(s.show_time);
                        newstoryboard2.Children.Add(txt_animation);
                        Storyboard.SetTargetName(txt_animation, new_fld.Name);
                        Storyboard.SetTargetProperty(txt_animation, new PropertyPath(MarginProperty));
                        newstoryboard2.Completed += new EventHandler(newstoryboard_Completed);
                        alist.Add(newstoryboard2);
                        //newstoryboard.Stop(content_stackpanel);
                        //newstoryboard.Begin(content_stackpanel);

                        Storyboard newstoryboard3 = new Storyboard();

                        DoubleAnimation newadouble = new DoubleAnimation();
                        newadouble.From = 1;
                        newadouble.To = 0;
                        newadouble.Duration = TimeSpan.FromSeconds(5);
                        newstoryboard3.Children.Add(newadouble);
                        Storyboard.SetTargetName(newadouble, new_fld.Name);
                        Storyboard.SetTargetProperty(newadouble, new PropertyPath(OpacityProperty));
                        newstoryboard3.Completed += new EventHandler(newstoryboard_Completed);
                        alist.Add(newstoryboard3);


                        Storyboard newstoryboard4 = new Storyboard();
                        //newstoryboard2.Name = "m" + s.ID;
                        ThicknessAnimation txt_animation4 = new ThicknessAnimation();
                        //txt_animation4.From = new Thickness(0, 0, 0, 0);
                        txt_animation4.To = new Thickness(0, 0, 0, 0);
                        // newdoc.Visibility = Visibility.Visible;
                        txt_animation4.Duration = TimeSpan.FromSeconds(0.1);
                        newstoryboard4.Children.Add(txt_animation4);
                        Storyboard.SetTargetName(txt_animation4, new_fld.Name);
                        Storyboard.SetTargetProperty(txt_animation4, new PropertyPath(MarginProperty));
                        newstoryboard4.Completed += new EventHandler(newstoryboard_Completed);
                        alist.Add(newstoryboard4);
                    }
                    else if (s.type == "图片")
                    {
                        Image new_img = content_stackpanel.FindName("newimage" + s.ID) as Image;
                        new_img.Name = "newimage" + s.ID;

                      //  new_img.UpdateLayout();

                        Storyboard newstoryboard_img1 = new Storyboard();
                        newstoryboard_img1.Name = "t1" + s.ID;
                        DoubleAnimation newadoubleop = new DoubleAnimation();
                        newadoubleop.From = 0;
                        newadoubleop.To = 1;
                        newadoubleop.Duration = TimeSpan.FromSeconds(0.5);
                        newstoryboard_img1.Children.Add(newadoubleop);
                        Storyboard.SetTargetName(newadoubleop, new_img.Name);
                        Storyboard.SetTargetProperty(newadoubleop, new PropertyPath(OpacityProperty));
                        newstoryboard_img1.Completed += new EventHandler(newstoryboard_img1_Completed);
                        alist.Add(newstoryboard_img1);
                        if (startanimation == 0)
                        {
                            newstoryboard_img1.Begin(content_stackpanel);
                            startanimation = 1;
                        }
                        //newstoryboard_img1.Begin(content_stackpanel);



                        Storyboard newstoryboard_img3 = new Storyboard();
                        newstoryboard_img3.Name = "t3" + s.ID;
                        DoubleAnimation newadoubleop3 = new DoubleAnimation();
                        newadoubleop3.From = 1;
                        newadoubleop3.To = 1;
                        newadoubleop3.Duration = TimeSpan.FromSeconds(10);
                        newstoryboard_img3.Children.Add(newadoubleop3);
                        Storyboard.SetTargetName(newadoubleop3, new_img.Name);
                        Storyboard.SetTargetProperty(newadoubleop3, new PropertyPath(OpacityProperty));
                        newstoryboard_img3.Completed += new EventHandler(newstoryboard_img1_Completed);
                        alist.Add(newstoryboard_img3);



                        Storyboard newstoryboard_img2 = new Storyboard();
                        newstoryboard_img1.Name = "t2" + s.ID;
                        DoubleAnimation newadoubleop2 = new DoubleAnimation();
                        newadoubleop2.From = 1;
                        newadoubleop2.To = 0;
                        newadoubleop2.Duration = TimeSpan.FromSeconds(0.5);
                        newstoryboard_img2.Children.Add(newadoubleop2);
                        Storyboard.SetTargetName(newadoubleop2, new_img.Name);
                        Storyboard.SetTargetProperty(newadoubleop2, new PropertyPath(OpacityProperty));
                        newstoryboard_img2.Completed += new EventHandler(newstoryboard_img1_Completed);
                        alist.Add(newstoryboard_img2);
                        //newstoryboard_img2.Begin(content_stackpanel);
                    }
                  
                }


                //newstoryboard.Begin(content_stackpanel);

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

        void newstoryboard_med1_Completed(object sender, EventArgs e)
        {
            if (anmilistep < alist.Count - 1)
            {
                anmilistep++;
            }
            else
            {
                anmilistep = 0;
            }
            Storyboard newstor = alist[anmilistep] as Storyboard;
            newstor.Begin(content_stackpanel);
        }

        void newmeida_MediaOpened(object sender, RoutedEventArgs e)
        {
            Duration a = (sender as MediaElement).NaturalDuration;
            double b = a.TimeSpan.TotalSeconds;
        }

        void newmeida_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Play();
            //Duration a = (sender as MediaElement).NaturalDuration;
            //double b = a.TimeSpan.TotalSeconds;
        }

        void newmeida_Unloaded(object sender, RoutedEventArgs e)
        {
            {
                //MediaElement.IsMediaPlay = false;
                
                (sender as MediaElement).Stop();
            }
        }

        void newmeida_MediaEnded(object sender, RoutedEventArgs e)
        {
            ++mvindex;
            if (mvindex == mvlist.Count)
            {
                mvindex = 0;
            }
            //MediaElement newmeida2 = sender as MediaElement;
            (sender as MediaElement).Source = new Uri(mvlist[mvindex], UriKind.Absolute);
            //(sender as MediaElement).Stop();
            (sender as MediaElement).Play();

            
        }

        void newstoryboard_img1_Completed(object sender, EventArgs e)
        {
         
                if (anmilistep < alist.Count - 1)
                {
                    anmilistep++;
                }
                else
                {
                    anmilistep = 0;
                }
                Storyboard newstor = alist[anmilistep] as Storyboard;
               
                foreach (string t in titlename)
                {
                    string[] newtitle = t.Split('|');
                    if (newstor.Name != null && newstor.Name.Substring(2, newstor.Name.Length - 2) == newtitle[0])
                    {
                        small_title.Text = newtitle[1];
                    }
                }

                newstor.Begin(content_stackpanel);
           
        }

        

        void newstoryboard_Completed(object sender, EventArgs e)
        {

            foreach (var t in content_stackpanel.Children)
            {
                FlowDocumentScrollViewer newdoc = t as FlowDocumentScrollViewer;
                if (newdoc != null)
                {
                    newdoc.Width = content_stackpanel.ActualWidth;
                }

                //Storyboard newsto = t as Storyboard;
                //if (newsto != null)
                //{
                //    if (newsto.Name.Substring(0, 1) == "m")
                //    {
                //        var s = newsto.Children.Last();
                //        ThicknessAnimation n = s as ThicknessAnimation;
                //        n.To = new Thickness(0, -newdoc.ActualHeight, 0, 0);
                //    }
                //}

            }
            if (anmilistep < alist.Count-1)
            {
                anmilistep++;
            }
            else
            {
                anmilistep = 0;
            }




            Storyboard newstor = alist[anmilistep] as Storyboard;
            //foreach (string t in titlename)
            //{
            //    string[] newtitle = t.Split('|');
            //    if (newstor.Name!=null && newstor.Name.Substring(1, newstor.Name.Length-1) == newtitle[0])
            //    {
            //        small_title.Text = newtitle[1];
            //    }
            //}
            newstor.Begin(content_stackpanel);
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
