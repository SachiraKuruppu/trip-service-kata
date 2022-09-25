using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripServiceRefactored
    {
        public List<Trip> GetTripsOfFriend(User.CurrentUser loggedUser, User.User user)
        {           
            if (loggedUser == null) throw new UserNotLoggedInException();

            return user.isFriendsWith(loggedUser) ? GetTripsOfUser(user) : new List<Trip>();
        }

        protected virtual List<Trip> GetTripsOfUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }
}
