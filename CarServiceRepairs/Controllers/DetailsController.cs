using CarServiceRepairs.DTO;
using DAL.EF;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarServiceRepairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly CarServiceDbContext _carServiceContext;

        public DetailsController(CarServiceDbContext carServiceDbContext)
        {
            _carServiceContext = carServiceDbContext;
        }
        [HttpGet]
        public IActionResult GetDetails()
        {
            return Ok(_carServiceContext.Details.Select(x => new
            {
                x.DetailId,
                x.Name,
                x.Amount,
                unit = new
                {
                    x.Unit.Name
                }
            }));
        }

        [HttpPost("/adddetail")]
        public IActionResult AddDetail(DetailDTO detail)
        {
            var unit = _carServiceContext.Units.Include(x => x.Details).ToList().First(x => x.UnitId == detail.UnitId);


            var res = _carServiceContext.Details.Add(new Detail
            {
                UnitId = detail.UnitId,
                Name = detail.Name,
                Amount = detail.Amount,
                Unit = unit,
            });
            _carServiceContext.SaveChanges();
            return Ok(res.Entity);
        }

        [HttpPost("/updatedetail")]
        public IActionResult UpdateDetail(DetailDTOwithId detail)
        {
            var entity = _carServiceContext.Details.Include(x => x.Unit).Include(x => x.Orders).FirstOrDefault(
                x => x.DetailId == detail.Id);

            if (entity != null)
            { 
                entity.Name = detail.Name;
                entity.Amount = detail.Amount;
                entity.UnitId = detail.UnitId;
            }
            _carServiceContext.Entry(entity).State = EntityState.Modified;
            _carServiceContext.SaveChanges();

            return Ok(entity);

        }

        [HttpPost("/deletedetail")]
        public IActionResult DeleteDetail(int id)
        {
            var detailToDelete = _carServiceContext.Details.Include(x => x.Unit).Include(x => x.Orders).ToList().Find(x => x.DetailId == id);
            var res = _carServiceContext.Details.Remove(detailToDelete);
            _carServiceContext.SaveChanges();
            return Ok(res.Entity);

        }
    }
}
