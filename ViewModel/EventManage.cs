using AddressBook.Model;
using AddressBook.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace AddressBook.ViewModel
{
    public class EventManage : INotifyPropertyChanged
    {

        // Список всех контактов
        private List<Contact> allContacts = WorkWithBD.GetAllContacts();
        public List<Contact> AllContacts
        {
            get
            {
                return WorkWithBD.GetAllContacts(); ;
            }
            private set
            {
                allContacts = value;
                NotifyPropertyChanged("AllContacts");
            }
        }

        public static string ContactName { get; set; }
        public static string ContactAddress { get; set; }
        public static string ContactPhone { get; set; }
        public static Contact Selected { get; set; }



        // Команда на добавление контакта из окна создания контактов
        public RelayCommand AddNewContact
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if (ContactName == null || ContactName.Replace(" ", "").Length == 0)
                    {
                        ShowNotify("Contact Full name is empty");
                    }
                    else if (ContactAddress == null || ContactAddress.Replace(" ", "").Length == 0)
                    {
                        ShowNotify("Contact Address is empty");
                    }
                    else if (ContactPhone == null || ContactPhone.Replace(" ", "").Length == 0)
                    {
                        ShowNotify("Contact Phone is empty");
                    }
                    else
                    {

                        WorkWithBD.CreateContact(ContactName, ContactAddress, ContactPhone);
                        UpdateAllUsersView();
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        // Команда на удаление контакта из основного окна
        public RelayCommand DeleteContact
        {
            get
            {
                return new RelayCommand(obj =>
               {
                   string resultStr = "Ничего не выбрано";
                   if (Selected != null)
                   {
                       resultStr = WorkWithBD.DeleteContact(Selected);
                       UpdateAllUsersView();
                   }

                   SetNullValuesToProperties();
                   ShowNotify(resultStr);
               }
                    );
            }
        }

        // Команда на редактирование контакта из окна редактирования контактов
        public RelayCommand EditСontact
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    if (Selected != null)
                    {

                        resultStr = WorkWithBD.EditContact(Selected, ContactName, ContactAddress, ContactPhone);
                        UpdateAllUsersView();
                        SetNullValuesToProperties();
                        ShowNotify(resultStr);
                        window.Close();

                    }
                    else ShowNotify(resultStr);

                }
                );
            }
        }

        // Команда на отображение окна добавления контакта
        public RelayCommand AddNewContcatWindow
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    AddNewUserWindow newUserWindow = new AddNewUserWindow();
                    newUserWindow.Owner = Application.Current.MainWindow;
                    newUserWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    newUserWindow.ShowDialog();
                }
                );
            }
        }

        // Команда на отображение окна редактирования контакта
        public RelayCommand EditContactWindow
        {
            get
            {
                return new RelayCommand(obj =>
                {

                    if (Selected != null)
                    {
                        EditUserWindow editUserWindow = new EditUserWindow(Selected);
                        editUserWindow.Owner = Application.Current.MainWindow;
                        editUserWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        editUserWindow.ShowDialog();

                    }
                    else
                    {

                        ShowNotify("Контакт не выбран");
                    }
                }
                    );
            }
        }


        // Функция для обнуления данных в текущем классе
        private void SetNullValuesToProperties()
        {
            ContactName = null;
            ContactAddress = null;
            ContactPhone = null;
        }

        // Функция обновления данных в основном окне
        private void UpdateAllUsersView()
        {
            AllContacts = WorkWithBD.GetAllContacts();
            MainWindow.AllContactsView.ItemsSource = null;
            MainWindow.AllContactsView.Items.Clear();
            MainWindow.AllContactsView.ItemsSource = AllContacts;
            MainWindow.AllContactsView.Items.Refresh();
        }

        // Показ окна с результатом выполнения команды
        private void ShowNotify(string message)
        {
            MessageView messageView = new MessageView(message);
            messageView.Owner = Application.Current.MainWindow;
            messageView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            messageView.ShowDialog();

        }

         
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
