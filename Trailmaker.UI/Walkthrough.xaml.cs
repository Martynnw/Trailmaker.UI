using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Trailmaker.UI
{
    [ContentProperty("Pages")]
    public partial class Walkthrough : ContentView
    {
        private double panX;
        private bool pageIsChanging = false;

        public Walkthrough()
        {
            InitializeComponent();
            ProgressMarker = ImageSource.FromResource("Trailmaker.UI.Images.circlefull.png");
            PagesLayout.SizeChanged += (sender, e) => { PositionPages(); };
        }

        public event EventHandler PageChanged;

        public bool ShowBack
        {
            get { return LabelBack.IsVisible; }
            set { LabelBack.IsVisible = value ; }
        }

        public string BackText
        {
            get { return LabelBack.Text; }
            set { LabelBack.Text = value; }
        }

        public bool ShowNext
        {
            get { return LabelNext.IsVisible; }
            set { LabelNext.IsVisible = value; }
        }

        public Thickness ControlsPadding 
        {
            get { return ControlsLayout.Padding; }
            set { ControlsLayout.Padding = value; }
        }

        public ImageSource ProgressMarker { get; set; }

        public int CurrentPage { get; private set; }

        public int PageCount 
        {
            get { return PagesLayout.Children.Count; }
        }

        public void SetPages(List<View> pages)
        {
            PagesLayout.Children.Clear();
            CurrentPage = 0;

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

        public async Task ChangePageForwards()
        {
            if (CurrentPage == PageCount - 1 || pageIsChanging)
            {
                return;                
            }

            pageIsChanging = true;
            CurrentPage++;
            await RunPageChange(Width * -1);
            ShowControls();
            pageIsChanging = false;
            PageChanged?.Invoke(this, new EventArgs());
        }

        public async Task ChangePageBackwards()
        {
            if (CurrentPage == 0 || pageIsChanging)
            {
                return;
            }

            pageIsChanging = true;
            CurrentPage--;
            await RunPageChange(Width);
            ShowControls();
            pageIsChanging = false;
            PageChanged?.Invoke(this, new EventArgs());
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
			LabelBack.IsVisible = CurrentPage > 0;
        	LabelNext.IsVisible = CurrentPage < PagesLayout.Children.Count - 1;

            for (int i = 0; i < LayoutProgress.Children.Count; i++)
            {
                if (i == CurrentPage)
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
                image.Source = ProgressMarker;
                LayoutProgress.Children.Add(image);
            }
        }

        private async void Handle_PanUpdated(object sender, Xamarin.Forms.PanUpdatedEventArgs e)
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
                        await ChangePageBackwards();
                    }
                    else
                    {
                        await ChangePageForwards();
                    }

                    break;
            }
        }
    }
}
