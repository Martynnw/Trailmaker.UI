using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Trailmaker.UI
{
    [ContentProperty("Pages")]
    public partial class Walkthrough : ContentView
    {
        public static BindableProperty ShowBackProperty = BindableProperty.Create("ShowBack", typeof(bool), typeof(Walkthrough), true);
        public static BindableProperty BackTextProperty = BindableProperty.Create("BackText", typeof(string), typeof(Walkthrough), "Back");
        public static BindableProperty ShowNextProperty = BindableProperty.Create("ShowNext", typeof(bool), typeof(Walkthrough), true);
        public static BindableProperty NextTextProperty = BindableProperty.Create("NextText", typeof(string), typeof(Walkthrough));


        private int currentPage = 0;
        private int lastPage = 0;
        private double panX;
        private bool pageIsChanging = false;

        public Walkthrough()
        {
            InitializeComponent();
            PagesLayout.SizeChanged += (sender, e) => { PositionPages(); };
        }

        public void SetPages(List<View> pages)
        {
            PagesLayout.Children.Clear();
            currentPage = 0;
            lastPage = pages.Count - 1;

            foreach(var view in pages)
            {
                PagesLayout.Children.Add(view);
            }

            PositionPages();
            AddProgress();
            ShowControls();
        }

        private async void Next_Tapped(object sender, System.EventArgs e)
        {
            await ChangePageForwards();
        }

		private async void Back_Tapped(object sender, System.EventArgs e)
		{
            await ChangePageBackwards();
		}

        private async Task ChangePageForwards()
        {
            if (currentPage == lastPage || pageIsChanging)
            {
                return;                
            }

            pageIsChanging = true;
            currentPage++;
            await RunPageChange(Width * -1);
            ShowControls();
            pageIsChanging = false;
        }

        private async Task ChangePageBackwards()
        {
            if (currentPage == 0 || pageIsChanging)
            {
                return;
            }

            pageIsChanging = true;
            currentPage--;
            await RunPageChange(Width);
            ShowControls();
            pageIsChanging = false;
        }

        private async Task RunPageChange(double translationChange)
        {
            var tasks = new List<Task>();

            foreach (var view in PagesLayout.Children)
            {
                tasks.Add(view.TranslateTo(view.TranslationX + translationChange, 0, easing: Easing.CubicOut));
            }

            await Task.WhenAll(tasks.ToArray());
        }

		private void ShowControls()
        {
			LabelBack.IsVisible = currentPage > 0;
        	LabelNext.IsVisible = currentPage < PagesLayout.Children.Count - 1;

            for (int i = 0; i < LayoutProgress.Children.Count; i++)
            {
                if (i == currentPage)
                {
                    LayoutProgress.Children[i].Opacity = 1;
                }
                else
                {
                    LayoutProgress.Children[i].Opacity = 0.5;
				}
            }
        }

		private void PositionPages()
        {
            var x = 0d;

            foreach (var view in PagesLayout.Children)
            {
                view.TranslationX = x;
                x += Width;
            }
        }

        private void AddProgress()
        {
            LayoutProgress.Children.Clear();

            for (int i = 0; i < PagesLayout.Children.Count; i++)
            {
                var image = new Image();
                image.Source = ImageSource.FromResource("Trailmaker.UI.Images.circlefull.png");
                image.Opacity = 0.6;

                LayoutProgress.Children.Add(image);
            }
        }

        void Handle_PanUpdated(object sender, Xamarin.Forms.PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    panX = 0;
                    break;

                case GestureStatus.Running:
                    panX = e.TotalX;
                    break;

                case GestureStatus.Completed:
                    if (panX > 0)
                    {
                        ChangePageBackwards();
                    }
                    else
                    {
                        ChangePageForwards();
                    }

                    break;
            }
        }
    }
}
