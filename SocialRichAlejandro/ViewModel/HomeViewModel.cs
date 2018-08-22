using SocialRichAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRichAlejandro.ViewModel
{
    public class HomeViewModel
    {
        public List<UsuarioViewModel> UserList { get; set; }
        public List<SocialNetwork> SocialNetwokList { get; set; }
        public List<SocialNetworkViewModel> SocialNetworkUsersList { get; set; }
    }
}
