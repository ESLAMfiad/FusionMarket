using AutoMapper;
using Order_Management.Core.Entities;
using Order_Management.Core.Repositories;
using Order_Management.Dto;
using Order_Managment.Service.Interfaces;
using Orders_Managment.Core;

namespace Order_Management.Service.implementation
{
    public class CustomerService : ICustomerService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public CustomerService( IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		//public async Task<Customer> CreateCustomerAsync(CustomerDto customerDto)
		//{
		//	var customer = _mapper.Map<Customer>(customerDto);

		//	await _unitOfWork.Repository<Customer>().AddAsync(customer);

		//	return customer;
		//}

		public async Task<IReadOnlyList<Order>> GetCustomerOrdersAsync(int customerId)
		{
			var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(customerId);
			if (customer == null)
			{
				throw new ArgumentException($"Customer with ID {customerId} not found.");
			}
			var orders = await _unitOfWork.OrderRepository.GetOrdersByCustomerIdAsync(customerId);

			return orders;
		}
	}
}
