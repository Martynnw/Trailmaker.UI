using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Trailmaker.UI
{
    public partial class ToolBar : ContentView
    {
        public ToolBar()
        {
            InitializeComponent();
            BackImage.Source = ImageSource.FromResource(Images.ImageId.BackLight);
            MenuImage.Source = ImageSource.FromResource(Images.ImageId.HamburgerLight);
            TitleLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, TitleLabel.GetType()) * 1.1;
        }

        private void MenuImage_Tapped(object sender, System.EventArgs e)
        {
            var options = new List<ToolbarMenuPage.OptionItem> { new ToolbarMenuPage.OptionItem(1, "Foo" ), new ToolbarMenuPage.OptionItem(1, "Bar"), new ToolbarMenuPage.OptionItem(1, "Foobar") };

            var menuPage = new ToolbarMenuPage(options);
            menuPage.OnOptionSelected = (obj) => {};
            Navigation.PushModalAsync(menuPage);
        }
    }
}
