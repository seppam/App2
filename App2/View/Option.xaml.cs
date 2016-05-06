using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App2.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Option : Page
    {
        public Option()
        {
            this.InitializeComponent();
        }

        private void ck2Mode_Checked(object sender, RoutedEventArgs e)
        {
            ck4Mode.IsChecked = false;
            Common.Common.SaveSettings("PlayMode", "0");
        }

        private void ck4Mode_Checked(object sender, RoutedEventArgs e)
        {
            ck2Mode.IsChecked = false;
            Common.Common.SaveSettings("PlayMode", "1");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= Option_BackRequested;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += Option_BackRequested;
            if(Common.Common.PlayMode == 0)
            {
                ck2Mode.IsChecked = true;
                ck4Mode.IsChecked = false;
            }
            else
            {
                ck2Mode.IsChecked = false;
                ck4Mode.IsChecked = true;
            }

            int sliderValue = Common.Common.Speed;
            slider.Value = sliderValue / 10;
        }

        private void Option_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Common.Common.Speed = int.Parse(slider.Value.ToString()) * 10;
            Common.Common.SaveSettings("Speed", Common.Common.Speed.ToString());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
