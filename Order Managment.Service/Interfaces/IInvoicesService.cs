using Order_Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment.Service.Interfaces
{
    public interface IInvoicesService
    {
        Task<Invoice> GetInvoiceByIdAsync(int invoiceId);
        Task<IReadOnlyList<Invoice>> GetAllInvoicesAsync();
    }
}
