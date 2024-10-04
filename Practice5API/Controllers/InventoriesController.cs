using Microsoft.AspNetCore.Mvc;
using Practice5API.Data;

namespace Practice5API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController(AppDbContext context) : BaseController<Product>(context);
}