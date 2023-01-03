using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfExam.Model;

namespace WpfExam.ViewModel.ViewModelChildren
{
    internal class SearchWiewModel : PropertyChangedBase
    {
        private string searchName = "Search";
        public string SearchName
        {
            set
            {
                searchName = value;
                NotifyOfPropertyChange();
            }
            get => searchName;
        }

        public void Search()
        {
            ExplorerSingleton.Instance.Search(SearchName);
        }
    }
}
