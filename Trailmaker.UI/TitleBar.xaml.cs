using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trailmaker.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitleBar : StackLayout
    {
        public TitleBar()
        {
            InitializeComponent();
            var page = GetParentPage(this);
        }

        private void MenuTapped(object sender, EventArgs e)
        {
            var page = GetParentPage(this);

            if (page.ToolbarItems.Any())
            {
            }
        }

        private ContentPage GetParentPage(Element element)
        {
            if (element.Parent == null || element.Parent is ContentPage)
            {
                return element.Parent as ContentPage;
            }
            else
            {
                return GetParentPage(element.Parent);
            }
        }
    }
}
