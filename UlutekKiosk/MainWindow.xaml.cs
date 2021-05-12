using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        int pubIndx = 0;

        public MainWindow()
        {
            InitializeComponent();
            Mysqldb.ConnectionString = Mysqldb.GetConnectionString("localhost", "publications", "Schoolserver", "School+admin3");
            Mysqldb.Open();
            publications = PublicationServices.GetPublications();
            if (publications.Count > 0)
            {
                if (publications[0].ExpiryDate <= DateTime.Now)
                {
                    PublicationServices.DeleteExpiredImage(publications[0].ID);
                    publications.RemoveAt(0);
                }

                SlaytShow.Source = publications[0].Image;
                InitProgressAnimation(publications[0].TimeOfView);
                pubIndx++;

            }
        }

        private bool ResetCountIndex()
        {
            bool rlt = false;
            if (pubIndx == publications.Count)
            {
                pubIndx = 0;
                rlt = true;
            }
            return rlt;
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            ResetCountIndex();

            if (publications[pubIndx].ExpiryDate <= DateTime.Now)
            {
                PublicationServices.DeleteExpiredImage(publications[pubIndx].ID);
                publications.RemoveAt(pubIndx);
                if (!ResetCountIndex())
                {
                    pubIndx++;
                }
                SlaytShow.Dispatcher.Invoke(() => { SlaytShow.Source = publications[pubIndx].Image; });
                InitProgressAnimation(publications[pubIndx].TimeOfView);
                pubIndx++;
            }
            else
            {
                SlaytShow.Dispatcher.Invoke(() => { SlaytShow.Source = publications[pubIndx].Image; });
                InitProgressAnimation(publications[pubIndx].TimeOfView);
                pubIndx++;
            }

            // Check OnChange
            string poc = PublicationServices.OnChange();
            if (poc != null)
            {
                publications = PublicationServices.GetPublications();
                Publication.PublicationOnChange = poc;
            }
        }

        private void InitProgressAnimation(int TimeOfView)
        {
            duration = new Duration(TimeSpan.FromSeconds(TimeOfView));
            doubleAnimation = new DoubleAnimation(100, duration);
            doubleAnimation.Completed += DoubleAnimation_Completed;
            doubleAnimation.FillBehavior = FillBehavior.Stop;
            SlaytShowProgress.BeginAnimation(RangeBase.ValueProperty, doubleAnimation, HandoffBehavior.SnapshotAndReplace);
        }
    }
}
