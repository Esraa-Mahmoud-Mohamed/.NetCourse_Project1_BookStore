using AutoMapper;
using BookStore.DTOs.BookDTOs;
using BookStore.Models;
using BookStore.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        IMapper mapper;
        UnitOfWork unitOfWork;

        public BooksController(UnitOfWork _unitOfWork, IMapper mapper)
        {
            this.unitOfWork = _unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "select all books ", Description = "example:  http:/localhost/api/books")]
        [SwaggerResponse(200, "return all books", typeof(List<DisplayBookDTO>))]
        public IActionResult GetAll()
        {
            List<Book> books = unitOfWork.bookGenericRepository.selectall();
            if (books.Count == 0) return NotFound();
            return Ok(mapper.Map<List<DisplayBookDTO>>(books));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "can earch on book by book id ", Description = "example:  http:/localhost/api/books")]
        [SwaggerResponse(200, "return book data", typeof(DisplayBookDTO))]
        [SwaggerResponse(404, "if no book founded")]
        //[SwaggerIgnore]
        public IActionResult GetById(int id)
        {
            Book book = unitOfWork.bookGenericRepository.selectbyid(id);
            if (book == null) return NotFound();
            return Ok(mapper.Map<DisplayBookDTO>(book));
        }
        [HttpPost]
        // [Authorize(Roles ="admin")]
        [SwaggerOperation(Summary = "add new book")]
        [SwaggerResponse(201, "if book created succcesfully")]
        [SwaggerResponse(400, "ifinvalid book data")]
        public IActionResult Add(AddBookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.bookGenericRepository.add(mapper.Map<Book>(bookDTO));
                unitOfWork.bookGenericRepository.save();
                return Ok();
            }
            else return BadRequest(ModelState);

        }
        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "edit book data")]
        [SwaggerResponse(204, "if book updated succcesfully")]
        [SwaggerResponse(400, "if invalid book data")]
        public IActionResult Edit(int id, AddBookDTO bookDTO)
        {
            if (ModelState.IsValid)
            {
                //unitOfWork.bookGenericRepository.edit(mapper.Map<Book>(bookDTO));
                Book b = new Book()
                {
                    Id = id,
                    Title = bookDTO.Title,
                    Stock = bookDTO.Stock,
                    PublishDate = bookDTO.PublishDate,
                    Price = bookDTO.Price,
                    auth_id = (int)bookDTO.auth_id,
                    cat_id = (int)bookDTO.cat_id,
                };
                unitOfWork.bookGenericRepository.edit(b);
                unitOfWork.bookGenericRepository.save();
                return NoContent();
            }
            else return BadRequest(ModelState);
        }
        [HttpDelete]
        //[Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "delete book from datatbase")]
        [SwaggerResponse(200, "if book deleted succcesfully")]
        public IActionResult delete(int id)
        {
            unitOfWork.bookGenericRepository.delete(id);
            unitOfWork.bookGenericRepository.save();
            return Ok();
        }
    }
}
