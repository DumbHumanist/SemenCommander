using System;

namespace SemenCommanderApi.Models
{
    public class BackupFileWe
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid FileId { get; set; }
    }
}
