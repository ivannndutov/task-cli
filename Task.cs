using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_cli
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
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }

        public Task(string description)
        {
            Id = Interlocked.Increment(ref _counter);
            Description = description;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }


        public void UpdateDescription(string newDescription)
        {
            if (!string.IsNullOrWhiteSpace(newDescription) && Description != newDescription)
            {
                Description = newDescription;
                UpdatedAt = DateTime.Now;
            }
        }

        public void UpdateStatus(TaskStatus newStatus)
        {
            if (Status != newStatus)
            {
                Status = newStatus;
                UpdatedAt = DateTime.Now;
            }
        }

        public override string ToString() =>
            $"Task {Id}: {Description}\n\rStatus: {GetStatusText()}\n\rCreated: {CreatedAt}\n\rUpdated: {UpdatedAt}";

        public string GetStatusText() => Status switch
        {
            TaskStatus.TODO => "Todo",
            TaskStatus.IN_PROGRESS => "In progress",
            TaskStatus.DONE => "Done",
            _ => "Unknown"
        };

    }
}
