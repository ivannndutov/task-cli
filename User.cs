using System.Text.Json;

namespace task_cli
{
    class User : IDisposable
    {
        private readonly string _dbPath = "tasks.json";
        public static List<Task> Tasks { get; private set; } = [];
        public User()
        {
            Tasks = LoadTasks();
        }

        private List<Task> LoadTasks()
        {
            if (!File.Exists(_dbPath)) return [];
            try
            {
                string json = File.ReadAllText(_dbPath);
                return JsonSerializer.Deserialize<List<Task>>(json) ?? [];
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error loading tasks: {ex.Message}");
                return [];
            }
        }

        private void SaveTasks()
        {
            try
            {
                string json = JsonSerializer.Serialize(Tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_dbPath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks: {ex.Message}");
            }
        }

        public void AddTask(string description)
        {
            Tasks.Add(new Task(description));
            SaveTasks();
        }

        public void DeleteTask(int id)
        {
            if (Tasks.RemoveAll(t => t.Id == id) > 0)
            {
                Console.WriteLine("Task successfully deleted");
                SaveTasks();
            }
            else
            {
                Console.WriteLine($"No task with id {id} were found.");
            }
        }

        public void MarkTaskInProgress(int id) =>
            UpdateTaskStatus(id, TaskStatus.IN_PROGRESS);

        public void MarkTaskDone(int id) =>
            UpdateTaskStatus(id, TaskStatus.DONE);
        private void UpdateTaskStatus(int id, TaskStatus status)
        {
            Task? task = Tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.UpdateStatus(status);
                SaveTasks();
            }
            else
            {
                Console.WriteLine($"No task with id {id} found");
            }
        }

        public void ListTasks(Predicate<Task> filter)
        {
            var filteredTasks = Tasks.FindAll(filter);
            if (filteredTasks.Count > 0)
            {
                foreach (Task task in filteredTasks)
                {
                    Console.WriteLine(task);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No matching tasks found.");
            }
        }

        public void UpdateTaskDescription(int id, string description)
        {
            var task = Tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.UpdateDescription(description);
                Console.WriteLine(task);
                SaveTasks();
            }
            else
            {
                Console.WriteLine($"No task with id {id} was found");
            }
        }

        public void Dispose() => SaveTasks();
    }
}
