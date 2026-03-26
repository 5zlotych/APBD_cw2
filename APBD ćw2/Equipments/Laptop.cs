namespace APBD_ćw2.Equipments;

public class Laptop(string name, int storage, int ram) : Equipment(name)
{
    public int Storage { get; set; } = storage; // in TB
    public int RAM { get; set; } = ram; // in GB

    public override void DisplayDetails()
    {
        Console.WriteLine($"[Laptop] ID: {ID}, Nazwa: {Name}, RAM: {RAM}GB, Dysk: {Storage}TB, Status: {Status}");
    }
}