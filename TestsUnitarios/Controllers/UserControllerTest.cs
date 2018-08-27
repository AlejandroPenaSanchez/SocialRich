using FizzWare.NBuilder;
using FluentAssertions;
using SocialRich.Entities;
using SocialRichAlejandro.ViewModel;
using SocialRichAlejandro.Controllers;
using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestsUnitarios.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public void GetSocialNetworkList()
        {
            //Arrange
            //var optionsBuilder = new DbContextOptionsBuilder<Context>();
            //optionsBuilder.UseSqlServer("Server=APENAS-LPT\\SQLEXPRESS;Database=PrimeraPrueba;Trusted_Connection=True;MultipleActiveResultSets=true", providerOptions => providerOptions.CommandTimeout(60)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //var userController = Builder<UserController>.CreateNew().WithFactory(() => new UserController(new Context(optionsBuilder.Options))).Build();
            var userController = Builder<UserController>.CreateNew().WithFactory(() => new UserController(new Context())).Build();

            //Act
            var result = userController.GetSocialNetworkList();

            //Assert
            result.Should().BeOfType<List<SocialNetworkViewModel>>();
            //result[0].Id.Should().Be(1);
        }

        [Fact]
        public void Edit()
        {
            //Arrange
            var userController = Builder<UserController>.CreateNew().WithFactory(() => new UserController(new Context())).Build();

            //Act
            var result = userController.Edit(1);

            //Assert
            result.Should().BeOfType<ViewResult> ();
        }
    }
}
