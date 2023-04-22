using LenkiMicroservice.Interface;
using LenkiMicroservice.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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


        /// <summary>
        /// Get a List of all Reserved Books 
        /// </summary>
        [SwaggerOperation("Get a List of all Reserved Books ")]
        [HttpGet("{GetListofReservedBooks}")]
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

        /// <summary>
        /// Reserved a Book in the Library
        /// </summary>
        [SwaggerOperation("Reserved a Book in the Library ")]
        [HttpPost]
        public IActionResult Post([FromBody] ReservedBook reserved )
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            var response = _reservedRepository.GetResevedBookByID(reserved.BookId);
            if (response != null )
            {
                return BadRequest("Dear Customer The Book Id: " + response.BookId + "  you requested has been reserved by Another Customer Thank.");
            }
            else
            {
                using (var scope = new TransactionScope())
                {
                    _reservedRepository.ReservedBook(reserved);
                    scope.Complete();
                    return new OkResult();
                }
            }
        }


        /// <summary>
        /// UnReserved a Book in the Library
        /// </summary>
        [SwaggerOperation("UnReserved a Book in the Library ")]
        [HttpPut("UnReservedBooks")]
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


        /// <summary>
        /// Delete a Book Reservation By Id
        /// </summary>
        [SwaggerOperation("Delete a Book Reservation By Id ")]
        [HttpDelete("{DeleteReservedById}")]
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
