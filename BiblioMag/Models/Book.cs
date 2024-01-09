using System.ComponentModel.DataAnnotations;

namespace BiblioMag.Models;

public class Book
{
    public Book() { }
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название книги")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Введите автора книги")]
    public string? Author { get; set; }

    public string? Genre { get; set; }

    [Range(-Double.NegativeInfinity, 2024, ErrorMessage = "Введите корректный год")]
    public int Year { get; set; }
    public byte[]? FileContent { get; set; }

    public ReadingStatus ReadingStatus { get; set; }

    // (возможность расширения проекта в будущем) Другие свойства книги, например, путь к файлу книги
}