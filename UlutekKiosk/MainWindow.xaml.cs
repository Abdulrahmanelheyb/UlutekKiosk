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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UlutekKiosk.BLL;
using UlutekKiosk.OAL;

/*
 * 
 > Creation Date: 3/29/2021
 > Copyright: (C) Ulutek
 > Developed By Abdulrahman Elheyb
 > Author: Ulutek

 */

namespace UlutekKiosk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Publication> pubs;
        Duration duration;
        DoubleAnimation doubleAnimation;

        int pubIndx = 0;

        public MainWindow()
        {
            InitializeComponent();
            pubs = PublicationServices.GetPublications();
            if (pubs.Count > 0)
            {
                SlaytShow.Source = pubs[0].Image;
                InitProgressAnimation(pubs[0].TimeOfView);
                pubIndx++;
            }
            
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            if (pubIndx == pubs.Count)
            {
                pubIndx = 0;
            }

            SlaytShow.Dispatcher.Invoke(() => { SlaytShow.Source = pubs[pubIndx].Image; });
            InitProgressAnimation(pubs[pubIndx].TimeOfView);
            pubIndx++;
        }

        private void InitProgressAnimation(int TimeOfView)
        {
            duration = new Duration(TimeSpan.FromSeconds(TimeOfView));
            doubleAnimation = new DoubleAnimation(100, duration);
            doubleAnimation.Completed += DoubleAnimation_Completed;
            doubleAnimation.FillBehavior = FillBehavior.Stop;
            SlaytShowProgress.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation, HandoffBehavior.SnapshotAndReplace);
        }
    }
}
