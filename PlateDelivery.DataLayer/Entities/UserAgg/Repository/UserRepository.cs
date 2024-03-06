using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.UserAgg.Repository;

internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(PlateDeliveryDBContext context) : base(context)
    {

    }

    public User GetUserByUserName(string userName)
    {
        var user = Context.Users.Where(u => u.UserName == userName).FirstOrDefault();
        if (user == null)
            return new User("User Not Found", "", "", "", false, false);
        return user;
    }
}