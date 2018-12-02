using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Threading.Tasks;
using TaskTest_one.ServiceReference1;


   public class PersonList : ObservableCollection<Person>
   {
    //Get all data
    public PersonList()
    {
        Service1Client service = new Service1Client();
        List<Person> list = new List<Person>();

        foreach (Person item in service.GetData())
        {
            Person person = new Person();
            person.Name = item.Name;
            person.Surname = item.Surname;
            person.Id = item.Id;
            Add(person);
        }
    }


   }

