using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Trailmaker.UI
{
    [ContentProperty("Pages")]
    public partial class Walkthrough : ContentView, WalkthroughController.IWalkthroughView
    {
        private WalkthroughController controller;
        private double panX;
        private bool userShowBack = true;
        private bool userShowNext = true;

        public Walkthrough()
        {
            InitializeComponent();
            ProgressMarker = ImageSource.FromResource("Trailmaker.UI.Images.circlefull.png");
            PagesLayout.SizeChanged += (sender, e) => { PositionPages(); };
            controller = new WalkthroughController(this, new List<View> { });
            ShowControls();
        }

        public event EventHandler PageChanged;

        public bool ShowBack
        {
            get { return userShowBack; }
            set { userShowBack = value; }
        }

        public string BackText
        {
            get { return LabelBack.Text; }
            set { LabelBack.Text = value; }
        }

        public bool ShowNext
        {
            get { return userShowNext; }
            set { userShowNext = value; }
        }

        public Thickness ControlsPadding
        {
            get { return ControlsLayout.Padding; }
            set { ControlsLayout.Padding = value; }
        }

        public ImageSource ProgressMarker { get; set; }

        public int CurrentPage { get { return controller.CurrentPage; } }

        public int PageCount { get { return controller.PageCount; } }

        public void SetPages(List<View> pages)
        {
            PagesLayout.Children.Clear();
            controller = new WalkthroughController(this, pages);

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
            await controller.ChangePageForwards();
        }

        private async void Back_Tapped(object sender, System.EventArgs e)
        {
            await controller.ChangePageBackwards();
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
                        await controller.ChangePageBackwards();
                    }
                    else
                    {
                        await controller.ChangePageForwards();
                    }

                    break;
            }
        }

        public async Task ChangePage(bool forwards)
        {
            if (forwards)
            {
                await RunPageChange(Width * -1);
            }
            else
            {
                await RunPageChange(Width);
            }

            ShowControls();
        }

        private void ShowControls()
        {
            LabelBack.IsVisible = userShowBack && controller.BackIsVisible;
            LabelNext.IsVisible = userShowNext && controller.NextIsVisible;
            UpdateProgress();
        }

        private void UpdateProgress()
		{
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
    }
}
