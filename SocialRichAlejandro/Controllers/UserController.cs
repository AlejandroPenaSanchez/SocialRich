using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SocialRich.Entities;
using SocialRichAlejandro.Models;
using SocialRichAlejandro.ViewModel;

namespace SocialRichAlejandro.Controllers
{
    public class UserController : Controller
    {
        #region properties and constructor

        private readonly Context _context;

        public UserController(Context context)
        {
            _context = context;
        }

        #endregion

        #region Index

        [HttpGet]
        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            List<UserViewModel> list = new List<UserViewModel>();
            foreach (var user in users)
            {
                list.Add(new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Subname = user.Subname,
                    FavouriteNetwork = user.SocialNetworkId == null ? new SocialNetwork() : _context.SocialNetwork.First(s => user.SocialNetworkId == s.Id),//user.SocialNetwork.Name
                    Networks = GetNetworksNames(user.Id)
                });
            }
            return View(new UsersListViewModel
            {
                UserList = list
            });
        }

        private List<SocialNetwork> GetNetworksNames(int UserId)
        {
            var networks = _context.Networks.Where(n => n.UsersId == UserId).ToList();
            List<SocialNetwork> list = new List<SocialNetwork>();
            foreach (var network in networks)
            {
                list.Add(_context.SocialNetwork.First(s => s.Id == network.SocialNetworksId));
            }
            return list;
        }

        #endregion

        #region details
        [HttpGet]
        public ActionResult Details(int Id)
        {
            try
            {
                var user = _context.Users.First(u => u.Id == Id);
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
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult EditFavourite(EditUserSocialNetViewModel model)
        {

            return RedirectToAction("Details", new RouteValueDictionary(new
            {
                Controller = "UserController",
                action = "Details",
                Id = 0
            }));
        }

        public ActionResult AddSocialNet(EditUserSocialNetViewModel model)
        {

            return RedirectToAction("Details", new RouteValueDictionary( new
            {
                Controller = "UserController",
                action = "Details",
                Id = 0
            }));
        }

        #endregion

        #region Create 

        [HttpGet]
        public ActionResult Create()
        {
            var networks = _context.SocialNetwork.ToList();
            List<SocialNetworkViewModel> list = new List<SocialNetworkViewModel>();
            foreach (var net in networks)
            {
                list.Add(new SocialNetworkViewModel
                {
                    Id = net.Id,
                    Name = net.Name,
                    Url = net.Url
                });
            }
            return View();
        }


        [HttpPost]
        public ActionResult CreateUser(AddUserViewModel model)
        {
            try
            {
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Create));
            }
        }

        [HttpPost]
        public List<SocialNetworkViewModel> GetSocialNetworkList()
        {
            var SNList = _context.SocialNetwork.ToList();
            List<SocialNetworkViewModel> list = new List<SocialNetworkViewModel>();
            foreach (var net in SNList)
            {
                list.Add(new SocialNetworkViewModel
                {
                    Id = net.Id,
                    Name = net.Name,
                    Url = net.Url
                });
            }
            return list;
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult Edit(int id)
        { 
            return View(new EditUserSocialNetViewModel
            {
                UserId = id
            });
        }

        [HttpPost]
        public ActionResult Edit(EditUserSocialNetViewModel model)
        {
            try
            {
                var querableUsuario = _context.Users.Where(u => u.Id == model.UserId);
                if (_context.SocialNetwork.Where(s => s.Id == model.SocialNetworkId).Any() && querableUsuario.Any())
                {
                    if (model.IsFavourite)
                    {
                        var usuario = querableUsuario.First();
                        if (usuario.SocialNetworkId != model.SocialNetworkId)
                        {
                            usuario.SocialNetworkId = model.SocialNetworkId;
                            _context.Users.Update(usuario);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        var networks = _context.Networks.Where(n => n.UsersId == model.UserId && n.SocialNetworksId == model.SocialNetworkId);
                        if (!networks.Any())
                        {
                            _context.Networks.Add(new Networks
                            {
                                UsersId = model.UserId,
                                SocialNetworksId = model.SocialNetworkId
                            });

                            if (!_context.Networks.Where(n => n.UsersId == model.UserId).Any())
                            {
                                var usuario = querableUsuario.First();
                                usuario.SocialNetworkId = model.SocialNetworkId;
                                _context.Users.Update(usuario);
                            }

                            _context.SaveChanges();
                        }
                    }
                }
                return RedirectToAction(nameof(Details));
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Delete
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}