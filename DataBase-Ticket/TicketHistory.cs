using MySql.Data.MySqlClient;
using System;

namespace EasyTicket
{
    public class TicketHistory
    {
        private static string connectionString = "Server=localhost;Port=3306;Database=easy_ticket;User=root;Password=12345;";

        public static void DisplayUserTickets(int userId)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("               MY PURCHASED TICKETS               ");
            Console.WriteLine("==================================================\n");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        t.TicketId,
                        t.PurchaseDate,
                        e.Title,
                        e.EventDate,
                        e.Location,
                        e.TicketPrice
                    FROM Ticket t
                    INNER JOIN Event e ON t.EventId = e.EventId
                    WHERE t.UserId = @UserId
                    ORDER BY t.PurchaseDate DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool hasTickets = false;

                        while (reader.Read())
                        {
                            hasTickets = true;
                            int ticketId = Convert.ToInt32(reader["TicketId"]);
                            string title = reader["Title"].ToString() ?? "";
                            DateTime eventDate = Convert.ToDateTime(reader["EventDate"]);
                            string location = reader["Location"].ToString() ?? "";
                            decimal price = Convert.ToDecimal(reader["TicketPrice"]);
                            DateTime purchaseDate = Convert.ToDateTime(reader["PurchaseDate"]);

                            Console.WriteLine($"  Ticket #{ticketId} | Event: {title.ToUpper()}");
                            Console.WriteLine($"• Event Date : {eventDate:dd/MM/yyyy HH:mm}");
                            Console.WriteLine($"• Location   : {location}");
                            Console.WriteLine($"• Price Paid : {(price == 0 ? "FREE" : $"{price:F2} BGN")}");
                            Console.WriteLine($"• Bought On  : {purchaseDate:dd/MM/yyyy HH:mm}");
                            Console.WriteLine("--------------------------------------------------");
                        }

                        if (!hasTickets)
                        {
                            Console.WriteLine("You haven't purchased any tickets yet.");
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("\n• Enter Ticket ID to cancel/delete ticket");
            Console.WriteLine("• Press 0 to go back");
            Console.Write("\nYour choice: ");

            if (int.TryParse(Console.ReadLine(), out int selectedTicketId) && selectedTicketId > 0)
            {
                Console.WriteLine("\nAre you sure you want to cancel this ticket?");
                Console.WriteLine("• Press 1 for YES");
                Console.WriteLine("• Press 2 for NO");
                Console.Write("\nYour choice: ");

                if (Console.ReadLine() == "1")
                {
                    if (CancelTicket(selectedTicketId, userId))
                    {
                        Console.WriteLine("\n🎉 Ticket cancelled successfully!");
                    }
                    else
                    {
                        Console.WriteLine("\n Failed to cancel ticket! Make sure the Ticket ID is correct and belongs to you.");
                    }
                }
            }
        }

        private static bool CancelTicket(int ticketId, int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string findEventQuery = "SELECT EventId FROM Ticket WHERE TicketId = @TicketId AND UserId = @UserId";
                        int eventId = 0;

                        using (MySqlCommand findCmd = new MySqlCommand(findEventQuery, conn, transaction))
                        {
                            findCmd.Parameters.AddWithValue("@TicketId", ticketId);
                            findCmd.Parameters.AddWithValue("@UserId", userId);

                            object? result = findCmd.ExecuteScalar();
                            if (result == null)
                            {
                                transaction.Rollback();
                                return false;
                            }
                            eventId = Convert.ToInt32(result);
                        }

                        string deleteTicketQuery = "DELETE FROM Ticket WHERE TicketId = @TicketId AND UserId = @UserId";
                        using (MySqlCommand deleteCmd = new MySqlCommand(deleteTicketQuery, conn, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@TicketId", ticketId);
                            deleteCmd.Parameters.AddWithValue("@UserId", userId);
                            deleteCmd.ExecuteNonQuery();
                        }

                        string updateEventQuery = "UPDATE Event SET CurrentGuests = CurrentGuests - 1 WHERE EventId = @EventId AND CurrentGuests > 0";
                        using (MySqlCommand updateCmd = new MySqlCommand(updateEventQuery, conn, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@EventId", eventId);
                            updateCmd.ExecuteNonQuery();
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