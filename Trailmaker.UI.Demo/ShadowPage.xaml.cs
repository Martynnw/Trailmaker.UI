using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trailmaker.UI.Demo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShadowPage : ContentPage
    {
        private SKPaint fillPaint;
        private SKPaint rulerPaint;

        public ShadowPage()
        {
            InitializeComponent();

            rulerPaint = new SKPaint { Color = Color.Lavender.ToSKColor(), Style = SKPaintStyle.Stroke, StrokeWidth = 4 };
            fillPaint = new SKPaint { Color = Color.DarkKhaki.ToSKColor(), Style = SKPaintStyle.Fill };
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();
            var rect = new SKRect(100, 200, 400, 500);

            fillPaint.ImageFilter = SKImageFilter.CreateDropShadow((float)OffsetXSlider.Value, (float)OffsetYSlider.Value, (float)SigmaXSlider.Value, (float)SigmaYSlider.Value, Color.Gray.ToSKColor(), SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);

            canvas.DrawRect(rect, fillPaint);

            var calculator = new FramePaddingCalculator();
            var padding = calculator.CalculatePadding(OffsetXSlider.Value, OffsetYSlider.Value, SigmaXSlider.Value, SigmaYSlider.Value);

            var left = rect.Left - padding.Left;
            var top = rect.Top - padding.Top;
            var right = rect.Right + padding.Right;
            var bottom = rect.Bottom + padding.Bottom;

            var rulerRect = new SKRect((float)left, (float)top, (float)right, (float)bottom);
            canvas.DrawRect(rulerRect, rulerPaint);
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Canvas.InvalidateSurface();
        }
    }
}