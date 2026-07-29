namespace EasyTicket
{
    class Login : User_Authentication
    {
        //Method Authentication_Login is for taking the data of our new user

        public Login Authentication_Login()
        {
            Console.Write("Email: ");
            Email = Console.ReadLine()!;
            Console.Write("Password: ");
            Password = Console.ReadLine()!;

            return this;
        }
    }
}