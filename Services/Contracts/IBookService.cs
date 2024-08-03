using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IBookService      //Ctrl+F12
    {
       Task< IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges); //: Tüm kitapları almak için bir koleksiyon döndürür.
       Task<BookDto> GetOneBookbyIdAsync(int id, bool trackChanges); //Belirli bir kitabı almak için bir kitap döndürür.
       Task<BookDto> CreateOneBookAsync(BookDtoForInsertion book);// Bir kitap oluşturur ve oluşturulan kitabı döndürür.
       Task UpdateBookAsync(int id, BookDtoForUpdate book, bool trackChanges);//Belirli bir kitabı günceller.
       Task DeleteOneBookAsync(int id, bool trackChanges);//Belirli bir kitabı silerS
       Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);
       Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate,Book book);
         
    }
}
