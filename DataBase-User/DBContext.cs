namespace EasyTicket
{
    public class DB_Context
    {
        //Here you can change the connection string to your own database connection string
        private string ConnectionString = "Server=localhost;Port=3306;Database=easy_ticket;User=root;Password=12345;";

        public string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}