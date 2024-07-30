using Order_Management.Core.Entities;
using Order_Management.Service.Dto;
using Order_Managment.Service.Dto;

namespace Order_Managment.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(OrderReqDto orderReqDto);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}
