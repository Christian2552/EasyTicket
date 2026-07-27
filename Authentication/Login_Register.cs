namespace EasyTicket
{
    class Register
    {
        public string name = "Guest";
        public string email = "N/A";
        public int Age;
        public string password = "N/A";
        public string rep_password = "N/A";

        public List<string> info_person = new List<string>();

        //Method Authentication_Register is for taking the data of our new user
        public List<string> Authentication_Register()
        {

            info_person.Add(name = Console.ReadLine()!);
            info_person.Add(email = Console.ReadLine()!);
            info_person.Add(password = Console.ReadLine()!);
            info_person.Add(rep_password = Console.ReadLine()!);


            return info_person;
        }

    }

    class Login : Register
    {
        public List<string> Authentication_Login()
        {

            info_person.Add(name = Console.ReadLine()!);
            info_person.Add(email = Console.ReadLine()!);
            info_person.Add(password = Console.ReadLine()!);


            return info_person;
        }
    }

}