using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace Trailmaker.UI.Demo
{
    public partial class FlowLayoutDemoPage : ContentPage
    {
        private Random random = new Random();

        public FlowLayoutDemoPage()
        {
            InitializeComponent();

            PaddingStepper.Value = FlowLayout1.Padding.Left;
            SpacingStepper.Value = FlowLayout1.Spacing.Left;

            for (int i = 0; i < 15; i++)
            {
                FlowLayout1.Children.Add(CreateRandomBoxview());
            }
        }

        private BoxView CreateRandomBoxview()
        {
            var view = new BoxView();
            view.WidthRequest = random.Next(40, 100);
            view.HeightRequest = random.Next(40, 100);
            view.Color = Color.FromRgb(random.NextDouble(), random.NextDouble(), random.NextDouble());

            return view;
        }

        private void Spacing_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            FlowLayout1.Spacing = SpacingStepper.Value;
        }

        private void Padding_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
	        FlowLayout1.Padding = PaddingStepper.Value;
        }

        private async void ResizeChild_Clicked(object sender, System.EventArgs e)
        {
            var child = GetRandomVisibleChild();
            var originalColor = child.Color;
            child.Color = Color.Yellow;
            await Task.Delay(500);
            child.HeightRequest = random.Next(40, 100);
            child.WidthRequest = random.Next(40, 100);
            await Task.Delay(500);
            child.Color = originalColor;
        }

        private async void HideChild_Clicked(object sender, System.EventArgs e)
        {
            var child = GetRandomVisibleChild();
            child.Color = Color.Red;
            await Task.Delay(500);
            child.IsVisible = false;
        }

        private BoxView GetRandomVisibleChild()
        {
            var visibleChildren = FlowLayout1.Children.Where(x => x.IsVisible).ToList();
            int index = random.Next(visibleChildren.Count());
            return (BoxView)visibleChildren[index];
        }
    }
}
