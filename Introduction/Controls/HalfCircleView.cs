using Microsoft.Maui.Controls.Shapes; 
using Path = Microsoft.Maui.Controls.Shapes.Path; 
 
namespace Introduction.Controls 
{
    public class HalfCircleView : ContentView
    {
        Path path;
        PathGeometry pathGeometry;
        PathFigure pathFigure; 

        public enum CircleDirection { Up, Down, Left, Right }

        public HalfCircleView()
        {
            path = new Path() 
            {
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                StrokeDashArray = StrokeDashArray,
                StrokeDashOffset = StrokeDashOffset
            };

            pathGeometry = new PathGeometry();
            pathFigure = new PathFigure();
            pathGeometry.Figures.Add(pathFigure);
            path.Data = pathGeometry;

            UpdatePath();

            Content = path; 
        }

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public CircleDirection Direction
        {
            get => (CircleDirection)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        public Color Stroke
        {
            get => (Color)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public double StrokeDashOffset
        {
            get => (double)GetValue(StrokeDashOffsetProperty);
            set => SetValue(StrokeDashOffsetProperty, value);
        }

        public DoubleCollection StrokeDashArray
        {
            get => (DoubleCollection)GetValue(StrokeDashArrayProperty);
            set => SetValue(StrokeDashArrayProperty, value);
        }

        // Bindable properties
        public static readonly BindableProperty RadiusProperty =
            BindableProperty.Create(nameof(Radius), typeof(double), typeof(HalfCircleView), 25.0, propertyChanged: OnPropertiesChanged);

        public static readonly BindableProperty DirectionProperty =
            BindableProperty.Create(nameof(Direction), typeof(CircleDirection), typeof(HalfCircleView), CircleDirection.Up, propertyChanged: OnPropertiesChanged);

        public static readonly BindableProperty StrokeProperty =
            BindableProperty.Create(nameof(Stroke), typeof(Color), typeof(HalfCircleView), Colors.Black, propertyChanged: OnPropertiesChanged);

        public static readonly BindableProperty StrokeThicknessProperty =
            BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(HalfCircleView), 1.0, propertyChanged: OnPropertiesChanged);

        public static readonly BindableProperty StrokeDashOffsetProperty =
            BindableProperty.Create(nameof(StrokeDashOffset), typeof(double), typeof(HalfCircleView), 0.0, propertyChanged: OnPropertiesChanged);

        public static readonly BindableProperty StrokeDashArrayProperty =
            BindableProperty.Create(nameof(StrokeDashArray), typeof(DoubleCollection), typeof(HalfCircleView), new DoubleCollection(), propertyChanged: OnPropertiesChanged);

        private static void OnPropertiesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (HalfCircleView)bindable;

            control.path.Stroke = control.Stroke;
            control.path.StrokeThickness = control.StrokeThickness;
            control.path.StrokeDashArray = control.StrokeDashArray;
            control.path.StrokeDashOffset = control.StrokeDashOffset;

            control.UpdatePath();
        }

        // Update the path based on Radius and Direction
        private void UpdatePath()
        {
            pathFigure.Segments.Clear();

            var arcSegment = new ArcSegment
            {
                IsLargeArc = true,
                Size = new Size(Radius, Radius),
                RotationAngle = 180
            };

            switch (Direction)
            {
                case CircleDirection.Up:
                    pathFigure.StartPoint = new Point(0, Radius);
                    arcSegment.Point = new Point(2 * Radius, Radius);
                    arcSegment.SweepDirection = SweepDirection.Clockwise;
                    break;
                case CircleDirection.Down:
                    pathFigure.StartPoint = new Point(0, Radius);
                    arcSegment.Point = new Point(2 * Radius, Radius);
                    arcSegment.SweepDirection = SweepDirection.CounterClockwise;
                    break;
                case CircleDirection.Left:
                    pathFigure.StartPoint = new Point(Radius, 0);
                    arcSegment.Point = new Point(Radius, 2 * Radius);
                    arcSegment.SweepDirection = SweepDirection.CounterClockwise;
                    break;
                case CircleDirection.Right:
                    pathFigure.StartPoint = new Point(Radius, 0);
                    arcSegment.Point = new Point(Radius, 2 * Radius);
                    arcSegment.SweepDirection = SweepDirection.Clockwise;
                    break;
            }

            pathFigure.Segments.Add(arcSegment);
        }
    }
}
