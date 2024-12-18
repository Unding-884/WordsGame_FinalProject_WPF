using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    public class TxTreadSave
    {
        private string _path { get; set; }
        public TxTreadSave(string path)
        {
            _path = path;
        }
        public void SaveToFile(List<string> lines)
        {
            System.IO.File.WriteAllLines(_path, lines);
        }
        public List<string> ReadFromFile()
        {
            if (System.IO.File.Exists(_path))
            {
                return System.IO.File.ReadAllLines(_path).ToList();
            }
            return new List<string>();
        }
    }
}
