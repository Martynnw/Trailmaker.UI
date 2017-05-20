﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Trailmaker.UI.Demo.Walkthrough
{
    public partial class WalkthroughDemoPage : ContentPage
    {
        public WalkthroughDemoPage()
        {
            InitializeComponent();
            var pages = new List<View>();
            pages.Add(new Content("Page 1"));
            pages.Add(new Content("Page 2"));
            pages.Add(new Content("Page 3"));
            pages.Add(new Content("Page 4"));
            WalkthroughView.SetPages(pages);
        }
    }
}
