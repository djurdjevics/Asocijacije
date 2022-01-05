using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataManipulation
{
    public class JsonCRUD
    {
        private static string userName = Environment.UserName;
        string path = String.Format(@"C:/Users/{0}/AppData/Roaming/BazaAsocijacija.json",userName);
        public List<AsocijacijaModel> UcitajSveAsocijacije()
        {
            List<AsocijacijaModel> result = new List<AsocijacijaModel>();
            if (File.Exists(path) == false)
            {
                StreamWriter sw = File.AppendText(path);
            }
            else
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    JsonSerializer jsonSer = new JsonSerializer();
                    result = (List<AsocijacijaModel>)jsonSer.Deserialize(sr, typeof(List<AsocijacijaModel>));
                }
            }

            if(result == null)
            {
                return new List<AsocijacijaModel>();
            }
            return result;
        }

        public void DodajAsocijaciju(AsocijacijaModel asocijacijaModel)
        {
            List<AsocijacijaModel> result = UcitajSveAsocijacije();
            result.Add(asocijacijaModel);

            using (StreamWriter sw = File.CreateText(path))
            {
                JsonSerializer jsonSer = new JsonSerializer();
                jsonSer.Serialize(sw, result);
            }
        }

        public AsocijacijaModel UcitajAsocijacijuPoID(Guid id)
        {
            List<AsocijacijaModel> list = UcitajSveAsocijacije();
            return list.Find(x => x.id == id);
        }

        public AsocijacijaModel ObrisiAsocijacijuPoId(Guid id)
        {
            List<AsocijacijaModel> asocijacije = UcitajSveAsocijacije();
            AsocijacijaModel asoc = asocijacije.Find(x => x.id == id);
            asocijacije.Remove(asoc);
            using (StreamWriter sw = File.CreateText(String.Format(@"C:/Users/{0}/AppData/Roaming/BazaAsocijacija.json",userName)))
            {
                JsonSerializer jsonSer = new JsonSerializer();
                jsonSer.Serialize(sw, asocijacije);
            }
            return asoc;
        }
    }
}
