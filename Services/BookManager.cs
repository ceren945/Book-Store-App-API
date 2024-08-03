using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using NLog;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;


        public BookManager(IRepositoryManager manager,ILoggerService logger,IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;

        }
        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges) 
        {
            var books= await _manager.Book.GetAllBooksAsync(trackChanges);         //IBookRepositoryde async tanımlı
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
        public async Task<BookDto> GetOneBookbyIdAsync(int id, bool trackChanges) 
        {

            /*var book= await _manager.Book.GetOneBookbyIdAsync(id, trackChanges);  */
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<BookDto>(book); 

        }
        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion BookDto)    
        {
            var entity = _mapper.Map<Book>(BookDto);          
            _manager.Book.CreateOneBook(entity);///IBookRepositoryde Async tanımlı değil        

            await _manager.SaveAsync();          //Save methodu IRepositoryManagerda asenkron tanımlanmıştır o yüzden asenkron yaptık.
            return _mapper.Map<BookDto>(entity);               
        }
        public async Task UpdateBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            //check entity
;
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);
            entity = _mapper.Map<Book>(bookDto);
            _manager.Book.UpdateOneBook(entity); //IBookRepositoryde Async tanımlı değil
            await _manager.SaveAsync();          //IRepositoryManager da async tanımlı

        }
        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);
            _manager.Book.DeleteOneBook(entity); 
            await _manager.SaveAsync();          
        }//IbookServisde async tanımlı
        async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> IBookService.GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);
            if (book == null) throw new BookNotFoundException(id);
            var bookDtoForUpdate=_mapper.Map<BookDtoForUpdate>(book);
            return(bookDtoForUpdate, book);

            //Bu GetOneBookByIdForPatch yöntemi, belirtilen bir id değeriyle bir kitap nesnesini veritabanından alır.
            //Eğer kitap bulunamazsa BookNotFoundException istisnasını fırlatır.
            //Ardından, bu kitap nesnesini BookDtoForUpdates tipine dönüştürerek bir veri aktarım nesnesi olan bookDtoForUpdate oluşturur.
            //Son olarak, bookDtoForUpdate ve orijinal book nesnesini içeren bir demet olarak çıktıyı döndürür.
        }
        async Task IBookService.SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate, book);
            await _manager.SaveAsync();
        }

        private async Task<Book> GetOneBookByIdAndCheckExists(int id, bool trackChanges)
        {
            // check entity 
            var entity = await _manager.Book.GetOneBookbyIdAsync(id, trackChanges);

            if (entity is null)
                throw new BookNotFoundException(id);
            return entity;
        }
    }
}

