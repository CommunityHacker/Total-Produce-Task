using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using TaskTest_one.ServiceReference1;
using System.Windows;

namespace TaskTest_one.ViewModels
{
    [POCOViewModel]
    public class PersonListViewModel
    {
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]

        //reference for Create Document
        protected virtual IDocumentManagerService DocumentManagerService { get { return null; } } 

        //reference for message box
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }

        // WCF reference
        Service1Client service = new Service1Client();

        public virtual PersonList People { get; set; }

        protected PersonListViewModel()
        {
            People = new PersonList();
        }
   
        public static PersonListViewModel Create()
        {          
            return ViewModelSource.Create(() => new PersonListViewModel());
        }

        public void AddPerson()
        {         
            Person person = new Person();
            person.Name = "";
            person.Surname = "";
            PersonViewModel n = PersonViewModel.Create(person, "Insert");
            var document = DocumentManagerService.CreateDocument("PersonView", n);
            document.Show();
            
            //delegate for trigger 
            n.InsertPersonRefresh += this.OnInsertPersonRefresh;
        }
        
        public void DeletePerson(object personObject)
        {
            Person person = personObject as Person;
            if (MessageBoxService.ShowMessage("Are you sure?",
                    "Question",
                    MessageButton.YesNo,
                    MessageIcon.Question,
                    MessageResult.No) == MessageResult.Yes)
            {
                
                string status = "";
                status = service.DeletePerson(person.Id);
                Console.WriteLine("Delete status: " + status);
                Refresh();
            }
        }

        public void EditPerson(object personObject)
        {           
            var person = personObject as Person;
            var document = DocumentManagerService.CreateDocument("PersonView", PersonViewModel.Create(person,"Update"));
            document.Show();            
        }

        public void Refresh()
        {
            People = new PersonList();  
        }
        public void OnInsertPersonRefresh(object source,EventArgs e)
        {
            Refresh();         
        }
    }
}