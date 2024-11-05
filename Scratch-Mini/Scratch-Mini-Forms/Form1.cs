namespace Scratch_Mini_Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //SetupGrid(5, 5); the default grid
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void chooseProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void basicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupGrid(3, 3);

        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupGrid(4, 4);
        }

        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupGrid(5, 5);
        }

        private void SetupGrid(int r, int c)
        {
            GridPanel.Controls.Clear();

            int pWidth = GridPanel.ClientSize.Width / c;
            int pHeight = GridPanel.ClientSize.Height / r;

            for (int x = 0; x < r; x++)
            {
                for (int y = 0; y < c; y++)
                {
                    PictureBox picBox = new PictureBox()
                    {
                        Width = pWidth,
                        Height = pHeight,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(y * pWidth, x * pHeight),
                        BackColor = Color.Yellow
                    };

                    GridPanel.Controls.Add(picBox);
                }
            }

        }

        private void OutputTxtBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}