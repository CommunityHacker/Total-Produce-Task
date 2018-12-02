using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


   public class PersonInfo
   {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }

        public PersonInfo()
        {

        }
        public PersonInfo(int trackId, string name, string surname)
        {
            this.id = trackId;
            this.name = name;
            this.surname = surname;
    }

   
}
