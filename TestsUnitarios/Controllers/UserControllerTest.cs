using FizzWare.NBuilder;
using FluentAssertions;
using SocialRich.Entities;
using SocialRichAlejandro.ViewModel;
using SocialRichAlejandro.Controllers;
using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TestsUnitarios.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public void GetSocialNetworkList()
        {
            //Arrange
            var userController = Builder<UserController>.CreateNew().Build();

            //Act
            var result = userController.GetSocialNetworkList();

            //Assert
            result.Should().BeOfType<SocialNetworkViewModel>();
        }

        [Fact]
        public void Edit()
        {
            //Arrange
            var userController = Builder<UserController>.CreateNew().Build();

            //Act
            var result = userController.Edit(1);

            //Assert
            result.Should().BeOfType<ActionResult>();
        }
    }
}
