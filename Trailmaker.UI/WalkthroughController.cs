using System;
using System.Collections;
using System.Threading.Tasks;

namespace Trailmaker.UI
{
    public class WalkthroughController
    {
        private IWalkthroughView view;
        private ICollection contents;

        public WalkthroughController(IWalkthroughView view, ICollection contents)
        {
            this.view = view;
            this.contents = contents;
            CurrentPage = 0;
            PageCount = contents.Count;
        }

        public int CurrentPage { get; private set; }

        public int PageCount { get; private set; }

        public bool OnFirstPage { get { return CurrentPage == 0; } }

        public bool OnLastPage { get { return CurrentPage == PageCount - 1; } }

        public bool BackIsVisible { get { return !OnFirstPage; } }

        public bool NextIsVisible { get { return !OnLastPage; } }

        public bool PageIsChanging { get; private set; }

        public async Task ChangePageForwards()
        {
            if (OnLastPage) { return; }

            await ChangePage(true);
        }

        public async Task ChangePageBackwards()
        {
            if (OnFirstPage) { return; }

            await ChangePage(false);
        }

        private async Task ChangePage(bool forwards)
        {
            if (PageIsChanging)
            {
                return;
            }

            if (forwards)
            {
                CurrentPage++;
            }
            else
            {
                CurrentPage--;
            }

            PageIsChanging = true;
            await view.ChangePage(forwards);
            PageIsChanging = false;
        }

        public interface IWalkthroughView
        {
            Task ChangePage(bool forwards);
        }
    }
}
