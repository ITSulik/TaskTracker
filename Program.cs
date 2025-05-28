using System;
using TaskTracker.Services;

namespace TaskTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskService = new TaskService();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Task Tracker CLI");
                Console.WriteLine("----------------");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. View tasks");
                Console.WriteLine("3. Delete task");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Enter task name: ");
                        var name = Console.ReadLine();
                        if (taskService.AddTask(name))
                            Console.WriteLine("Task added.");
                        else
                            Console.WriteLine("Invalid or duplicate task.");
                        Pause();
                        break;

                    case "2":
                        var tasks = taskService.GetAllTasks();
                        if (tasks.Count == 0)
                            Console.WriteLine("No tasks found.");
                        else
                            for (int i = 0; i < tasks.Count; i++)
                                Console.WriteLine($"{i + 1}. {tasks[i]}");
                        Pause();
                        break;

                    case "3":
                        var allTasks = taskService.GetAllTasks();
                        if (allTasks.Count == 0)
                        {
                            Console.WriteLine("No tasks to delete.");
                        }
                        else
                        {
                            for (int i = 0; i < allTasks.Count; i++)
                                Console.WriteLine($"{i + 1}. {allTasks[i]}");

                            Console.Write("Enter task number to delete: ");
                            if (int.TryParse(Console.ReadLine(), out int idx))
                            {
                                if (taskService.DeleteTask(idx - 1))
                                    Console.WriteLine("Task deleted.");
                                else
                                    Console.WriteLine("Invalid number.");
                            }
                            else
                            {
                                Console.WriteLine("Not a number.");
                            }
                        }
                        Pause();
                        break;

                    case "4":
                        taskService.SaveTasks();
                        Console.WriteLine("Exiting... Tasks saved.");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
