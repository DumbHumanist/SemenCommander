using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WpfExam.Model.Exporer;
using WpfExam.View.ViewChildren;
using WpfExam.ViewModel.ViewModelChildren;

namespace WpfExam.Model
{
    class ExplorerSingleton : PropertyChangedBase
    {
        // Singleton
        static ExplorerSingleton instance;
        private ExplorerSingleton()
        {
            LoadDirectory("C:\\Program Files", false);
        }
        public static ExplorerSingleton Instance => instance ?? (instance = new ExplorerSingleton());
        public static ExplorerSingleton getInstance()
        {
            if (instance == null)
                instance = new ExplorerSingleton();
            return instance;
        }

        // settings
        public bool ShowHidden { get; set; } = false;
        private int itemWidth = 100;
        public int ItemWidth
        {
            set
            {
                itemWidth = value;
                NotifyOfPropertyChange();
            }
            get => itemWidth;
        }
        private int itemHeight = 100;
        public int ItemHeight
        {
            set
            {
                itemHeight = value;
                NotifyOfPropertyChange();
            }
            get => itemHeight;
        }

        private int iconSize = 40;
        public int IconSize
        {
            set
            {
                iconSize = value;
                NotifyOfPropertyChange();
            }
            get => iconSize;
        }
        private int fontSize = 12;
        public int FontSize
        {
            set
            {
                fontSize = value;
                NotifyOfPropertyChange();
            }
            get => fontSize;
        }

        private int margin = 5;
        public string Margin 
        { 
            get => $"{margin},5,{margin},2"; 
            set 
            { 
                margin = Convert.ToInt32(value); 
                NotifyOfPropertyChange(); 
            } 
        }

        public int IconViewOrder { get; set; } = 0;

        private IconView iconView = new IconView();

        private int windowWidth = 1243;
        private int windowHeight = 658;

        public void ChangeIconView(IconView iconView)
        {
            this.iconView = iconView;
            ItemHeight = iconView.ItemHeight;
            ItemWidth = iconView.ItemWidth;
            IconSize = iconView.IconSize;
            FontSize = iconView.FontSize;
            IconViewOrder = iconView.Order;

            Sort(windowWidth, windowHeight);
        }

        // navigation

        public bool CanUp()
        {
            return Path.Count() > 3;
        }
        public bool CanBack()
        {
            return backHistory.Any();
        }
        public bool CanForward()
        {
            return forwardHistory.Any();
        }

        // history

        private List<string> backHistory = new List<string>();
        private List<string> forwardHistory = new List<string>();

        // content
        private string lastPath;
        public string LastPath { get => lastPath; }
        private string path { get; set; }
        public string Path 
        { 
            get => path;
            set
            {
                path = value;
                NotifyOfPropertyChange();
            }
        }

        private ObservableCollection<IExplorerItem> content = new ObservableCollection<IExplorerItem>();
        public ObservableCollection<IExplorerItem> Content
        {
            set
            {
                content = value;
                NotifyOfPropertyChange();
            }
            get => content;
        }
        private ObservableCollection<ExplorerItemGroup> sortedContent = new ObservableCollection<ExplorerItemGroup>();
        public ObservableCollection<ExplorerItemGroup> SortedContent
        {
            set
            {
                sortedContent = value;
                NotifyOfPropertyChange();
            }
            get => sortedContent;
        }

        public void Search(string name)
        {
           this.Path = name;
           Sort(windowWidth, windowHeight, InnerSearch(name, lastPath));
        }

        private List<IExplorerItem> InnerSearch(string name, string path)
        {
            List<IExplorerItem> result = new List<IExplorerItem>();

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);
                foreach (string file in files)
                {
                    if (file.Substring(file.LastIndexOf('\\') + 1) == name)
                    {
                        result.Add(new ExplorerFile
                        {
                            Name = ProcessName(file.Substring(file.LastIndexOf('\\') + 1)),
                            Path = file,
                            Type = "file",
                            Icon = Directory.GetCurrentDirectory() + $"\\icons\\file.png",
                        });
                    }
                }

                foreach (string directory in directories)
                {
                    if (directory.Substring(directory.LastIndexOf('\\') + 1) == name)
                    {
                        result.Add(new ExplorerDirectory
                        {
                            Name = ProcessName(directory.Substring(directory.LastIndexOf('\\') + 1)),
                            Path = directory,
                            Type = "directory",
                            Icon = Directory.GetCurrentDirectory() + $"\\icons\\directory.png",
                        });
                    }
                    result.AddRange(InnerSearch(name, directory));
                }
            }
            catch { }

