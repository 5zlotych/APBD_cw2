namespace APBD_ćw2.Users;

public class EmployeeRole : IUserRole
{
    public string RoleName => "Pracownik";
    public int MaxItemsAllowed => 5;
}