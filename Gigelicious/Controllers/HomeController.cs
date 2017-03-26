using Gigelicious.Models;
using Gigelicious.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Gigelicious.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }








        public ActionResult Index(string query = null)
        {

            


            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g=>g.Genre)
                .Where(g => g.DateTime > DateTime.Now && g.IsCancelled == false);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) || g.Genre.Name.Contains(query) || g.Venue.Contains(query));
            }


            var viewModel = new GigViewModel()
            {
                UpCominGigs = upcomingGigs.ToList(),
                IsAuthenticated = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs"

            };

            if (viewModel.IsAuthenticated)
            {
                var useId = User.Identity.GetUserId();
                var userAttendences =
                    _context.Attendances.Where(a => a.AttendeeId == useId).ToList().ToLookup(l => l.GigId);
                viewModel.Attendences = userAttendences;


                viewModel.Follows =
                    _context.Follows.Where(f => f.FollowerId == useId).Select(a => a.FolloweeId).ToList();
            }





            return View("Gigs", viewModel);
        }

        public ActionResult Detail(int id)
        {

            var returnViewModel = new DetailViewModel();
            var gig = _context.Gigs.Include(g => g.Artist).Include(g => g.Genre).SingleOrDefault(g => g.Id == id);
            if (gig == null)
                return HttpNotFound();

            var isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                var currentUser = User.Identity.GetUserId();
                var isFollow = _context.Follows.Any(f => f.FollowerId == currentUser && f.FolloweeId == gig.ArtistId);
                var isAttend = _context.Attendances.Any(a => a.GigId == gig.Id && a.AttendeeId == currentUser);
                returnViewModel = new DetailViewModel(gig, isFollow, isAttend, isAuthenticated);
            }
            else
            {
                returnViewModel = new DetailViewModel(gig);
            }

            

            

            return View(returnViewModel);


        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        


    }
}