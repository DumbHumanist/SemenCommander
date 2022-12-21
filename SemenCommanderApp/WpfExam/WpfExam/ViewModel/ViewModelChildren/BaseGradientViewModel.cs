using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExam.ViewModel.ViewModelChildren
{
    class BaseGradientViewModel : PropertyChangedBase
    {
        public string Color1 { set; get; } = "Blue";
        public string Color2 { set; get; } = "Violet";

               

    }
}
