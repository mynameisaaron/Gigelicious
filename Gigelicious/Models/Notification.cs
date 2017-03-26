using System;
using System.ComponentModel.DataAnnotations;

namespace Gigelicious.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {
            //for EntityFramework
        }


        private Notification(Gig gig, NotificationType notificationType)
        {
            if(gig == null)
                throw new ArgumentNullException("gig");
            if(notificationType == null)
                throw new ArgumentNullException("notificationType");

            Type = notificationType;
            Gig = gig;
            DateTime = DateTime.Now;

        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }
        public static Notification GigUpdated(Gig newGig, DateTime origiDateTime, string originalVenue)
        {
            var notification = new Notification(newGig, NotificationType.GigUpdated);
            notification.OriginalVenue = originalVenue;
            notification.OriginalDateTime = origiDateTime;
            return notification;
        }
        public static Notification GigCancelled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCancelled);
        }


    }
}