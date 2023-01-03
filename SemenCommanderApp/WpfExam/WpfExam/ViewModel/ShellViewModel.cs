using System;
using System.Net.Http;
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
            ExplorerSingleton.Instance.LoadDirectory(ExplorerSingleton.Instance.Path, false);
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
        public async void OpenBackup()
        {
            string data = await GetBackupFilesAsync();
        }
        private async Task<string> GetBackupFilesAsync()
        {
            var client = new HttpClient();
            return await client.GetStringAsync("https://localhost:44340/backup/f886c565-0d27-4f49-9608-0cc975ce5299/all");
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