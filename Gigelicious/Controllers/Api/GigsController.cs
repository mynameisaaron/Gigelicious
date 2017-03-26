using Gigelicious.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Gigelicious.Controllers.Api
{
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        
        [HttpDelete]
        [Authorize]
        public IHttpActionResult Cancel(int id)
        {
            var currentUser = User.Identity.GetUserId();
            var gigToCancel = _context.Gigs.Include(g=>g.Attendances.Select(a=>a.Attendee)).Single(g => g.Id == id && g.ArtistId == currentUser);

            if (gigToCancel.IsCancelled)
                return NotFound();
            
            gigToCancel.Cancel();

            _context.SaveChanges();

            return Ok();
          }
       }
    }
