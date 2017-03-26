using Gigelicious.Models;
using Gigelicious.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Gigelicious.Controllers
{
    public class GigsController : Controller
    {

        private readonly ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //Both Create and Update Use the Create.shtml
        //Proper name should be GigForm

        [Authorize]        
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Heading = "Create a New Gig",
                Genres = _context.Genres.ToList()

            };

            return View("GigForm",viewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View(viewModel);
            }
            

            Gig gig = new Gig()
            {
                Venue = viewModel.Venue,
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre

            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");





            


        }




        [Authorize]        
        public ActionResult Update(int id)
        {
            var currentUser = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == currentUser);


            var viewModel = new GigFormViewModel()
            {
                Id = gig.Id,
                Venue = gig.Venue,
                Date = gig.DateTime.ToString("dd MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Genres = _context.Genres.ToList(),
                Heading = "Update Gig"
            };

            return View("GigForm",viewModel);
        }

        //old copy of update gig
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Update(GigFormViewModel viewModel, int id)
        //{
        //    int Id = id;
        //    if (!ModelState.IsValid)
        //    {
        //        viewModel.Genres = _context.Genres.ToList();
        //        return View("GigForm",viewModel);
        //    }

        //    var logedUser = User.Identity.GetUserId();
        //    var gig = _context.Gigs.Single(g => g.Id == viewModel.Id && logedUser == g.ArtistId);

        //    gig.DateTime = viewModel.GetDateTime();
        //    gig.Venue = viewModel.Venue;
        //    gig.GenreId = viewModel.Genre;



        //    _context.SaveChanges();
        //    return RedirectToAction("Mine", "Gigs");
        //    }


        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UpdateGig(GigFormViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        viewModel.Genres = _context.Genres.ToList();
        //        return View("GigUpdateForm",viewModel);
        //    }


        //    var logedUser = User.Identity.GetUserId();
        //    var gig = _context.Gigs.Single(g => g.Id == viewModel.Id && logedUser == g.ArtistId);

        //    gig.DateTime = viewModel.GetDateTime();
        //    gig.Venue = viewModel.Venue;
        //    gig.GenreId = viewModel.Genre; 

        //    _context.SaveChanges();

        //    return RedirectToAction("Mine", "Gigs");








        //}


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel, int id)
        {
            int Id = id;
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var logedUser = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(a=>a.Attendances.Select(b=>b.Attendee))
                .Single(g => g.Id == viewModel.Id && logedUser == g.ArtistId);


            gig.DateTime = viewModel.GetDateTime();
            gig.Venue = viewModel.Venue;
            gig.GenreId = viewModel.Genre;

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);


            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }


        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigsAttending = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g=>g.Artist)
                .Include(g=>g.Genre)
                .ToList();

            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);

            var viewModel = new GigViewModel()
            {
                IsAuthenticated = true,
                UpCominGigs = gigsAttending,
                Heading = "Gigs I'm Attending",
                Attendences = attendances
            };

            return View("Gigs",viewModel);



        }

        [Authorize]
        public ActionResult Mine()
        {
            var currentUser = User.Identity.GetUserId();
            var listOfGigs = _context.Gigs.Where(g => g.ArtistId == currentUser && g.DateTime > DateTime.Now && g.IsCancelled == false).Include(g=>g.Genre).ToList();


            return View(listOfGigs);





        }

        [HttpPost]
        public ActionResult Search(GigViewModel gigViewModel)
        {
            return RedirectToAction("Index", "Home", new {query = gigViewModel.Search});

        }
    }
}