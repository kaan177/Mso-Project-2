using ScratchMini;

namespace Scratch_Mini_Forms
{
    public partial class Form1 : Form
    {
        public ICommandLine scratchMini;
        public Form1()
        {
            InitializeComponent();
            scratchMini = new ICommandLine();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //SetupGrid(5, 5); the default grid
        }

        private void chooseProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void basicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowProgram(scratchMini.basic);

        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowProgram(scratchMini.advanced);
        }

        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowProgram(scratchMini.expert);
        }

        private void ShowProgram(ScratchMini.Program program)
        {

        }

        private void SetupDialogue(ScratchMini.Program program)
        {

        }

        private void SetupGrid(ScratchMini.Program program)
        {
            GridPanel.Controls.Clear();

            int c; //moeten bepaald worden door Program
            int r; 


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

        private void RunProgramButton_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}