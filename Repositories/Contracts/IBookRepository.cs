using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);////Tüm kitapları almak için bir sorgu döndürür.
        Task<Book> GetOneBookbyIdAsync(int id,bool trackChanges); //Belirli bir kitabı almak için bir sorgu döndürür.
        void CreateOneBook(Book book);// Bir kitap oluşturur.
        void UpdateOneBook(Book book);//: Bir kitabı günceller.
        void DeleteOneBook(Book book);//Bir kitabı siler.

      

    }
}