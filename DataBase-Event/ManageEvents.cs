using MySql.Data.MySqlClient;
using System;

namespace EasyTicket
{
    public class ManageEvents
    {
        private static string connectionString = "Server=localhost;Port=3306;Database=easy_ticket;User=root;Password=12345;";

        public static void DisplayMyCreatedEvents(int userId)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("               MY CREATED EVENTS                  ");
            Console.WriteLine("==================================================\n");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT EventId, Title, EventDate, Location, MaxGuests, CurrentGuests 
                    FROM Event 
                    WHERE UserId = @UserId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool hasEvents = false;

                        while (reader.Read())
                        {
                            hasEvents = true;
                            int eventId = Convert.ToInt32(reader["EventId"]);
                            string title = reader["Title"].ToString() ?? "";
                            DateTime date = Convert.ToDateTime(reader["EventDate"]);
                            string location = reader["Location"].ToString() ?? "";
                            int maxGuests = Convert.ToInt32(reader["MaxGuests"]);
                            int currentGuests = Convert.ToInt32(reader["CurrentGuests"]);

                            Console.WriteLine($"[ID: {eventId}] {title.ToUpper()}");
                            Console.WriteLine($"• Date     : {date:dd/MM/yyyy HH:mm}");
                            Console.WriteLine($"• Location : {location}");
                            Console.WriteLine($"• Attendees: {currentGuests}/{maxGuests} (1 is Creator)");
                            Console.WriteLine("--------------------------------------------------");
                        }

                        if (!hasEvents)
                        {
                            Console.WriteLine("You have not created any events yet.");
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("\n• Enter Event ID to DELETE event");
            Console.WriteLine("• Press 0 to go back");
            Console.Write("\nYour choice: ");

            if (int.TryParse(Console.ReadLine(), out int selectedEventId) && selectedEventId > 0)
            {
                DeleteEvent(selectedEventId, userId);
            }
        }

        private static void DeleteEvent(int eventId, int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // 1. Проверка дали събитието съществува и колко гости има
                string checkQuery = "SELECT CurrentGuests FROM Event WHERE EventId = @EventId AND UserId = @UserId";
                int currentGuests = 0;

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@EventId", eventId);
                    checkCmd.Parameters.AddWithValue("@UserId", userId);

                    object? result = checkCmd.ExecuteScalar();
                    if (result == null)
                    {
                        Console.WriteLine("\n❌ Event not found or you are not the creator!");
                        return;
                    }

                    currentGuests = Convert.ToInt32(result);
                }

                // Ако има други хора освен създателя (CurrentGuests > 1)
                if (currentGuests > 1)
                {
                    Console.WriteLine("\n❌ Cannot delete event! Other users have already booked tickets for it.");
                    return;
                }

                Console.WriteLine("\nAre you sure you want to delete this event? (This action cannot be undone)");
                Console.WriteLine("• Press 1 for YES");
                Console.WriteLine("• Press 2 for NO");
                Console.Write("\nYour choice: ");

                if (Console.ReadLine() != "1")
                {
                    Console.WriteLine("\nDeletion cancelled.");
                    return;
                }

                // 2. Изтриване с транзакция (изтрива билета на създателя и самото събитие)
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string deleteTicketsQuery = "DELETE FROM Ticket WHERE EventId = @EventId";
                        using (MySqlCommand delTicketsCmd = new MySqlCommand(deleteTicketsQuery, conn, transaction))
                        {
                            delTicketsCmd.Parameters.AddWithValue("@EventId", eventId);
                            delTicketsCmd.ExecuteNonQuery();
                        }

                        string deleteEventQuery = "DELETE FROM Event WHERE EventId = @EventId AND UserId = @UserId";
                        using (MySqlCommand delEventCmd = new MySqlCommand(deleteEventQuery, conn, transaction))
                        {
                            delEventCmd.Parameters.AddWithValue("@EventId", eventId);
                            delEventCmd.Parameters.AddWithValue("@UserId", userId);
                            delEventCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        Console.WriteLine("\n🎉 Event deleted successfully!");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"\n❌ Failed to delete event: {ex.Message}");
                    }
                }
            }
        }
    }
}