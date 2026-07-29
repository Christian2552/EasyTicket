using System.Threading;
namespace EasyTicket
{
    class AuthenticationCheck
    {
        // the sign ? is used for not warn me about which method will take the data
        private Register? User_InfoReg;
        private Login? User_InfoLog;


        public AuthenticationCheck(Register User_Data)
        {
            string FirstName = User_Data.FirstName;
            string LastName = User_Data.LastName;
            string Email = User_Data.Email;
            string Age = User_Data.Age;
            string Password = User_Data.Password;
            string RepPassword = User_Data.RepPassword;



            User_InfoReg = User_Data;
        }

        public AuthenticationCheck(Login User_Data)
        {
            User_InfoLog = User_Data;
        }

        public void LoadingTxt()
        {
            Console.WriteLine("---Loading---");
            Thread.Sleep(3000);
            Console.WriteLine("10%");
        }
    }
}