using MySqlConnector;

namespace EasyTicket
{
    public class UserData
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public int Age { get; set; }
        public int Subscription { get; set; }

        private static DB_Context dbContext = new DB_Context();

        public static UserData? GetUserByEmail(string email)
        {
            string cleanEmail = email?.Trim() ?? "";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(dbContext.GetConnectionString()))
                {
                    connection.Open();

                    string query = "SELECT UserID, FirstName, LastName, Email, Age, Subscription FROM `User` WHERE Email = @Email";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", cleanEmail);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new UserData
                                {
                                    Id = reader.GetInt32("UserID"),
                                    FirstName = reader.GetString("FirstName"),
                                    LastName = reader.GetString("LastName"),
                                    Email = reader.GetString("Email"),
                                    Age = reader.GetInt32("Age"),
                                    Subscription = reader.GetInt32("Subscription")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user data: {ex.Message}");
            }

            return null;
        }
    }
}