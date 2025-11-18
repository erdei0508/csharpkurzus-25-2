// Program.cs – főprogram kommentekkel
using LibraryApp.Commands;
using LibraryApp.Interfaces;
using LibraryApp.Models;
using LibraryApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace LibraryApp;

public class Program
{
    public static void Main()
    {
        string filePath = "books.json";

        IFileService fileService = new JsonFileService();
        ILoggerService logger = new ConsoleLoggerService();
        var libraryService = new LibraryService(fileService, logger);

        List<Book> books = libraryService.LoadBooks(filePath);

        var commands = new List<ICommand>
        {
            new ListCommand(books),
            new AddCommand(books, logger),
            new DeleteCommand(books, logger),
            new RecentCommand(books),
            new SearchCommand(books, logger),
            new SaveCommand(libraryService, books, filePath)
        };

        commands.Add(new HelpCommand(commands));

        Console.WriteLine("Könyvtárkezelő rendszer");
        Console.WriteLine("Parancsok: " + string.Join(", ", commands.Select(c => c.Name)) + ", exit");

        bool running = true;

        while (running)
        {
            Console.Write("\n> ");
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "exit")
            {
                running = false;
                continue;
            }

            var command = commands.FirstOrDefault(c => c.Name == input);

            if (command != null)
                command.Execute();
            else
                logger.Error("Ismeretlen parancs!");
        }

        Console.WriteLine("Kilépés...");
    }
}
