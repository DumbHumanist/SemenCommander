using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WpfExam.Model.Exporer;

namespace WpfExam.Model
{
    class ThemeSingleton : PropertyChangedBase
    {
        static ThemeSingleton instance;
        public static ThemeSingleton Instance => instance ?? (instance = new ThemeSingleton());
        private ThemeSingleton()
        {
            LoadThemes();
        }

        Theme currentTheme = new Theme();
        public Theme CurrentTheme
        {
            set
            {
                currentTheme = value;
                SwitchTheme();
                NotifyOfPropertyChange();
            }
            get => currentTheme;
        }
        
        ObservableCollection<Theme> themes = new ObservableCollection<Theme>();
        public ObservableCollection<Theme> Themes
        {
            set
            {
                themes = value;
                NotifyOfPropertyChange();
            }
            get => themes;
        }

        public List<IconView> IconViews = new List<IconView>()
        {
            new IconView(),
            new IconView()
            {
                Name = "Small",
                ItemWidth = 80,
                ItemHeight = 80,
                IconSize = 30,
                FontSize = 10,
                Order = 1
            },
            new IconView()
            {
                Name = "Big",
                ItemWidth = 120,
                ItemHeight = 120,
                IconSize = 50,
                FontSize = 14,
                Margin = 4,
                Order = 2
            }
        };

        public void SaveThemes()
        {
            File.Delete("currentTheme.txt");
            using (StreamWriter sw = new StreamWriter("currentTheme.txt", true))
            {
                sw.Write($"{currentTheme.Name};{currentTheme.BackgroundColor};{currentTheme.TextColor};{currentTheme.OverlayColor};{currentTheme.AccentColor};{ExplorerSingleton.Instance.IconViewOrder}");
            }
            File.Delete("themes.txt");
            using (StreamWriter sw = new StreamWriter("themes.txt", true))
            {
                foreach(Theme t in Themes)
                    sw.Write($"{t.Name};{t.BackgroundColor};{t.TextColor};{t.OverlayColor};{t.AccentColor};");
            }
        }

        public void SwitchTheme()
        {
            File.Delete("currentTheme.txt");
            using (StreamWriter sw = new StreamWriter("currentTheme.txt", true))
            {
                sw.Write($"{currentTheme.Name};{currentTheme.BackgroundColor};{currentTheme.TextColor};{currentTheme.OverlayColor};{currentTheme.AccentColor};{ExplorerSingleton.Instance.IconViewOrder}");
            }
        }

        public void LoadThemes()
        {
            string[] currentThemeLoad = File.ReadAllText("currentTheme.txt").Split(';');
            currentTheme = new Theme(currentThemeLoad[0], currentThemeLoad[1], currentThemeLoad[2], currentThemeLoad[3], currentThemeLoad[4]);
            ExplorerSingleton.Instance.ChangeIconView(IconViews.FirstOrDefault(x => x.Order == Convert.ToInt32(currentThemeLoad[5])));

            string[] themesLoad = File.ReadAllText("themes.txt").Split(';');
            for (int i = 0; i < themesLoad.Length - 1;)
            {
                Themes.Add(new Theme(themesLoad[i++], themesLoad[i++], themesLoad[i++], themesLoad[i++], themesLoad[i++]));
            }
        }
    }
}
