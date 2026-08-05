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

            // Запис в базата данни
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO Event (UserId, Title, Description, EventDate, Location, TicketPrice, MaxGuests, CurrentGuests)
                    VALUES (@UserId, @Title, @Description, @EventDate, @Location, @TicketPrice, @MaxGuests, 0)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@EventDate", eventDate);
                    cmd.Parameters.AddWithValue("@Location", location);
                    cmd.Parameters.AddWithValue("@TicketPrice", ticketPrice);
                    cmd.Parameters.AddWithValue("@MaxGuests", maxGuests);

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("\nSuccess! Your event has been created successfully!");
        }
    }
}