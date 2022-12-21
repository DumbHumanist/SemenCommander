using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExam.Model.Exporer
{
    public class ExplorerItemGroup : PropertyChangedBase
    {
        private ObservableCollection<IExplorerItem> group = new ObservableCollection<IExplorerItem>();
        public ObservableCollection<IExplorerItem> Group
        {
            set
            {
                group = value;
                NotifyOfPropertyChange();
            }
            get
            {
                return group;
            }
        }
    }
}
