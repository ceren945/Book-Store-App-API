using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {

            CreateMap<BookDtoForUpdate, Book>().ReverseMap(); //. Bu, BookDtoForUpdates nesnesinin verilerini Book nesnesine taşımak için kullanılabilir.
            CreateMap<Book, BookDto>();           //Bu, Book nesnesinin verilerini BookDto nesnesine taşımak için kullanılabilir.
            CreateMap<BookDtoForInsertion, Book>();
        }
        }     
}
