using TaskCLI;

namespace TaskCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            User? user = null;
            if (args.Length >= 1)
            {
                user = new User();

                if (args[0] == "add" && args.Length >= 2)
                {
                    user.AddTask(args[1]);
                }
                else if (args[0] == "update")
                {
                    if (args.Length >= 3)
                    {
                        if (int.TryParse(args[1], out int id))
                        {
                            user.UpdateTaskDescription(id, args[2]);
                        }
                        else
                        {
                            Console.WriteLine("Id must be a number!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not enough arguments to update a task");
                    }
                }
                else if (args[0] == "list")
                {
                    user.ListTasks();
                }
            }

            if (user != null)
            {
                user.Dispose();
            }
        }
    }
}
