namespace Task06_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AuthService auth = new AuthService();
            while (true)
            {
                Console.WriteLine("\n--- Login Menu ---");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Qeydiyyat");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Username: ");
                    string u = Console.ReadLine();
                    Console.Write("Password: ");
                    string p = Console.ReadLine();

                    var user = auth.Login(u, p);
                    if (user != null)
                    {
                        Console.WriteLine($"Xoş gəldiniz, {user.Name} {user.SurName}");
                        Menu.Show(user);
                    }
                    else
                    {
                        Console.WriteLine("Login və ya parol yanlışdır");
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Ad: ");
                    string name = Console.ReadLine();
                    Console.Write("Soyad: ");
                    string surname = Console.ReadLine();
                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();

                    if (!ValidationHelper.CheckUserName(username))
                    {
                        Console.WriteLine("Username 3-16 xarakter olmalıdır");
                        continue;
                    }
                    if (!ValidationHelper.CheckPassword(password))
                    {
                        Console.WriteLine("Password 6-16 xarakter, ən azı 1 böyük, 1 kiçik, 1 rəqəm olmalıdır");
                        continue;
                    }

                    auth.Register(new Users
                    {
                        Name = name,
                        SurName = surname,
                        UserName = username,
                        Password = password,
                        Role = Role.User
                    });

                    Console.WriteLine("Qeydiyyat uğurla tamamlandı");
                }
            }
        }
    }
}
