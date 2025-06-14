using AutoMapper;
using eBlog.Application.DTOs;
using eBlog.Application.DTOs.Dashboard;
using eBlog.Domain.Entities;
using eBlog.Domain.Enums;
using eBlog.Domain.Models;
using eBlog.Domain.Models.Dashboard;

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

            // User
            CreateMap<User, UserListDto>();
            CreateMap<User, UserDetailDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            // Post
            CreateMap<Post, PostListDto>()
                .ForMember(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.UserName));
            CreateMap<Post, PostDetailDto>()
                .ForMember(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SeoMetadata, opt => opt.MapFrom(src => src.SeoMetadata));
            CreateMap<PostCreateDto, Post>()
                .ForMember(dest => dest.SeoMetadata, opt => opt.MapFrom(src => src.SeoMetadata));
            CreateMap<PostUpdateDto, Post>();

            // Category
            CreateMap<Category, CategoryListDto>();
            CreateMap<Category, CategoryDetailDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            // Tag
            CreateMap<Tag, TagListDto>();
            CreateMap<TagCreateDto, Tag>();
            CreateMap<Tag, TagDetailDto>()
                .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.PostTags.Count));

            // Comment
            CreateMap<Comment, CommentListDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<CommentCreateDto, Comment>();

            // ProductOrder
            CreateMap<ProductOrder, ProductOrderListDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.BuyerUserName, opt => opt.MapFrom(src => src.Buyer.UserName))
                .ForMember(dest => dest.BuyerEmail, opt => opt.MapFrom(src => src.Buyer.Email));
            CreateMap<ProductOrderCreateDto, ProductOrder>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "pending"))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.UnitPrice * src.Quantity));

            // Cart
            CreateMap<Cart, CartDto>();
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            // Notification, Like
            CreateMap<Notification, NotificationDto>();
            CreateMap<Like, LikeDto>();

            // Favorite
            CreateMap<Favorite, FavoriteListDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : null))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
                .ForMember(dest => dest.CommentContent, opt => opt.MapFrom(src => src.Comment != null ? src.Comment.Content : null));
            CreateMap<FavoriteCreateDto, Favorite>();

            // Follow
            CreateMap<Follow, FollowListDto>()
                .ForMember(dest => dest.FollowerUserName, opt => opt.MapFrom(src => src.Follower.UserName))
                .ForMember(dest => dest.FollowingUserName, opt => opt.MapFrom(src => src.Following.UserName));
            CreateMap<FollowCreateDto, Follow>();

            // Dashboard DTOs
            CreateMap<DashboardTotals, DashboardTotalsDto>().ReverseMap();
            CreateMap<TopLikedPost, TopLikedPostDto>().ReverseMap();
            CreateMap<TopSellingProduct, TopSellingProductDto>().ReverseMap();
            CreateMap<TopCommentedPost, TopCommentedPostDto>().ReverseMap();
            CreateMap<TopBuyer, TopBuyerDto>().ReverseMap();
            CreateMap<TopRatedProduct, TopRatedProductDto>().ReverseMap();
            CreateMap<OrderStatusCount, OrderStatusCountDto>().ReverseMap();
            CreateMap<UserGrowthStat, UserGrowthStatDto>().ReverseMap();
            CreateMap<CategoryDistribution, CategoryDistributionDto>().ReverseMap();
            CreateMap<ActiveAuthor, ActiveAuthorDto>().ReverseMap();
            CreateMap<PostModuleUsage, PostModuleUsageDto>().ReverseMap();
            CreateMap<CouponUsage, CouponUsageDto>().ReverseMap();
            CreateMap<LoginActivity, LoginActivityDto>().ReverseMap();
            CreateMap<ErrorLogCount, ErrorLogCountDto>().ReverseMap();
            CreateMap<HourlyTraffic, HourlyTrafficDto>().ReverseMap();
            CreateMap<TagUsage, TagUsageDto>().ReverseMap();
            CreateMap<PersonalSummary, PersonalSummaryDto>().ReverseMap();
            CreateMap<Role, RoleDto>()
       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
       .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            // PostModule
            CreateMap<PostModule, PostModuleDto>().ReverseMap();

            CreateMap<PostModule, PostModuleListDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<ModuleType>(src.Type)));

            CreateMap<PostModule, PostModuleCreateDto>()
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<ModuleType>(src.Type)));

            CreateMap<PostModule, PostModuleUpdateDto>()
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<ModuleType>(src.Type)));

            CreateMap<Post, PostWithModulesDto>()
                .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.PostModules));
            CreateMap<SeoMetadata, SeoMetadataDto>().ReverseMap();
            CreateMap<SeoMetadata, SeoMetadataCreateDto>().ReverseMap();
            CreateMap<SeoMetadata, SeoMetadataUpdateDto>().ReverseMap();
        }
    }
}
