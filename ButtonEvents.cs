using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace wg_confGen
{
    public partial class MainWindow
    {
        //nav buttons
        private static Byte PageAmmount = 3;
        private static Byte CurrentPage = 0;

        private void NextPage(object sender, RoutedEventArgs e)
        {
            if (CurrentPage == PageAmmount)
            {
                return;
            }

            BackButton.IsEnabled = true;

            ChangePageView(CurrentPage, ++CurrentPage);

            if (CurrentPage == PageAmmount)
            {
                NxtButton.IsEnabled = false;
            }
        }

        private void PageBack(object sender, RoutedEventArgs e)
        {
            if (CurrentPage == 0)
            {
                return;
            }

            NxtButton.IsEnabled = true;

            ChangePageView(CurrentPage, --CurrentPage);

            if (CurrentPage == 0)
            {
                BackButton.IsEnabled = false;
            }
        }

        private void ChangePageView(Byte OldPage, Byte NewPage)
        {
            if (CreateServerBox.IsChecked == false) 
            {
            
            
            
            }















            //show new
            switch (NewPage)
            {
                case 0:
                    StartGrid.Visibility = Visibility.Visible;
                    break;

                case 1:
                    Page1.Visibility = Visibility.Visible;
                    break;

                case 2:
                    Page2.Visibility = Visibility.Visible;
                    break;

                case 3:
                    Page3.Visibility = Visibility.Visible;
                    break;
            }

            //deactivate old
            switch (OldPage)
            {
                case 0:
                    StartGrid.Visibility = Visibility.Collapsed;
                    break;

                case 1:
                    Page1.Visibility = Visibility.Collapsed;
                    break;

                case 2:
                    Page2.Visibility = Visibility.Visible;
                    break;

                case 3:
                    Page3.Visibility = Visibility.Visible;
                    break;
            }
        }

        //# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #


        //main page
        private void CreateServerConfig(object sender, RoutedEventArgs e)
        {
            if ((bool)CreateServerBox.IsChecked)
            {
                S2S.IsEnabled = true;
            }
            else
            {
                S2S.IsEnabled = false;
                S2S.IsChecked = false;
            }
        }

        private void ClientBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UInt32.TryParse(ClientBox.Text, out Clients))
            {
                ClientBox.Foreground = Brushes.Black;

                SetButtonStatus(true);
            }
            else
            {
                SetButtonStatus(false);

                ClientBox.Foreground = Brushes.OrangeRed;
            }

            void SetButtonStatus(Boolean NewState)
            {
                Dispatcher.BeginInvoke(new Action(() => { NxtButton.IsEnabled = NewState; }), System.Windows.Threading.DispatcherPriority.DataBind, null);
            }
        }


















        
    }
}
