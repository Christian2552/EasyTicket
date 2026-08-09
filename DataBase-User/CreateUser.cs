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

                    //delete all spaces from the email to avoid errors
                    string cleanEmail = User.Email?.Trim() ?? "";


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
                    }

                    // Statement if the email already exists in the database
                    if (emailExists)
                    {
                        Console.WriteLine("Error: This email is already registered!");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        return;
                    }


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
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}