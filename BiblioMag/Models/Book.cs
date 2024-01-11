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
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите автора книги")]
        public string Author { get; set; }

        public string? Genre { get; set; }

        public int Year { get; set; }

        public byte[]? FileContent { get; set; }

        public ReadingStatus ReadingStatus { get; set; }
    }
}