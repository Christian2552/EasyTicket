namespace EasyTicket
{
    class MainClass
    {
        public static int num;

        static void Main()
        {
            Console.Clear();
            Console.WriteLine("=============================");
            Console.WriteLine("      EASY TICKET MENU       ");
            Console.WriteLine("=============================");

            Console.WriteLine("• Press 1 for Register");
            Console.WriteLine("• Press 2 for Login");
            Console.WriteLine("• Press 3 to continue as Guest");
            Console.WriteLine("");
            Console.Write("Your num: ");
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("INVALID, PLEASE INSERT NUMBER!!!");
                Console.Write("User number: ");
            }

            switch (num)
            {
                case 1:

                    Register obj = new Register();
                    obj.Authentication_Register();

                    break;

                case 2:

                    Login User = new Login();
                    User.Authentication_Login();

                    break;
                case 3:
                    Console.WriteLine("Welcome to Easy Ticket");

                    break;
            }
        }
    }
}