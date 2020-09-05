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
using SelfMonitoringApp.Models;

namespace SelfMonitoringApp.Controls
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiGraph : Frame
    {
        public ObservableCollection<OccuranceModel> Occurances
        {
            get { return (ObservableCollection<OccuranceModel>)GetValue(OccurancesProperty); }
            set { SetValue(OccurancesProperty, value); }
        }

        public static readonly BindableProperty OccurancesProperty =
            BindableProperty.Create(nameof(Occurances), typeof(ObservableCollection<OccuranceModel>), typeof(MultiGraph),
                default(ObservableCollection<OccuranceModel>), BindingMode.TwoWay, propertyChanged: HandleOccurancesChanged);

        public bool InitialSet { get; set; }

        private static void HandleOccurancesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var block = (MultiGraph)bindable;

            if (block.InitialSet)
            {
                block.CanvasView.InvalidateSurface();
            }
        }

        SKCanvasView CanvasView { get; set; }

        public MultiGraph()
        {
            CanvasView = new SKCanvasView();
            CanvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = CanvasView;
           
        }

        public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            int occuranceStartHr = Occurances.Min(x => x.Time).Hour - 1;
            int occuranceEndHr = Occurances.Max(x => x.Time).Hour + 1;
            int deltaHours = occuranceEndHr - occuranceStartHr;

            int borderOffset = 60;
            int height = info.Height - borderOffset;
            int width = info.Width - borderOffset;
            int totalXSegments = 11;
            int totalYSegments = deltaHours;
            float horizontalLineSpacing = height / totalXSegments;
            float verticalLineSpacing = width / totalYSegments;

            using (var linePaint = new SKPaint())
            using (var pointPaint = new SKPaint())
            using (var borderPaint = new SKPaint())
            using (var moodLinePaint = new SKPaint())
            using (var moodPointPaint = new SKPaint())
            using (var textPaint = new SKPaint())
            {
                canvas.Clear();
                linePaint.Style = SKPaintStyle.Stroke;
                linePaint.StrokeWidth = 1;
                linePaint.StrokeCap = SKStrokeCap.Square;
                linePaint.StrokeJoin = SKStrokeJoin.Miter;
                linePaint.Color = SKColors.Black;

                moodPointPaint.Style = SKPaintStyle.StrokeAndFill;
                moodPointPaint.StrokeWidth = 4;
                moodPointPaint.Color = SKColors.Blue;

                moodLinePaint.Style = SKPaintStyle.Stroke;
                moodLinePaint.StrokeWidth = 4;
                moodLinePaint.Color = SKColors.Purple;
                moodLinePaint.IsAntialias = true;
                
                borderPaint.Style = SKPaintStyle.Stroke;
                borderPaint.StrokeWidth = 5;
                borderPaint.Color = SKColors.Black;

                textPaint.Style = SKPaintStyle.Stroke;
                textPaint.StrokeWidth = 1;
                textPaint.FakeBoldText = true;
                textPaint.Color = SKColors.Black;
                textPaint.TextSize = 25;

                //Draw rectangle to outline area, make it to canvas bounds, not modified height.
                canvas.DrawRect(0, 0, info.Width, info.Height, borderPaint);
                int textOffset = 20;

                //Draw the X axis lines.
                float y = borderOffset;
                int startCount = totalXSegments - 1;
                for (int lineCount = 0; lineCount < totalXSegments; lineCount++)
                {
                    canvas.DrawText($"{startCount}", new SKPoint(textOffset, y), textPaint);
                    canvas.DrawLine(borderOffset, y, width, y, linePaint);
                    y += horizontalLineSpacing;
                    startCount--;
                }

                //Draw y axis lines
                float x = borderOffset;
                bool dontWriteText = false;
                for (int lineCount = 0; lineCount < totalYSegments; lineCount++)
                {
                    if (!dontWriteText)
                    {
                        canvas.DrawText($"{occuranceStartHr + lineCount}:00", new SKPoint(x, info.Height - textOffset), textPaint);
                        dontWriteText = true;
                    }
                    else
                        dontWriteText = false;

                    canvas.DrawLine(x, borderOffset, x, height, linePaint);
                    x += verticalLineSpacing;
                }

                // Draw each point from the user collection
                SKPoint lastPoint = SKPoint.Empty;
                foreach(OccuranceModel mood in Occurances)
                {
                    float pX = (float)((mood.Time.TimeOfDay.TotalHours-occuranceStartHr) * verticalLineSpacing + borderOffset);
                    float pY = (float)(height - mood.Ammount * horizontalLineSpacing);

                    SKPoint newPoint = new SKPoint(pX, pY);
                    canvas.DrawCircle(newPoint, 10, moodPointPaint);

                    // draw a line between the points if not first point
                    if (lastPoint != SKPoint.Empty)
                        canvas.DrawLine(lastPoint, newPoint, moodLinePaint);

                    lastPoint = newPoint;
                }
            }

            InitialSet = true;
            canvas.Save();
        }      
    }
}