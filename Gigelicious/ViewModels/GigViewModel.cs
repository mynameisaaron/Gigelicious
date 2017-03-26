using Gigelicious.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gigelicious.ViewModels
{
    public class GigViewModel
    {
        public IEnumerable<Gig> UpCominGigs { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Heading { get; set; }
        public string Search { get; set; }
        public ILookup<int,Attendance> Attendences { get; set; }
        public IEnumerable<string> Follows { get; set; }
    }
}