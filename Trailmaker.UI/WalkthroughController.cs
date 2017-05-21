using System;
namespace Trailmaker.UI
{
    public class WalkthroughController
    {
        public WalkthroughController()
        {
        }

        public event EventHandler PageChanged;

        public int CurrentPage { get; private set; }

        public int PageCount { get; private set; }

        public bool OnFirstPage { get; private set; }

        public bool OnLastPage { get; private set; }



        public void ChangePageForwards()
        {
        }

        public void ChangePageBackwards()
        {
        }
    }
}
