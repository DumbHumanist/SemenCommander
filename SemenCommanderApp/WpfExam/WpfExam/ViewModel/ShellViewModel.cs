using Newtonsoft.Json;
using SemenCommanderApi.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfExam.Ifrastructure;
using WpfExam.Model;
using WpfExam.View.ViewChildren;
using WpfExam.ViewModel.ViewModelChildren;

namespace WpfExam {
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell
    {
        // Views
        private IUserView settingsView = new SettingsView();
        private IUserView exporerView = new ExporerView();
        public IUserView currentView;
        public IUserView CurrentView
        {
            set
            {
                currentView = value;
                NotifyOfPropertyChange();
            }
            get => currentView;
        }

        public ShellViewModel()
        {
            SettingsView tmp = new SettingsView();
            SettingsViewModel tmpVm = new SettingsViewModel();
            tmpVm.ChangeTheme += ChangeTheme;
            tmp.DataContext = tmpVm;
            settingsView = tmp;
            CurrentView = exporerView;
        }
        public void Up()
        {
            ExplorerSingleton.Instance.Up();
        }
        public void Back()
        {
            ExplorerSingleton.Instance.Back();
        }
        public void Forward()
        {
            ExplorerSingleton.Instance.Forward();
        }
        public void GoTo()
        {
            ExplorerSingleton.Instance.GoTo();
        }
        public void Refresh()
        {
            ExplorerSingleton.Instance.LoadDirectory(ExplorerSingleton.Instance.LastPath, false);
        }
        public void Search()
        {
            SearchView modalWindow = new SearchView();
            modalWindow.ShowDialog();
        }
        public void Settings()
        {
            CurrentView = CurrentView == settingsView ? exporerView : settingsView;
        }
        public void OpenBackup()
        {
            BackupView modalWindow = new BackupView();
            modalWindow.ShowDialog();
        }
        

        //Change theme
        public void ChangeTheme(int index)
        {
            try
            {
                ThemeSingleton.Instance.CurrentTheme = ThemeSingleton.Instance.Themes[index];
            }
            catch (Exception) { }
        }

    }
}