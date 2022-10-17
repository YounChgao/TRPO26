using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AppViewModel();
        }
    }

    public class Customer : INotifyPropertyChanged //Подключается интерфейс, который позволяет уведомлять об изменении состояния
    {
        //Данные о задаче
        private string name;
        private string task;
        private int price;
        private DateTime deadline;
        private bool isSolved;

        public Customer(string name, string task, int price, DateTime deadline) //Простой конструктор
        {
            this.name = name;
            this.task = task;
            this.price = price;
            this.deadline = deadline;
            this.isSolved = false;
        }

        //Геттеры и сеттеры
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string Task
        {
            get
            {
                return this.task;
            }
        }

        public int Price
        {
            get
            {
                return this.price;
            }
        }

        public string DeadlineString
        {
            get
            {
                return $"{this.deadline.Day}.{this.deadline.Month}.{this.deadline.Year}";
            }
        }

        public bool IsSolved
        {
            get
            {
                return this.isSolved;
            }

            set
            {
                this.isSolved = value;
                OnPropertyChanged("IsSolved"); //Если свойство меняется, вызывается метод, который уведомляет об изменении модели
                OnPropertyChanged("Color"); //Если изменено несколько значений, можно вызвать дополнительный метод
            }
        }

        public string Color
        {
            get
            { //Если задача решена, будет возвращён синий цвет, иначе он будет зависеть от того, прошёл ли дедлайн
                return this.isSolved ? "Blue" : DateTime.Now.CompareTo(this.deadline) == -1 ? "Black" : "Red";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged; //Событие, которое будет вызвано при изменении модели 
        public void OnPropertyChanged([CallerMemberName] string prop = "") //Метод, который скажет ViewModel, что нужно передать виду новые данные
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    //Теперь создайте модель представления:
public class AppViewModel : INotifyPropertyChanged
    {

        private Customer selectedCustomer;
        private ObservableCollection<Customer> customers;


        public AppViewModel()
        {
            customers = new ObservableCollection<Customer>() //Добавление данных для тестирования
                {
                        new Customer("Josh", "Fix printer", 500, new DateTime(2019, 5, 11)),
                        new Customer("Josh", "Install fax", 350, new DateTime(2019, 6, 15)),
                        new Customer("Tyler", "Update soft", 100, new DateTime(2019, 6, 17)),
                        new Customer("Nico", "Install antivirus", 400, new DateTime(2019, 6, 19)),
                        new Customer("Tyler", "Fix printer", 500, new DateTime(2019, 6, 21)),
                        new Customer("Nico", "Update soft", 200, new DateTime(2019, 6, 27))
                };
        }

        public Customer SelectedCustomer
        {
            get
            {
                return this.selectedCustomer;
            }

            set
            {
                this.selectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return this.customers;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
