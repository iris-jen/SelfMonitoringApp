using Acr.UserDialogs;
using SelfMonitoringApp.Services;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using SkiaSharp.Views.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;

namespace SelfMonitoringApp.Controls
{
    public class ChartData
    {
        public ChartData(int value, SKColor color)
        {
            Value = value;
            Color = color;
        }

        public int Value { private set; get; }

        public SKColor Color { private set; get; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PercentageCircle : Frame
    {
        public double Ammount
        {
            get { return (double)GetValue(AmmountProperty); }
            set { SetValue(AmmountProperty, value); }
        }

        public static readonly BindableProperty AmmountProperty =
            BindableProperty.Create(nameof(Ammount), typeof(double), typeof(HeaderBlockAddRemove),
                default(string), BindingMode.TwoWay);


        public PercentageCircle()
        {
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            int totalValues = 0;

            int greenSeg = (int)(Ammount * 10);
            int redSeg = 100 - greenSeg;

            ChartData[] chartData =
            {
                new ChartData(redSeg, SKColors.Red),
                new ChartData(greenSeg, SKColors.Green),
            };

            foreach (ChartData item in chartData)
            {
                totalValues += item.Value;
            }

            int heightFudge = -4;
            int height = info.Height + heightFudge;

            SKPoint center = new SKPoint(info.Width / 2, height / 2);
            float radius = Math.Min(info.Width / 2, (height -4) / 2);

            if (greenSeg == 100 || redSeg == 100)
            {
                using (SKPaint fillPaint = new SKPaint())
                using (SKPaint outlinePaint = new SKPaint())
                {
                    fillPaint.Style = SKPaintStyle.Fill;
                    fillPaint.Color = greenSeg == 100 ? SKColors.Green : SKColors.Red;
                    outlinePaint.Style = SKPaintStyle.Stroke;
                    outlinePaint.Color = SKColors.Black;
                    canvas.Restore();
                    canvas.DrawCircle(center, radius, fillPaint);
                }
                return;
            }

            SKRect rect = new SKRect(center.X - radius, center.Y - radius,
                                     center.X + radius, center.Y + radius);

            float startAngle = 0;

            foreach (ChartData item in chartData)
            {
                float sweepAngle = 360f * item.Value / totalValues;
           
                using (SKPath path = new SKPath())
                using (SKPaint fillPaint = new SKPaint())
                using (SKPaint outlinePaint = new SKPaint())
                {
                    path.MoveTo(center);
                    path.ArcTo(rect, startAngle, sweepAngle, false);
                    path.Close();

                    fillPaint.Style = SKPaintStyle.Fill;
                    fillPaint.Color = item.Color;

                    outlinePaint.Style = SKPaintStyle.Stroke;
                    outlinePaint.StrokeWidth = 5;
                    outlinePaint.Color = SKColors.Black;

                    canvas.Save();

                    // Fill and stroke the path
                    canvas.DrawPath(path, fillPaint);
                    canvas.DrawPath(path, outlinePaint);
                    canvas.Restore();
                }

                startAngle += sweepAngle;
            }
        }
    }
}