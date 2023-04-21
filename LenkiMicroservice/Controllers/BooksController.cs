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

       
        [HttpGet("/api/BooktList")]     
        [SwaggerOperation("Get Product List information")]
        public IActionResult Get()
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }

            var books = _booksRepository.GetBooks();
            return new OkObjectResult(books);
        }

        [HttpGet("{id}", Name = "Get")]
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

        [HttpPost]
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

        [HttpPut]
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

        [HttpDelete("{id}")]
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