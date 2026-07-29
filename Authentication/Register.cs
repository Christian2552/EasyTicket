namespace EasyTicket
{
    class Register : User_Authentication
    {
        public string Age { get; set; } = "";
        public string RepPassword { get; set; } = "";

        //Method Authentication_Register is for taking the data of our new user
        public Register Authentication_Register()
        {
            Console.Write("First Name: ");
            firstName = Console.ReadLine()!;
            Console.Write("Last Name: ");
            lastName = Console.ReadLine()!;
            Console.Write("Email: ");
            Email = Console.ReadLine()!;

            Console.Write("Age: ");
            Age = Console.ReadLine()!;

            Console.Write("Password: ");
            Password = Console.ReadLine()!;
            Console.Write("Repeat Password: ");
            RepPassword = Console.ReadLine()!;


            return this;
        }

    }
}