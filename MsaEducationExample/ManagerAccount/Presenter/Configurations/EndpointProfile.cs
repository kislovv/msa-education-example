using AutoMapper;
using ManagerAccount.Models;
using ManagerAccount.Presenter.Models.Requests;
using ManagerAccount.Presenter.Models.Responses;
using ManagerAccount.UseCases.Dtos;
using ManagerAccount.UseCases.Entities.Models;

namespace ManagerAccount.Presenter.Configurations;

public class EndpointProfile : Profile
{
    public EndpointProfile()
    {
        CreateMap<ManagerRequest, Manager>();
        CreateMap<OrderRequest, OrderDto>().ForMember(or => or.ClientEmail,
            expression => 
                expression.MapFrom(dto => dto.Email));
        CreateMap<OrderDetails, OrderDetailsDto>();
        CreateMap<TypeOfProduct, TypeOfProductDto>().ReverseMap();
        CreateMap<OrderStatus, OrderStatusDto>().ReverseMap();
        CreateMap<OrderDto, PrepareOrderResponse>()
            .ForMember(or => or.Value,
            expression => 
                expression.MapFrom(dto => dto.OrderDetails.Value))
            .ForMember(or => or.Status,
                expression => 
                    expression.MapFrom(dto => dto.OrderStatus))
            .ForMember(or => or.TypeOfProduct,
                expression => 
                    expression.MapFrom(dto => dto.OrderDetails.TypeOfProduct));
    }
}