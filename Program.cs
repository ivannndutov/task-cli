// See https://aka.ms/new-console-template for more information
using TaskCLI;

namespace TaskCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            User? user = null;
            if (args.Length >= 2)
            {
                user = new User();
                if (args[0] == "add")
                {
                    user.AddTask(args[1]);
                }
            }
            if (user != null)
            {
                user.Dispose();
            }
        }
    }
}