using MySql.Data.MySqlClient;
using System;

namespace EasyTicket
{
    public class CreateEvent
    {
        private static string connectionString = "Server=localhost;Port=3306;Database=easy_ticket;User=root;Password=12345;";

        public static void AddNewEvent(int userId)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("               CREATE NEW EVENT                   ");
            Console.WriteLine("==================================================\n");

            Console.Write("Enter Event Title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Enter Description: ");
            string description = Console.ReadLine() ?? "";

            Console.Write("Enter Date and Time (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime eventDate))
            {
                Console.WriteLine("\nInvalid date format! Event creation cancelled.");
                return;
            }

            Console.Write("Enter Location: ");
            string location = Console.ReadLine() ?? "";

            Console.Write("Enter Ticket Price (BGN): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal ticketPrice) || ticketPrice < 0)
            {
                Console.WriteLine("\nInvalid price! Event creation cancelled.");
                return;
            }

            Console.Write("Enter Maximum Capacity (Guests): ");
            if (!int.TryParse(Console.ReadLine(), out int maxGuests) || maxGuests <= 0)
            {
                Console.WriteLine("\nInvalid capacity! Event creation cancelled.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string insertEventQuery = @"
                            INSERT INTO Event (UserId, Title, Description, EventDate, Location, TicketPrice, MaxGuests, CurrentGuests)
                            VALUES (@UserId, @Title, @Description, @EventDate, @Location, @TicketPrice, @MaxGuests, 1);
                            SELECT LAST_INSERT_ID();";

                        int newEventId;
                        using (MySqlCommand cmd = new MySqlCommand(insertEventQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@Title", title);
                            cmd.Parameters.AddWithValue("@Description", description);
                            cmd.Parameters.AddWithValue("@EventDate", eventDate);
                            cmd.Parameters.AddWithValue("@Location", location);
                            cmd.Parameters.AddWithValue("@TicketPrice", ticketPrice);
                            cmd.Parameters.AddWithValue("@MaxGuests", maxGuests);

                            newEventId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        string insertTicketQuery = @"
                            INSERT INTO Ticket (UserId, EventId, PurchaseDate)
                            VALUES (@UserId, @EventId, @PurchaseDate)";

                        using (MySqlCommand ticketCmd = new MySqlCommand(insertTicketQuery, conn, transaction))
                        {
                            ticketCmd.Parameters.AddWithValue("@UserId", userId);
                            ticketCmd.Parameters.AddWithValue("@EventId", newEventId);
                            ticketCmd.Parameters.AddWithValue("@PurchaseDate", DateTime.Now);
                            ticketCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        Console.WriteLine("\n Success! Your event has been created and your organizer ticket was assigned!");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"\n Failed to create event: {ex.Message}");
                    }
                }
            }
        }
    }
}