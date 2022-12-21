using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExam.Model.Exporer
{
    public class ExplorerDirectory : BaseExplorerItem
    {
        public override void Open()
        {
            ExplorerSingleton.Instance.LoadDirectory(Path);
            ExplorerSingleton.Instance.ClearForward();
        }
    }
}
