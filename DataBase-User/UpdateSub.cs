using MySqlConnector;

namespace EasyTicket
{
    class UpdateSub
    {
        private static DB_Context dbContext = new DB_Context();

        public static void UpdateSubscription(int userId, int newSubscription)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(dbContext.GetConnectionString()))
                {
                    connection.Open();

                    string updateQuery = "UPDATE `User` SET Subscription = @Subscription WHERE UserID = @UserID";

                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Subscription", newSubscription);
                        updateCommand.Parameters.AddWithValue("@UserID", userId);

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Subscription updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("No user found with the provided ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating subscription: {ex.Message}");
            }
        }
    }
}