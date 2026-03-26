namespace APBD_ćw2.Users;

public class User(string firstName, string lastName, IUserRole role)
{
    private static int IDcounter = 1;
    
    public int ID { get; private set; } = IDcounter++;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public IUserRole Role { get; set; } = role;
    public int MaxItemsAllowed => Role.MaxItemsAllowed;
    
    public void DisplayInfo()
    {
        Console.WriteLine($"[{Role.RoleName}] ID: {ID}, {FirstName} {LastName}, Limit wypożyczeń: {MaxItemsAllowed}");
    }
}