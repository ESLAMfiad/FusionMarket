using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Management.Errors;

namespace Order_Management.Controllers
{
	[Route("error/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{
		public ActionResult Error(int code)
		{
			return NotFound(new ApiResponse(code));
		}
	}
}
