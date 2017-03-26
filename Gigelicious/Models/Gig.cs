using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Gigelicious.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCancelled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Attendance> Attendances { get;private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            this.IsCancelled = true;

            var notification = Notification.GigCancelled(this);


            foreach (var atendee in this.Attendances.Select(a => a.Attendee))
            {
                atendee.Notify(notification);
            }
        }

        public void Modify(DateTime getDateTime, string viewModelVenue, byte viewModelGenre)
        {
            var notification = Notification.GigUpdated(this, getDateTime,viewModelVenue);
            

            Venue = viewModelVenue;
            DateTime = getDateTime;
            GenreId = viewModelGenre;

            foreach (var attendee in Attendances.Select(a=>a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}
