﻿using DoubanFM.Desktop.Infrastructure;
using DoubanFM.Desktop.Infrastructure.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DoubanFM.Desktop.Shell.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly IModuleManager _moduleManager;
        private readonly IEventAggregator _eventAggregator;
        private Brush _backgroundColor;

        public ShellViewModel(
            IModuleManager moduleManager,
            IEventAggregator eventAggregator)
        {
            this._moduleManager = moduleManager;
            this._eventAggregator = eventAggregator;
            this._backgroundColor = new SolidColorBrush(Colors.DeepSkyBlue);

            _eventAggregator.GetEvent<SwitchBackgroudColorEvent>().Subscribe(ChangeBackgroundColor);

            this.MinimizeCommand = new DelegateCommand(Minimize);
            this.SwitchToMiniModeCommand = new DelegateCommand(SwitchToMiniMode);
            this.ExitCommand = new DelegateCommand(Exit);
            this.ModuleLoadRequestCommand = new DelegateCommand<IList>(LoadSelectedModule);
        }

        public Brush BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                if (value != _backgroundColor)
                {
                    _backgroundColor = value;
                    OnPropertyChanged(() => this.BackgroundColor);
                }
            }
        }


        public ICommand MinimizeCommand { get; private set; }

        public ICommand SwitchToMiniModeCommand { get; private set; }

        public ICommand ExitCommand { get; private set; }

        public ICommand ModuleLoadRequestCommand { get; private set; }


        private void LoadSelectedModule(IList list)
        {
            if (list == null || list.Count <= 0) return;
            var tabItem = list[0] as TabItem;
            var tag = (tabItem?.Content as FrameworkElement)?.Tag;
            if (tag == null) return;
            switch (tag.ToString())
            {
                case "Account":
                    this._moduleManager.LoadModule("AccountModule");
                    break;
                case "Search":
                    this._moduleManager.LoadModule("SearchModule");
                    break;
                case "Settings":
                    this._moduleManager.LoadModule("SettingsModule");
                    break;
                default:
                    break;
            }


        }

        private void ChangeBackgroundColor(Color color)
        {
            this.BackgroundColor = new SolidColorBrush(color);
        }

        private void Minimize()
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void SwitchToMiniMode()
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
            //TODO: After Hide MainWindow should exist in SystemTray, but it dispear, can only shutdown it down use TaskManager.
            //App.Current.MainWindow.Hide();
        }

        private void Exit()
        {
            App.Current.MainWindow.Close();
            //App.Current.Shutdown();
        }

    }
}
