using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Domain.Entities;

namespace eBlog.Application.Mappers
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product
            CreateMap<Product, ProductListDto>()
                .ForMember(dest => dest.SellerUserName, opt => opt.MapFrom(src => src.Seller.UserName));
            CreateMap<Product, ProductDetailDto>()
                .ForMember(dest => dest.SellerUserName, opt => opt.MapFrom(src => src.Seller.UserName));
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();

            // SeoMetadata
            CreateMap<SeoMetadata, SeoMetadataDto>().ReverseMap();

            CreateMap<User, UserListDto>();
            CreateMap<User, UserDetailDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<Post, PostListDto>()
    .ForMember(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.UserName));
            CreateMap<Post, PostDetailDto>()
                .ForMember(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<PostCreateDto, Post>();
            CreateMap<PostUpdateDto, Post>();

            CreateMap<Category, CategoryListDto>();
            CreateMap<Category, CategoryDetailDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<Tag, TagListDto>();
            CreateMap<TagCreateDto, Tag>();
            CreateMap<Tag, TagDetailDto>()
    .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.PostTags.Count));

            CreateMap<Comment, CommentListDto>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<CommentCreateDto, Comment>();

            CreateMap<ProductOrder, ProductOrderListDto>()
    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
    .ForMember(dest => dest.BuyerUserName, opt => opt.MapFrom(src => src.Buyer.UserName));
            CreateMap<ProductOrderCreateDto, ProductOrder>();

            CreateMap<Cart, CartDto>();
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<Notification, NotificationDto>();

            CreateMap<Like, LikeDto>();

            CreateMap<Favorite, FavoriteListDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : null))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null)) // Eğer Product entity'si 'Book' olarak geçiyorsa, Product olarak güncelle
                .ForMember(dest => dest.CommentContent, opt => opt.MapFrom(src => src.Comment != null ? src.Comment.Content : null));

            CreateMap<FavoriteCreateDto, Favorite>();

            CreateMap<Follow, FollowListDto>()
                .ForMember(dest => dest.FollowerUserName, opt => opt.MapFrom(src => src.Follower.UserName))
                .ForMember(dest => dest.FollowingUserName, opt => opt.MapFrom(src => src.Following.UserName));

            CreateMap<FollowCreateDto, Follow>();
        }
    }
}
