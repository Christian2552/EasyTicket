using MySqlConnector;

namespace EasyTicket
{
    class CreateUser : AuthenticationCheck
    {
        private static DB_Context dbContext = new DB_Context();

        public CreateUser(Register User) : base(User)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(dbContext.GetConnectionString()))
                {
                    connection.Open();

                    // 1. Изчистваме имейла от празните пространства
                    string cleanEmail = User.Email?.Trim() ?? "";

                    // 2. ПРОВЕРКА: Проверяваме бройката съвпадения в базата
                    string checkQuery = "SELECT COUNT(*) FROM `User` WHERE Email = @Email";
                    bool emailExists = false;

                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Email", cleanEmail);

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (count > 0)
                        {
                            emailExists = true;
                        }
                    } // Тук checkCommand се затваря официално!

                    // 3. Ако имейлът съществува - спираме тук
                    if (emailExists)
                    {
                        Console.WriteLine("Error: This email is already registered!");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        return;
                    }

                    // 4. ЗАПИС: Ако имейлът е свободен, правим INSERT
                    string insertQuery = "INSERT INTO `User` (FirstName, LastName, Email, Age, Password) " +
                                         "VALUES (@FirstName, @LastName, @Email, @Age, @Password)";

                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@FirstName", User.FirstName);
                        insertCommand.Parameters.AddWithValue("@LastName", User.LastName);
                        insertCommand.Parameters.AddWithValue("@Email", cleanEmail);
                        insertCommand.Parameters.AddWithValue("@Age", User.Age);
                        insertCommand.Parameters.AddWithValue("@Password", User.Password);

                        insertCommand.ExecuteNonQuery();
                    }

                    Console.WriteLine("User created successfully!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(); // Задържа екрана, за да потвърдиш успещния запис!
                }
            }
            catch (Exception ex) // Прихваща ВСИЧКИ грешки
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}