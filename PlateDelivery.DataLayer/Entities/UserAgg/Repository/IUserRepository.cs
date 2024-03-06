using PlateDelivery.Common.Repository;

namespace PlateDelivery.DataLayer.Entities.UserAgg.Repository;
public interface IUserRepository : IBaseRepository<User>
{
    User GetUserByUserName(string userName);
}
