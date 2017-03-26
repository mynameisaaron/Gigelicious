using Gigelicious.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace Gigelicious.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        [Authorize]
        public ActionResult ArtistFollowing()
        {
            var currentUserId = User.Identity.GetUserId();

            var listOfArtists = _context.Follows.Where(f => f.FollowerId == currentUserId).Select(f => f.Followee).ToList();

            return View(listOfArtists);

        }
    }
}