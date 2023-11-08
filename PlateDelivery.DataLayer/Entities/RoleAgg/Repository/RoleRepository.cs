using PlateDelivery.DataLayer._Utilities;
using PlateDelivery.DataLayer.Context;

namespace PlateDelivery.DataLayer.Entities.RoleAgg.Repository;

internal class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(PlateDeliveryDBContext context) : base(context)
    {
    }

    public bool CheckExistsRole(string RoleName)
    {
        if (Context.Roles.Where(p => p.RoleName == RoleName).Any())
            return true;
        return false;
    }

    public bool RemoveRole(long Id)
    {
        var role = Context.Roles.Find(Id);
        if (role == null)
            return false;
        Context.Roles.Remove(role);
        Context.SaveChanges();
        return true;
    }
}