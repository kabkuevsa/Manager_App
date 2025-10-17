using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagerApp
{
    class Program
    {
        private static List<Task> tasks = new List<Task>();
        private static int nextId = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("=== МЕНЕДЖЕР ЗАДАЧ ===");

            while (true)
            {
                ShowMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ShowAllTasks();
                        break;
                    case "3":
                        CompleteTask();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "5":
                        Console.WriteLine("Выход...");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n--- МЕНЮ ---");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Показать все задачи");
            Console.WriteLine("3. Завершить задачу");
            Console.WriteLine("4. Удалить задачу");
            Console.WriteLine("5. Выход");
            Console.Write("Выберите: ");
        }

        static void AddTask()
        {
            Console.Write("Введите описание задачи: ");
            var description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Ошибка: описание не может быть пустым!");
                return;
            }

            var task = new Task(nextId++, description, DateTime.Now);
            tasks.Add(task);
            Console.WriteLine($"Задача добавлена (ID: {task.Id})");
        }

        static void ShowAllTasks()
        {
            if (!tasks.Any())
            {
                Console.WriteLine("Задач нет");
                return;
            }

            Console.WriteLine("\n--- ВСЕ ЗАДАЧИ ---");
            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
        }

        static void CompleteTask()
        {
            var task = FindTaskById();
            if (task != null)
            {
                task.IsCompleted = true;
                Console.WriteLine($"Задача {task.Id} завершена!");
            }
        }

        static void DeleteTask()
        {
            var task = FindTaskById();
            if (task != null)
            {
                tasks.Remove(task);
                Console.WriteLine($"Задача {task.Id} удалена!");
            }
        }

        static Task FindTaskById()
        {
            Console.Write("Введите ID задачи: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task == null)
                    Console.WriteLine("Задача не найдена!");
                return task;
            }

            Console.WriteLine("Неверный ID!");
            return null;
        }
    }

    public class Task
    {
        public int Id { get; }
        public string Description { get; }
        public DateTime CreatedDate { get; }
        public bool IsCompleted { get; set; }

        public Task(int id, string description, DateTime createdDate)
        {
            Id = id;
            Description = description;
            CreatedDate = createdDate;
            IsCompleted = false;
        }

        public override string ToString()
        {
            var status = IsCompleted ? "✓" : " ";
            return $"[{status}] ID: {Id}, Задача: {Description}, Создана: {CreatedDate:dd.MM.yy HH:mm}";
        }
    }
}