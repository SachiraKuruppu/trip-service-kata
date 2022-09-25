using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripServiceKata.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void ShouldBeAbleToIdentifyWhetherFriends()
        {
            var user1 = new User.User();
            var user2 = new User.User();
            var user3 = new User.User();

            user1.AddFriend(user2);

            Assert.IsTrue(user1.isFriendsWith(user2));
            Assert.IsFalse(user1.isFriendsWith(user3));
        }
    }
}
