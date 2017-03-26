using Gigelicious.Dto;
using Gigelicious.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace Gigelicious.Controllers.Api
{
    [Authorize]
    public class AttendanceController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendanceController()
        {
            _context = new ApplicationDbContext();
            
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // /Api/Attend  POST gigID into body 
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
                return BadRequest("This Attendance is already in the database");



            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();

        }

        
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            var user = User.Identity.GetUserId();
            var attendanceToRemove = _context.Attendances.Single(a => a.AttendeeId == user && a.GigId == id);

            _context.Attendances.Remove(attendanceToRemove);
            _context.SaveChanges();
            return Ok();
        }
    }
}
