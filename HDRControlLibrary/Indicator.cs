using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace WinFormsControlLibrary1
{
    public partial class Indicator : UserControl
    {
        #region Properties
        [Category("HMI Data")]
        private bool _value = false;
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                //this.Refresh(); // Marks the control for repaint and immidiately calls OnPaint(). Blocking, Fast update
                this.Invalidate(); // Marks the control of repaint. Windows decides when to repaint. Nonblocking, Slow update
            }
        }
        #endregion

        #region Constructor
        public Indicator()
        {
            InitializeComponent();

        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Graphics g = e.Graphics;
            PointF pointf = new PointF((float)this.Width / 2f, (float)this.Height / 2f);// center point of graphics
            float n1 = Math.Min(pointf.X, pointf.Y);
            float n2 = (float)((double)n1 * 65.0 / 100.0); // Inner circle radius
            float n3 = (float)((double)n1 * 95.0 / 100.0); // Outer Circle
            // Generate two Diganol points
            Point p1 = new Point((int)((double)pointf.X - (double)n2), (int)((double)pointf.Y - (double)n2));
            Point p2 = new Point((int)((double)pointf.X + (double)n2), (int)((double)pointf.Y + (double)n2));

            

            // Default Color
            LinearGradientBrush brush = new LinearGradientBrush(p1, p2, Color.FromArgb(255, 0, 255, 0), Color.FromArgb(1, 0, 255, 0));
            Pen myPen= new Pen(Color.FromArgb(255, 0, 255, 0),3);
            if (_value)
            {
                brush = new LinearGradientBrush(p1, p2, Color.FromArgb(255, 255, 0, 0), Color.FromArgb(1, 255, 0, 0));
                myPen= new Pen(Color.FromArgb(255, 255, 0, 0),3);

            }

            g.FillEllipse(brush, p1.X, p1.Y, n2 * 2f, n2 * 2f);// Creats a filled circle
            g.DrawEllipse(myPen,(int)p1.X-5,(int)p1.Y-5,(int)(n2+5)*2,(int)(n2+5.0)*2f);
            
            brush.Dispose();
            g.Dispose();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            float num = (float)Math.Min(this.Width, this.Height);
            if ((double)num < 20.0)
                num = 20f;
            this.Width = (int)num;
            this.Height= (int)num;
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0,0,this.Width,this.Height);
            this.Region=new Region(path);
        }
    }
}