using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using TripServiceKata.User;

namespace TripServiceKata.Test
{
    [TestClass]
    public class TripServiceKataTest
    {
        private static readonly User.User NOT_LOGGED_USER = null;

        private class TestTripService : TripService
        {
            private readonly User.User loggedInUser;

            public TestTripService(User.User loggedInUser)
            {
                this.loggedInUser = loggedInUser;
            }

            protected sealed override User.User GetLoggedInUser()
            {
                return loggedInUser;
            }

            protected sealed override List<Trip.Trip> GetTripsOfUser(User.User user)
            {
                return user.Trips();
            }
        }

        private static TestTripService createTripService(User.User loggedInUser)
        {
            return new TestTripService(loggedInUser);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotLoggedInException), "null logged in user inappropriately allowed")]
        public void ShouldThrowExceptionWhenUserIsNotLoggedIn()
        {
            var tripService = TripServiceKataTest.createTripService(TripServiceKataTest.NOT_LOGGED_USER);
            tripService.GetTripsByUser(new User.User());
        }

        [TestMethod]
        public void ShouldReturnAnEmptyListIfNotFriends()
        {
            var loggedInUser = new User.User();
            var givenUser = new User.User();
            Trip.Trip[] givenUsersTrips = { new Trip.Trip(), new Trip.Trip(), new Trip.Trip() };

            foreach (Trip.Trip trip in givenUsersTrips)
            {
                givenUser.AddTrip(trip);
            }

            List<Trip.Trip> trips = TripServiceKataTest.createTripService(loggedInUser).GetTripsByUser(givenUser);
            Assert.AreEqual(0, trips.Count);
        }

        [TestMethod]
        public void ShouldReturnTheTripsOfTheUser()
        {
            var loggedInUser = new User.User();
            var givenUser = new User.User();
            Trip.Trip[] givenUsersTrips = { new Trip.Trip(), new Trip.Trip(), new Trip.Trip() };

            givenUser.AddFriend(loggedInUser);
            foreach (Trip.Trip trip in givenUsersTrips)
            {
                givenUser.AddTrip(trip);
            }

            List<Trip.Trip> trips = TripServiceKataTest.createTripService(loggedInUser).GetTripsByUser(givenUser);
            Assert.AreEqual(3, trips.Count);
        }
    }
}
