﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public enum LoadingState
    {
        LOADING,
        NETWORK_UNAVAILABLE,
        SUCCESS,
        DONE
    }

    public sealed partial class NetworkLoading : UserControl
    {
        #region Field && Property

        public Action RetryAction { get; set; }

        #endregion

        #region Constructor

        public NetworkLoading()
        {
            this.InitializeComponent();
            
            this.loading.Visibility = Visibility.Collapsed;
            this.noNetwork.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Public Method

        public void SetState(LoadingState state)
        {
            switch (state)
            {
                case LoadingState.LOADING:
                    ShowLoadingState();
                    break;
                case LoadingState.NETWORK_UNAVAILABLE:
                    ShowNetworkUnAvailableState();
                    break;
                case LoadingState.SUCCESS:
                case LoadingState.DONE:
                    this.loading.Visibility = Visibility.Collapsed;
                    this.noNetwork.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void ShowNetworkUnAvailableState()
        {
            this.loading.Visibility = Visibility.Collapsed;
            this.loading.StopLoading();
            this.noNetwork.Visibility = Visibility.Visible;
        }

        private void ShowLoadingState()
        {
           
            this.noNetwork.Visibility = Visibility.Collapsed;
            this.loading.Visibility = Visibility.Visible;
            this.loading.StartLoading();
        }

        #endregion

        #region Event

        private void RetryButton_Click(object sender, RoutedEventArgs e)
        {
            if (RetryAction != null)
            {
                RetryAction.Invoke();
            }
        }

        #endregion
    }
}
