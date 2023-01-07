using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExam.Model.Exporer
{
    public abstract class BaseExplorerItem : PropertyChangedBase, IExplorerItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }


        public int Size { get; set; }
        public DateTime UploadDate { get; set; }

        public virtual void Open() { }
    }
}
