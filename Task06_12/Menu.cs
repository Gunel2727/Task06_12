using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Task06_12
{
    public static class Menu
    {
        public static void Show(Users user)
        {
            var ps = new ProductService();
            var auth = new AuthService();

            List<(Products, int)> cart = new List<(Products, int)>();
            while (true)
            {
                Console.WriteLine("\n--- Menu ---");
                Console.WriteLine("1. Pizzalara bax");
                Console.WriteLine("2. Sifariş ver");
                if (user.Role == Role.Admin)
                {
                    Console.WriteLine("3. Pizzalar");
                    Console.WriteLine("4. İstifadəçilər");
                }
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    ProductMenu(user, ps, cart);
                }
                else if (choice == "2")
                {
                    OrderMenu(cart);
                }
                else if (choice == "4" && user.Role == Role.Admin)
                {
                    ProductCrudMenu(ps);
                }
                else if (choice == "5" && user.Role == Role.Admin)
                {
                    UserCrudMenu(auth);
                }
                else
                {
                    Console.WriteLine("Yanlış seçim, yenidən cəhd edin.");
                }

            }

        }
        private static void ProductMenu(Users user, ProductService ps, List<(Products, int)> cart)
        {

            var products = ps.GetAll();
            while (true)
            {
                Console.WriteLine("\n--- Pizzalar ---");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.Id}, Ad: {product.Name}");
                }
                Console.WriteLine("0 → Çıxış, G → Yenidən göstərin");
                Console.Write("Pizza ID daxil edin: ");
                string input = Console.ReadLine();
                if (input.ToUpper() == "G")
                    continue;
                if (input == "0")
                    break;
                if (!int.TryParse(input, out int pizzaId))
                {
                    Console.WriteLine("Yanlış ID");
                    continue;
                }
                var pizza = products.FirstOrDefault(p => p.Id == pizzaId);
                if (pizza == null)
                {
                    Console.WriteLine("Belə pizza yoxdur");
                    continue;
                }
                Console.WriteLine($"\n{pizza.Name} inqredientləri:");
                for (int i = 0; i < pizza.Ingredients.Count; i++)
                    Console.WriteLine($"{i + 1}. {pizza.Ingredients[i]}");

                Console.Write("S → Səbətə əlavə et, G → Pizzaları yenidən göstər: ");
                string action = Console.ReadLine().ToUpper();
                if (action == "S")
                {
                    Console.Write("Neçə ədəd əlavə etmək istəyirsiniz? ");
                    if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
                    {
                        cart.Add((pizza, count));
                        Console.WriteLine($"{count} ədəd {pizza.Name} səbətə əlavə olundu!");
                    }
                }
                else if (action == "G")
                    continue;
            }
        }
        private static void OrderMenu(List<(Products, int)> cart)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Səbət boşdur!");
                return;
            }
            decimal total = (decimal)cart.Sum(p => p.Item1.Price * p.Item2);
            Console.WriteLine($"Sifarişin ümumi məbləği: {total} AZN");
            Console.Write("Çatdırılma ünvanı: ");
            string address = Console.ReadLine();
            Console.Write("Telefon nömrəsi: ");
            string phone = Console.ReadLine();

            Console.WriteLine($"Sifariş uğurla qəbul olundu! Ünvan: {address}, Telefon: {phone}");
            cart.Clear();
        }
        private static void ProductCrudMenu(ProductService ps)
        {
            while (true)
            {
                Console.WriteLine("\n--- Pizza CRUD ---");
                Console.WriteLine("1. Hamısına bax");
                Console.WriteLine("2. Əlavə et");
                Console.WriteLine("3. Düzəliş et (ID-ə görə)");
                Console.WriteLine("4. Sil (ID-ə görə)");
                Console.WriteLine("0. Çıxış");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    var products = ps.GetAll();
                    foreach (var p in products)
                        Console.WriteLine($"{p.Id}. {p.Name} - {p.Price} AZN");
                }
                else if (choice == "2")
                {
                    Console.Write("Pizza adı: ");
                    string name = Console.ReadLine();
                    Console.Write("Qiymət: ");
                    decimal price = decimal.Parse(Console.ReadLine());
                    Console.Write("İnqredientləri vergül ilə ayırın: ");
                    List<string> ing = Console.ReadLine().Split(',').ToList();
                    ps.Add(new Products { Name = name, Price = (double)price, Ingredients = ing });
                }
                else if (choice == "3")
                {
                    Console.Write("Düzəliş etmək istədiyiniz pizza ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Yeni pizza adı: ");
                    string name = Console.ReadLine();
                    Console.Write("Yeni qiymət: ");
                    decimal price = decimal.Parse(Console.ReadLine());
                    Console.Write("Yeni inqredientlər (vergül ilə): ");
                    List<string> ing = Console.ReadLine().Split(',').ToList();
                    ps.Edit(id, new Products { Name = name, Price = (double)price, Ingredients = ing });
                }
                else if (choice == "4")
                {
                    Console.Write("Silmək istədiyiniz pizza ID: ");
                    int id = int.Parse(Console.ReadLine());
                    ps.Delete(id);
                }
                else if (choice == "0")
                    break;
            }
        }
        private static void UserCrudMenu(AuthService auth)
        {
            while (true)
            {
                Console.WriteLine("\n--- User CRUD ---");
                Console.WriteLine("1. Hamısına bax");
                Console.WriteLine("2. Rol dəyiş");
                Console.WriteLine("3. Sil (ID-ə görə)");
                Console.WriteLine("0. Çıxış");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    var users = auth.GetAllUsers();
                    foreach (var u in users)
                        Console.WriteLine($"{u.Id}. {u.Name} {u.SurName} - {u.Role}");
                }
                else if (choice == "2")
                {
                    Console.Write("User ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Yeni rol (1-User, 2-Admin): ");
                    Role r = (Role)int.Parse(Console.ReadLine());
                    auth.ChangeUserRole(id, r);
                }
                else if (choice == "3")
                {
                    Console.Write("Silinəcək user ID: ");
                    int id = int.Parse(Console.ReadLine());
                    auth.DeleteUser(id);
                }
                else if (choice == "0")
                    break;
            }
        }
    }

}
