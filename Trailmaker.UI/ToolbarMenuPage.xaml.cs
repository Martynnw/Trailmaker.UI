using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Trailmaker.UI
{
    public partial class ToolbarMenuPage : ContentPage
    {
        private Command optionSelectedCommand;

        public ToolbarMenuPage(IEnumerable<OptionItem> options)
        {
            InitializeComponent();

            optionSelectedCommand = new Command<OptionItem>(OptionSelected);
            PopulateOptions(options);
        }

        public Action<OptionItem> OnOptionSelected;

        private void PopulateOptions(IEnumerable<OptionItem> options)
        {
            foreach (var option in options)
            {
                var optionCell = CreateOption(option);
                OptionSection.Add(optionCell);
            }
        }

        private TextCell CreateOption(OptionItem option)
        {
            var textCell = new TextCell();
            textCell.Text = option.Text;
            textCell.Command = optionSelectedCommand;
            textCell.CommandParameter = option;
            return textCell;
        }

        private void Cancel_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopModalAsync();
            return true;
        }

        private void OptionSelected(OptionItem option)
        {
            if (OnOptionSelected != null)
            {
                OnOptionSelected(option);
            }

			Navigation.PopModalAsync();
        }

        public class OptionItem
        {
            public OptionItem(int id, string text)
            {
                Id = id;
                Text = text;
            }

            public int Id { get; set; }
            public string Text { get; set; }
        }
    }
}
