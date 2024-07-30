using AutoMapper;
using Order_Management.Dto;
using Order_Management.Core.Entities;
using Order_Management.Service.Dto;
using Order_Managment.Service.Dto;
namespace Order_Management.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
			CreateMap<Order, OrderDto>().ReverseMap();
			CreateMap<Order, OrderReqDto>().ReverseMap();
			CreateMap<Order, OrderResponseDto>().ReverseMap();
			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Invoice, InvoiceDto>().ReverseMap();
			CreateMap<User, UserDto>().ReverseMap();
			CreateMap<UserLoginDto, User>().ReverseMap();
			CreateMap<OrderItem, OrderItemDto>().ReverseMap();
			CreateMap<customerBasketDto,CustomerBasket>().ReverseMap();
		}
    }
}
