using BookStore.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        UnitOfWork unitOfWork;
        public CatalogsController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

    }
}
