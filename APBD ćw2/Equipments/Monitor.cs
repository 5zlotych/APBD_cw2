namespace APBD_ćw2.Equipments;

public class Monitor(string name, int size, string resolution) : Equipment(name)
{
    public int Size { get; set; } = size; // in inches
    public string Resolution { get; set; } = resolution;

    public override void DisplayDetails()
    {
        Console.WriteLine($"[Monitor] ID: {ID}, Nazwa: {Name}, Przekątna: {Size}\", Rozdzielczość: {Resolution}, Status: {Status}");
    }
}