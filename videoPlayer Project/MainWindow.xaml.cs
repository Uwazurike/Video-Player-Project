using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace videoPlayer_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(500);
            timer.Tick += new EventHandler(Timer_Tick);
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            slider_seek.Value = mediaElement.Position.TotalSeconds;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();

        }

        private void slider_volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = (double)slider_volume.Value;

        }

        private void slider_seek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Position = TimeSpan.FromSeconds(slider_seek.Value);
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string fileName = (string)((DataObject)e.Data).GetFileDropList()[0];
            mediaElement.Source = new Uri(fileName);
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Manual;
            mediaElement.Volume = (double)slider_volume.Value;
            mediaElement.Play();
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = mediaElement.NaturalDuration.TimeSpan;
            slider_seek.Maximum = ts.TotalSeconds;
            timer.Start();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.DefaultExt = ".mp4";
            fileDialog.Filter = "videofile(.mp4) | *.mp4";

            if (fileDialog.ShowDialog() == true)
            {
                string fileName = fileDialog.FileName;
                mediaElement.Source = new Uri(fileName);
                mediaElement.LoadedBehavior = MediaState.Manual;
                mediaElement.UnloadedBehavior = MediaState.Manual;
                mediaElement.Volume = (double)slider_volume.Value;
                mediaElement.Play();

            }
        }
    }
}
