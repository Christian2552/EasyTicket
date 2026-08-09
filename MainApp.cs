namespace EasyTicket
{
    class MainClass
    {
        public static UserData? currentUser;

        static void Main()
        {
            bool appRunning = true;

            while (appRunning)
            {
                bool isAuthenticated = false;

                while (!isAuthenticated)
                {
                    Console.Clear();
                    Console.WriteLine("=============================");
                    Console.WriteLine("       EASY TICKET MENU      ");
                    Console.WriteLine("=============================");
                    Console.WriteLine("• Press 1 for Register");
                    Console.WriteLine("• Press 2 for Login");
                    Console.WriteLine("• Press 3 to continue as Guest");
                    Console.WriteLine("• Press 4 to Exit Application\n");
                    Console.Write("Your num: ");

                    if (!int.TryParse(Console.ReadLine(), out int menuChoice) || menuChoice < 1 || menuChoice > 4)
                    {
                        Console.WriteLine("INVALID, PLEASE INSERT VALID NUMBER!!!");
                        Console.ReadKey();
                        continue;
                    }

                    switch (menuChoice)
                    {
                        case 1:
                            Register userRegister = new Register();
                            bool complete = false;
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Register:\n---------");
                                userRegister.Authentication_Register();
                                AuthenticationCheck infoCheck = new AuthenticationCheck(userRegister);
                                infoCheck.ErrorCheck_Register();
                                complete = infoCheck.Complete;
                            } while (!complete);

                            CreateUser dbSave = new CreateUser(userRegister);
                            currentUser = UserData.GetUserByEmail(userRegister.Email ?? "");
                            isAuthenticated = true;
                            break;

                        case 2:
                            Login userLogin = new Login();
                            bool completeLog = false;
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Login:\n------");
                                userLogin.Authentication_Login();
                                AuthenticationCheck infoCheck1 = new AuthenticationCheck(userLogin);
                                infoCheck1.ErrorCheck_Login();
                                ExistsUser existsCheck = new ExistsUser(userLogin);

                                completeLog = infoCheck1.Complete && existsCheck.IsSuccess;
                            } while (!completeLog);

                            currentUser = UserData.GetUserByEmail(userLogin.Email ?? "");
                            isAuthenticated = true;
                            break;

                        case 3:
                            currentUser = null;
                            isAuthenticated = true;
                            break;

                        case 4:
                            Console.Clear();
                            Console.WriteLine("Exiting the application. Goodbye!");
                            Console.WriteLine("");
                            return;
                    }
                }

                bool inMainMenu = true;
                while (inMainMenu)
                {
                    Console.Clear();
                    Console.WriteLine("=============================");
                    Console.WriteLine("       EASY TICKET APP       ");
                    Console.WriteLine("=============================");
                    Console.WriteLine("• Press 1 for User Management");
                    Console.WriteLine("• Press 2 for Check Events");
                    if (currentUser != null)
                    {
                        Console.WriteLine("• Press 3 to Log Out");
                    }
                    else
                    {
                        Console.WriteLine("• Press 3 to Register / Login");
                    }
                    Console.WriteLine("• Press 4 to Exit Application\n");
                    Console.Write("Your num: ");

                    if (!int.TryParse(Console.ReadLine(), out int appChoice) || appChoice < 1 || appChoice > 4)
                    {
                        Console.WriteLine("INVALID, PLEASE INSERT VALID NUMBER!!!");
                        Console.ReadKey();
                        continue;
                    }

                    switch (appChoice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("User Management:\n----------------");

                            if (currentUser != null)
                            {
                                Console.WriteLine($"Welcome, {currentUser.FirstName} {currentUser.LastName}!");
                                Console.WriteLine($"Current Status: {(currentUser.Subscription == 1 ? "Premium" : "Standard")}\n");

                                Console.WriteLine("• Press 1 to View My Purchased Tickets");

                                if (currentUser.Subscription == 0)
                                {
                                    Console.WriteLine("• Press 2 to Upgrade to Premium");
                                    Console.WriteLine("• Press 3 to Back");
                                }
                                else
                                {
                                    Console.WriteLine("• Press 2 to Create a New Event");
                                    Console.WriteLine("• Press 3 to Manage / Delete My Created Events");
                                    Console.WriteLine("• Press 4 to Back");
                                }

                                Console.Write("\nYour choice: ");
                                string choice = Console.ReadLine() ?? "";

                                switch (choice)
                                {
                                    case "1":
                                        TicketHistory.DisplayUserTickets(currentUser.Id);
                                        break;

                                    case "2":
                                        if (currentUser.Subscription == 0)
                                        {
                                            currentUser.Subscription = 1;
                                            UpdateSub.UpdateSubscription(currentUser.Id, 1);
                                            Console.WriteLine("\nSuccess! You are now a Premium User!");
                                        }
                                        else
                                        {
                                            CreateEvent.AddNewEvent(currentUser.Id);
                                        }
                                        break;

                                    case "3":
                                        if (currentUser.Subscription == 1)
                                        {
                                            ManageEvents.DisplayMyCreatedEvents(currentUser.Id);
                                        }
                                        break;

                                    case "4":
                                        break;

                                    default:
                                        Console.WriteLine("\nInvalid option!");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Guest user has no profile details. Please register or log in!");
                            }

                            Console.WriteLine("\nPress Enter to return to main menu...");
                            Console.ReadLine();
                            break;

                        case 2:
                            EventCatalog.DisplayAndSelectEvent(currentUser);
                            Console.WriteLine("\nPress Enter to return to main menu...");
                            Console.ReadLine();
                            break;

                        case 3:
                            inMainMenu = false;

                            if (currentUser != null)
                            {
                                Console.Clear();
                                Console.WriteLine("Logging out...\n");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Redirecting to Register/Login...\n");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        case 4:
                            Console.Clear();
                            Console.WriteLine("Exiting the application. Goodbye!");
                            Console.WriteLine("");
                            return;
                    }
                }
            }
        }
    }
}