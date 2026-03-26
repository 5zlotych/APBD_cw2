namespace APBD_ćw2.Users;

public class StudentRole : IUserRole
{
    public string RoleName => "Student";
    public int MaxItemsAllowed => 2;
}