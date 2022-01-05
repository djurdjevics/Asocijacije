using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataManipulation;

namespace AsocijacijeGame
{
    public partial class Igra : Form
    {
        AsocijacijaModel asocijacijaModel;
        JsonCRUD jsonCRUD = new JsonCRUD();
        public Igra()
        {
            InitializeComponent();
        }
        public Igra(Guid id)
        {
            asocijacijaModel = jsonCRUD.UcitajAsocijacijuPoID(id);
            InitializeComponent();
        }

        private void Igra_Load(object sender, EventArgs e)
        {
            label1.Text = asocijacijaModel.name.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            char[] s = btn.Text.ToCharArray();
            int col = new int();
            if (s.Contains('A')) col = 0;
            if (s.Contains('B')) col = 1;
            if (s.Contains('C')) col = 2;
            if (s.Contains('D')) col = 3;
            TextBox textBox = GetAllTextBoxes().Find(txt => btn.Name.Contains(txt.Name.ToLower()));
            FillAndDisableButton(btn, col);
            textBox.Enabled = true;
        }

        private List<Button> GetFalseButtons()
        {
            List<Button> buttons = new List<Button>();
            foreach(Button btn in this.Controls.OfType<Button>())
            {
                if(btn.Enabled == true)
                {
                    buttons.Add(btn);
                }
            }
            return buttons;
        }

        private List<Button> GetAllButtons()
        {
            List<Button> buttons = new List<Button>();
            foreach(Button btn in this.Controls.OfType<Button>())
            {
                buttons.Add(btn);
            }
            return buttons;
        }

        private List<TextBox> GetTextBoxes()
        {
            List<TextBox> textBoxes = new List<TextBox>();
            foreach(TextBox txt in this.Controls.OfType<TextBox>()) {
               if(txt.Enabled == true)
                    textBoxes.Add(txt);
            }
            return textBoxes;
        }

        private List<TextBox> GetAllTextBoxes()
        {
            List<TextBox> textBoxes = new List<TextBox>();
            foreach(TextBox txt in this.Controls.OfType<TextBox>())
            {
                textBoxes.Add(txt);
            }
            return textBoxes;
        }

        private void FillAndDisableButton(Button btn,int col)
        {
            char[] s = btn.Text.ToCharArray();
            if (btn.Enabled == true)
            {
                if (s[1] == '1') btn.Text = asocijacijaModel.members[col].col1.ToUpper();
                if (s[1] == '2') btn.Text = asocijacijaModel.members[col].col2.ToUpper();
                if (s[1] == '3') btn.Text = asocijacijaModel.members[col].col3.ToUpper();
                if (s[1] == '4') btn.Text = asocijacijaModel.members[col].col4.ToUpper();
                btn.Enabled = false;
            }
        }

        private void FillAndDisableTextBox(TextBox textBox,int col)
        {
            if (col == 4) textBox.Text = asocijacijaModel.result.ToUpper();
            else textBox.Text = asocijacijaModel.members[col].result.ToUpper();
            textBox.Enabled = false;
        }


        private bool CheckAnswer(string s, int col)
        {
            if (col == 4)
            {
                if (s.ToUpper() == asocijacijaModel.result.ToUpper())
                {
                    return true;
                }
            }
            if (s.ToUpper() == asocijacijaModel.members[col].result.ToUpper())
            {
                return true;
            }
            return false;
        }

        private void enter(object sender, KeyEventArgs e)
        {
            int col = new int();
            TextBox textBox = (TextBox)sender;
            if(textBox.Name == "A") col = 0;
            if(textBox.Name == "B") col = 1;
            if(textBox.Name == "C") col = 2;
            if(textBox.Name == "D") col = 3;
            if(textBox.Name == "result") col = 4;
            if (e.KeyCode == Keys.Enter)
            {
                    if (CheckAnswer(textBox.Text, col) == true)
                    {
                        MessageBox.Show("Tacan odgovor!");
                        result.Enabled = true;
                        List<Button> buttons = GetFalseButtons();
                        if (col == 4)
                        {
                            MessageBox.Show("Asocijacija je resena!");
                            foreach (var btn in buttons)
                            {
                                if (btn.Text.Contains('A')) FillAndDisableButton(btn, 0);
                                if (btn.Text.Contains('B')) FillAndDisableButton(btn, 1);
                                if (btn.Text.Contains('C')) FillAndDisableButton(btn, 2);
                                if (btn.Text.Contains('D')) FillAndDisableButton(btn, 3);
                            }
                            List<TextBox> textBoxes = GetTextBoxes();
                            foreach (var text in textBoxes)
                            {
                                if (text.Name == "A") FillAndDisableTextBox(text, 0);
                                if (text.Name == "B") FillAndDisableTextBox(text, 1);
                                if (text.Name == "C") FillAndDisableTextBox(text, 2);
                                if (text.Name == "D") FillAndDisableTextBox(text, 3);
                            }
                        }
                        else
                        {
                            List<Button> buttonsCol = buttons.FindAll(c => c.Text.Contains(textBox.Name.ToCharArray()[0]));
                            foreach (Button btn in buttonsCol)
                            {
                                FillAndDisableButton(btn, col);
                            }
                        }
                        FillAndDisableTextBox(textBox, col);
                    }
                    else
                    {
                        MessageBox.Show("Netacan odgovor!");
                    }
                }
            }
        }
    }
