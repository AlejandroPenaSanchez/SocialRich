using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRichAlejandro.Models
{
    public class Networks
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public int SocialNetworksId { get; set; }

        public Users Users { get; set; }
        public SocialNetwork SocialNetworks { get; set; }
    }
}
