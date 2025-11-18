using System;
using System.Collections.Generic;
using System.Linq;
using LibraryApp.Interfaces;
using LibraryApp.Models;

namespace LibraryApp.Commands
{
    public class RecentCommand : ICommand
    {
        private readonly List<Book> _books;

        public RecentCommand(List<Book> books)
        {
            _books = books;
        }

        public string Name => "recent";
        public string Description => "Könyvek listázása megadott kezdő évtől.";

        public void Execute()
        {
            if (_books.Count == 0)
            {
                Console.WriteLine("Nincs könyv.");
                return;
            }

            int startYear;

            // kezdő év bekérése hibakezeléssel
            while (true)
            {
                Console.Write("Add meg a kezdő évet: ");
                if (int.TryParse(Console.ReadLine(), out startYear))
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hibás év! Kérlek adj meg egy számot.");
                Console.ResetColor();
            }

            // szűrés
            var result = _books
                .Where(b => b.Year >= startYear)
                .OrderByDescending(b => b.Year)
                .ToList();

            if (!result.Any())
            {
                Console.WriteLine("Nincs könyv a megadott évtől kezdve.");
                return;
            }

            Console.WriteLine($"\nKönyvek {startYear}-től:");

            foreach (var b in result)
                Console.WriteLine($"[{b.Id}] {b.Title} - {b.Author} ({b.Year}, {b.Genre})");
        }
    }
}
