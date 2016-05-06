using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class PlayAdvanced : Page
    {
        private Random randomMath = new Random();
        private int Score = 0, State = 1, bestScore = 0, staticNumA, staticNumB, staticResult;
        private DispatcherTimer dispatcherTimer;

        void setupProgressBar()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();

            progressBar.Value = 9999;
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            progressBar.Value -= Common.Common.Speed;
            if(progressBar.Value <= 0)
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver),Score.ToString());

            }
        }

        public PlayAdvanced()
        {
            this.InitializeComponent();
        }

        private int randomNumber()
        {
            return randomMath.Next(1, 9);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= PlayAdvanced_BackRequested;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += PlayAdvanced_BackRequested;
            bestScore = int.Parse(Common.Common.LoadSettings("BestScore"));
            txtBestScore.Text = String.Format("BEST : {0}", bestScore);
            dispatcherTimer = null;
            Playing();

        }

        private int randomMathvalue()
        {
            return randomMath.Next(0, 3); 
        }
        private void Playing()
        {

            int numberA = randomNumber();
            int numberB = randomMath.Next(0, numberA - 1);
            int mathValue = randomMathvalue();
            int result = -1;

            if (mathValue == 0)
                result = numberA + numberB;
            else if (mathValue == 1)
                result = numberA - numberB;
            else if (mathValue == 2)
                result = numberA * numberB;
            else
                result = numberA / numberB;

            staticNumA = numberA;
            staticNumB = numberB;
            staticResult = result;
            txtMath.Text = String.Format("{0} ... {1} = {2}", staticNumA, staticNumB, staticResult);

            setupProgressBar();
        }

        private async void PlayAdvanced_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            var msg = new MessageDialog("Do you want to stop?");
            var okBtn = new UICommand("Yes");
            var cancelBtn = new UICommand("No");
            msg.Commands.Add(okBtn);
            msg.Commands.Add(cancelBtn);
            IUICommand result = await msg.ShowAsync();

            if (result != null && result.Label.Equals("Yes"))
            {
                Frame.Navigate(typeof(GameOver), Score.ToString());  //navigate GameOver page and send Score
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            if(staticNumA+staticNumB==staticResult)
            {
                txtScore.Text = String.Format("Score : {0}".ToUpper(), ++Score);
                txtState.Text = String.Format("{0}", ++State);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Playing();

            }
            else
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            if (staticNumA - staticNumB == staticResult)
            {
                txtScore.Text = String.Format("Score : {0}".ToUpper(), ++Score);
                txtState.Text = String.Format("{0}", ++State);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Playing();

            }
            else
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }

        private void btnMulit_Click(object sender, RoutedEventArgs e)
        {
            if (staticNumA * staticNumB == staticResult)
            {
                txtScore.Text = String.Format("Score : {0}".ToUpper(), ++Score);
                txtState.Text = String.Format("{0}", ++State);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Playing();

            }
            else
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }

        private void btnDiv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (staticNumA / staticNumB == staticResult)
                {
                    txtScore.Text = String.Format("Score : {0}".ToUpper(), ++Score);
                    txtState.Text = String.Format("{0}", ++State);
                    dispatcherTimer.Stop();
                    dispatcherTimer = null;
                    Playing();

                }
                else
                {
                    dispatcherTimer.Stop();
                    dispatcherTimer = null;
                    Frame.Navigate(typeof(GameOver), Score.ToString());
                }
            }
            catch(DivideByZeroException)
            {
                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }
    }
}
