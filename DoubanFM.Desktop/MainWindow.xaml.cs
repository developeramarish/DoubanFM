﻿using DoubanFM.Desktop.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace DoubanFM.Desktop
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            var vm = new MainPageViewModel();
            this.DataContext = vm;
            this.spectrumAnalyzer.RegisterSoundPlayer(vm.Player);
            this.waveformTimeline.RegisterSoundPlayer(vm.Player);
        }


        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

    }

}
