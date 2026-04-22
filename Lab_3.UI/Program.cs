using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lab_3.BLL.Interfaces;
using Lab_3.BLL.Services;
using Lab_3.BLL.Mapping;
using Lab_3.DAL.Data;
using Lab_3.DAL.UoW;
using Lab_3.DAL.Entities;
using Microsoft.Extensions.Logging;

namespace Lab_3.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Зчитування конфігурації з appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration config = builder.Build();

            string hotelName = config["HotelSettings:HotelName"] ?? "Наш Готель";

            // 2. Налаштування Dependency Injection (DI)
            var serviceProvider = ConfigureServices();

            // 3. Отримання головного сервісу з контейнера DI
            var hotelService = serviceProvider.GetRequiredService<IBusinessService>();
            
            // Отримуємо UnitOfWork лише для того, щоб залити початкові дані (Seed)
            var uow = serviceProvider.GetRequiredService<IUnitOfWork>();
            SeedData(uow);

            // 4. Запуск UI (Консольне меню)
            RunMenu(hotelService, hotelName);
        }

        // Метод для налаштування всіх залежностей
        private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // 1. Додаємо базові сервіси логування (це вирішить помилку ILoggerFactory)
        services.AddLogging(config => config.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Error));

        // 2. Реєстрація DAL
        services.AddDbContext<AppDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // 3. Реєстрація BLL
        services.AddScoped<IBusinessService, BusinessService>();
    
        // 4. Реєстрація AutoMapper
        services.AddAutoMapper(cfg => 
        {
            cfg.AddProfile<MappingProfile>();
        });

        return services.BuildServiceProvider();
    }

        // Головний цикл взаємодії з користувачем
        private static void RunMenu(IBusinessService hotelService, string hotelName)
{
    while (true)
    {
        Console.Clear(); 
        Console.WriteLine("\x1b[3J");
        
        Console.WriteLine($"=== Ласкаво просимо в {hotelName} ===");
        Console.WriteLine("1. Переглянути всі номери");
        Console.WriteLine("2. Знайти вільні номери");
        Console.WriteLine("3. Забронювати номер");
        Console.WriteLine("4. Зняти бронь");
        Console.WriteLine("0. Вийти");
        Console.Write("\nОберіть дію: ");

        var choice = Console.ReadLine();

        // Додаємо відступ для краси після вибору
        Console.WriteLine(); 

        switch (choice)
        {
            case "1":
                ShowRooms(hotelService, onlyFree: false);
                break;
            case "2":
                ShowRooms(hotelService, onlyFree: true);
                break;
            case "3":
                BookRoom(hotelService);
                break;
            case "4":
                CancelBooking(hotelService);
                break;
            case "0":
                Console.WriteLine("Дякуємо за використання системи!");
                return;
            default:
                Console.WriteLine("Невідома команда. Спробуйте ще раз.");
                break;
        }

        // Залізобетонна пауза, яка не проскочить випадково
        Console.WriteLine("\nНатисніть клавішу Enter для повернення в меню...");
        Console.ReadLine(); // Використовуємо ReadLine замість ReadKey для стабільності в терміналі VS Code
    }
}

        private static void ShowRooms(IBusinessService service, bool onlyFree)
        {
            Console.WriteLine(onlyFree ? "\n--- ВІЛЬНІ НОМЕРИ ---" : "\n--- ВСІ НОМЕРИ ---");
            var rooms = onlyFree ? service.GetFreeRooms() : service.GetAllRooms();

            foreach (var room in rooms)
            {
                Console.WriteLine($"ID: {room.Id} | Номер: {room.Number} | Категорія: {room.Category} | Ціна: {room.PricePerNight} грн | Статус: {room.Status}");
            }
        }

        private static void BookRoom(IBusinessService service)
        {
            Console.Write("\nВведіть ID номеру для бронювання: ");
            if (int.TryParse(Console.ReadLine(), out int roomId))
            {
                Console.Write("Введіть кількість днів перебування: ");
                if (int.TryParse(Console.ReadLine(), out int days))
                {
                    DateTime start = DateTime.Now;
                    DateTime end = start.AddDays(days);

                    bool success = service.BookRoom(roomId, start, end);
                    if (success)
                        Console.WriteLine($"Успіх! Номер заброньовано до {end.ToShortDateString()}.");
                    else
                        Console.WriteLine("Помилка! Можливо, номер не існує або вже зайнятий.");
                }
            }
        }

        private static void CancelBooking(IBusinessService service)
        {
            Console.Write("\nВведіть ID бронювання для скасування (для демо введіть ID номеру, який заброньовано - логіка спрощена): ");
            // Примітка: у повноцінній системі користувач вводить номер броні. 
            // Для спрощення роботи в консолі ми скасовуємо просто за ID, який збігається.
            if (int.TryParse(Console.ReadLine(), out int bookingId))
            {
                bool success = service.CancelBooking(bookingId);
                if (success)
                    Console.WriteLine("Бронювання успішно скасовано. Номер знову вільний!");
                else
                    Console.WriteLine("Помилка! Бронювання з таким ID не знайдено.");
            }
        }

        // Заповнення бази початковими даними при першому запуску
        private static void SeedData(IUnitOfWork uow)
        {
            var existingRooms = uow.Rooms.GetAll();
            if (!System.Linq.Enumerable.Any(existingRooms))
            {
                uow.Rooms.Add(new Room { Number = 101, Category = "Стандарт", PricePerNight = 1000 });
                uow.Rooms.Add(new Room { Number = 102, Category = "Стандарт", PricePerNight = 1000 });
                uow.Rooms.Add(new Room { Number = 201, Category = "Напівлюкс", PricePerNight = 1800 });
                uow.Rooms.Add(new Room { Number = 301, Category = "Люкс", PricePerNight = 3500 });
                uow.Save();
            }
        }
    }
}