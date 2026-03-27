# System Wypożyczalni Sprzętu (APBD ĆW 2)

## Opis projektu
Aplikacja konsolowa w języku C# symulująca działanie wypożyczalni sprzętu elektronicznego (laptopy, aparaty, monitory). System obsługuje różne typy użytkowników (studenci, pracownicy) z odrębnymi limitami wypożyczeń oraz automatycznie nalicza kary za przeterminowane zwroty.

## Podział klas i plików
Projekt został podzielony na logiczne foldery (`Users`, `Equipments`, `Rent`), co porządkuje kod i ułatwia nawigację. Zamiast wrzucać wszystko do jednego pliku, wydzieliłem:
1.  **Modele danych:** Klasy reprezentujące konkretne obiekty (`User`, `Equipment`, `Rental`). Przechowują one swoje dane i wykonują podstawowe operacje tylko na sobie.
2.  **Klasę zarządzającą (`RentalService`):** Działa jako główne centrum operacyjne. Przechowuje listy danych i pilnuje zasad wypożyczeń (np. sprawdzanie limitów).
3.  **Punkt wejścia (`Program.cs`):** Zawiera tylko kod odpowiedzialny za uruchomienie scenariusza demonstracyjnego i wyświetlanie wyników w konsoli. Nie ma tu logiki decyzyjnej.

## Odpowiedzialność klas
Każda klasa ma jedno, ściśle określone zadanie:
* Klasy sprzętowe odpowiadają tylko za definicję konkretnego typu urządzenia.
* Klasa wypożyczenia (`Rental`) odpowiada wyłącznie za własny stan. To wewnątrz niej znajduje się metoda zamykająca wypożyczenie, która samodzielnie wylicza karę za spóźnienie na podstawie przypisanych do niej dat.
* Klasa usługi (`RentalService`) nie wnika w sposób naliczania kar. Jej odpowiedzialnością jest zarządzanie inwentarzem i weryfikacja reguł nakładanych na użytkowników przed dodaniem wypożyczenia do listy.

## Kohezja i coupling

**Wysoka kohezja:**
Logika jest skupiona dokładnie tam, gdzie znajdują się dane, na których operuje. Przykładem jest klasa wypożyczenia, ponieważ to ona posiada informacje o terminie zwrotu i faktycznej dacie oddania sprzętu, to właśnie ona odpowiada za obliczenie ewentualnej kary. Metody i dane są ze sobą silnie i logicznie związane wewnątrz jednej klasy.

**Niski coupling:**
Zamiast tworzyć sztywne drzewo dziedziczenia dla użytkowników, zastosowałem wzorzec kompozycji. Użytkownik posiada właściwość interfejsową określającą jego rolę. Dzięki temu zasady biznesowe (takie jak maksymalna liczba wynajętych przedmiotów) są wstrzykiwane do obiektu. Zmniejsza to coupling między klasami. Dodanie nowej roli w przyszłości nie będzie wymagało modyfikacji samej klasy użytkownika.
