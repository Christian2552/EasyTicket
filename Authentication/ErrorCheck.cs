using System.Threading;
using System.Collections.Generic;
namespace EasyTicket
{
    class AuthenticationCheck
    {
        // the sign ? is used for not warn me about which method will take the data
        private Register? User_InfoReg;
        private Login? User_InfoLog;
        private List<string> RepportReg_List = new List<string>();
        public bool Complete { get; set; }
        //-----------------------------------------------------------------------REGISTER ZONE
        //constructor which takes info from register
        public AuthenticationCheck(Register User_Data)
        {
            User_InfoReg = User_Data;
        }

        public void ErrorCheck_Register()
        {

            // Console.Clear();
            Console.WriteLine("---Loading---");


            if ((User_InfoReg?.FirstName ?? "").Length < 2 || (User_InfoReg?.FirstName ?? "").Length > 15)
            {
                RepportReg_List.Add("Please insert FirstName from 2 to 15 MAX symbols ");
            }
            else
            {
                Thread.Sleep(1000);
                Console.WriteLine("10%");
            }

            if ((User_InfoReg?.LastName ?? "").Length < 2 || (User_InfoReg?.LastName ?? "").Length > 15)
            {
                RepportReg_List.Add("Please insert LastName from 2 to 15 MAX symbols ");
            }
            else
            {
                Thread.Sleep(1000);
                Console.WriteLine("20%");
            }

            if (!(User_InfoReg?.Email ?? "").EndsWith("@gmail.com"))
            {
                RepportReg_List.Add("Email must end with @gmail.com");
            }
            else
            {
                Thread.Sleep(1000);
                Console.WriteLine("30%");
            }

            if (!int.TryParse(User_InfoReg?.Age, out int age) || age < 18 || age > 100)
            {
                RepportReg_List.Add("Please insert valid age  between (18-100)");

            }
            else
            {
                Thread.Sleep(1000);
                Console.WriteLine("40%");
            }

            if ((User_InfoReg?.Password ?? "").Length < 8)
            {
                RepportReg_List.Add("Password should be more than 8 symbols");
            }
            else
            {
                Thread.Sleep(1000);
                Console.WriteLine("50%");
            }

            if ((User_InfoReg?.Password ?? "") != (User_InfoReg?.RepPassword ?? ""))
            {
                RepportReg_List.Add("Password and Repeat Password are not the same");
            }
            else
            {
                Thread.Sleep(1000);
                Console.WriteLine("60%");
            }


            if (RepportReg_List.Count == 0)
            {
                Thread.Sleep(1000);
                Console.WriteLine("70%");
                Complete = true;

                CreateUser createUser = new CreateUser(User_InfoReg!);

            }
            else
            {
                Console.Clear();
                Console.WriteLine("ERROR");
                Console.WriteLine("-----");
                foreach (string i in RepportReg_List)
                {
                    Console.WriteLine($"• {i}");
                }
                Console.WriteLine("-----");
                Complete = false;
                Console.WriteLine("Press Enter to repeat OR BackSpace to get in the Main Menu");
                while (true)
                {
                    ConsoleKey key = Console.ReadKey(intercept: true).Key;

                    if (key == ConsoleKey.Enter)
                    {
                        Complete = false;
                        break;
                    }
                    else if (key == ConsoleKey.Backspace)
                    {
                        Complete = true;
                        break;
                    }
                }
            }




        }

        //-----------------------------------------------------------------------REGISTER ZONE



        //-----------------------------------------------------------------------LOGIN ZONE

        //Constructor which takes info from login
        public AuthenticationCheck(Login User_Data)
        {
            User_InfoLog = User_Data;
        }

        public void ErrorCheck_Login()
        {
            Console.Clear();
            Console.WriteLine("Press Enter to repeat OR BackSpace to get in the Main Menu");
            while (true)
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;

                if (key == ConsoleKey.Enter)
                {
                    Complete = false;
                    break;
                }
                else if (key == ConsoleKey.Backspace)
                {
                    Complete = true;
                    break;
                }
            }
        }
        //-----------------------------------------------------------------------LOGIN ZONE


    }
}