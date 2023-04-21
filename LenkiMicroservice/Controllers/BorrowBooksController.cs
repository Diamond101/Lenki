using System.Transactions;
using LenkiMicroservice.Interface;
using LenkiMicroservice.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LenkiMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BorrowBooksController : ControllerBase
    {

        private readonly IBorrow _borrowRepository;
       
        public BorrowBooksController(IBorrow borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }

        /// <summary>
        /// Borrow a Book from the Library
        /// </summary>
        [SwaggerOperation("Get List of Borrowed Books")]
        [HttpGet("{GetBorrowedBookList}")]
        public IActionResult GetBorrowBooksList(int customerId)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            var book = _borrowRepository.ListBoorwedBooks(customerId);
            return new OkObjectResult(book);
        }


        /// <summary>
        /// Get Borrowed book by Id
        /// </summary>
        [SwaggerOperation("Get Borrowed book by Id")]
        [HttpGet("{GetBorrodBooksById}", Name = "GetBorrowBooksById")]
        public IActionResult Get(int id)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            var customer = _borrowRepository.GetBookByID(id);
            return new OkObjectResult(customer);
        }


        /// <summary>
        /// Borrow a Book to a Customer
        /// </summary>
        [SwaggerOperation("Borrow a Book to a Customer")]
        [HttpPost("{BorrowedBook}")]
        public IActionResult Post([FromBody] BorrowBook borrowBooks)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            using (var scope = new TransactionScope())
            {
                _borrowRepository.BorrowBook(borrowBooks);
                scope.Complete();
                return CreatedAtAction(nameof(Get), 
                    new { CustomerId = borrowBooks.CustomerId }, borrowBooks);
            }
        }


        /// <summary>
        /// Return Book to the Library
        /// </summary>
        [SwaggerOperation("Return Book to the Library")]
        [HttpPut("{ReturnBook}")]
        public IActionResult Put([FromBody] BorrowBooks borrow)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            if (borrow != null)
            {
                using (var scope = new TransactionScope())
                {
                    _borrowRepository.ReturnBook(borrow);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }


        /// <summary>
        /// Delete borrowed Book By Id
        /// </summary>
        [SwaggerOperation("Delete borrowed Book By Id")]
        [HttpDelete("{DeleteBorrowedBookById}")]
        public IActionResult Delete(int id)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            _borrowRepository.DeleteBorow(id);
            return new OkResult();
        }

    }
}