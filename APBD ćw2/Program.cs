using APBD_ćw2;
using APBD_ćw2.Equipments;
using APBD_ćw2.Rent;
using APBD_ćw2.Users;

var service = new RentalService();

service.AddUser(new User("Szymon", "Dev", new StudentRole())); 
service.AddUser(new User("Pan", "Profesor", new EmployeeRole())); 

service.AddEquipment(new Laptop("MacBook Pro", 1, 16)); // ID 1
service.AddEquipment(new Camera("Sony A7", 24, new CameraType())); // ID 2
service.AddEquipment(new Screen("Dell 4K", 27, "3840x2160")); // ID 3

bool running = true;

while (running)
{
    Console.WriteLine("\n--- SYSTEM WYPOŻYCZALNI ---");
    Console.WriteLine("1. Lista sprzętu (wszystko)");
    Console.WriteLine("2. Dostępny sprzęt");
    Console.WriteLine("3. Wypożycz sprzęt");
    Console.WriteLine("4. Zwróć sprzęt");
    Console.WriteLine("5. Aktywne wypożyczenia użytkownika");
    Console.WriteLine("6. Raport i przeterminowane");
    
    Console.WriteLine("0. Wyjście");
    Console.Write("Wybierz opcję: ");

    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            service.ShowAllInventory();
            break;
        case "2":
            service.ShowAvailableItems();
            break;
        case "3":
            // Uproszczone pobieranie ID dla testu
            Console.Write("Podaj ID użytkownika: ");
            int uId = int.Parse(Console.ReadLine());
            Console.Write("Podaj ID sprzętu: ");
            int eId = int.Parse(Console.ReadLine());
            
            var user = service.Users.Find(u => u.ID == uId);
            var item = service.Inventory.Find(i => i.ID == eId);
            
            if (user != null && item != null)
                Console.WriteLine(service.RentItem(user, item));
            else
                Console.WriteLine("Nie znaleziono użytkownika lub sprzętu!");
            break;
        case "4":
            Console.Write("Podaj ID wypożyczenia do zwrotu: ");
            int rId = int.Parse(Console.ReadLine());
            service.ReturnItem(rId);
            break;
        case "5":
            Console.Write("Podaj ID użytkownika: ");
            int searchId = int.Parse(Console.ReadLine());
            service.ShowUserRentals(searchId);
            break;
        case "6":
            service.GenerateReport();
            Console.WriteLine("\nPrzeterminowane:");
            service.ShowOverdueRentals();
            break;
        case "7":
            service.RunDemoScene();
            break;
        case "0":
            running = false;
            break;
        default:
            Console.WriteLine("Niepoprawna opcja.");
            break;
    }
}