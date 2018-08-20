using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRich.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }
        public SocialNetwork FavouriteNetwork { get; set; }
        public List<SocialNetwork> SocialNet { get; set; }
    }
}
