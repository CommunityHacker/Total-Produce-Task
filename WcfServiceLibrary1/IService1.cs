using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Person> GetData();

        [OperationContract]
        string InsertPerson(Person person);

        [OperationContract]
        string UpdatePerson(Person person);

        [OperationContract]
        string DeletePerson(int value);

    }

    [DataContract]
    public class Person
    {
        int id;
        string name;
        string surname;

        public Person(int id, string name, string surname)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
        }
        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DataMember]
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
    }
}
