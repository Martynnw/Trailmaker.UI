using System;
using Xamarin.Forms;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Trailmaker.UI
{
    public class Repeater : StackLayout
    {
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(Repeater), null);
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(Repeater), propertyChanging: ItemsSourceChanging);

        public Repeater()
        {
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)this.GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        private static void ItemsSourceChanging(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != null && oldValue is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)oldValue).CollectionChanged -= ((Repeater)bindable).OnCollectionChanged;
            }

            if (newValue != null && newValue is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)newValue).CollectionChanged += ((Repeater)bindable).OnCollectionChanged;
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ItemTemplateProperty.PropertyName || propertyName == ItemsSourceProperty.PropertyName)
            {
                this.Populate();
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            Populate();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.Populate();
        }

        public void Populate()
        {
            if (this.ItemsSource != null)
            {
                this.Children.Clear();

                foreach (var item in this.ItemsSource)
                {
                    var content = this.ItemTemplate.CreateContent();
                    var viewCell = content as ViewCell;


                    if (viewCell != null)
                    {
                        this.Children.Add(viewCell.View);
                        viewCell.BindingContext = item;
                    }
                }
            }
        }
    }
}
