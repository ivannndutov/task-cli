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
                else if (args[0] == "delete")
                {
                    if (args.Length >= 2)
                    {
                        if (int.TryParse(args[1], out int id))
                        {
                            user.DeleteTask(id);
                        } else
                        {
                            Console.WriteLine("Id must be a number!");
                        }
                    } else
                    {
                        Console.WriteLine("Not enough arguments to delete a task");
                    }
                } else if (args[0] == "mark-in-progress")
                {
                    if (args.Length >= 2)
                    {
                        if (int.TryParse(args[1], out int id))
                        {
                            user.MarkTaskInProgress(id);
                        } else
                        {
                            Console.WriteLine("Id must be a number!");
                        }
                    } else
                    {
                        Console.WriteLine("Not enough arguments to mark the task");
                    }
                }
                else if (args[0] == "mark-done")
                {
                    if (args.Length >= 2)
                    {
                        if (int.TryParse(args[1], out int id))
                        {
                            user.MarkTaskDone(id);
                        }
                        else
                        {
                            Console.WriteLine("Id must be a number!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not enough arguments to mark the task");
                    }
                }
                else if (args[0] == "list")
                {
                    if (args.Length >= 2)
                    {
                        if (args[1] == "done")
                        {
                            user.ListTasks(task => task.Status == TaskStatus.DONE);
                        } else if (args[1] == "todo")
                        {
                            user.ListTasks(task => task.Status == TaskStatus.TODO);
                        } else if (args[1] == "in-progress")
                        {
                            user.ListTasks(task => task.Status == TaskStatus.IN_PROGRESS);
                        } else
                        {
                            Console.WriteLine("Cannot list by this condition. The only available are: 'todo', 'in-progress' and 'done'");
                        }
                    } else
                    {
                        user.ListTasks(task => true);
                    }   
                }
            }

            if (user != null)
            {
                user.Dispose();
            }
        }
    }
}
