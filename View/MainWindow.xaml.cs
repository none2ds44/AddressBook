using AddressBook.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace AddressBook.View
{
    public partial class MainWindow : Window
    {

        public static ListView AllContactsView;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new EventManage();
            AllContactsView = ViewAllContacts;
        }
    }
}
