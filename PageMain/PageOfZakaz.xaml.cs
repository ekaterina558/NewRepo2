using KP_Balashova_.ApplicationData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using System.Windows.Threading;


namespace KP_Balashova_.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageOfZakaz.xaml
    /// </summary>
    public partial class PageOfZakaz : Page, INotifyPropertyChanged
    {
        private ObservableCollection<PageMenu.CartMenu> _cartItems;
        private decimal _totalPrice;
        private bool _isDineIn;
        private bool _needReceipt = true;

        public PageOfZakaz()
        {
            InitializeComponent();
            DataContext = this;
            if (DineInRadio == null)
                throw new InvalidOperationException("DineInRadio not found in XAML");
            if (TableNumberBlock == null)
                throw new InvalidOperationException("TableNumberBlock not found in XAML");
            LoadFreeTables();
        }

        private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isDineIn && TableNumberComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите столик", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool isSbpPayment = CashRadio.IsChecked == true;
                var paymentPage = new PageOplata(TotalCartPrice, isSbpPayment);
                NavigationService.Navigate(paymentPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void CompletePayment()
        {
            _needReceipt = ReceiptYesRadio.IsChecked == true;
            SaveOrderToDB();
            PageMenu.CartItems.Clear();

            string receiptMessage = _needReceipt ? "Чек будет распечатан." : "Чек не будет распечатан.";
            MessageBox.Show($"Оплата на сумму {TotalCartPrice} ₽ прошла успешно!\n{receiptMessage}",
                          "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            PaymentBlock.Visibility = Visibility.Collapsed;
            NavigationService.Navigate(new Uri("PageMenu.xaml", UriKind.Relative));
        }

        private void CancelPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            PaymentBlock.Visibility = Visibility.Collapsed;
            MessageBox.Show("Оплата отменена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        

        private void LoadFreeTables()
        {
            // Здесь должна быть логика загрузки свободных столиков из БД
            
            var freeTables = new List<Table>
        {
            new Table { Id = 1, Number = "Столик 1" },
            new Table { Id = 2, Number = "Столик 2" },
            new Table { Id = 3, Number = "Столик 3" },
            new Table { Id = 3, Number = "Столик 4" },
            new Table { Id = 3, Number = "Столик 5" }
        };  

            TableNumberComboBox.ItemsSource = freeTables;
            if (freeTables.Any())
            {
                TableNumberComboBox.SelectedIndex = 0;
            }
        }

        public void SetOrderData(ObservableCollection<PageMenu.CartMenu> items, decimal total)
        {
            _cartItems = items;
            _totalPrice = total;
            OnPropertyChanged(nameof(CartItems));
            OnPropertyChanged(nameof(TotalCartPrice));
        }

        public ObservableCollection<PageMenu.CartMenu> CartItems => _cartItems ?? new ObservableCollection<PageMenu.CartMenu>();
        public decimal TotalCartPrice => _totalPrice;

        private void OrderType_Checked(object sender, RoutedEventArgs e)
        {
            if (DineInRadio == null || TableNumberBlock == null) return;

            _isDineIn = DineInRadio.IsChecked == true;
            TableNumberBlock.Visibility = _isDineIn ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveOrderToDB()
        {
            
            var orderType = _isDineIn ? "В кафе" : "С собой";
            var tableNumber = _isDineIn ? (TableNumberComboBox.SelectedItem as Table)?.Number : null;

            // Реализация сохранения в БД
            


            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Table
    {
        public int Id { get; set; }
        public string Number { get; set; }
    }

}


