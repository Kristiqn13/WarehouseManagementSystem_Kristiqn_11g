using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem_Kristiqn_11g
{
    internal class WarehouseApp
    {
        private const int MaxCapacity = 331;
        private List<Article> articles = new List<Article>();
        private string filePath = "warehouse_data.txt";

        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine(" СКЛАДОВО СТОПАНСТВО - МЕНЮ ");
                Console.WriteLine("1. Добавяне на нов артикул");
                Console.WriteLine("2. Извеждане на всички артикули");
                Console.WriteLine("3. Справка за броя на артикулите");
                Console.WriteLine("4. Преглед на артикул по номер");
                Console.WriteLine("5. Редактиране на артикул");
                Console.WriteLine("6. Справка за обща наличност по име");
                Console.WriteLine("7. Запис във файл");
                Console.WriteLine("8. Зареждане от файл");
                Console.WriteLine("9. Изход");
                Console.Write("Изберете опция (1-9): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddNewArticle();
                            break;
                        case "2":
                            PrintAllArticles();
                            break;
                        case "3":
                            PrintCountStatus();
                            break;
                        case "4":
                            ViewArticleByIndex();
                            break;
                        case "5":
                            EditArticleData();
                            break;
                        case "6":
                            CheckStockByName();
                            break;
                        case "7":
                            SaveToFile();
                            Console.WriteLine("Данните бяха успешно записани във файл!");
                            break;
                        case "8":
                            LoadFromFile();
                            Console.WriteLine("Данните бяха успешно заредени от файла!");
                            break;
                        case "9":
                            running = false;
                            Console.WriteLine("Изход от програмата...");
                            break;
                        default:
                            Console.WriteLine("Невалиден избор! Моля, изберете число от 1 до 9.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ГРЕШКА]: {ex.Message}");
                }

                if (running)
                {
                    Console.WriteLine("\nНатиснете произволен клавиш, за да продължите...");
                    Console.ReadKey();
                }
            }
        }

        private void AddNewArticle()
        {
            if (articles.Count >= MaxCapacity)
            {
                Console.WriteLine("Складът е пълен! Достигнат е максималният капацитет.");
                return;
            }

            Console.Write("Име: ");
            string name = Console.ReadLine();

            Console.Write("Година на производство: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Срок на годност (месеци): ");
            int months = int.Parse(Console.ReadLine());

            Console.Write("Количество: ");
            int quantity = int.Parse(Console.ReadLine());

            articles.Add(new Article(name, year, months, quantity));
            Console.WriteLine("Артикулът е добавен успешно!");
        }

        private void PrintAllArticles()
        {
            if (articles.Count == 0)
            {
                Console.WriteLine("Складът е празен.");
                return;
            }

            for (int i = 0; i < articles.Count; i++)
            {
                Console.WriteLine($"[{i}] {articles[i]}");
            }
        }

        private void PrintCountStatus()
        {
            Console.WriteLine($"Текущ брой записи: {articles.Count} / Максимален капацитет: {MaxCapacity}");
        }

        private void ViewArticleByIndex()
        {
            if (articles.Count == 0)
            {
                Console.WriteLine("Складът е празен.");
                return;
            }

            Console.Write("Въведете пореден номер: ");
            int index = int.Parse(Console.ReadLine());

            if (index < 0 || index >= articles.Count)
            {
                Console.WriteLine("Невалиден индекс.");
                return;
            }

            Console.WriteLine(articles[index]);
        }

        private void EditArticleData()
        {
            if (articles.Count == 0)
            {
                Console.WriteLine("Складът е празен.");
                return;
            }

            Console.Write("Въведете пореден номер за редакция: ");
            int index = int.Parse(Console.ReadLine());

            if (index < 0 || index >= articles.Count)
            {
                Console.WriteLine("Невалиден индекс.");
                return;
            }

            Console.Write("Ново име: ");
            articles[index].Name = Console.ReadLine();

            Console.Write("Нова година на производство: ");
            articles[index].ProductionYear = int.Parse(Console.ReadLine());

            Console.Write("Нов срок на годност (месеци): ");
            articles[index].ExpirationMonths = int.Parse(Console.ReadLine());

            Console.Write("Ново количество: ");
            articles[index].Quantity = int.Parse(Console.ReadLine());

            Console.WriteLine("Редакцията е успешна!");
        }

        private void CheckStockByName()
        {
            Console.Write("Въведете име на артикул: ");
            string name = Console.ReadLine();

            int total = articles
                .Where(a => a.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase))
                .Sum(a => a.Quantity);

            Console.WriteLine($"Общо количество за '{name}': {total} бр.");
        }

        private void SaveToFile()
        {
            File.WriteAllLines(filePath, articles.Select(a => a.ToFileString()));
        }

        private void LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файлът не е намерен.");
            }

            var lines = File.ReadAllLines(filePath);
            if (lines.Length > MaxCapacity)
            {
                throw new InvalidOperationException("Файлът надвишава максималния капацитет на склада!");
            }

            articles.Clear();
            foreach (var line in lines)
            {
                var parts = line.Split(" ;");
                if (parts.Length == 4)
                {
                    articles.Add(new Article(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3])));
                }
            }
        }
    }
}