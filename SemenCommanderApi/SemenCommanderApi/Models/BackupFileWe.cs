using System;

namespace SemenCommanderApi.Models
{
    public class BackupFileWe
    {
        public BackupFileWe(File file)
        {
            Name = file.FileName;
            Size = file.FileContent.Length;
            UploadDate = file.UploadDate;
            FileId = file.FileId;
        }
        public string Name { get; set; }
        public int Size { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid FileId { get; set; }
    }
}
