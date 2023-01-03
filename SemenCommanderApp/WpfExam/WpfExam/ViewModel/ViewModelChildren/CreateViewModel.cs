using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfExam.Model;
using WpfExam.Model.Exporer;

namespace WpfExam.ViewModel.ViewModelChildren
{
    internal class CreateViewModel : PropertyChangedBase
    {
        private string create;
        public string Create
        {
            set
            {
                create = value;
                NotifyOfPropertyChange();
            }
            get => create;
        }

        private string type;

        public CreateViewModel(string type)
        {
            this.type = type;
        }

        public void CreateItem()
        {
            ExplorerSingleton.Instance.Create(create, type);
        }
    }
}
