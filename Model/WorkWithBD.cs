using AddressBook.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook.Model
{
    public static class WorkWithBD
    {

        // Получение всех контактов из БД
        public static List<Contact> GetAllContacts()
        {
            using (DBContext db = new DBContext())
            {
                var result = db.Contacts.ToList();
                return result;
            }
        }

        // Создание контакты в БД
        public static void CreateContact(string fullname, string address, string phone)
        {
            using (DBContext db = new DBContext())
            {
                Contact newContact = new Contact
                {
                    FullName = fullname,
                    Address = address,
                    Phone = phone,
                };
                db.Contacts.Add(newContact);
                db.SaveChanges();
            }
        }

        // Удаление контакт из БД
        public static string DeleteContact(Contact contact)
        {
            
            using (DBContext db = new DBContext())
            {
                db.Contacts.Remove(contact);
                db.SaveChanges();
                return  "Контакт \"" + contact.FullName + "\" удален";
            }
           
        }

        // Редактирование контакта в БД
        public static string EditContact(Contact oldContact, string newFullName, string newAddress, string newPhone)
        {
            string result = "Такого контакт не существует";
            using (DBContext db = new DBContext())
            {
                
                Contact FindedContact = db.Contacts.FirstOrDefault(p => p.Id == oldContact.Id);
                if (FindedContact != null)
                {
                    FindedContact.FullName = newFullName;
                    FindedContact.Address = newAddress;
                    FindedContact.Phone = newPhone;
                    db.SaveChanges();
                    result = "Данные контакта \"" + FindedContact.FullName + "\" изменены";
                }
            }
            return result;
        }
    }
}
