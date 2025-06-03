using AutoMapper;
using Booksaw.Dto.BookDtos;
using Booksaw.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booksaw.Business.Mappings
{
    public class BookMapping : Profile
    {
        public BookMapping()
        {
            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();
            CreateMap<Book, ResultBookDto>();
        }

    }
}
