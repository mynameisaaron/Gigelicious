using System;
using Gigelicious.Models;

namespace Gigelicious.Dto
{
    public class NotificationDto
    {

        public int Id { get;  set; }
        public DateTime DateTime { get;  set; }
        public NotificationType Type { get;  set; }
        public DateTime? OriginalDateTime { get;  set; }
        public string OriginalVenue { get;  set; }
        public GigDto Gig { get;  set; }
    }
}