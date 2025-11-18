using System;
using System.Collections.Generic;
using LibraryApp.Interfaces;
using LibraryApp.Models;

namespace LibraryApp.Commands;

public class ListCommand : ICommand
{
    private readonly List<Book> _books;

    public ListCommand(List<Book> books)
    {
        _books = books;
    }

    public string Name => "list";
    public string Description => "Könyvek listázása.";

    public void Execute()
    {
        if (_books.Count == 0)
        {
            Console.WriteLine("Nincs könyv.");
            return;
        }

        foreach (var b in _books)
            Console.WriteLine($"[{b.Id}] {b.Title} - {b.Author} ({b.Year}, {b.Genre})");
    }
}
