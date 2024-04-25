using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebAPI.Data;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public BookController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("get-all-books")]
        public IActionResult GetAll()
        {
            //var allBooksDomain = _dbContext.Books.ToList();
            //get data from database -domain model
            var allBooksDomain = _dbContext.Books;
            //map domain mdels to DTOs
            var allBooksDTO = allBooksDomain.Select(Books => new BookDTO()
            {
                Id = Books.Id,
                Title = Books.Title,
                Description = Books.Description,
                IsRead = Books.IsRead,
                DateRead = Books.IsRead ? Books.DateRead.Value : null,
                Rate = Books.IsRead ? Books.Rate.Value : null,
                Genre = Books.Genre,
                CoverUrl = Books.CoverUrl,
                PublisherName = Books.Publisher.Name,
                AuthorNames = Books.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).ToList();
            //return dtos
            return Ok(allBooksDTO);
        }
        [HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            //get book domain model from Db
            var bookWithDomain = _dbContext.Books.Where(n => n.Id == id);
            if (bookWithDomain == null)
            {
                return NotFound();
            }
            //map domain model to DTOs
            var bookWithIdDTO = bookWithDomain.Select(book => new BookDTO()
            {
                Id =book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            });
            return Ok(bookWithIdDTO);
        }
        [HttpPost("add-book")]
        public IActionResult Addbook([FromBody] AddBookRequestDTO addBookRequestDTO)
        {
            //map dto to domain model 
            var bookDomainModel = new Books
            {
                Title = addBookRequestDTO.Title,
                Description = addBookRequestDTO.Description,
                IsRead = addBookRequestDTO.IsRead,
                DateAdded = addBookRequestDTO.DateAdded,
                Rate = addBookRequestDTO.Rate,
                Genre = addBookRequestDTO.Genre,
                CoverUrl = addBookRequestDTO.CoverUrl,
                DateRead = addBookRequestDTO.DateRead,
                PublisherID = addBookRequestDTO.Publishers.Id
            };
            _dbContext.Books.Add(bookDomainModel);
            _dbContext.SaveChanges();
            foreach (var id in addBookRequestDTO.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = bookDomainModel.Id,
                    AuthorId = id,
                };
                _dbContext.Books_Authors.Add(_book_author);
                _dbContext.SaveChanges();
            }
            return Ok();
        }
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBodyById(int id, [FromBody] AddBookRequestDTO bookDTO)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if(bookDomain != null)
            {
                bookDomain.Title= bookDTO.Title;
                bookDomain.Description= bookDTO.Description;
                bookDomain.DateRead = bookDTO.DateRead;
                bookDomain.IsRead = bookDTO.IsRead;
                bookDomain.Rate = bookDTO.Rate;
                bookDomain.Genre = bookDTO.Genre;
                bookDomain.CoverUrl = bookDTO.CoverUrl;
                bookDomain.DateAdded = bookDTO.DateAdded;
                _dbContext.SaveChanges();
            }
            var authorDomain = _dbContext.Books_Authors.Where(a => a.BookId == id).ToList();
            if(authorDomain != null)
            {
                _dbContext.Books_Authors.RemoveRange(authorDomain);
                _dbContext.SaveChanges();
            }
            foreach(var authorid in bookDTO .AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = id,
                    AuthorId = authorid
                };
                _dbContext.Books_Authors.Add(_book_author);
                _dbContext.SaveChanges();
            }
            return Ok(bookDTO);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult deletebook(int id)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (bookDomain != null)
            {
                _dbContext.Books.Remove(bookDomain);
                _dbContext.SaveChanges();
            }
            return Ok();
        }
    }
}
