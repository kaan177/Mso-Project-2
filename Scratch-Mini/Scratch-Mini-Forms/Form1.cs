using Scratch_Mini;
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

            int size = program.field.Grid.GetLength(0); //checken of dit klopt

            int pWidth = GridPanel.ClientSize.Width / size;
            int pHeight = GridPanel.ClientSize.Height / size;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
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

        private void TurnButton_Click(object sender, EventArgs e)
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
    }
}