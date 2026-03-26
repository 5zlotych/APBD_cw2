namespace APBD_ćw2;

public abstract class Equipment
{ 
    private static int IDcounter = 1;
    public int ID { get; set; }
    public string Name { get; set; }
    public EquipmentStatus Status { get; set; }
    
    protected  Equipment(string name)
    {
        ID = IDcounter++;
        Name = name;
        Status = EquipmentStatus.Available;
    }
    public abstract void DisplayDetails();
}