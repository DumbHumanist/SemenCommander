using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExam.Model.Exporer
{
    internal class IconView
    {
        public string Name { get; set; } = "Default";
        public int ItemWidth { get; set; } = 100;
        public int ItemHeight { get; set; } = 100;
        public int IconSize { get; set; } = 40;
        public int FontSize { get; set; } = 12;
        public int Margin { get; set; } = 4;
        public int Order { get; set; } = 0;

        public override string ToString()
        {
            return Name;
        }
    }
}
