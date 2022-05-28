using CarServiceRepairs.DTO;
using DAL.EF;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarServiceRepairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly CarServiceDbContext _carServiceContext;

        public WorkersController(CarServiceDbContext carServiceDbContext)
        {
            _carServiceContext = carServiceDbContext;
        }

        [HttpGet]
        public IActionResult GetWorkers()
        {
            return Ok(_carServiceContext.Workers);
        }
        [HttpPost]
        [Route("/addoworker")]
        public IActionResult AddWorker(WorkerDTO worker)
        {
            var res = _carServiceContext.Workers.Add(new Worker
            {
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Email = worker.Email,
                PhoneNumber = worker.PhoneNumber,
                Specialisation = worker.Specialisation
            });
            _carServiceContext.SaveChanges();
            return Ok(res.Entity);
        }

        [HttpPost]
        [Route("/updateworker")]
        public IActionResult UpdateWorker(WorkerDTOwithId worker)
        {
            var entity = _carServiceContext.Workers.FirstOrDefault(x => x.WorkerId == worker.Id);

            if (entity != null)
            {
                entity.FirstName = worker.FirstName;
                entity.LastName = worker.LastName;
                entity.Email = worker.Email;
                entity.PhoneNumber = worker.PhoneNumber;
                entity.Specialisation = worker.Specialisation;
            }
            _carServiceContext.Entry(entity).State = EntityState.Modified;
            _carServiceContext.SaveChanges();

            return Ok(entity);
        }

        [HttpPost]
        [Route("/deleteworker")]
        public IActionResult DeleteWorker(int id)
        {
            var workerToDelete = _carServiceContext.Workers.ToList().Find(x => x.WorkerId== id);
            var res = _carServiceContext.Workers.Remove(workerToDelete);
            _carServiceContext.SaveChanges();
            return Ok(res.Entity);

        }

    }
}
