﻿using MagicMirror.UniversalApp.ViewModels;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MagicMirror.UniversalApp.Views
{
    public sealed partial class WiFiSettings : Page
    {
        public WiFiSettingsViewModel ViewModel { get; } = new WiFiSettingsViewModel();

        public WiFiSettings()
        {
            DataContext = ViewModel;
            InitializeComponent();
            ViewModel.InitializeAsync();
            ViewModel.OnReady += ViewModel_OnReady;
            ViewModel.OnConnecting += ViewModel_OnConnecting;
            ViewModel.OnError += ViewModel_OnError;
            ViewModel.OnSelect += ViewModel_OnSelect;
            ViewModel.OnDisconnected += ViewModel_OnDisconnected;
        }

        private void ViewModel_OnDisconnected(object sender, EventArgs e)
        {
            SetSelectedItemState(ViewModel.SelectedWiFiNetwork);
        }

        private void ViewModel_OnSelect(object sender, EventArgs e)
        {
            ResultsListView.ScrollIntoView(ViewModel.SelectedWiFiNetwork);
            SetSelectedItemState(ViewModel.SelectedWiFiNetwork);
            ResultsListView.Focus(FocusState.Pointer);
        }

        private void ViewModel_OnError(object sender, Exception e)
        {
            // TODO
        }

        private void ViewModel_OnReady(object sender, EventArgs e)
        {
            ScanButton.IsEnabled = true;
        }

        private void ViewModel_OnConnecting(object sender, EventArgs e)
        {
            SwitchToItemState(ViewModel.SelectedWiFiNetwork, WifiConnectedState, false);
        }

        private async void Scan_Click(object sender, RoutedEventArgs e)
        {
            ScanButton.IsEnabled = false;
            await ViewModel.ScanNetworksAsync();
            ScanButton.IsEnabled = true;
        }

        private ListViewItem SwitchToItemState(object dataContext, DataTemplate template, bool forceUpdate)
        {
            if (forceUpdate)
            {
                ResultsListView.UpdateLayout();
            }
            var item = ResultsListView.ContainerFromItem(dataContext) as ListViewItem;
            if (item != null)
            {
                item.ContentTemplate = template;
            }
            return item;
        }

        private void ResultsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedNetwork = ResultsListView.SelectedItem as WiFiNetworkViewModel;
            if (selectedNetwork == null)
            {
                return;
            }

            foreach (var item in e.RemovedItems)
            {
                SwitchToItemState(item, WifiInitialState, true);
            }

            foreach (var item in e.AddedItems)
            {
                var network = item as WiFiNetworkViewModel;
                SetSelectedItemState(network);
            }
        }
        private void SetSelectedItemState(WiFiNetworkViewModel network)
        {
            if (network == null)
                return;

            if (ViewModel.IsConnected(network.AvailableNetwork))
            {
                SwitchToItemState(network, WifiConnectedState, true);
            }
            else
            {
                SwitchToItemState(network, WifiConnectState, true);
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ConnectAsync();
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DisconnectAsync();
        }
    }
}