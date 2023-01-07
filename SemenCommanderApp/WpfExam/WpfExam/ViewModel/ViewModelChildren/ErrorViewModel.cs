using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExam.ViewModel.ViewModelChildren
{
    public class ErrorViewModel : PropertyChangedBase
    {
        public ErrorViewModel() { }
        public ErrorViewModel(string message)
        {
            ErrorMessage = message;
        }
        public string ErrorMessage { get; set; }
    }
}
