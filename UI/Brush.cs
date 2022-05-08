using TerrEditor.Application;

namespace UI;

public partial class MainForm : Form
{
    public class DrawingPoints
    {
        private int index = 0;
        private Point[] points;

        public DrawingPoints(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Размер должен быть положительным");
            points = new Point[size];
        }

        public void DrawPoint(int x, int y)
        {
            if (index >= points.Length)
                ResetPoints();
            points[index] = new Point(x, y);
            index++;
        }

        public void ResetPoints()
        {
            index = 0;
        }

        public Point[] GetPoints()
        {
            return points;
        }

        public int GetCountPoints()
        {
            return index;
        }
    }

    private bool isDrawing = false;
    private DrawingPoints drawingPoints = new DrawingPoints(2);
    private Pen pen = new Pen(Color.Black, 3f);
    private Bitmap drawingImage = new Bitmap(100, 100);
    private Graphics graphics;

    private void setSize()
    {
        Rectangle rectangle = Screen.PrimaryScreen.Bounds;
        drawingImage = new Bitmap(rectangle.Width, rectangle.Height);
        graphics = Graphics.FromImage(drawingImage);

        pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
        pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
    }

    private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
    {
        isDrawing = true;
    }

    private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
        isDrawing = false;
        drawingPoints.ResetPoints();
    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
        if (!isDrawing)
            return;

        drawingPoints.DrawPoint(e.X, e.Y);
        if (drawingPoints.GetCountPoints() >= 2)
        {
            graphics.DrawLines(pen, drawingPoints.GetPoints());
            pictureBox1.Image = drawingImage;
            drawingPoints.DrawPoint(e.X, e.Y);
        }
    }

    private void ColorGreen_Click(object sender, EventArgs e)
    {
        pen.Color = ((Button)sender).BackColor;
    }
    
    private void ColorRed_Click(object sender, EventArgs e)
    {
        pen.Color = ((Button)sender).BackColor;
    }
    
    private void ColorYellow_Click(object sender, EventArgs e)
    {
        pen.Color = ((Button)sender).BackColor;
    }
    
    private void ColorBlack_Click(object sender, System.EventArgs e)  
    {  
        pen.Color = ((Button)sender).BackColor;
    } 
    
    private void Clear_Click(object sender, System.EventArgs e)
    {
        graphics.Clear(Color.Empty);
        pictureBox1.Image = drawingImage;
    }
    
    private void PenWidth_ValueChabged(object sender, EventArgs e){
        pen.Width = PenWidth.Value;
    }

    private void MainForm_Load(object sender, EventArgs e)
    {

    }
}