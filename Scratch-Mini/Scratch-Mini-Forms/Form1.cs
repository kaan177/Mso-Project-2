using Microsoft.VisualBasic.ApplicationServices;
using Scratch_Mini_Forms.Properties;
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
            SetupDialogue(program);
            SetupGrid(program);
        }

        private void SetupDialogue(ScratchMini.Program program)
        {
            UserInputTxtBox.Clear();
            List<ICommand> commands = program.Commands;
            string commandText = "";
            foreach (ICommand command in commands)
            {
                commandText += command.ToString() + "\n";
            }

            UserInputTxtBox.Text = commandText.TrimEnd();
        }

        private void SetupGrid(ScratchMini.Program program)
        {
            GridPanel.Controls.Clear();

            Field field = program.field;

            int size = field.Grid.GetLength(0);

            int pWidth = GridPanel.ClientSize.Width / size;
            int pHeight = GridPanel.ClientSize.Height / size;

            for (int x = 0; x < size; x++)
            {
                for (int y = size - 1; y >= 0; y--)
                {
                    if (field.Grid[x, y] is Player)
                    {
                        Image image = Resources.guy1;
                        image.RotateFlip(DetermineRotation(field.GetPlayer().CardinalDirection));
                        PictureBox picBox = new PictureBox()
                        {
                            Width = pWidth,
                            Height = pHeight,
                            BorderStyle = BorderStyle.FixedSingle,
                            Location = new Point(x * pWidth, y * pHeight),
                            BackColor = Color.Yellow,
                            Image = image,
                            SizeMode = PictureBoxSizeMode.StretchImage
                            //rotateFlip()
                        };
                        GridPanel.Controls.Add(picBox);
                    }
                    else if (field.Grid[x, y] is Wall)
                    {
                        
                        PictureBox picBox = new PictureBox()
                        {
                            Width = pWidth,
                            Height = pHeight,
                            BorderStyle = BorderStyle.FixedSingle,
                            Location = new Point(x * pWidth, y * pHeight),
                            BackColor = Color.Yellow,
                            Image = Resources.Designer,
                            SizeMode = PictureBoxSizeMode.StretchImage
                            //rotateFlip()
                        };
                        GridPanel.Controls.Add(picBox);
                    }
                    else if (field.Grid[x, y] is EmptySpace)
                    {
                        PictureBox picBox = new PictureBox()
                        {
                            Width = pWidth,
                            Height = pHeight,
                            BorderStyle = BorderStyle.FixedSingle,
                            Location = new Point(x * pWidth, y * pHeight),
                            BackColor = Color.Yellow
                        };
                        GridPanel.Controls.Add(picBox);
                    }
                }
            }
            
        }

        private RotateFlipType DetermineRotation(CardinalDirection direction)
        {
            switch (direction)
            {
                case CardinalDirection.North:
                    return RotateFlipType.Rotate270FlipNone;
                case CardinalDirection.South:
                    return RotateFlipType.Rotate90FlipNone;
                case CardinalDirection.East:
                    return RotateFlipType.RotateNoneFlipNone;
                case CardinalDirection.West:
                    return RotateFlipType.Rotate180FlipNone;
            }
            return RotateFlipType.RotateNoneFlipNone;
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

        private void TurnButton_Click(object sender, EventArgs e)
        {

        }
    }
}