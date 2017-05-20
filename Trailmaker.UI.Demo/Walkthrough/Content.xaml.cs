using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Trailmaker.UI.Demo.Walkthrough
{
    public partial class Content : ContentView
    {
        private const string ImageUri = "http://lorempixel.com/400/400/nature";

        public Content(string title)
        {
            InitializeComponent();
            TitleLabel.Text = title;
            var uri = new Uri(ImageUri);
            MainImage.Source = new UriImageSource { CachingEnabled = false, Uri = uri };
        }
    }
}
