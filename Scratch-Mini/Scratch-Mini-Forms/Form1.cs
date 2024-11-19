using Microsoft.VisualBasic.ApplicationServices;
using Scratch_Mini_Forms.Properties;
using Scratch_Mini;
using ScratchMini;

namespace Scratch_Mini_Forms
{
    public partial class Form1 : Form
    {
        public ICommandLine scratchMini;
        public ScratchMini.Program activeProgram;
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
            activeProgram = scratchMini.basic;
        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowProgram(scratchMini.advanced);
            activeProgram = scratchMini.advanced;
        }

        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowProgram(scratchMini.expert);
            activeProgram = scratchMini.expert;
        }

        private void ShowProgram(ScratchMini.Program program)
        {
            SetupDialogue(program);
            SetupGrid(program.field);
        }

        private void SetupDialogue(ScratchMini.Program program)
        {
            UserInputTxtBox.Clear();
            List<ICommand> commands = program.Commands;
            string commandText = "";
            foreach (ICommand command in commands)
            {
                commandText += command.ToString();
            }

            UserInputTxtBox.Text = commandText.TrimEnd();
        }

        private void SetupGrid(Field field)
        {
            GridPanel.Controls.Clear();

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
                            Image = Resources.Designer1,
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
            if (activeProgram != null)
            {
                Field field;
                try
                {
                    activeProgram.Commands = scratchMini.LoadCommands(UserInputTxtBox.Text);
                    textBox1.Text = activeProgram.Execute(out _, out field);
                    SetupGrid(field);
                }
                catch (Exception ex)
                {
                    textBox1.Text = ex.Message;
                }

            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void TurnButton_Click(object sender, EventArgs e)
        {
            try {
                activeProgram.Commands = scratchMini.LoadCommands(UserInputTxtBox.Text);
                if (activeProgram != null)
                {
                    ShowProgram(activeProgram);
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void choosePathExerciseToolStrip_Click(object sender, EventArgs e)
        {

        }

        private void chooseShapeExerciseToolStrip_Click(object sender, EventArgs e)
        {

        }

        private void Exercise1Shape_Click(object sender, EventArgs e)
        {
            ShowProgram(scratchMini.exerciseShape1.exerciseProgram);
        }

        private void exercise1PathFinding_Click(object sender, EventArgs e)
        {
            ShowProgram(scratchMini.exercisePath1.exerciseProgram);
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text Files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    string allText = System.IO.File.ReadAllText(filePath);
                    MessageBox.Show("File selected" + filePath);

                    ProgramImporter programImporter = new ProgramImporter();
                    ScratchMini.Program importedProgram = programImporter.Import(allText);

                    ShowProgram(importedProgram);
                }

                catch (Exception ex) 
                {
                    MessageBox.Show("Error reading file: " + ex.Message);
                }
            }
        }

        private void MetricsButton_Click(object sender, EventArgs e)
        {
            try
            {
                activeProgram.Commands = scratchMini.LoadCommands(UserInputTxtBox.Text);
                if (activeProgram != null)
                {
                    textBox1.Text = scratchMini.GetMetricsProgram(activeProgram);
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }
        }
    }
}