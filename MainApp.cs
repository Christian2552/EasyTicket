using Microsoft.VisualBasic;

namespace EasyTicket
{
    class MainClass
    {
        public static int Num;

        public static UserData? currentUser;
        public static bool isAuthenticated = false;

        static void Main()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=============================");
                Console.WriteLine("      EASY TICKET MENU       ");
                Console.WriteLine("=============================");
                Console.WriteLine("• Press 1 for Register");
                Console.WriteLine("• Press 2 for Login");
                Console.WriteLine("• Press 3 to continue as Guest");
                Console.WriteLine("• Press 4 to Exit");
                Console.WriteLine("");
                Console.Write("Your num: ");

                while (!int.TryParse(Console.ReadLine(), out Num) || Num < 1 || Num > 4)
                {
                    Console.WriteLine("INVALID, PLEASE INSERT VALID NUMBER!!!");
                    Console.Write("User number: ");
                }

                switch (Num)
                {
                    case 1:
                        {
                            Register userRegister = new Register();
                            bool complete = false;

                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Register:");
                                Console.WriteLine("---------");

                                userRegister.Authentication_Register();
                                AuthenticationCheck infoCheck = new AuthenticationCheck(userRegister);
                                infoCheck.ErrorCheck_Register();
                                complete = infoCheck.Complete;

                            } while (!complete);

                            CreateUser dbSave = new CreateUser(userRegister);

                            currentUser = UserData.GetUserByEmail(userRegister.Email ?? "");
                            isAuthenticated = true;

                            bool inMainMenu = true;
                            while (inMainMenu)
                            {
                                Console.Clear();
                                Console.WriteLine("=============================");
                                Console.WriteLine("       EASY TICKET APP       ");
                                Console.WriteLine("=============================");
                                Console.WriteLine("• Press 1 for User Management");
                                Console.WriteLine("• Press 2 for Check Events");
                                Console.WriteLine("• Press 3 to Exit");
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
                                        {
                                            Console.Clear();
                                            Console.WriteLine("User Management:");
                                            Console.WriteLine("----------------");

                                            if (currentUser != null)
                                            {
                                                Console.WriteLine($"Welcome, {currentUser.FirstName} {currentUser.LastName}!");
                                                Console.WriteLine($"Current Status: {(currentUser.Subscription == 1 ? "Premium" : "Standard")}");

                                                if (currentUser.Subscription == 0)
                                                {
                                                    Console.WriteLine("\n*Would you like to become Premium User and make your own events?*");
                                                    Console.WriteLine("Press 1 for Yes");
                                                    Console.WriteLine("Press 2 for No");

                                                    while (!int.TryParse(Console.ReadLine(), out Num) || Num < 1 || Num > 2)
                                                    {
                                                        Console.WriteLine("INVALID NUMBER!");
                                                    }

                                                    if (Num == 1)
                                                    {

                                                        if (currentUser != null)
                                                        {
                                                            currentUser.Subscription = 1;
                                                            UpdateSub.UpdateSubscription(currentUser.Id, 1); // Change the subscription in the database
                                                        }
                                                        Console.WriteLine("\nSuccess! You are now a Premium User!");
                                                    }

                                                    else
                                                    {
                                                        Console.WriteLine("\nYou chose not to upgrade.");
                                                    }
                                                }

                                            }


                                            Console.WriteLine("\nPress Enter to return to main menu...");
                                            Console.ReadLine();
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.WriteLine("\n[Events list view goes here]");
                                            Console.ReadLine();
                                            break;
                                        }
                                    case 3:
                                        {
                                            inMainMenu = false;
                                            Console.WriteLine("Goodbye!");
                                            break;
                                        }
                                }
                            }


                            break;
                        }
                    case 2:
                        {
                            Login userLogin = new Login();
                            bool completeLog = false;

                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Login:");
                                Console.WriteLine("------");

                                userLogin.Authentication_Login();

                                AuthenticationCheck infoCheck1 = new AuthenticationCheck(userLogin);
                                infoCheck1.ErrorCheck_Login();

                                ExistsUser existsCheck = new ExistsUser(userLogin);

                                completeLog = infoCheck1.Complete && existsCheck.IsSuccess;

                            } while (!completeLog);

                            currentUser = UserData.GetUserByEmail(userLogin.Email ?? "");
                            isAuthenticated = true;

