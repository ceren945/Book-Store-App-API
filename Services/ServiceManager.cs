using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System;
using Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Services
{
    public class ServiceManager : IServiceManager
    {

        private readonly Lazy<IBookService> _bookService; //Lazy<IBookService> türünden _bookService adında bir özel bir alan (field) tanımladık.
                                                          //Lazy<T> sınıfı, değeri ilk defa kullanıldığında oluşturulan tembel (lazy) bir örneği temsil eder.
        public ServiceManager(IRepositoryManager repositoryManager,ILoggerService logger,IMapper mapper) {  //Yapıcı metodun içinde _bookService özelliğini tembel olarak başlatıyoruz. new Lazy<IBookService>(...) ifadesi,
                                                                                              //IBookService türünden bir tembel örneği oluşturur.
                                                                                              //() => new BookManager(repositoryManager) ifadesi ise tembel örneğin değeri kullanıldığında çalışacak olan işlevi (delegate) tanımlar.
                                                                                              //BookManager'ın yapıcı metodu repositoryManager ile çağrılır.
            _bookService = new Lazy<IBookService>(() => new BookManager(repositoryManager,logger,mapper));

        }
        public IBookService BookService => _bookService.Value;  
    }
}

//Burada yapılan enjeksiyon, IRepositoryManager örneğinin ServiceManager sınıfının bir örneğine dışarıdan sağlanmasıdır.