using CarServiceRepairs.DTO;
using DAL.EF;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarServiceRepairs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutosController : ControllerBase
    {
        private readonly CarServiceDbContext _context;
        public AutosController(CarServiceDbContext carServiceDbContext)
        {
            _context = carServiceDbContext;
        }
        [HttpGet]
        public IActionResult GetAutos()
        {
            return Ok(_context.Autos.Include(x => x.Owner));
        }
        [HttpPost]
        [Route("/add")]
        public IActionResult AddCar(AutoDTO auto)
        {
            var owner = _context.Owners.Include(x => x.Autos).ToList().First(x => x.OwnerId == auto.OwnerId);
            var car = new Auto
            {
                OwnerId = auto.OwnerId,
                Model = auto.Model,
                VinCode = auto.VinCode,
                TechPass = auto.TechPass,
                Owner = owner
            };
            var res = _context.Autos.Add(car);
            _context.SaveChanges();
            return Ok(res.Entity);;
        }
        [HttpPost]
        [Route("/delete")]
        public IActionResult DeleteAuto(int id)
        {
            var carToDelete = _context.Autos.Include(x => x.Owner).ToList().Find(x => x.AutoId == id);
            var res = _context.Autos.Remove(carToDelete);
            _context.SaveChanges();
            return Ok(res.Entity);
        }

        [HttpPost]
        [Route("/update")]
        public IActionResult UpdateCar(AutoDTOwithId auto)
        {
            var owner = _context.Owners.Include(x => x.Autos).ToList().First(x => x.OwnerId == auto.OwnerId);
            var entity = _context.Autos.Include(x => x.Owner).FirstOrDefault(x => x.AutoId == auto.Id);

            if(entity != null)
            {
                entity.AutoId = auto.Id;
                entity.OwnerId = auto.OwnerId;
                entity.Model = auto.Model;
                entity.VinCode = auto.VinCode;
                entity.TechPass = auto.TechPass;
                entity.Owner = owner;
            };
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(entity); ;

        }
    }
}