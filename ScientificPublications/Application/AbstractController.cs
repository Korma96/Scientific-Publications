using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ScientificPublications.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbstractController : ControllerBase
    {
        protected async Task GetSession()
        {
            throw new NotImplementedException();
        }
    }
}
