using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace KP_Balashova_.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageOplata.xaml
    /// </summary>
    public partial class PageOplata : Page, INotifyPropertyChanged
    {
        private decimal _totalAmount;
        private bool _isSbpPayment;
        private DispatcherTimer _paymentTimer;

        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChanged();
            }
        }

        public PageOplata(decimal amount, bool isSbpPayment)
        {
            InitializeComponent();
            DataContext = this;

            TotalAmount = amount;
            _isSbpPayment = isSbpPayment;

            if (isSbpPayment)
            {
                QrCodeImage.Source = GenerateQrCode();
                QrCodeImage.Visibility = Visibility.Visible;
            }
            else
            {
                CardPaymentText.Visibility = Visibility.Visible;
            }

            StartPaymentTimer();
        }

        private BitmapImage GenerateQrCode()
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/Resources/qr-kod.png");
            bitmap.EndInit();
            return bitmap;
        }

        private void StartPaymentTimer()
        {
            _paymentTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
            _paymentTimer.Tick += (s, e) =>
            {
                _paymentTimer.Stop();
                PaymentCompleted();
            };
            _paymentTimer.Start();
        }

        private void PaymentCompleted()
        {
            // Показываем сообщение об успехе
            SuccessText.Visibility = Visibility.Visible;
            QrCodeImage.Visibility = Visibility.Collapsed;
            CardPaymentText.Visibility = Visibility.Collapsed;

            // Через 3 секунды выполняем переход
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            timer.Tick += (s, e) =>
            {
                timer.Stop();

                // Передаем null или конкретную категорию, если нужно
                var menuPage = new PageMenu(null); // или new PageMenu("Салаты") и т.д.
                NavigationService.Navigate(menuPage);
            };
            timer.Start();
        }

        private void CancelPayment_Click(object sender, RoutedEventArgs e)
        {
              _paymentTimer?.Stop();
              NavigationService.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
