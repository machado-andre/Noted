using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noted.Classes
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public short done { get; set; }

        public Note()
        {}

        public override string ToString()
        {
            return description + " - " + date.ToString();
        }

    }
}
