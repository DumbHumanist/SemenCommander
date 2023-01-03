using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfExam.Model;

namespace WpfExam.ViewModel.ViewModelChildren
{
    internal class RenameViewModel : PropertyChangedBase
    {
        private string rename;
        public string Rename 
        { 
            set
            {
                rename = value;
                NotifyOfPropertyChange();
            } 
            get => rename;
        }

        private IExplorerItem renamedItem;

        public RenameViewModel(IExplorerItem item)
        {
            renamedItem = item;
            Rename = item.Name;
        }

        public void RenameItem()
        {
            ExplorerSingleton.Instance.Rename(renamedItem, Rename);
        }
    }
}
