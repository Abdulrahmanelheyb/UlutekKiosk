using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UlutekKiosk.Models;
using UlutekKioskModels;

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
        List<Publication> publications;
        Duration duration;
        DoubleAnimation doubleAnimation;
        readonly int DefaultTimeOfView = 10;
        int pubIndx = 0;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize mysql database connection.
            Mysqldb.Open();


            // Get publications list.
            publications = PublicationServices.GetPublications();

            // Check publications count bigger than 0.
            if (publications.Count > 0)
            {
                //Check first publication expiry date is expired or not.
                if (publications[0].ExpiryDate < DateTime.Now.Date)
                {
                    //Remove first publication from list
                    publications.RemoveAt(0);
                }

                //Set first index publication to image view.
                SlaytShow.Dispatcher.Invoke(() => { SlaytShow.Source = publications[0].Image; });
                //Set animation slider.
                InitProgressAnimation(publications[0].TimeOfView);
                pubIndx++;
            }
            else
            {
                // Set default image when no publications stored in db.
                SlaytShow.Source = GetDefaultImage();
                InitProgressAnimation(DefaultTimeOfView);
                SlaytShowProgress.Visibility = Visibility.Hidden;
                SlaytShow.Margin = new Thickness(0, 0, 0, 0);
            }
        }

        private BitmapImage GetDefaultImage()
        {
            //Get default image.
            using (var stream = new FileStream("images/default.png", FileMode.Open))
            {
                // Create new BitmapImage instance.
                var DefaultImage = new BitmapImage();
                DefaultImage.BeginInit();
                DefaultImage.CacheOption = BitmapCacheOption.OnLoad;
                DefaultImage.StreamSource = stream;
                DefaultImage.EndInit();
                return DefaultImage;
            }
        }

        private bool ResetCountIndex()
        {
            // This method for reset index count after access last index in publications list to loop again.
            bool rlt = false;
            if (pubIndx >= publications.Count)
            {
                pubIndx = 0;
                rlt = true;
            }
            return rlt;
        }

        private void InitProgressAnimation(int TimeOfView)
        {
            // This method initialize progressbar animation duration with publication time of view value. 
            duration = new Duration(TimeSpan.FromSeconds(TimeOfView));
            doubleAnimation = new DoubleAnimation(100, duration);
            doubleAnimation.Completed += DoubleAnimation_Completed;
            doubleAnimation.FillBehavior = FillBehavior.Stop;
            SlaytShowProgress.BeginAnimation(RangeBase.ValueProperty, doubleAnimation, HandoffBehavior.SnapshotAndReplace);
        }


        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            // This method after complete animation duration set next publication and check is data in db is changed.
            ResetCountIndex();

            if (publications.Count > 0)
            {
                // Check publication expiry date.
                if (publications[pubIndx].ExpiryDate < DateTime.Now.Date)
                {
                    publications.RemoveAt(pubIndx);
                    if (!ResetCountIndex())
                    {
                        pubIndx++;
                    }
                    SlaytShow.Dispatcher.Invoke(() => { SlaytShow.Source = publications[pubIndx].Image; });
                    InitProgressAnimation(publications[pubIndx].TimeOfView);
                    SlaytShowProgress.Visibility = Visibility.Visible;
                    SlaytShow.Margin = new Thickness(0, 0, 0, 10);
                    pubIndx++;
                }
                else
                {
                    SlaytShow.Dispatcher.Invoke(() => { SlaytShow.Source = publications[pubIndx].Image; });
                    InitProgressAnimation(publications[pubIndx].TimeOfView);
                    SlaytShowProgress.Visibility = Visibility.Visible;
                    SlaytShow.Margin = new Thickness(0, 0, 0, 10);
                    pubIndx++;
                }
            }
            else
            {
                // Set default image when no publications stored in db.
                SlaytShow.Dispatcher.Invoke(() => { SlaytShow.Source = GetDefaultImage(); });
                InitProgressAnimation(DefaultTimeOfView);
                SlaytShowProgress.Visibility = Visibility.Hidden;
                SlaytShow.Margin = new Thickness(0, 0, 0, 0);
            }

            

            // Check if have new publications in database inserted to get new publications.
            string poc = PublicationServices.OnChange();
            if (poc != null)
            {
                publications = PublicationServices.GetPublications();
                PublicationBase.PublicationOnChange = poc;
            }
        }

    }
}
