using System;
using System.Collections.Generic;

#nullable disable

namespace SemenCommanderApi.Models
{
    public partial class File
    {
        public File(byte[] content, Guid userId)
        {
            FileContent = content;
            UserId = userId;
        }

        public Guid FileId { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
