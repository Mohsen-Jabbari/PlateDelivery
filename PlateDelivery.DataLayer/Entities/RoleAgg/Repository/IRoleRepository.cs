using PlateDelivery.Common.Repository;

namespace PlateDelivery.DataLayer.Entities.RoleAgg.Repository;
public interface IRoleRepository : IBaseRepository<Role>
{
    bool CheckExistsRole(string RoleName);
    bool RemoveRole(long Id);
}
