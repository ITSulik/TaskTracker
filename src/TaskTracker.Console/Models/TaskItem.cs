using System;

namespace TaskTracker.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"{Name} (added on {CreatedAt})";
        }

        public string ToFileString()
        {
            return $"{Name}|||{CreatedAt}";
        }

        public static TaskItem FromFileString(string line)
        {
            var parts = line.Split("|||");
            if (parts.Length != 2 || !DateTime.TryParse(parts[1], out var createdAt))
                return null;

            return new TaskItem
            {
                Name = parts[0],
                CreatedAt = createdAt
            };
        }
    }
}
