using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository //Bu sınıf, RepositoryBase<Book> sınıfından türetilir ve IBookRepository arabirimini uygular.
                                                                               //sealed anahtar kelimesiyle sınıfın miras alınmasını engelledik,
                                                                               //yani başka bir sınıf bu sınıfı kalıtamaz.
    {
        public BookRepository(RepositoryContext context) : base(context) //BookRepository sınıfı, RepositoryBase sınıfının yapıcı metodunu çağırarak _context alanını başlatır
                                                                         //ardından _context nesnesine erişerek veritabanı işlemlerini gerçekleştirir.
        {                                                                  //Tekrar ayrı bir enjeksiyon yapmanıza gerek yoktur.
        

        }
       public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(x => x.Id).ToListAsync();
        public async Task<Book> GetOneBookbyIdAsync(int id, bool trackChanges) => await FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreateOneBook(Book book) => Create(book);
        public void DeleteOneBook(Book book) => Delete(book);
        public void UpdateOneBook(Book book)=>Update(book);

        
    }
}