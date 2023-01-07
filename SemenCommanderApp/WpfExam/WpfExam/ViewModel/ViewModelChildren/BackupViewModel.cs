using Caliburn.Micro;
using Newtonsoft.Json;
using SemenCommanderApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfExam.Ifrastructure;
using WpfExam.Model;

namespace WpfExam.ViewModel.ViewModelChildren
{
    internal class BackupViewModel : PropertyChangedBase
    {
        public ICommand DeleteCommand { get => new RelayCommand(ContextMenuDelete); }
        public ICommand LoadCommand { get => new RelayCommand(ContextMenuLoad); }


        public BackupViewModel()
        {
            BackupSingleton.Instance.LoadFiles();
        }

        public void LoadItem(IExplorerItem item)
        {
            BackupSingleton.Instance.LoadFile(item.Path);
        }
        public async void ContextMenuDelete(object item)
        {
            await BackupSingleton.Instance.DeleteFile(((IExplorerItem)item).Path);
            BackupSingleton.Instance.LoadFiles();
        }
        public async void ContextMenuLoad(object item)
        {
            LoadItem((IExplorerItem)item);
        }

        public void ResizeContent(ListView source)
        {
            BackupSingleton.Instance.Sort((int)source.ActualWidth, (int)source.ActualHeight);
        }
    }
}
