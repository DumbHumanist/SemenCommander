using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfExam.Ifrastructure;
using WpfExam.Model;

namespace WpfExam.ViewModel.ViewModelChildren
{
    class ExplorerViewModel : BaseGradientViewModel
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
        public ICommand CopyCommand { get => new RelayCommand(ContextMenuCopy); }
        public ICommand BackupCommand { get => new RelayCommand(ContextMenuBackup); }
        public ICommand DeleteCommand { get => new RelayCommand(ContextMenuDelete); }

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
            string data = await GetBackupFilesAsync();
        }

        private async Task<string> GetBackupFilesAsync()
        {
            var client = new HttpClient();
            return await client.GetStringAsync("https://localhost:44340/backup/f886c565-0d27-4f49-9608-0cc975ce5299/all");
        }

        private void ContextMenuDelete(object obj)
        {
            OpenItem((IExplorerItem)obj);
        }

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
