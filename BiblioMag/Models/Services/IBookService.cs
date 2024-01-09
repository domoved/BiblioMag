using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BiblioMag.Models;

namespace BiblioMag.Models.Services
{
    public interface IBookService
    {
        Task<Book> AddBookAsync(Book book);
        Task<Book> GetBookByIdAsync(int bookId);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<byte[]?> DownloadBookAsync(int bookId);
        Task StartReadingAsync(int bookId);
        Task EndReadingAsync(int bookId);
        Task<bool> RemoveBookAsync(int bookId);
        Task<Book> UpdateBookAsync(Book book);
    }
}