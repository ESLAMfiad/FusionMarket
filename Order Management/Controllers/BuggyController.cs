using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Management.Errors;
using Order_Management.Repository.Data;

namespace Order_Management.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuggyController : ControllerBase
	{
        private readonly OrderManagementDbContext _context;
        public BuggyController(OrderManagementDbContext context)
        {
                _context = context;
        }
        [HttpGet("Not Found")]
        public ActionResult GetNotFoundReq()
        {
            var Product = _context.Products.Find(100);
            if(Product is null) return NotFound(new ApiResponse(404));
            return Ok(Product);
        }

		[HttpGet("ServerError")]
		public ActionResult GetServerError()
		{
			var Product = _context.Products.Find(100);
			var productreturn = Product?.ToString(); //error 
			return Ok(productreturn); // throw null reference exception
		}

		[HttpGet("BadRequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
		}

		[HttpGet("BadRequest/{id}")] //validation error
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}

		//server error and  validation error handling are once per project
	}
}
