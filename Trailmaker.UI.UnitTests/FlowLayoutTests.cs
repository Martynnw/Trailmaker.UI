using System;
using NUnit.Framework;
using Xamarin.Forms;
using Trailmaker.UI;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace Trailmaker.UI.UnitTests
{
    [TestFixture]
    public class FlowLayoutTests
    {
        [Test]
        public void TestEmptyLayoutDoesntCrash()
        {
            var layoutInfo = new FlowLayout.LayoutInfo(new Xamarin.Forms.Thickness(0));
            var views = new List<View>();

            layoutInfo.ProcessLayout(views, 100);
        }

        [Test]
        public void TestSingleRowLayoutMeasure()
        {
            var layout = new FlowLayout();
            layout.Padding = 10;
            layout.Spacing = new Xamarin.Forms.Thickness(5);
            layout.Children.Add(new BoxView { WidthRequest = 30, HeightRequest = 30 });
            layout.Children.Add(new BoxView { WidthRequest = 30, HeightRequest = 30 });

            var sizeRequest = layout.Measure(100, double.PositiveInfinity);

            Assert.AreEqual(100, sizeRequest.Request.Width);
            Assert.AreEqual(50, sizeRequest.Request.Height);
        }

        [Test]
        public void TestMultiRowLayoutMeasure()
        {
            var layout = new FlowLayout();
            layout.Padding = 10;
            layout.Spacing = new Xamarin.Forms.Thickness(5);
            layout.Children.Add(new BoxView { WidthRequest = 90, HeightRequest = 30 });
            layout.Children.Add(new BoxView { WidthRequest = 30, HeightRequest = 30 });

            var sizeRequest = layout.Measure(100, double.PositiveInfinity);

            Assert.AreEqual(100, sizeRequest.Request.Width);
            Assert.AreEqual(90, sizeRequest.Request.Height);
        }

        [Test]
        public void TestChildPositionsHorizontal()
        {
            var layoutInfo = new FlowLayout.LayoutInfo(new Thickness(3, 6, 4, 5));
            var children = new List<View>();
            children.Add(new BoxView { WidthRequest = 30, HeightRequest = 30 });
            children.Add(new BoxView { WidthRequest = 30, HeightRequest = 30 });

            layoutInfo.ProcessLayout(children, 100);

            Assert.AreEqual(new Rectangle(0, 0, 30, 30), layoutInfo.Bounds[0]);
            Assert.AreEqual(new Rectangle(37, 0, 30, 30), layoutInfo.Bounds[1]);
        }

        [Test]
        public void TestChildPositionsVertical()
        {
            var layoutInfo = new FlowLayout.LayoutInfo(new Thickness(3, 6, 4, 5));
            var children = new List<View>();
            children.Add(new BoxView { WidthRequest = 90, HeightRequest = 30 });
            children.Add(new BoxView { WidthRequest = 30, HeightRequest = 30 });

            layoutInfo.ProcessLayout(children, 100);

            Assert.AreEqual(new Rectangle(0, 0, 90, 30), layoutInfo.Bounds[0]);
            Assert.AreEqual(new Rectangle(0, 41, 30, 30), layoutInfo.Bounds[1]);
        }

        [Test]
        public void TestOversizeChildRestricted()
        {
            var layoutInfo = new FlowLayout.LayoutInfo(new Thickness(0));
            var children = new List<View>();
            children.Add(new BoxView { WidthRequest = 110, HeightRequest = 30 });

            layoutInfo.ProcessLayout(children, 100);

            Assert.AreEqual(100, layoutInfo.Bounds[0].Width);
        }

        [Test]
        public void InvisibleChildHidden()
        {
            var layoutInfo = new FlowLayout.LayoutInfo(new Thickness(0));
            var children = new List<View>();
            children.Add(new BoxView { WidthRequest = 20, HeightRequest = 30 });
            children.Add(new BoxView { WidthRequest = 30, HeightRequest = 30, IsVisible = false});
            children.Add(new BoxView { WidthRequest = 40, HeightRequest = 30 });
            children.Add(new BoxView { WidthRequest = 20, HeightRequest = 30 });

            layoutInfo.ProcessLayout(children, 100);

            Assert.AreEqual(new Rectangle(0, 0, 20, 30), layoutInfo.Bounds[0], "Box 1");
            Assert.AreEqual(new Rectangle(20, 0, 40, 30), layoutInfo.Bounds[2], "Box 3");
            Assert.AreEqual(new Rectangle(60, 0, 20, 30), layoutInfo.Bounds[3], "Box 4");
        }

        [Test]
        public void InvisibleChildHiddenThreeLine()
        {
        	var layoutInfo = new FlowLayout.LayoutInfo(new Thickness(0));
        	var children = new List<View>();
        	children.Add(new BoxView { WidthRequest = 90, HeightRequest = 30 });
        	children.Add(new BoxView { WidthRequest = 90, HeightRequest = 30, IsVisible = false });
        	children.Add(new BoxView { WidthRequest = 90, HeightRequest = 30 });

        	layoutInfo.ProcessLayout(children, 100);

        	Assert.AreEqual(new Rectangle(0, 0, 90, 30), layoutInfo.Bounds[0], "Box 1");
        	Assert.AreEqual(new Rectangle(0, 30, 90, 30), layoutInfo.Bounds[2], "Box 3");
        }
    }
}
