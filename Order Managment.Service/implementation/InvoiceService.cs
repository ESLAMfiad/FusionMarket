using Order_Management.Core.Entities;
using Order_Management.Core.Repositories;
using Order_Managment.Service.Interfaces;
using Orders_Managment.Core;


namespace Order_Management.Service.implementation
{
    public class InvoiceService : IInvoicesService
	{
		private readonly IUnitOfWork _unitOfWork;
		public InvoiceService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IReadOnlyList<Invoice>> GetAllInvoicesAsync()
		{
			return await _unitOfWork.Repository<Invoice>().GetAllAsync();
		}

		public async Task<Invoice> GetInvoiceByIdAsync(int invoiceId)
		{
			return await _unitOfWork.Repository<Invoice>().GetByIdAsync(invoiceId);
		}
	}
}
