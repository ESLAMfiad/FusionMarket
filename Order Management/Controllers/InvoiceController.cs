using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Management.Errors;
using Order_Management.Service.Dto;
using Order_Managment.Service.Interfaces;

namespace Order_Management.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class InvoiceController : ControllerBase
	{
		private readonly IInvoicesService _invoiceService;

		private readonly IMapper _mapper;
		public InvoiceController(IInvoicesService invoiceService, IMapper mapper)
		{
			_invoiceService = invoiceService;
			_mapper = mapper;
		}

		[HttpGet("{invoiceId}")]
		public async Task<IActionResult> GetInvoiceById(int invoiceId)
		{
			var invoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);
			if (invoice == null)
			{
				return NotFound(new ApiResponse(404,"invoice not found"));
			}

			var invoiceDto = _mapper.Map<InvoiceDto>(invoice);
			return Ok(invoiceDto);
		}
		[HttpGet]
		public async Task<IActionResult> GetAllInvoices()
		{
			var invoices = await _invoiceService.GetAllInvoicesAsync();
			var invoiceDtos = _mapper.Map<IReadOnlyList<InvoiceDto>>(invoices);
			return Ok(invoiceDtos);
		}
	}
}
