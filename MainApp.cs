namespace EasyTicket
{
    class MainClass
    {
        public static int Num;

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
            while (!int.TryParse(Console.ReadLine(), out Num) || Num < 1 || Num > 3)
            {
                Console.WriteLine("INVALID, PLEASE INSERT VALID NUMBER!!!");
                Console.Write("User number: ");
            }

            switch (Num)
            {
                case 1:

                    Register User_Register = new Register();
                    User_Register.Authentication_Register();
                    //AuthenticationCheck is checkng if everything is correct of user data
                    AuthenticationCheck Info_Check = new AuthenticationCheck(User_Register);
                    Info_Check.LoadingTxt();

                    break;

                case 2:

                    Login User_Login = new Login();
                    User_Login.Authentication_Login();
                    //AuthenticationCheck is checkng if everything is correct of user data
                    AuthenticationCheck Info_Check1 = new AuthenticationCheck(User_Login);
                    Info_Check1.LoadingTxt();

                    break;
                case 3:
                    Console.WriteLine("Welcome to Easy Ticket");

                    break;
            }
        }
    }
}