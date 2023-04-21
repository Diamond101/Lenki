using LenkiMicroservice.Interface;
using LenkiMicroservice.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace LenkiMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservedBookController : ControllerBase
    {
        private readonly IReserved _reservedRepository;

        public ReservedBookController(IReserved reservedRepository)
        {
            _reservedRepository = reservedRepository;
        }

        [HttpGet]
        public IActionResult GetReservedBooks(int customerId)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            var customers = _reservedRepository.ReservedBookscustomers(customerId);
            return new OkObjectResult(customers);
        }


        [HttpPost]
        public IActionResult Post([FromBody] ReservedBooks reserved )
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            using (var scope = new TransactionScope())
            {
                _reservedRepository.ReservedBook(reserved);
                scope.Complete();
                return new OkResult();
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] ReservedBooks reserved)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            if (reserved != null)
            {
                using (var scope = new TransactionScope())
                {
                    _reservedRepository.UpdateReserved(reserved);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            _reservedRepository.DeleteReseved(id);
            return new OkResult();
        }
    }
}
