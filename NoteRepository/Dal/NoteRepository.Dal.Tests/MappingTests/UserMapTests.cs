﻿using System;
using NoteRepository.Common.Utility.Dal;
using NoteRepository.Dal.NH;
using NoteRepository.Core.DomainEntity;
using NUnit.Framework;
using System.Linq;

namespace NoteRepository.Dal.Tests.MappingTests
{
    [TestFixture]
    public class UserMapTests : TestFixtureBase
    {
        #region private fields

        private IRepository<User> _userRepository;

        #endregion private fields

        #region setup environment

        [SetUp]
        public void SetupEnvironment()
        {
            // Arrange - create repository
            _userRepository = new NhRepository<User>(UnitOfWorkTest);
        }

        #endregion setup environment

        #region tests

        [Test]
        public void Can_Get_User_With_Valid_FirstName()
        {
            // Act
            var user = _userRepository.FindEntities().FirstOrDefault(c => c.FirstName == "Jack");

            // Assert
            Assert.NotNull(user);
            Assert.AreEqual(user.FirstName, "Jack");
            Assert.AreEqual(user.LastName, "Fang");
            Assert.AreEqual(user.AccountName, "jfang");
        }

        [Test]
        public void Get_User_With_InvalidFirst_Name_Null_Returned()
        {
            // Act
            var user = _userRepository.FindEntities().FirstOrDefault(c => c.FirstName == null);

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public void Get_User_With_NoExisted_FirstName_Null_Retured()
        {
            // Act
            var user = _userRepository.FindEntities().FirstOrDefault(c => c.FirstName == "123456");

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public void Can_Save_NewUser_With_ValidInfo()
        {
            // Arrange
            var newUsr = new User
            {
                FirstName = "Ying",
                LastName = "Wang",
                AccountName = "ywang",
                BirthDay = new DateTime(1971,10,21),
                Password = "1234",
                Salt = "5678",
                IsActivated = true,
            };

            // Act
            _userRepository.Add(newUsr);

            // Assert
            var srcUsr = _userRepository.FindEntities().FirstOrDefault(u => u.AccountName == "ywang");
            Assert.NotNull(srcUsr);
            Assert.AreEqual(srcUsr.AccountName, "ywang");
            Assert.AreEqual(srcUsr.FirstName, "Ying");

            // Tear down changes
            _userRepository.Delete(newUsr);
        }

        [Test]
        public void Can_Delete_SavedUser()
        {
            // arrange
            var newUsr = new User
            {
                FirstName = "Ying",
                LastName = "Wang",
                AccountName = "ywang",
                Password = "1234",
                BirthDay = DateTime.Now,
                Salt = "5678",
                IsActivated = true,
            };

            _userRepository.Add(newUsr);

            var srcUsr = _userRepository.FindEntities().FirstOrDefault(u => u.AccountName == "ywang");
            Assert.NotNull(srcUsr);
            Assert.AreEqual(srcUsr.AccountName, "ywang");
            Assert.AreEqual(srcUsr.FirstName, "Ying");

            // act
            _userRepository.Delete(newUsr);

            // assert
            srcUsr = _userRepository.FindEntities().FirstOrDefault(u => u.AccountName == "ywang");
            Assert.Null(srcUsr);
        }

        #endregion tests
    }
}