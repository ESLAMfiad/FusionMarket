using Order_Management.Dto;
using Order_Management.Core.Entities;


namespace Order_Managment.Service.Interfaces
{
    public interface ICustomerService
    {
       // Task<Customer> CreateCustomerAsync(CustomerDto customerDto);
        Task<IReadOnlyList<Order>> GetCustomerOrdersAsync(int customerId);
        //Task<Customer> GetCustomerByIdAsync(int customerId);
    }
}
