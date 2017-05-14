using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trailmaker.UI.Demo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var items = GetMenuItems();
            Menu.ItemsSource = items;
        }

        private List<MenuItem> GetMenuItems()
        {
            var items = new List<MenuItem>();

            items.Add(new MenuItem("Flow Layout", "", typeof(FlowLayoutDemoPage)));
            items.Add(new MenuItem("Repeater", "", typeof(RepeaterDemoPage)));
            items.Add(new MenuItem("Toolbar", "", typeof(ToolbarDemoPage)));

            return items;
        }

        private void Menu_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (Menu.SelectedItem != null)
            {
                var menuItem = (MenuItem)Menu.SelectedItem;
                ShowPage(menuItem.PageType);
                Menu.SelectedItem = null;
            }
        }

        private void ShowPage(Type pageType)
        {
            var page = Activator.CreateInstance(pageType);
            Navigation.PushAsync((Page)page);
        }

        private class MenuItem
        {
            public MenuItem(string title, string detail, Type pageType)
            {
                Title = title;
                Detail = detail;
                PageType = pageType;
            }

            public string Title { get; set; }
            public string Detail { get; set; }
            public Type PageType { get; set; }
        }
    }
}
