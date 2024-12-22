using AutoMapper;
using BookStore.DTOs.OrderDTOs;
using BookStore.Models;
using BookStore.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IMapper mapper;
        UnitOfWork unitOfWork;

        public OrdersController(UnitOfWork _unitOfWork, IMapper mapper)
        {
            this.unitOfWork = _unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Add(AddOrderDTO _order)
        {
            Order baicorderinfo = new Order()
            {
                cust_id = _order.cust_id,
                OrderDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                Status = "created"

            };
            unitOfWork.orderGenericRepository.add(baicorderinfo);
            unitOfWork.save();
            decimal totalprice = 0;
            foreach (var item in _order.books)
            {
                Book b = unitOfWork.bookGenericRepository.selectbyid(item.book_id);
                totalprice = totalprice + (b.Price * item.Quantity);
                OrderDetails _details = new OrderDetails()
                {
                    order_id = baicorderinfo.Id,
                    book_id = item.book_id,
                    Quantity = item.Quantity,
                    UnitPrice = b.Price,
                };
                if (b.Stock > _details.Quantity)
                {
                    unitOfWork.orderDetailsGenericRepository.add(_details);
                    //baicorderinfo.OrderDetails.Add(_details);

                    b.Stock -= item.Quantity;
                    unitOfWork.bookGenericRepository.edit(b);
                    unitOfWork.save();
                }
                else
                {
                    unitOfWork.orderGenericRepository.delete(baicorderinfo.Id);
                    unitOfWork.save();
                    return BadRequest("invalid quantity");
                }
            }
            baicorderinfo.TotalPrice = totalprice;
            unitOfWork.orderGenericRepository.edit(baicorderinfo);
            unitOfWork.save();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Order order = unitOfWork.orderGenericRepository.selectbyid(id);
            if (order == null) return NotFound();
            return Ok(mapper.Map<DisplayOrderDTO>(order));
        }
        [HttpPut("{id}")]
        public IActionResult EditStatus(int id,EditOrderDTO orderDTO)
        {
            Order order = unitOfWork.orderGenericRepository.selectbyid(id);
            if (order == null) return NotFound();
            order.Status = orderDTO.Status;
            unitOfWork.save();
            return Ok();
        }
    }
}
