using CarServiceRepairs.DTO;
using DAL.EF;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarServiceRepairs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly CarServiceDbContext _carServiceContext;

        public OwnersController(CarServiceDbContext carServiceDbContext)
        {
            _carServiceContext = carServiceDbContext;
        }

        [HttpGet]
        public IActionResult GetOwners()
        {
            return Ok(_carServiceContext.Owners.Include(x => x.Autos));
        }
        [HttpPost]
        [Route("/addowner")]
        public IActionResult AddOwner(OwnerDTO owner)
        {
            var res = _carServiceContext.Owners.Add(new Owner
            {
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email,
                PhoneNumber = owner.PhoneNumber
            });
            _carServiceContext.SaveChanges();
            return Ok(res.Entity) ;
        }

        [HttpPost]
        [Route("/updateowner")]
        public IActionResult UpdateOwner(OwnerDTOwithId owner)
        {
            var entity = _carServiceContext.Owners.Include(x => x.Autos).FirstOrDefault(x => x.OwnerId == owner.Id);

            if(entity != null)
            {
                entity.FirstName = owner.FirstName;
                entity.LastName = owner.LastName;
                entity.Email = owner.Email;
                entity.PhoneNumber = owner.PhoneNumber;
            }
            _carServiceContext.Entry(entity).State = EntityState.Modified;
            _carServiceContext.SaveChanges();

            return Ok(entity);
        }

        [HttpPost]
        [Route("/deleteowner")]
        public IActionResult DeleteOwner(int id)
        {
            var ownerToDelete = _carServiceContext.Owners.Include(x => x.Autos).ToList().Find(x => x.OwnerId == id);
            var res = _carServiceContext.Owners.Remove(ownerToDelete);
            _carServiceContext.SaveChanges();
            return Ok(res.Entity);

        }
    }
}
