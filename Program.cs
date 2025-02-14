using task_cli;

namespace task_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }

            using var user = new User();
            string command = args[0].ToLowerInvariant();
            switch (command)
            {
                case "add":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Not enough arguments to add the task.");
                    }
                    else
                    {
                        user.AddTask(args[1]);
                    }
                    break;
                case "update":
                    if (!TryGetId(args, out int updateId, minArgs: 3))
                    {
                        Console.WriteLine("Not enough arguments to update a task or id is invalid.");
                    }
                    else
                    {
                        user.UpdateTaskDescription(updateId, args[2]);
                    }
                    break;
                case "delete":
                    if (!TryGetId(args, out int deleteId, minArgs: 2))
                    {
                        Console.WriteLine("Not enough arguments to delete a task or id is invalid.");
                    }
                    else
                    {
                        user.DeleteTask(deleteId);
                    }
                    break;

                case "mark-in-progress":
                    if (!TryGetId(args, out int markInProgressId, minArgs: 2))
                    {
                        Console.WriteLine("Not enough arguments to mark task in-progress or id is invalid.");
                    }
                    else
                    {
                        user.MarkTaskInProgress(markInProgressId);
                    }
                    break;

                case "mark-done":
                    if (!TryGetId(args, out int markDoneId, minArgs: 2))
                    {
                        Console.WriteLine("Not enough arguments to mark task done or id is invalid.");

                    }
                    else
                    {
                        user.MarkTaskDone(markDoneId);
                    }
                    break;

                case "list":
                    HandleListCommand(user, args);
                    break;

                default:
                    Console.WriteLine("Unknown command.");
                    PrintUsage();
                    break;
            }
        }

        private static bool TryGetId(string[] args, out int id, int minArgs)
        {
            id = 0;
            if (args.Length < minArgs)
                return false;
            return int.TryParse(args[1], out id);
        }

        private static void HandleListCommand(User user, string[] args)
        {
            if (args.Length < 2)
            {
                user.ListTasks(task => true);
            }
            else
            {
                string condition = args[1].ToLowerInvariant();
                switch (condition)
                {

                    case "done":
                        user.ListTasks(task => task.Status == TaskStatus.DONE);
                        break;
                    case "todo":
                        user.ListTasks(task => task.Status == TaskStatus.TODO);
                        break;
                    case "in-progress":
                        user.ListTasks(task => task.Status == TaskStatus.IN_PROGRESS);
                        break;
                    default:
                        Console.WriteLine("Cannot list by this condition. Available conditions: 'todo', 'in-progress' and 'done'");
                        break;
                }

            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: task_cli <command> [parameters]");
            Console.WriteLine("Commands:");
            Console.WriteLine("  add <description>");
            Console.WriteLine("  update <id> <new description>");
            Console.WriteLine("  delete <id>");
            Console.WriteLine("  mark-in-progress <id>");
            Console.WriteLine("  mark-done <id>");
            Console.WriteLine("  list [todo|in-progress|done]");
        }
    }
}
