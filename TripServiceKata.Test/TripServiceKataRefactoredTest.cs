using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using TripServiceKata.User;

namespace TripServiceKata.Test
{
    [TestClass]
    public class TripServiceKataRefactoredTest
    {
        private static readonly User.CurrentUser NOT_LOGGED_USER = null;

        private class TestTripService : TripServiceRefactored
        {
            protected sealed override List<Trip.Trip> GetTripsOfUser(User.User user)
            {
                return user.Trips();
            }
        }

        private static TestTripService CreateTripService()
        {
            return new TestTripService();
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotLoggedInException), "null logged in user inappropriately allowed")]
        public void ShouldThrowExceptionWhenUserIsNotLoggedIn()
        {
            var tripService = TripServiceKataRefactoredTest.CreateTripService();
            tripService.GetTripsOfFriend(TripServiceKataRefactoredTest.NOT_LOGGED_USER, new User.User());
        }

        [TestMethod]
        public void ShouldReturnAnEmptyListIfNotFriends()
        {
            var loggedInUser = new User.CurrentUser();
            var givenUser = new User.User();
            Trip.Trip[] givenUsersTrips = { new Trip.Trip(), new Trip.Trip(), new Trip.Trip() };

            foreach (var trip in givenUsersTrips)
            {
                givenUser.AddTrip(trip);
            }

            List<Trip.Trip> trips = TripServiceKataRefactoredTest.CreateTripService().GetTripsOfFriend(loggedInUser, givenUser);
            Assert.AreEqual(0, trips.Count);
        }

        [TestMethod]
        public void ShouldReturnTheTripsOfTheUser()
        {
            var loggedInUser = new User.CurrentUser();
            var givenUser = new User.User();
            Trip.Trip[] givenUsersTrips = { new Trip.Trip(), new Trip.Trip(), new Trip.Trip() };

            givenUser.AddFriend(loggedInUser);
            foreach (var trip in givenUsersTrips)
            {
                givenUser.AddTrip(trip);
            }

            List<Trip.Trip> trips = TripServiceKataRefactoredTest.CreateTripService().GetTripsOfFriend(loggedInUser, givenUser);
            Assert.AreEqual(3, trips.Count);
        }
    }
}
