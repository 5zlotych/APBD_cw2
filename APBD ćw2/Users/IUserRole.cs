namespace APBD_ćw2.Users;

public interface IUserRole
{
    string RoleName { get; }
    int MaxItemsAllowed { get; }
}