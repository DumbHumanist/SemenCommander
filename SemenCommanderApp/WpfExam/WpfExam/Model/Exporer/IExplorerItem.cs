using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExam.Model
{
    public interface IExplorerItem
    {
        string Name { get; set; }
        string Path { get; set; }
        string Type { get; set; }
        string Icon { get; set; }

        void Open();
    }
}
