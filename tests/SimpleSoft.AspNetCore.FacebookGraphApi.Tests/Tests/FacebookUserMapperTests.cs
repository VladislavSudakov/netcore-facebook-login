using System;
using System.Dynamic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleSoft.AspNetCore.FacebookGraphApi.Mappers;
using SimpleSoft.AspNetCore.FacebookGraphApi.Models;

namespace SimpleSoft.AspNetCore.FacebookGraphApi.Tests.Tests
{
    [TestClass]
    public class FacebookUserMapperTests
    {
        [TestMethod]
        public void FacebookUserMapper_MapUser_ShouldMapAllFields()
        {
            dynamic picture = new ExpandoObject();
            picture.data = new ExpandoObject();
            picture.data.url = "https://image.com/aJfdhdndgKD";

            var model = new UserProfileResponse
                            {
                                Email = "testEmail@mail.com",
                                UserId = "11010102919819294",
                                Name = "Name Surname",
                                Picture = picture
                            };

            var result = FacebookUserMapper.MapUser(model);

            Assert.AreEqual(model.Email, result.Email);
            Assert.AreEqual(model.UserId, result.UserId);
            Assert.AreEqual(model.Name, result.FullName);
            Assert.AreEqual(model.ImageUrl, result.ImageUrl);
        }

        [TestMethod]
        public void FacebookUserMapper_MapUser_NullModel_ShouldThrow()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FacebookUserMapper.MapUser(null));
        }
    }
}