                            bool inMainMenu = true;
                            while (inMainMenu)
                            {
                                Console.Clear();
                                Console.WriteLine("=============================");
                                Console.WriteLine("       EASY TICKET APP       ");
                                Console.WriteLine("=============================");
                                Console.WriteLine("• Press 1 for User Management");
                                Console.WriteLine("• Press 2 for Check Events");
                                Console.WriteLine("• Press 3 to Exit");
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
                                        {
                                            Console.Clear();
                                            Console.WriteLine("User Management:");
                                            Console.WriteLine("----------------");

                                            if (currentUser != null)
                                            {
                                                Console.WriteLine($"Welcome, {currentUser.FirstName} {currentUser.LastName}!");
                                                Console.WriteLine($"Current Status: {(currentUser.Subscription == 1 ? "Premium" : "Standard")}");

                                                if (currentUser.Subscription == 0)
                                                {
                                                    Console.WriteLine("\n*Would you like to become Premium User and make your own events?*");
                                                    Console.WriteLine("Press 1 for Yes");
                                                    Console.WriteLine("Press 2 for No");

                                                    while (!int.TryParse(Console.ReadLine(), out Num) || Num < 1 || Num > 2)
                                                    {
                                                        Console.WriteLine("INVALID NUMBER!");
                                                    }

                                                    if (Num == 1)
                                                    {

                                                        if (currentUser != null)
                                                        {
                                                            currentUser.Subscription = 1;
                                                            UpdateSub.UpdateSubscription(currentUser.Id, 1); // Change the subscription in the database
                                                        }
                                                        Console.WriteLine("\nSuccess! You are now a Premium User!");
                                                    }

                                                    else
                                                    {
                                                        Console.WriteLine("\nYou chose not to upgrade.");
                                                    }
                                                }

                                            }


                                            Console.WriteLine("\nPress Enter to return to main menu...");
                                            Console.ReadLine();
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.WriteLine("\n[Events list view goes here]");
                                            Console.ReadLine();
                                            break;
                                        }
                                    case 3:
                                        {
                                            inMainMenu = false;
                                            Console.WriteLine("Goodbye!");
                                            break;
                                        }
                                }
                            }

                            break;
                        }
                    case 3:
                        {
                            currentUser = null;
                            isAuthenticated = true;

                            bool inMainMenu = true;
                            while (inMainMenu)
                            {
                                Console.Clear();
                                Console.WriteLine("=============================");
                                Console.WriteLine("       EASY TICKET APP       ");
                                Console.WriteLine("=============================");
                                Console.WriteLine("• Press 1 for User Management");
                                Console.WriteLine("• Press 2 for Check Events");
                                Console.WriteLine("• Press 3 to Exit");
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
                                        {
                                            Console.Clear();
                                            Console.WriteLine("User Management:");
                                            Console.WriteLine("----------------");

                                            if (currentUser != null)
                                            {
                                                Console.WriteLine($"Welcome, {currentUser.FirstName} {currentUser.LastName}!");
                                                Console.WriteLine($"Current Status: {(currentUser.Subscription == 1 ? "Premium" : "Standard")}");

                                                if (currentUser.Subscription == 0)
                                                {
                                                    Console.WriteLine("\n*Would you like to become Premium User and make your own events?*");
                                                    Console.WriteLine("Press 1 for Yes");
                                                    Console.WriteLine("Press 2 for No");

                                                    while (!int.TryParse(Console.ReadLine(), out Num) || Num < 1 || Num > 2)
                                                    {
                                                        Console.WriteLine("INVALID NUMBER!");
                                                    }

                                                    if (Num == 1)
                                                    {

                                                        if (currentUser != null)
                                                        {
                                                            currentUser.Subscription = 1;
                                                            UpdateSub.UpdateSubscription(currentUser.Id, 1); // Change the subscription in the database
                                                        }
                                                        Console.WriteLine("\nSuccess! You are now a Premium User!");
                                                    }

                                                    else
                                                    {
                                                        Console.WriteLine("\nYou chose not to upgrade.");
                                                    }
                                                }

                                            }


                                            Console.WriteLine("\nPress Enter to return to main menu...");
                                            Console.ReadLine();
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.WriteLine("\n[Events list view goes here]");
                                            Console.ReadLine();
                                            break;
                                        }
                                    case 3:
                                        {
                                            inMainMenu = false;
                                            Console.WriteLine("Goodbye!");
                                            break;
                                        }
                                }
                            }


                            break;
                        }
                }
            }
        }

    }
}