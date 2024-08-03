using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager          //enjeksiyon
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IBookRepository> _bookRepository; //Lazy<IBookRepository> türünde _bookRepository adında bir alan daha tanımlanır.
                                                                //Lazy<T> tipi, isteğe bağlı (lazy) olarak bir nesnenin oluşturulmasını sağlar.
                                                                //Bu durumda, IBookRepository türünde bir nesnenin oluşturulması isteğe bağlıdır.

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _bookRepository= new Lazy<IBookRepository>(()=>new BookRepository(_context)); //Lazy<T> sınıfının yapıcı metodu, bir işlev (delegate) alır.
                                                                                          //Bu işlev, tembelce oluşturulacak T türünde bir nesneyi tanımlar.
                                                                                          //İşlev, çağırıldığında T türünde bir nesne oluşturmalıdır
                                                                                          //.Lazy<IBookRepository> sınıfının yapıcı metodu
                                                                                          //(() => new BookRepository(_context)) işlevini alır.
                                                                                          //BookRepository sınıfı RepositoryBase<Book> sınıfından miras aldığı için
                                                                                          //_context alanına erişebilir
                                                                                          //BookRepository sınıfının bir örneğini oluştururken _context nesnesini kullanır. 
        }



        public IBookRepository Book => _bookRepository.Value;                             //_bookRepository değişkeninin değeri alındığında, tembelce oluşturulan                                                                                          //IBookRepository türündeki nesne gerçekten oluşturulur ve döndürülür.    
        public async Task SaveAsync()           
        {
            await _context.SaveChangesAsync();                                //_context nesnesi üzerindeki SaveChanges metodunu çağırarak yapılan değişiklikleri veritabanına kaydeder.
                                                                   
        }

       
    }
}
