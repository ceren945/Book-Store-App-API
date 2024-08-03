using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    [ServiceFilter(typeof(LogFilterAttribute), Order = 1)] //Bütün metodlar loglansın.
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {

            _manager = manager;
        }
        //BookService  / IBookService BookService { get; }/ IServiceManagerda tanımlı
        //IBookService metodlarına erişmek için kullanılır
        //IBookService metodlarının hepsi asenkrondur burada bu metodları güncelleriz ayrıca conrollerda tanımlanan metodlarımıza da asenkron hale getiririz.

        [HttpGet]
        public async Task<IActionResult> GetAllBookAsync()
        {

            var books = await _manager.BookService.GetAllBooksAsync(false);
            return Ok(books);


        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {

            var book = await _manager.BookService.GetOneBookbyIdAsync(id, false);

            if (book is null)
                throw new BookNotFoundException(id);
            return Ok(book);


        }


        [ServiceFilter(typeof(ValidationFilterAttribute),Order =2)] // Bu öznitelik, eyleme bir hizmet filtresi uygulanacağını belirtir
                                                           //ValidationFilterAttribute sınıfı, bir eylem filtresi olarak tanımlanmış bir sınıftır ve doğrulama işlemlerini gerçekleştirir.
                                                           //Bu öznitelik, eylemin üzerine yerleştirilerek doğrulama filtresinin çalışmasını sağlar.
        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion BookDto)
        {

           /* if (BookDto is null)
                return BadRequest(); // 400 
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState); */

            var book = await _manager.BookService.CreateOneBookAsync(BookDto);
            return StatusCode(201, book);

        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id,
            [FromBody] BookDtoForUpdate bookDto)
        {
            await _manager.BookService.UpdateBookAsync(id, bookDto, true);
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {

            await _manager.BookService.DeleteOneBookAsync(id, false);

            return NoContent();


        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();

            var result = await _manager.BookService.GetOneBookForPatchAsync(id, false);
            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);
            TryValidateModel(result.bookDtoForUpdate);
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);



            return NoContent(); // 204
        }



    }

}





