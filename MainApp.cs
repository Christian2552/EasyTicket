namespace EasyTicket
{
    class MainClass
    {
        static void Main()
        {
            //______________________________________________________________________________
            // THIS CODE IS FOR TEST PURPOSES
            // Register User = new Register();

            // User.Authentication_Register();

            // List<string> user = User.info_person;

            // foreach (string i in user)
            // {
            //     Console.WriteLine(i);
            // }
            // ________________________________________________________________________________


            Login User = new Login();

            User.Authentication_Login();

            List<string> user = User.info_person;

            foreach (string i in user)
            {
                Console.WriteLine(i);
            }
        }
    }
}