using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialRich.Entities;
using SocialRichAlejandro.Models;
using SocialRichAlejandro.ViewModel;

namespace SocialRichAlejandro.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var test = _context.SocialNetwork.Where(s => s.Id == 1).First().User.ToList();
            

            List<SocialNetworkViewModel> SNVMList = new List<SocialNetworkViewModel>();
            var SNList = _context.SocialNetwork.ToList();

            foreach (var socialNet in SNList)
            {
                List<Users> SNUsersList = new List<Users>();
                var NetList = _context.Networks.Where(n => n.SocialNetworksId == socialNet.Id).ToList();
                foreach (var net in NetList)
                {
                    SNUsersList.Add(_context.Users.First(u => u.Id == net.UsersId));
                }

                SocialNetworkViewModel model = new SocialNetworkViewModel
                {
                    Id = socialNet.Id,
                    Name = socialNet.Name,
                    Url = socialNet.Url,
                    SocialNetworkUsers = SNUsersList
                };

                SNVMList.Add(model);
            }
            

            return View(new HomeViewModel() {
                SocialNetwokList = _context.SocialNetwork.ToList(),
                SocialNetworkUsersList = SNVMList
            });
        }






        [HttpPost]
        public IActionResult AddUser(AddUserViewModel model) {
            if (model.Name != null && model.Subname != null) {
                var newUser = new Users
                {
                    Name = model.Name,
                    Subname = model.Subname
                };
                if (model.FavouriteNetwork > 0)
                {
                    newUser.SocialNetworkId = model.FavouriteNetwork;
                }

                var user = _context.Users.Add(newUser);

                _context.SaveChanges();

                if (model.Netwoks != null)
                {
                    foreach (var net in model.Netwoks)
                    {
                        _context.Networks.Add(new Networks
                        {
                            UsersId = user.Entity.Id,
                            SocialNetworksId = net,
                        });
                    }

                    _context.SaveChanges();
                }
            }
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult ShowUser(int Id)
        {
            try
            {
                var user = _context.Users.Where(u => u.Id == Id).First();
                var networksList = _context.Networks.Where(n => n.UsersId == user.Id).ToList();
                List<SocialNetwork> networks = new List<SocialNetwork>();
                foreach (var net in networksList)
                {
                    networks.Add(_context.SocialNetwork.First(s => s.Id == net.SocialNetworksId));
                }
                var model = new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Subname = user.Subname,
                    FavouriteNetwork = user.SocialNetworkId == null ? new SocialNetwork() : _context.SocialNetwork.First(s => s.Id == user.SocialNetworkId),
                    Networks = networks
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return Redirect("Index");
            } 
        }

        [HttpPost]
        public IActionResult ShowUser(int UserId, int socialNetworkid, bool IsFavourite)
        {
            try
            {
                var querableUsuario = _context.Users.Where(u => u.Id == UserId);
                if (_context.SocialNetwork.Where(s => s.Id == socialNetworkid).Any() && querableUsuario.Any())
                {
                    if (IsFavourite)
                    {       
                        var usuario = querableUsuario.First();
                        if (usuario.SocialNetworkId != socialNetworkid)
                        {
                            usuario.SocialNetworkId = socialNetworkid;
                            _context.Users.Update(usuario);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        var networks = _context.Networks.Where(n => n.UsersId == UserId && n.SocialNetworksId == socialNetworkid);
                        if (!networks.Any())
                        {                      
                            _context.Networks.Add(new Networks
                            {
                                UsersId = UserId,
                                SocialNetworksId = socialNetworkid
                            });

                            if (!_context.Networks.Where(n => n.UsersId == UserId).Any())
                            {
                                var usuario = querableUsuario.First();
                                usuario.SocialNetworkId = socialNetworkid;
                                _context.Users.Update(usuario);
                            }

                            _context.SaveChanges();
                        }
                    }
                }
                return Redirect("Index");
            }
            catch (Exception ex)
            {
                return Redirect("Index");
            }
        }



        public IActionResult AddSocialNetwork(string Name, string Url)
        {
            if (Name != null && Url != null)
            {
                _context.SocialNetwork.Add(new SocialNetwork
                {
                    Name = Name,
                    Url = Url
                });

                _context.SaveChanges();
            }
            return Redirect("Index");
        }















        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
