using CarServiceRepairs.DTO;
using DAL.EF;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarServiceRepairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly CarServiceDbContext _carServiceContext;

        public OrdersController(CarServiceDbContext carServiceDbContext)
        {
            _carServiceContext = carServiceDbContext;
        }
        [HttpGet]
        public IActionResult GetOrders()
        {
            var res = _carServiceContext.Orders.Include(x => x.Auto).ThenInclude( x => x.Owner)
                .Include(x => x.Details).ThenInclude(w => w.Detail).ThenInclude(x => x.Unit)
                .Include(x => x.Worker)
                .Select(q => new
                {
                    q.OrderId,
                    q.Breakage,
                    AdmissionDate = q.AdmissionDate.ToString(),
                    IssueDate = q.IssueDate.ToString(),
                    WarrantyEnd = q.WarrantyEnd.ToString(),
                    Detail = q.Details.Select(x => new
                    {
                        detail = new
                        {
                            x.Detail.DetailId,
                            x.Detail.Name,
                            unit = new
                            {
                                x.Detail.Unit.Name
                            },
                        },
                        x.Amount
                    }),
                    auto = new
                    {
                        q.Auto.AutoId,
                        q.Auto.Model,
                        q.Auto.VinCode,
                        q.Auto.TechPass,
                        owner = new
                        {
                            q.Auto.Owner.OwnerId,
                            q.Auto.Owner.FirstName,
                            q.Auto.Owner.LastName,
                            q.Auto.Owner.Email,
                            q.Auto.Owner.PhoneNumber
                        }
                    },
                    q.Worker


                });
            return Ok(res);
        }
        [HttpPost("/addorder")]
        public IActionResult AddOrder(OrderDTO order)
        {
            var auto = _carServiceContext.Autos.Include(q => q.Owner).First(x => x.AutoId == order.AutoId);
            var worker = _carServiceContext.Workers.First(x => x.WorkerId == order.WorkerId);
            var detail = _carServiceContext.Details.Include(x => x.Unit).First(x => x.DetailId == order.DetailId);

            var res = _carServiceContext.Orders.Add(new Order
            {
                Breakage = order.Breakage,
                AdmissionDate = order.AdmissionDate,
                IssueDate = order.IssueDate,
                WarrantyEnd = order.WarrantyEnd,
                Price = order.Price,
                AutoId = order.AutoId,
                WorkerId = order.WorkerId,

                Auto = auto,
                Worker = worker,
                Details = new List<DetailOrder>() { new DetailOrder
                {
                    Amount = order.Amount,
                    Detail = detail
                } }
            });
            _carServiceContext.SaveChanges();
            return Ok(res.Entity);
        }

        [HttpPost("/updareorder")]
        public IActionResult UpdateOrder(OrderDTOwithId order)
        {
            var entity = _carServiceContext.Orders
                .Include(x => x.Auto)
                .Include(x => x.Worker)
                .Include(x => x.Details)
                .FirstOrDefault(x => x.OrderId == order.Id);

            if(entity != null)
            {
                entity.Breakage = order.Breakage;
                entity.AdmissionDate = order.AdmissionDate;
                entity.IssueDate = order.IssueDate;
                entity.WarrantyEnd = order.WarrantyEnd;
                entity.Price = order.Price;

                entity.AutoId = order.AutoId;
                entity.WorkerId = order.WorkerId;
                entity.Details = new List<DetailOrder>(){new DetailOrder
                {
                    Amount = order.Amount,
                    DetailId = order.DetailId
                } };
            }

            _carServiceContext.Entry(entity).State = EntityState.Modified;
            _carServiceContext.SaveChanges();

            return Ok(entity);

        }
        [HttpPost("/deleteorder")]
        public IActionResult DeleteOrder(int id)
        {
            var orderToDelete = 
                _carServiceContext.Orders
                .Include(x => x.Auto)
                .Include(x => x.Worker)
                .Include(x => x.Details)
                .ToList().Find(x => x.OrderId == id);
            var res = _carServiceContext.Orders.Remove(orderToDelete);
            _carServiceContext.SaveChanges();
            return Ok(res.Entity);

        }
    }
}
