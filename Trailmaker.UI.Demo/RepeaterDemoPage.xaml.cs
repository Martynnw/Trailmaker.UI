using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trailmaker.UI.Demo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RepeaterDemoPage : ContentPage
    {
        private Random random = new Random();
        private ObservableCollection<RepeaterItem> items = new ObservableCollection<RepeaterItem>();

        public RepeaterDemoPage()
        {
            InitializeComponent();
            SetItemsSource();
        }

        private void SetItemsSource()
        {
            items = GetRandomItems();
            MainRepeater.ItemsSource = items;
        }

        private ObservableCollection<RepeaterItem> GetRandomItems()
        {
            var count = random.Next(20);
            var result = new ObservableCollection<RepeaterItem>();

            for (int i = 0; i < count; i++)
            {
                var item = GetRandomItem();
                result.Add(item);
            }

            return result;
        }

        private RepeaterItem GetRandomItem()
        {
            var itemId = random.Next(1000);
            var item = new RepeaterItem(itemId);
            return item;
        }

        private class RepeaterItem
        {
            public RepeaterItem(int id)
            {
                Title = $"Item {id}";
            }

            public string Title { get; set; }
        }

        private void ChangeSource_OnClicked(object sender, EventArgs e)
        {
            SetItemsSource();   
        }

        private void AddItem_OnClicked(object sender, EventArgs e)
        {
            var newItem = GetRandomItem();
            items.Add(newItem);
        }
    }
}
