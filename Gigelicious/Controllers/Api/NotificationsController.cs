using AutoMapper;
using Gigelicious.Dto;
using Gigelicious.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Gigelicious.Controllers.Api
{



    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;
        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize]
        public IHttpActionResult GetNotifications()
        {
            var loggedIn = User.Identity.GetUserId();
            var listOfNotificationDto = _context.UserNotifications
                .Where(un => un.ApplicationUserId == loggedIn && un.IsRead == false)
                .Select(n => n.Notification)
                .Include(g=>g.Gig.Artist)
                .ToList().Select(Mapper.Map<Notification,NotificationDto>);
            return Ok(listOfNotificationDto);
            



        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult UserNotificationsIsRead()
        {
            var user = User.Identity.GetUserId();
            var usersNotifications =
                _context.UserNotifications.Where(u => u.ApplicationUserId == user && u.IsRead == false).ToList();

            usersNotifications.ForEach(un => un.Read());
            
            _context.SaveChanges();

            return Ok();
        }
    }
}
