using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class TaskService
    {
        private readonly List<TaskItem> _tasks;
        private readonly string _filePath = "tasks.txt";

        public TaskService()
        {
            _tasks = new List<TaskItem>();
            LoadTasks();
        }

        public List<TaskItem> GetAllTasks() => _tasks;

        public bool AddTask(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            if (_tasks.Any(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase))) return false;

            _tasks.Add(new TaskItem
            {
                Name = name.Trim(),
                CreatedAt = DateTime.Now
            });

            return true;
        }

        public bool DeleteTask(int index)
        {
            if (index < 0 || index >= _tasks.Count) return false;
            _tasks.RemoveAt(index);
            return true;
        }

        public void SaveTasks()
        {
            var lines = _tasks.Select(t => t.ToFileString());
            File.WriteAllLines(_filePath, lines);
        }

        private void LoadTasks()
        {
            if (!File.Exists(_filePath)) return;

            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var task = TaskItem.FromFileString(line);
                if (task != null)
                    _tasks.Add(task);
            }
        }
    }
}
