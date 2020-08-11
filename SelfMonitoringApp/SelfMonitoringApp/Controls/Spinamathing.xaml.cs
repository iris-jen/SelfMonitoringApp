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
using System.Diagnostics;

namespace SelfMonitoringApp.Controls
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Spinamathing : Frame
    {
        const double cycleTime = 250;       // in milliseconds

        SKCanvasView canvasView;
        Stopwatch stopwatch = new Stopwatch();
        bool pageIsActive;
        float dashPhase;

        public Spinamathing()
        {
    

            canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;


            pageIsActive = true;
            stopwatch.Start();

            Device.StartTimer(TimeSpan.FromMilliseconds(33), () =>
            {
                double t = stopwatch.Elapsed.TotalMilliseconds % cycleTime / cycleTime;
                dashPhase = (float)(10 * t);
                canvasView.InvalidateSurface();

                if (!pageIsActive)
                {
                    stopwatch.Stop();
                }

                return pageIsActive;
            });
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float radius = Math.Min(center.X, center.Y);

            using (SKPath path = new SKPath())
            {
                for (float angle = 0; angle < 3600; angle += 1)
                {
                    float scaledRadius = radius * angle / 3600;
                    double radians = Math.PI * angle / 180;
                    float x = center.X + scaledRadius * (float)Math.Cos(radians);
                    float y = center.Y + scaledRadius * (float)Math.Sin(radians);
                    SKPoint point = new SKPoint(x, y);

                    if (angle == 0)
                    {
                        path.MoveTo(point);
                    }
                    else
                    {
                        path.LineTo(point);
                    }
                }

                using (SKPaint paint = new SKPaint())
                {
                    paint.Style = SKPaintStyle.Stroke;
                    paint.Color = SKColors.Red;
                    paint.StrokeWidth = 5;
                    paint.PathEffect = SKPathEffect.CreateDash(new float[] { 5, 5 }, dashPhase);

                    canvas.DrawPath(path, paint);
                }
            }
        }
    }
}