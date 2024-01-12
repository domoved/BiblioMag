using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BiblioMag.Models
{
    public class Book
    {
        public Book()
        {
        }

        public int Id { get; set; }
        [Required] 
        public string Title { get; set; }

        [Required] public string Author { get; set; }

        public string? Genre { get; set; } = "Unknown";

        public int Year { get; set; } = 1;

        [DataType(DataType.Upload)]
        public byte[]? FileContent { get; set; }

        public ReadingStatus ReadingStatus { get; set; } = 0;
    }
}