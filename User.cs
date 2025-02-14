using System.Text.Json;

namespace TaskCLI
{
    class User
    {
        private static FileStream _stream;
        private static readonly string _dbPath = "tasks.json";
        public static List<Task> Tasks { get; set; }
        static User()
        {
            Tasks = new List<Task>();

            if (File.Exists(_dbPath) && new FileInfo(_dbPath).Length > 0)
            {
                try
                {
                    _stream = new FileStream(_dbPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    var tasks = JsonSerializer.Deserialize<Task[]>(_stream);
                    if (tasks != null)
                    {
                        Tasks = tasks.ToList();
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error deserializing file: {ex.Message}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            } else
            {
                _stream = new FileStream(_dbPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            
        }
        public void AddTask(string description)
        {
            Tasks.Add(new Task(description));
        }

        public void DeleteTask(int id)
        {
            int removed = Tasks.RemoveAll(t => t.Id == id);
            if (removed > 0)
            {
                Console.WriteLine("Task successfully deleted");
            } else
            {
                Console.WriteLine($"No tasks wit id {id} were found.");
            }
        }

        public void MarkTaskInProgress(int id)
        {
            UpdateTaskStatus(id, TaskStatus.IN_PROGRESS);
        }

        public void MarkTaskDone(int id)
        {
            UpdateTaskStatus(id, TaskStatus.DONE);
        }

        private void UpdateTaskStatus(int id, TaskStatus status)
        {
            Task? foundTask = Tasks.Find(t => t.Id == id);
            if (foundTask != null)
            {
                foundTask.Status = status;
                foundTask.UpdatedAt = DateTime.Now;
            }
            else
            {
                Console.WriteLine($"There is no task with id {id}.");
            }
        }

        public void ListTasks(Predicate<Task> filter)
        {
            if (Tasks.Count > 0)
            {
                var filteredTasks = Tasks.FindAll(filter);
                if (filteredTasks != null && filteredTasks.Count > 0)
                {
                    foreach(Task task in filteredTasks)
                    {
                        Console.WriteLine(task);
                        Console.WriteLine();
                    }
                } else
                {
                    Console.WriteLine("There are no tasks for specified condition.");
                }
            } else
            {
                Console.WriteLine("There are no tasks yet.");
            }
        }

        public void UpdateTaskDescription(int id, string description)
        {
            Task? foundTask = Tasks.Find(t => t.Id == id);
            if (foundTask != null)
            {
                foundTask.Description = description;
                foundTask.UpdatedAt = DateTime.Now;
                Console.WriteLine(foundTask);
            } else
            {
                Console.WriteLine($"No task with id {id} was found");
            }
        }

        public void SaveTasks()
        {
            try
            {
                _stream.SetLength(0);
                _stream.Seek(0, SeekOrigin.Begin);

                JsonSerializer.Serialize<Task[]>(_stream, Tasks.ToArray());
                _stream.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks: {ex.Message}");
            }
        }

        public void Dispose()
        {
            if (Tasks.Count > 0)
            {
                SaveTasks();
            }
            _stream?.Dispose();
        }
    }
}
