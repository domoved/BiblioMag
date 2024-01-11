using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BiblioMag.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название книги")]
        public string Title { get; set; }   = string.Empty;

        [Required(ErrorMessage = "Введите автора книги")]
        public string Author { get; set; } = string.Empty;

        public string? Genre { get; set; } = string.Empty;

        public int Year { get; set; } = 0;

        public byte[]? FileContent { get; set; }

        public ReadingStatus ReadingStatus { get; set; } = ReadingStatus.NotStarted;
    }
}