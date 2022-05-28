using CarServiceRepairs.DTO;
using DAL.EF;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarServiceRepairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly CarServiceDbContext _carServiceContext;
        public UnitsController(CarServiceDbContext carServiceDbContext)
        {
            _carServiceContext = carServiceDbContext;
        }
        [HttpGet]
        public IActionResult GetUnits()
        {
            var units = _carServiceContext.Units.Select(x => new
            {
                x.UnitId,
                x.Name
            });   
            return Ok(units);
        }

        [HttpPost("/addunit")]
        public IActionResult AddUnit(string name)
        {
            var res = _carServiceContext.Units.Add(new Unit
            {
                Name = name,
            });

            _carServiceContext.SaveChanges();
            return Ok(res.Entity);
        }

        [HttpPost("/updateunit")]
        public IActionResult UpdateUnit(UnitDTO unit)
        {
            var entity = _carServiceContext.Units.FirstOrDefault(x => x.UnitId == unit.UnitId);

            if (entity != null)
            {
                entity.Name = unit.Name;
            }
            _carServiceContext.Entry(entity).State = EntityState.Modified;
            _carServiceContext.SaveChanges();

            return Ok(entity);
        }

        [HttpPost("/deleteunit")]
        public IActionResult DeleteUnit(int id)
        {
            var unitToDelete = _carServiceContext.Units.Include(x => x.Details).ToList().Find(x => x.UnitId == id);
            var res = _carServiceContext.Units.Remove(unitToDelete);
            _carServiceContext.SaveChanges();
            return Ok(res.Entity);
        }
    }
}
