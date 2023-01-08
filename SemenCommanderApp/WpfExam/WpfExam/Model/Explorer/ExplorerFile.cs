using SemenCommanderApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExam.Model.Exporer
{
    public class ExplorerFile : BaseExplorerItem
    {
        public ExplorerFile() { }
        public ExplorerFile(BackupFileWe file)
        {
            Name = file.Name;
            UploadDate = file.UploadDate;
            Size = file.Size;
            Type = "file";
            Icon = Directory.GetCurrentDirectory() + $"\\icons\\file.png";
            Path = file.FileId.ToString();
        }
        public override void Open() 
        {
            try
            {
                System.Diagnostics.Process.Start(Path);
            }
            catch { }
        }
    }
}
