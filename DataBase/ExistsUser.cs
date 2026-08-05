using MySqlConnector;

namespace EasyTicket
{
    class ExistsUser : AuthenticationCheck
    {
        private static DB_Context dbContext = new DB_Context();

        public bool IsSuccess { get; private set; }

        public ExistsUser(Login User) : base(User)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(dbContext.GetConnectionString()))
                {
                    connection.Open();

                    string cleanEmail = User.Email?.Trim() ?? "";

                    string checkQuery = "SELECT COUNT(*) FROM `User` WHERE Email = @Email AND Password = @Password";

                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Email", cleanEmail);
                        checkCommand.Parameters.AddWithValue("@Password", User.Password ?? "");

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        //checker if the user exists in the database
                        IsSuccess = count > 0;
                    }
                }


                if (IsSuccess)
                {
                    Console.WriteLine("Login successful! Welcome back!");
                }
                else
                {
                    Console.WriteLine("Invalid email or password!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}