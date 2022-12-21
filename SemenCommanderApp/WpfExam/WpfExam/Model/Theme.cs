using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExam.Model
{
    class Theme : PropertyChangedBase
    {

        private string name = "Default";
        public string Name
        {
            set
            {
                name = value;
                NotifyOfPropertyChange();
            }
            get => name;
        }

        private string backgroundColor = "White";
        public string BackgroundColor
        {
            set
            {
                backgroundColor = value;
                NotifyOfPropertyChange();
            }
            get => backgroundColor;
        }

        private string textColor = "Black";
        public string TextColor
        {
            set
            {
                textColor = value;
                NotifyOfPropertyChange();
            }
            get => textColor;
        }
        
        private string overlayColor = "LightGray";
        public string OverlayColor
        {
            set
            {
                overlayColor = value;
                NotifyOfPropertyChange();
            }
            get => overlayColor;
        }

        private string accentColor = "#2a2930";
        public string AccentColor
        {
            set
            {
                accentColor = value;
                NotifyOfPropertyChange();
            }
            get => accentColor;
        }

        public Theme()
        {

        }
        public Theme(string name, string backgroundColor, string textColor, string overlayColor, string accentColor)
        {
            Name = name;
            BackgroundColor = backgroundColor;
            TextColor = textColor;
            OverlayColor = overlayColor;
            AccentColor = accentColor;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
