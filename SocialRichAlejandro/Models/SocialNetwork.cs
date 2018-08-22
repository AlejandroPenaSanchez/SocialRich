using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRichAlejandro.Models
{
    public class SocialNetwork
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Users> User { get; set; }
        public virtual ICollection<Networks> Networks { get; set; }
    }
}