            return result;
        }
        

        public void LoadDirectory(string path, bool addToHistory = true)
        {
            path = path.Replace("/", "\\");
            if (path.Count() == 2)
                path += '\\';

            Content.Clear();
            ParseContent<ExplorerDirectory>("directory", Directory.GetDirectories(path));
            ParseContent<ExplorerFile>("file", Directory.GetFiles(path));
            Sort(1243, 658);
            ReloadIcons();

            if (addToHistory)
                backHistory.Add(lastPath);
            Path = lastPath = path;
        }

        private void ParseContent<T>(string type, string[] content) where T : IExplorerItem, new()
        {
            foreach (string item in content)
            {
                if (item.Substring(item.IndexOf('\\') + 1)[0] == '$' && !ShowHidden)
                    continue;
                Content.Add(
                    new T()
                    {
                        Name = ProcessName(item.Substring(item.LastIndexOf('\\') + 1)),
                        Path = item,
                        Type = type,
                        Icon = Directory.GetCurrentDirectory() + $"\\icons\\{type}.png",
                    }
                );
            }
        }

        private string ProcessName(string name)
        {
            if (name.Length > 25)
            {
                name = name.Substring(0, 25) + "...";
            }
            return name;
        }

        public void Up()
        {
            if (CanUp())
            {
                try
                {
                    LoadDirectory(Path.Substring(0, Path.LastIndexOf('\\')));
                }
                catch
                {
                    LoadDirectory(lastPath);
                    Sort(windowWidth, windowHeight);
                }
            }
        }
        public void Back()
        {
            if (CanBack())
            {
                forwardHistory.Add(Path);
                LoadDirectory(backHistory[backHistory.Count() - 1], false);
                Sort(windowWidth, windowHeight);
                backHistory.RemoveAt(backHistory.Count() - 1);
                ReloadIcons();
            }
        }
        public void Forward()
        {
            if (CanForward())
            {
                LoadDirectory(forwardHistory[forwardHistory.Count() - 1]);
                Sort(windowWidth, windowHeight);
                forwardHistory.RemoveAt(forwardHistory.Count() - 1);
                ReloadIcons();
            }
        }

        public void GoTo()
        {
            try
            {
                LoadDirectory(Path);
                Sort(windowWidth, windowHeight);
                ClearForward();
            }
            catch
            {
                ShowErrorMessage("Invalid path");
                Path = lastPath;
            }
        }

        public void ClearForward()
        {
            forwardHistory.Clear();
            ReloadIcons();
        }

        public void Rename(IExplorerItem item, string newName)
        {
            try
            {
                var str = item.Path.Substring(0, item.Path.LastIndexOf('\\')) + newName;
                if (item.Type == "file")
                    File.Move(item.Path, item.Path.Substring(0, item.Path.LastIndexOf('\\') + 1) + newName);
                else
                    Directory.Move(item.Path, item.Path.Substring(0, item.Path.LastIndexOf('\\')) + newName);
                LoadDirectory(lastPath, false);
                Sort(windowWidth, windowHeight);
            }
            catch { }
        }
        public void Delete(IExplorerItem item)
        {
            try
            {
                if (item.Type == "file")
                    File.Delete(item.Path);
                else
                    DeleteFolder(item.Path);
                LoadDirectory(lastPath, false);
                Sort(windowWidth, windowHeight);
            }
            catch { }
        }
        private void DeleteFolder(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                File.Delete(file);
            }
            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                DeleteFolder(directory);
            }
            Directory.Delete(path);
        }
        public void Create(string name, string type)
        {
            try
            {
                if (type == "file")
                    using (File.Create(lastPath + "\\" + name)) { }
                else
                    Directory.CreateDirectory(lastPath + "\\" + name);
                LoadDirectory(lastPath, false);
                Sort(windowWidth, windowHeight);
            }
            catch { }
        }

        private void ReloadIcons()
        {
            IconSingleton.Instance.UpIcon = "";
            IconSingleton.Instance.BackIcon = "";
            IconSingleton.Instance.ForwardIcon = "";
        }

        public void Sort(int width, int height)
        {
            windowHeight = height;
            windowWidth = width;

            SortedContent.Clear();
            ObservableCollection<ExplorerItemGroup> items = new ObservableCollection<ExplorerItemGroup>();
            int rowSize = (width / (ItemWidth + 10));
            int margin = iconView.Margin + (width - rowSize * (ItemWidth + 10)) / rowSize / 2;
            Margin = margin.ToString();

            items.Add(new ExplorerItemGroup());
            int j = 0;
            int i = 0;
            while (i < Content.Count)
            {
                items[items.Count - 1].Group.Add(Content[i]);
                j++;

                if (j % rowSize == 0)
                    items.Add(new ExplorerItemGroup());
                i++;
            }
            SortedContent = items;
        }

        public void Sort(int width, int height, List<IExplorerItem> content)
        {
            windowHeight = height;
            windowWidth = width;

            SortedContent.Clear();
            ObservableCollection<ExplorerItemGroup> items = new ObservableCollection<ExplorerItemGroup>();
            int rowSize = (width / (ItemWidth + 10));
            int margin = iconView.Margin + (width - rowSize * (ItemWidth + 10)) / rowSize / 2;
            Margin = margin.ToString();

            items.Add(new ExplorerItemGroup());
            int j = 0;
            int i = 0;
            while (i < content.Count - 1)
            {
                items[items.Count - 1].Group.Add(content[i]);
                j++;

                if (j % rowSize == 0)
                    items.Add(new ExplorerItemGroup());
                i++;
            }
            SortedContent = items;
        }
        public void ShowErrorMessage(string message)
        {
            var window = new ErrorView();
            var viewModel = new ErrorViewModel(message);
            window.DataContext = viewModel;
            window.Show();
        }
    }
}
