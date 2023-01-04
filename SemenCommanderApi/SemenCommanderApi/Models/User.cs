using System;
using System.Collections.Generic;

#nullable disable

namespace SemenCommanderApi.Models
{
    public partial class User
    {
        public User()
        {
            Files = new HashSet<File>();
        }

        public Guid UserId { get; set; }
        public string Password { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
