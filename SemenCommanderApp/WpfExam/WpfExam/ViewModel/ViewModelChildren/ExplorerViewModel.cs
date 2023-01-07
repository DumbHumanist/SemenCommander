using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfExam.Ifrastructure;
using WpfExam.Model;
using WpfExam.Model.Exporer;
using WpfExam.View.ViewChildren;

namespace WpfExam.ViewModel.ViewModelChildren
{
    class ExplorerViewModel : PropertyChangedBase
    {
        private IExplorerItem selectedItem;
        public IExplorerItem SelectedItem
        {
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange();
            }
            get => selectedItem;
        }
        public ICommand OpenCommand { get => new RelayCommand(ContextMenuOpen); }
        public ICommand RenameCommand { get => new RelayCommand(ContextMenuRename); }
        public ICommand CopyCommand { get => new RelayCommand(ContextMenuCopy); }
        public ICommand BackupCommand { get => new RelayCommand(ContextMenuBackup); }
        public ICommand PropertiesCommand { get => new RelayCommand(ContextMenuProperties); }
        public ICommand DeleteCommand { get => new RelayCommand(ContextMenuDelete); }

        public ICommand CreateFolderCommand { get => new RelayCommand(ContextMenuCreateFolder); }
        public ICommand CreateFileCommand { get => new RelayCommand(ContextMenuCreateFile); }

        // Item context menu
        private void ContextMenuOpen(object obj)
        {
            OpenItem((IExplorerItem)obj);
        }
        private void ContextMenuCopy(object obj)
        {
            StringCollection paths = new StringCollection();
            paths.Add(((IExplorerItem)obj).Path);
            Clipboard.SetFileDropList(paths);
        }
        private async void ContextMenuBackup(object obj)
        {
            string data = await BackupFileAsync((ExplorerFile)obj);
        }

        private async Task<string> BackupFileAsync(ExplorerFile file)
        {
            return await BackupSingleton.Instance.BackupFile(file);
        }

        private void ContextMenuDelete(object obj)
        {
            ExplorerSingleton.Instance.Delete((IExplorerItem)obj);
        }
        private void ContextMenuRename(object obj)
        {
            var window = new RenameView();
            var viewModel = new RenameViewModel((IExplorerItem)obj);
            window.DataContext = viewModel;
            window.Show();
        }
        private void ContextMenuProperties(object obj)
        {
            var window = new PropertiesView();
            var viewModel = new PropertiesViewModel();
            window.DataContext = viewModel;
            window.Show();
        }

        // List context menu
        private void ContextMenuCreateFolder(object obj)
        {
            var window = new CreateView();
            var viewModel = new CreateViewModel("folder");
            window.DataContext = viewModel;
            window.Show();
        }
        private void ContextMenuCreateFile(object obj)
        {
            var window = new CreateView();
            var viewModel = new CreateViewModel("file");
            window.DataContext = viewModel;
            window.Show();
        }
        //

        public void OpenItem(IExplorerItem item)
        {
            item.Open();
        }

        public void ResizeContent(ListView source)
        {
            ExplorerSingleton.Instance.Sort((int)source.ActualWidth, (int)source.ActualHeight);
        }
    }
}
