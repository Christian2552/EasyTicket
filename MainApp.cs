namespace EasyTicket
{
    class MainClass
    {
        static void Main()
        {
            Register obj = new Register();

            obj.Authentication_Register();

            Console.WriteLine($"{obj.firstName} with email: {obj.Email}, he is {obj.Age} years old and password is {obj.Password} and the rep one is {obj.Password == obj.RepPassword}");
        }
    }
}