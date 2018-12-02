using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.ComponentModel;
using TaskTest_one.ServiceReference1;
using System.Windows;


namespace TaskTest_one.ViewModels
{
    [POCOViewModel]
    public class PersonViewModel:IEditableObject
    {
        //Event to refresh PersonListViewModel
        public delegate void InsertPersonRefreshEventHandler(object source, EventArgs e);
        public event InsertPersonRefreshEventHandler InsertPersonRefresh;

        Service1Client service = new Service1Client();
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }

        private Person person { get; set; }
        public PersonViewModel(){}
 
        protected PersonViewModel(Person person,string type)
        {           
            if (person == null) 
            {
                throw new ArgumentException("Person", "Person is NULL! ");
            }
            Load(person);

            //Set ActionType used for Save() functions to recognize action
            this.ActionType = type;                  
        }
        
        public static PersonViewModel Create()
        {
            return ViewModelSource.Create(() => new PersonViewModel());
        }

        public static PersonViewModel Create(Person person,string type)
        {
            return ViewModelSource.Create(() => new PersonViewModel(person,type));
        }

        public void Save()
        {          
            if (MessageBoxService.ShowMessage("Are you sure?",
                    "Question",
                    MessageButton.YesNo,
                    MessageIcon.Question,
                    MessageResult.No) == MessageResult.Yes)
            {
                //Check for action type
                bool isNew = this.ActionType == "Insert" ? true : false;
                
                person.Name = PersonName;
                person.Surname = PersonSurname;
                string updateStatus = "";
                
                //Insert person if its new or Update 
                updateStatus = isNew ? "Insert Status:" + service.InsertPerson(person) : "Update Status: " +service.UpdatePerson(person);
               
                foreach (Window window in Application.Current.Windows)
                {
                    //alternative to close window and show other one.. Good for this situation but not for multy windows application
                    if(window.ToString() == "DevExpress.Xpf.Core.DXWindow")
                    {                                          
                        window.Close();
                    }else
                    {
                        window.Activate();
                    }
                }
                Console.WriteLine(updateStatus);

                //
                OnInsertPersonRefresh();               
            }
            else
            {
                ((IEditableObject)this).CancelEdit();
            }           
        }
        public void Revert()
        {
            ((IEditableObject)this).CancelEdit();
        }
        private void Load(Person person)
        {
            this.person = person;
            this.PersonName = person.Name;
            this.PersonSurname = person.Surname;
        }

        void IEditableObject.BeginEdit()
        {
            //  throw new NotImplementedException();
        }
        void IEditableObject.EndEdit()
        {
            //Check if details was changed and apply to person
            if (!string.Equals(PersonName, person.Name)) { person.Name = PersonName; }
            if (!string.Equals(PersonSurname, person.Surname)) { person.Surname = PersonSurname; }
        }
        void IEditableObject.CancelEdit()
        {
            Load(this.person);
        }

        public virtual string ActionType { get; set;}
        public virtual string PersonName { get; set; }
        public virtual string PersonSurname { get; set; }

        //Event
        protected virtual void OnInsertPersonRefresh()
        {
            InsertPersonRefresh?.Invoke(this, EventArgs.Empty);
        }

    }
}