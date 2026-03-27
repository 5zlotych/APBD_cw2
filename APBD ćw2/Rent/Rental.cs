using APBD_ćw2.Equipments;
using APBD_ćw2.Users;

namespace APBD_ćw2.Rent;

public class Rental
{
    private static int IDcounter = 1;
    public int ID { get; private set; } = IDcounter++;
    public User Borrower { get; set; }
    public Equipment Item { get; set; }
    public DateTime RentalDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal PenaltyAmount { get; set; } = 0;
    private static decimal PenaltyPerDay { get; set; } = 5;
    public Rental(User user, Equipment equipment, int daysToReturn = 7)
    {
        Borrower = user;
        Item = equipment;
        DueDate = DateTime.Now.AddDays(daysToReturn);
        Item.Status = EquipmentStatus.Rented;
    }

    public void DisplayRentalInfo()
    {
        string returnStatus = ReturnDate.HasValue 
            ? $"Oddano: {ReturnDate.Value:yyyy-MM-dd}" 
            : "W trakcie wypożyczenia";

        Console.WriteLine($"[Wypożyczenie #{ID}] Użytkownik: {Borrower.FirstName} {Borrower.LastName}, " +
                          $"Sprzęt: {Item.Name}, Termin: {DueDate:yyyy-MM-dd}, {returnStatus}, Kara: {PenaltyAmount} PLN");
    }
    public void MarkAsReturned()
    {
        ReturnDate = DateTime.Now;
        Item.Status = EquipmentStatus.Available;
        if (ReturnDate.Value.Date > DueDate.Date)
        {
            int overdueDays = (ReturnDate.Value.Date - DueDate.Date).Days;
            PenaltyAmount = overdueDays * PenaltyPerDay; 
        }
    }
}