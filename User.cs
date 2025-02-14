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

        public void ListTasks()
        {
            if (Tasks.Count > 0)
            {
                foreach(Task task in Tasks)
                {
                    Console.WriteLine(task.Print());
                    Console.WriteLine();
                }
            } else
            {
                Console.WriteLine("No tasks found.");
            }
        }

        public void UpdateTaskDescription(int id, string description)
        {
            Task? foundTask = Tasks.Find(t => t.Id == id);
            if (foundTask != null)
            {
                foundTask.Description = description;
                foundTask.UpdatedAt = DateTime.Now;
                foundTask.Print();
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
