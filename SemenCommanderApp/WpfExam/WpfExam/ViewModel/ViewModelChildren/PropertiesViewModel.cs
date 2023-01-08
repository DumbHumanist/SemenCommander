using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfExam.Model;

namespace WpfExam.ViewModel.ViewModelChildren
{
    public class PropertiesViewModel : PropertyChangedBase
    {
        private string fileName;
        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                NotifyOfPropertyChange();
            }
        }
        
        private string path;
        public string Path
        {
            get => path;
            set
            {
                path = value;
                NotifyOfPropertyChange();
            }
        }
        
        private string size;
        public string Size
        {
            get => size;
            set
            {
                size = value;
                NotifyOfPropertyChange();
            }
        }

        private string created;
        public string Created
        {
            get => created;
            set
            {
                created = value;
                NotifyOfPropertyChange();
            }
        }

        private string modified;
        public string Modified
        {
            get => modified;
            set
            {
                modified = value;
                NotifyOfPropertyChange();
            }
        }

        public PropertiesViewModel() { }
        
        public PropertiesViewModel(IExplorerItem item) 
        {
            FileName = item.Path.Substring(item.Path.LastIndexOf('\\') + 1);
            Path = item.Path;
            if (item.Type == "file")
            {
                Size = new System.IO.FileInfo(item.Path).Length.ToString();
                Created = File.GetCreationTime(item.Path).ToString();
                Modified = File.GetLastWriteTime(item.Path).ToString();
            }
            else
            {
                Size = DirSize(new System.IO.DirectoryInfo(item.Path)).ToString();
                Created = Directory.GetCreationTime(item.Path).ToString();
                Modified = Directory.GetLastWriteTime(item.Path).ToString();
            }
        }
        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}
