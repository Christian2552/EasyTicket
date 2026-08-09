using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyTicket
{
    public class EventCatalog
    {
        private static string connectionString = "Server=localhost;Port=3306;Database=easy_ticket;User=root;Password=12345;";

        public static List<EventData> GetAllEvents()
        {
            List<EventData> events = new List<EventData>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        e.EventId, e.UserId, e.Title, e.Description, 
                        e.EventDate, e.Location, e.TicketPrice, 
                        e.MaxGuests, e.CurrentGuests,
                        CONCAT(u.FirstName, ' ', u.LastName) AS OrganizerName
                    FROM Event e
                    INNER JOIN user u ON e.UserId = u.UserID";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new EventData
                        {
                            EventId = Convert.ToInt32(reader["EventId"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            OrganizerName = reader["OrganizerName"].ToString() ?? "Unknown",
                            Title = reader["Title"].ToString() ?? "",
                            Description = reader["Description"].ToString() ?? "",
                            EventDate = Convert.ToDateTime(reader["EventDate"]),
                            Location = reader["Location"].ToString() ?? "",
                            TicketPrice = Convert.ToDecimal(reader["TicketPrice"]),
                            MaxGuests = Convert.ToInt32(reader["MaxGuests"]),
                            CurrentGuests = Convert.ToInt32(reader["CurrentGuests"])
                        });
                    }
                }
            }
            return events;
        }

        public static void DisplayAndSelectEvent(UserData? currentUser)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("                 EVENT CATALOG                    ");
            Console.WriteLine("==================================================\n");

            List<EventData> events = GetAllEvents();

            if (events.Count == 0)
            {
                Console.WriteLine("No active events found at the moment.");
                return;
            }

            foreach (var ev in events)
            {
                Console.WriteLine($"[ID: {ev.EventId}] {ev.Title.ToUpper()}");
                Console.WriteLine($"• Organizer  : {ev.OrganizerName}");
                Console.WriteLine($"• Date & Time: {ev.EventDate:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"• Location   : {ev.Location}");
                Console.WriteLine($"• Price      : {(ev.TicketPrice == 0 ? "FREE" : $"{ev.TicketPrice:F2} BGN")}");
                Console.WriteLine($"• Capacity   : {ev.CurrentGuests}/{ev.MaxGuests} seats taken");
                Console.WriteLine("--------------------------------------------------");
            }

            Console.WriteLine("\n• Enter Event ID to select and book");
            Console.WriteLine("• Press 0 to go back");
            Console.Write("\nYour choice: ");

            if (!int.TryParse(Console.ReadLine(), out int selectedId) || selectedId == 0)
            {
                return;
            }

            var selectedEvent = events.FirstOrDefault(e => e.EventId == selectedId);

            if (selectedEvent == null)
            {
                Console.WriteLine("\nInvalid Event ID!");
                return;
            }

            // Изглед за избраното събитие
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine($"           EVENT: {selectedEvent.Title.ToUpper()}");
            Console.WriteLine("==================================================");
            Console.WriteLine($"• Organizer  : {selectedEvent.OrganizerName}");
            Console.WriteLine($"• Date & Time: {selectedEvent.EventDate:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"• Location   : {selectedEvent.Location}");
            Console.WriteLine($"• Price      : {(selectedEvent.TicketPrice == 0 ? "FREE" : $"{selectedEvent.TicketPrice:F2} BGN")}");
            Console.WriteLine($"• Capacity   : {selectedEvent.CurrentGuests}/{selectedEvent.MaxGuests} seats taken");
            Console.WriteLine($"• Description: {selectedEvent.Description}");
            Console.WriteLine("--------------------------------------------------\n");

            if (currentUser == null)
            {
                Console.WriteLine("*Guests cannot book tickets. Please register or log in first.*");
                return;
            }

            if (selectedEvent.CurrentGuests >= selectedEvent.MaxGuests)
            {
                Console.WriteLine("*Sorry, this event is FULL! No available seats.*");
                return;
            }

            Console.WriteLine("Would you like to book a ticket?");
            Console.WriteLine("• Press 1 for YES");
            Console.WriteLine("• Press 2 for NO");
            Console.Write("\nYour choice: ");

            if (Console.ReadLine() == "1")
            {
                if (BookTicket(selectedEvent.EventId, currentUser.Id))
                {
                    Console.WriteLine("\n Success! You have booked a ticket for this event.");
                }
                else
                {
                    Console.WriteLine("\n Booking failed. Please try again.");
                }
            }
        }

        private static bool BookTicket(int eventId, int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Транзакция: ако една от двете операции се счупи, базата връща първоначалното състояние
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Увеличаваме броя гости в събитието
                        string updateQuery = @"
                    UPDATE Event 
                    SET CurrentGuests = CurrentGuests + 1 
                    WHERE EventId = @EventId AND CurrentGuests < MaxGuests";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@EventId", eventId);
                            int rows = updateCmd.ExecuteNonQuery();

                            if (rows == 0)
                            {
                                transaction.Rollback();
                                return false; // Няма свободни места
                            }
                        }

                        // 2. Записваме купения билет в таблицата Ticket
                        string insertTicketQuery = @"
                    INSERT INTO Ticket (UserId, EventId, PurchaseDate) 
                    VALUES (@UserId, @EventId, @PurchaseDate)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertTicketQuery, conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@UserId", userId);
                            insertCmd.Parameters.AddWithValue("@EventId", eventId);
                            insertCmd.Parameters.AddWithValue("@PurchaseDate", DateTime.Now);
                            insertCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}