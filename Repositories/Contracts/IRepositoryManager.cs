using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IBookRepository Book { get; } //IRepositoryManager arayüzünde, Book adında bir özellik (property) tanımlanmıştır.
                                      //Bu özellik, IBookRepository arabirimini döndürür.
                                      //IBookRepository, kitaplarla ilgili işlemleri gerçekleştirmek için gereken metotların tanımlandığı bir arabirimdir.
                                      //IRepositoryManager arayüzündeki Book özelliği ise,
                                      //bu kitap işlemlerini gerçekleştirmek için IBookRepository arabirimine erişimi sağlar.
        Task SaveAsync();
    }
}
