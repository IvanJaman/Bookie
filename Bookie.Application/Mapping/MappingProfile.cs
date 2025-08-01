using AutoMapper;
using Bookie.Domain.Entities;
using Bookie.Application.DTOs;

namespace Bookie.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<User, UserSummaryDto>();

            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Shelves, opt => opt.MapFrom(src => src.Shelves))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Role, opt => opt.Ignore()) 
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<Shelf, ShelfDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.ShelfBooks));

            CreateMap<ShelfBook, ShelfBookDto>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Book.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Book.Author))
                .ForMember(dest => dest.CoverPhotoUrl, opt => opt.MapFrom(src => src.Book.CoverPhotoUrl))
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.AddedAt));


            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));

            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.Language))
                .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUser.Username));

            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.GenreId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UpdateBookDto, Book>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Genre, GenreDto>();
        }
    }
}
