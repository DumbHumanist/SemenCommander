using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WpfExam.Model;
using WpfExam.Model.Exporer;

namespace WpfExam.ViewModel.ViewModelChildren
{
    class SettingsViewModel : PropertyChangedBase
    {
        public string Text { set; get; } = "Settings";

        private int themeIndex;

        public int ThemeIndex
        {
            set
            {
                themeIndex = value;
                NotifyOfPropertyChange();
            }
            get => themeIndex;
        }
        
        private string themeBackground = "Background Color";

        public string ThemeBackground
        {
            set
            {
                themeBackground = value;
                NotifyOfPropertyChange();
            }
            get => themeBackground;
        }
        
        private string themeName = "Theme Name";

        public string ThemeName
        {
            set
            {
                themeName = value;
                NotifyOfPropertyChange();
            }
            get => themeName;
        }
        
        private string themeText = "Text Color";

        public string ThemeText
        {
            set
            {
                themeText = value;
                NotifyOfPropertyChange();
            }
            get => themeText;
        }
        
        private string themeOverlay = "Overlay Color";

        public string ThemeOverlay
        {
            set
            {
                themeOverlay = value;
                NotifyOfPropertyChange();
            }
            get => themeOverlay;
        }
        
        private string themeAccent = "Accent Color";

        public string ThemeAccent
        {
            set
            {
                themeAccent = value;
                NotifyOfPropertyChange();
            }
            get => themeAccent;
        }

        public delegate void ThemeChanged(int index);
        public ThemeChanged ChangeTheme { get; set; }

        public void AddTheme()
        {
            ThemeSingleton.Instance.Themes.Add(new Theme(ThemeName, ThemeBackground, ThemeText, ThemeOverlay, ThemeAccent));
            ThemeName = "Theme Name";
            ThemeBackground = "Background Color";
            ThemeText = "Text Color";
            ThemeOverlay = "Overlay Color";
            ThemeAccent = "Accent Color";
            ThemeSingleton.Instance.SaveThemes();
        }
        public void GenerateUser()
        {
            UserSingleton.Instance.GenerateUser();
        }
        public void RestoreUser()
        {
            UserSingleton.Instance.GenerateUser();
        }

        public void ChangeThemeList()
        {
            ChangeTheme(themeIndex);
        }

        public void RemoveTheme()
        {
            if (ThemeIndex != 0 && ThemeIndex != -1)
            {
                ThemeSingleton.Instance.Themes.RemoveAt(ThemeIndex);
            }
        }

        // Icon View

        private ObservableCollection<IconView> iconViews = new ObservableCollection<IconView>(ThemeSingleton.Instance.IconViews);
        public ObservableCollection<IconView> IconViews
        {
            set
            {
                iconViews = value;
                NotifyOfPropertyChange();
            }
            get => iconViews;
        }

        private IconView selectedIconView = new IconView();
        public IconView SelectedIconView
        {
            set
            {
                selectedIconView = value;
                NotifyOfPropertyChange();
            }
            get => selectedIconView;
        }

        public void ChangeIconView()
        {
            ExplorerSingleton.Instance.ChangeIconView(SelectedIconView);
            ThemeSingleton.Instance.SwitchTheme();
        }
    }
}
