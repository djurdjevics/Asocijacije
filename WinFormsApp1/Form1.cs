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


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        JsonCRUD jsonCrud = new JsonCRUD();
        public Form1()
        {
            InitializeComponent();
            List<AsocijacijaModel> asocijacijaModels = jsonCrud.UcitajSveAsocijacije();
            foreach(AsocijacijaModel asoc in asocijacijaModels)
            {
                dataGridAsocijacije.Rows.Add(asoc.id, asoc.name, asoc.notes);
            }
        }

        private bool checkFields(List<ColumnModel> columnModels)
        {
            bool result = true;
            foreach (var i in columnModels)
            {
                if (i.col1 == "" || i.col2 == "" || i.col3 == "" || i.col4 == "")
                {
                    result = false;
                }
            }
            if (solveTextBox.Text == "") result = false;
            return result;
            
        }

        private void DodajAsocijacijuBtn_Click(object sender, EventArgs e)
        {
            List<ColumnModel> columnModels = new List<ColumnModel>();
            ColumnModel columnToAdd = new ColumnModel
            {
                col1 = a1.Text,
                col2 = a2.Text,
                col3 = a3.Text,
                col4 = a4.Text,
                result = aSolveTextBox.Text
            };
            columnModels.Add(columnToAdd);
            columnToAdd = new ColumnModel {
                col1 = b1.Text,
                col2 = b2.Text,
                col3 = b3.Text,
                col4 = b4.Text,
                result = bSolveTextBox.Text
            };
            columnModels.Add(columnToAdd);
            columnToAdd = new ColumnModel
            {
                col1 = c1.Text,
                col2 = c2.Text,
                col3 = c3.Text,
                col4 = c4.Text,
                result = cSolveTextBox.Text
            };
            columnModels.Add(columnToAdd);
            columnToAdd = new ColumnModel
            {
                col1 = d1.Text,
                col2 = d2.Text,
                col3 = d3.Text,
                col4 = d4.Text,
                result = dSolveTextBox.Text
            };
            columnModels.Add(columnToAdd);
            if (checkFields(columnModels) == false)
            {
                MessageBox.Show("Morate uneti sva polja!");
            }
            else
            {
                AsocijacijaModel asocijacijaModel = new AsocijacijaModel
                {
                    id = Guid.NewGuid(),
                    name = textBox1.Text,
                    members = columnModels,
                    result = solveTextBox.Text,
                    notes = notes.Text
                };
                JsonCRUD jsonCRUD = new JsonCRUD();
                jsonCRUD.DodajAsocijaciju(asocijacijaModel);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Guid id = Guid.Parse(dataGridAsocijacije.SelectedRows[0].Cells[0].Value.ToString());
                jsonCrud.ObrisiAsocijacijuPoId(id);
                MessageBox.Show("Asocijacija je uspesno obrisana!");
                dataGridAsocijacije.Rows.RemoveAt(dataGridAsocijacije.SelectedRows[0].Index);
                List<AsocijacijaModel> asocijacijaModels = jsonCrud.UcitajSveAsocijacije();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
