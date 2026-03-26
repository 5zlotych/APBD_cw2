namespace APBD_ćw2.Equipments;

public class Camera(string name, int megaPixels, CameraType type) : Equipment(name)
{
    public int MegaPixels { get; set; } = megaPixels;
    public CameraType Type { get; set; } = type;

    public override void DisplayDetails()
    {
        Console.WriteLine($"[Aparat] ID: {ID}, Nazwa: {Name}, Rozdzielczość: {MegaPixels}MP, Typ: {Type}, Status: {Status}");
    }
}