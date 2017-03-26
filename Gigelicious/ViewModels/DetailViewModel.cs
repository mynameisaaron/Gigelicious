using Gigelicious.Models;

namespace Gigelicious.ViewModels
{
    public class DetailViewModel
    {
        public Gig Gig { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsGoing { get; set; }
        public bool IsAuthenticated { get; set; }

        public DetailViewModel()
        {
            
        }
        public DetailViewModel(Gig gig)
        {
            Gig = gig;

        }

        public DetailViewModel(Gig gig,bool isfollowing, bool isgoing, bool isauthenticated)
        {
            Gig = gig;
            IsFollowing = isfollowing;
            IsGoing = isgoing;
            IsAuthenticated = isauthenticated;
        }
    }
}