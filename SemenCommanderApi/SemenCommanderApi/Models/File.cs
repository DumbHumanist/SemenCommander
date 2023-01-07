using System;
using System.Collections.Generic;

#nullable disable

namespace SemenCommanderApi.Models
{
    public partial class File
    {
        public File() { }
        public File(byte[] content, string name, Guid userId)
        {
            FileContent = content;
            FileName = name;
            UserId = userId;
        }
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
