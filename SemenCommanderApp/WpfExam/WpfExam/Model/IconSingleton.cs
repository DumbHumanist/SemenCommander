using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExam.Model
{
    internal class IconSingleton : PropertyChangedBase
    {
        static IconSingleton instance;
        public static IconSingleton Instance => instance ?? (instance = new IconSingleton());
        private string localDir = Directory.GetCurrentDirectory() + "\\Icons\\";
        public string DoneIcon { get => localDir + "done.png"; }
        public string BackIcon
        {
            get
            {
                return localDir + (ExplorerSingleton.Instance.CanBack() ? "backHighlighted.png" : "back.png");
            }
            set
            {
                NotifyOfPropertyChange();
            }
        }
        public string ForwardIcon
        {
            get
            {
                return localDir + (ExplorerSingleton.Instance.CanForward() ? "forwardHighlighted.png" : "forward.png");
            }
            set
            {
                NotifyOfPropertyChange();
            }
        }
        public string ForwardHighlightedIcon { get => localDir + "forwardHighlighted.png"; }
        public string UpIcon
        {
            get
            {
                return localDir + (ExplorerSingleton.Instance.CanUp() ? "upHighlighted.png" : "up.png");
            }
            set
            {
                NotifyOfPropertyChange();
            }
        }
        public string RefreshIcon { get => localDir + "refresh.png"; }
        public string SearchIcon { get => localDir + "search.png"; }
        public string StorageIcon { get => localDir + "storage.png"; }
        public string SettingsIcon { get => localDir + "settings.png"; }
    }
}
