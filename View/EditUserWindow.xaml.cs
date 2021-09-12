using AddressBook.Model;
using AddressBook.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace AddressBook.View
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public EditUserWindow(Contact userToEdit)
        {
            InitializeComponent();
            DataContext = new EventManage();
            EventManage.Selected = userToEdit;
            EventManage.ContactName = userToEdit.FullName;
            EventManage.ContactAddress = userToEdit.Address;
            EventManage.ContactPhone = userToEdit.Phone;
            //DataManageVM.UserPosition = userToEdit.Position;
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
