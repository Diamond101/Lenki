using System.Net;
using System.Transactions;
using LenkiData.Interface;
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
    public class BooksController : ControllerBase
    {

        private readonly IBooks _booksRepository;

        public BooksController(IBooks booksRepository)
        {
            _booksRepository = booksRepository;
        }


        /// <summary>
        /// Get  a List of all Books in the Library
        /// </summary>
        [SwaggerOperation("Get  a List of all Books in the Library")]
        [HttpGet("GetBooksList")]
        public IActionResult Get( string BookName)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }

            var books = _booksRepository.GetBooks(BookName);
            return new OkObjectResult(books);
        }

        /// <summary>
        /// Get  a Book by Id from the Library
        /// </summary>
        [SwaggerOperation("Get  a Book by Id from the Library")]
        [HttpGet("{GetBookById}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            var product = _booksRepository.GetBookByID(id);
            return new OkObjectResult(product);
        }

        /// <summary>
        /// Create a Book in the Library
        /// </summary>
        [SwaggerOperation("Create a Book in the Library")]
        
        [HttpPost("CreateBooks")]
        public IActionResult Post([FromBody] Book books)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            using (var scope = new TransactionScope())
            {
                _booksRepository.InsertBook(books);
                scope.Complete();
                return CreatedAtAction(nameof(Get), 
                    new { BookName = books.BookName }, books);
            }
        }

        /// <summary>
        /// Create Notification
        /// </summary>
        [SwaggerOperation("Create Notification")]

        [HttpPost("CreateNotification")]
        public IActionResult CreateNotification([FromBody] Notification notification)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            using (var scope = new TransactionScope())
            {
                _booksRepository.SetNotification(notification);
                scope.Complete();
                return CreatedAtAction(nameof(Get),
                    new { CustomerId = notification.CustomerId }, notification);
            }
        }


        /// <summary>
        /// Update Book Information in the Library
        /// </summary>
        [SwaggerOperation("Update Book Information in the Library")]
        [HttpPut("UpdateBooks")]
        public IActionResult Put([FromBody] Books books)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            if (books != null)
            {
                using (var scope = new TransactionScope())
                {
                    _booksRepository.UpdateBook(books);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }



        /// <summary>
        /// Delete a Book from the Library by Id
        /// </summary>
        [SwaggerOperation("Delete a Book from the Library by Id")]
        [HttpDelete("{DeleteBookId}")]
        public IActionResult Delete(int id)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            _booksRepository.DeleteBook(id);
            return new OkResult();
        }
    }
}