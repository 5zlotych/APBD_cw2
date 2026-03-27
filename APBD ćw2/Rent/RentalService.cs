using APBD_ćw2.Equipments;
using APBD_ćw2.Users;

namespace APBD_ćw2.Rent;

public class RentalService
{
    public List<User> Users { get; set; } = new();
    public List<Equipment> Inventory { get; set; } = new();
    public List<Rental> Rentals { get; set; } = new();
    
    
    public void AddUser(User user) => Users.Add(user);
    
    public void AddEquipment(Equipment item) => Inventory.Add(item);
    
    public void ShowAllInventory() => Inventory.ForEach(i => i.DisplayDetails());
    
    public void ShowAvailableItems()
    {
        var available = Inventory.Where(i => i.Status == EquipmentStatus.Available).ToList();
        available.ForEach(i => i.DisplayDetails());
    }
    
    public string RentItem(User user, Equipment item)
    {
        int activeCount = Rentals.Count(r => r.Borrower.ID == user.ID && r.ReturnDate == null);
        
        if (activeCount >= user.MaxItemsAllowed)
            return $"Błąd: {user.FirstName} {user.LastName} osiągnął limit {user.MaxItemsAllowed} aktywnych wypożyczeń.";

        if (item.Status != EquipmentStatus.Available)
            return "Błąd: Sprzęt jest niedostępny.";

        var newRental = new Rental(user, item);
        Rentals.Add(newRental);
        item.Status = EquipmentStatus.Rented;
        return $"Sukces: Wypożyczono {item.Name} użytkownikowi {user.FirstName} {user.LastName}.";
    }

    public void ReturnItem(int rentalId)
    {
        var rental = Rentals.FirstOrDefault(r => r.ID == rentalId);
        if (rental != null)
        {
            rental.MarkAsReturned();
            Console.WriteLine($"Sukces! Zwrócono: {rental.Item.Name}.");
            if (rental.PenaltyAmount > 0)
            {
                Console.WriteLine($"Naliczono karę za opóźnienie: {rental.PenaltyAmount} PLN.");
            }
        }
        else
        {
            Console.WriteLine($"Błąd: Wypożyczenie o ID {rentalId} nie istnieje w systemie.");
        }
    }
    public void MarkAsDamaged(int equipmentId)
    {
        var item = Inventory.FirstOrDefault(i => i.ID == equipmentId);
        if (item != null) item.Status = EquipmentStatus.InRepair;
    }
    
    public void ShowUserRentals(int userId)
    {
        var userRentals = Rentals.Where(r => r.Borrower.ID == userId && r.ReturnDate == null).ToList();
        userRentals.ForEach(r => r.DisplayRentalInfo());
    }
    
    public void ShowOverdueRentals()
    {
        var overdue = Rentals.Where(r => r.ReturnDate == null && DateTime.Now > r.DueDate).ToList();
        overdue.ForEach(r => r.DisplayRentalInfo());
    }
    
    public void GenerateReport()
    {
        Console.WriteLine("\n================ RAPORT STANU SYSTEMU ================");
    
        
        int totalUsers = Users.Count;
        int studentCount = Users.Count(u => u.Role is StudentRole);
        int employeeCount = Users.Count(u => u.Role is EmployeeRole);

        Console.WriteLine($"Użytkownicy ogółem: {totalUsers}");
        Console.WriteLine($"  - Studenci:    {studentCount}");
        Console.WriteLine($"  - Pracownicy:  {employeeCount}");
        Console.WriteLine("------------------------------------------------------");
        
        int totalItems = Inventory.Count;
        int availableItems = Inventory.Count(i => i.Status == EquipmentStatus.Available);
        int rentedItems = Inventory.Count(i => i.Status == EquipmentStatus.Rented);
        int inRepairItems = Inventory.Count(i => i.Status == EquipmentStatus.InRepair);

        Console.WriteLine($"Sprzęt ogółem: {totalItems}");
        Console.WriteLine($"  - Dostępny:    {availableItems}");
        Console.WriteLine($"  - Wypożyczony: {rentedItems}");
        Console.WriteLine($"  - W serwisie:  {inRepairItems}");
        Console.WriteLine("------------------------------------------------------");

        
        int activeRentals = Rentals.Count(r => r.ReturnDate == null);
        decimal totalPenalties = Rentals.Sum(r => r.PenaltyAmount);

        Console.WriteLine($"Aktywne wypożyczenia: {activeRentals}");
        Console.WriteLine($"Łączna suma naliczonych kar: {totalPenalties} PLN");
        Console.WriteLine("======================================================\n");
    }
    public void RunDemoScene()
    {
        Console.WriteLine("=== URUCHAMIANIE SCENARIUSZA DEMONSTRACYJNEGO ===");
        
        var student = new User("Jan", "Kowalski", new StudentRole());
        var employee = new User("Anna", "Nowak", new EmployeeRole());
        var laptop = new Laptop("MacBook Pro", 1, 16);
        var camera = new Camera("Sony A1", 50, new CameraType());
        var monitor = new Screen("Dell 32", 32, "4K");

        AddUser(student);
        AddUser(employee);
        AddEquipment(laptop);
        AddEquipment(camera);
        AddEquipment(monitor);
        
        Console.WriteLine(RentItem(student, laptop));
        Console.WriteLine(RentItem(student, camera));
        Console.WriteLine("Próba wypożyczenia 3. przedmiotu dla studenta:");
        Console.WriteLine(RentItem(student, monitor)); 
        
        Console.WriteLine("Próba wypożyczenia tego samego laptopa komuś innemu:");
        Console.WriteLine(RentItem(employee, laptop));
        
        var rentalToReturn = Rentals.FirstOrDefault(r => r.Borrower.ID == student.ID);
        if (rentalToReturn != null)
        {
            ReturnItem(rentalToReturn.ID);
        }
        
        var lateRental = new Rental(employee, monitor);
        lateRental.DueDate = DateTime.Now.AddDays(-5);
        Rentals.Add(lateRental);
    
        Console.WriteLine("\nSymulacja spóźnionego zwrotu o 5 dni:");
        ReturnItem(lateRental.ID); 
        
        GenerateReport();
    }
}