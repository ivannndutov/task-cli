using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCLI
{
    public enum TaskStatus
    {
        TODO,
        IN_PROGRESS,
        DONE
    }
    class Task
    {
        private static int _counter = 0;
        public int Id { get; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt{ get; set; }
        public DateTime UpdatedAt { get; set; }

        public Task(string description)
        {
            Id = System.Threading.Interlocked.Increment(ref _counter);
            Description = description;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }

        public override string ToString()
        {
            return $"Task {Id}: {Description}\n\rStatus: {PrintStatus()}\n\rCreated: {CreatedAt}\n\rUpdated: {UpdatedAt}";
        }

        public string PrintStatus()
        {
           switch(Status)
            {
                case TaskStatus.TODO:
                    return "Todo";
                case TaskStatus.IN_PROGRESS:
                    return "In progress";
                case TaskStatus.DONE:
                    return "Done";
                default:
                    return "None";
            }
        }

    }
}
