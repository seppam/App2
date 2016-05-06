﻿using System;
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
    
    public sealed partial class PlaySingle : Page
    {
        private int staticNumA, staticNumB, staticResult, staticRandomResult,Score=0,State=1,BestScore=0,mode;
        private DispatcherTimer dispatcherTimer;

        private void settupProgressBar()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0,1);
            dispatcherTimer.Start();

            progressBar.Value = 9999;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= PlaySingle_BackRequested;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += PlaySingle_BackRequested;
            dispatcherTimer = null;

            Playing();
        }

        private async void PlaySingle_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            dispatcherTimer.Stop();
            dispatcherTimer = null;

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

        private void DispatcherTimer_Tick(object sender, object e)
        {
            progressBar.Value -= Common.Common.Speed;
            if(progressBar.Value <= 0)
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;

                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }

        public PlaySingle()
        {
            this.InitializeComponent();
        }

        private void btnTrue_Click(object sender, RoutedEventArgs e)
        {
            if(mode == 1)  //mode = 1, TRUE
            {
                txtScore.Text = String.Format("Score:{0}".ToUpper(), ++Score);
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

        private void Playing()
        {
            Random rd = new Random();
            int value = rd.Next(1, 4);
            if(value == 1) //+
            {
                staticNumA = rd.Next(1, 9);
                staticNumB = rd.Next(0, staticNumA - 1);
                staticResult = staticNumA + staticNumB;
                staticRandomResult = rd.Next(0, 99);

                mode = rd.Next(0, 1); //Random mode show answer. if mode = 0 show incorrect result
                if (mode == 0)
                    txtMath.Text = String.Format("{0} + {1} = {2}", staticNumA, staticNumB, staticRandomResult);
                else
                    txtMath.Text = String.Format("{0} + {1} = {2}", staticNumA, staticNumB, staticResult);
            }
            if (value == 2) //-
            {
                staticNumA = rd.Next(1, 9);
                staticNumB = rd.Next(0, staticNumA - 1);
                staticResult = staticNumA - staticNumB;
                staticRandomResult = rd.Next(0, 99);

                mode = rd.Next(0, 1); //Random mode show answer. if mode = 0 show incorrect result
                if (mode == 0)
                    txtMath.Text = String.Format("{0} - {1} = {2}", staticNumA, staticNumB, staticRandomResult);
                else
                    txtMath.Text = String.Format("{0} - {1} = {2}", staticNumA, staticNumB, staticResult);
            }
            if (value == 3) //*
            {
                staticNumA = rd.Next(1, 9);
                staticNumB = rd.Next(0, staticNumA - 1);
                staticResult = staticNumA * staticNumB;
                staticRandomResult = rd.Next(0, 99);

                mode = rd.Next(0, 1); //Random mode show answer. if mode = 0 show incorrect result
                if (mode == 0)
                    txtMath.Text = String.Format("{0} * {1} = {2}", staticNumA, staticNumB, staticRandomResult);
                else
                    txtMath.Text = String.Format("{0} * {1} = {2}", staticNumA, staticNumB, staticResult);
            }
            if (value == 4) // divide /
            {
                staticNumA = rd.Next(1, 9);
                staticNumB = rd.Next(1, staticNumA); //tidak boleh 0 karena hasilnya akan tak hingga
                staticResult = staticNumA / staticNumB;
                staticRandomResult = rd.Next(0, 99);

                mode = rd.Next(0, 1); //Random mode show answer. if mode = 0 show incorrect result
                if (mode == 0)
                    txtMath.Text = String.Format("{0} / {1} = {2}", staticNumA, staticNumB, staticRandomResult);
                else
                    txtMath.Text = String.Format("{0} / {1} = {2}", staticNumA, staticNumB, staticResult);
            }

            settupProgressBar();
        }

        private void btnFalse_Click(object sender, RoutedEventArgs e)
        {
            if (mode == 0)  
            {
                txtScore.Text = String.Format("Score:{0}".ToUpper(), ++Score);
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
    }
}
