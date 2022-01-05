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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            JsonCRUD jsonCRUD = new JsonCRUD();
            List<AsocijacijaModel> result = jsonCRUD.UcitajSveAsocijacije();
            foreach(var item in result)
            {
                dataGridView1.Rows.Add(item.id, item.name,item.notes);
            }
            dataGridView1.MultiSelect = false;
        }

        private Guid GetGuidFromDataView()
        {
            object selectedItem = null;
            try
            {
                selectedItem = dataGridView1.SelectedRows[0].Cells[0].Value;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Nije selektovan red asocijacije!");
            }
            if (selectedItem == null)
            {
                return Guid.Empty;
            }
            return Guid.Parse(selectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Guid id = GetGuidFromDataView();
            if (id != Guid.Empty)
            {
                Igra f1 = new Igra(id);
                f1.Show();
            }
        }
    }
}
