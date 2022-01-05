using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataManipulation
{
    public class AsocijacijaModel
    {
        public Guid id { get; set; } = new Guid();
        public string name { get; set; }
        public List<ColumnModel> members { get; set; }
        public string result { get; set; }
        public string notes { get; set; }
    }
}
