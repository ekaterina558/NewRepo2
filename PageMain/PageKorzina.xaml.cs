using KP_Balashova_.ApplicationData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using static KP_Balashova_.PageMain.PageMenu;

namespace KP_Balashova_.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageKorzina.xaml
    /// </summary>
    public partial class PageKorzina : Page, INotifyPropertyChanged
    {
        public PageKorzina()
        {
            InitializeComponent();
            DataContext = this;
            PageMenu.CartItems.CollectionChanged += (s, e) => UpdateCart();
        }

        public ObservableCollection<PageMenu.CartMenu> CartItems => PageMenu.CartItems;

        public decimal TotalCartPrice => CartItems.Sum(item => item.TotalPrice);

        public string FormattedTotalCartPrice
        {
            get
            {
                return TotalCartPrice % 1 == 0
                    ? $"{TotalCartPrice:0} ₽"
                    : $"{TotalCartPrice:0.##} ₽";
            }
        }

        private void UpdateCart()
        {
            OnPropertyChanged(nameof(CartItems));
            OnPropertyChanged(nameof(TotalCartPrice));
            OnPropertyChanged(nameof(FormattedTotalCartPrice));
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is PageMenu.CartMenu item)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    CartItems.Remove(item);
                }
                UpdateCart();
            }
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is PageMenu.CartMenu item)
            {
                item.Quantity++;
                UpdateCart();
            }
        }

        private void BtnCheckout_Click(object sender, RoutedEventArgs e)
        {
            if (!CartItems.Any())
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }

            var orderItems = new ObservableCollection<PageMenu.CartMenu>(CartItems.Select(item => new PageMenu.CartMenu
            {
                DishesName = item.DishesName,
                Quantity = item.Quantity,
                Price = item.Price,
                MenuItemId = item.MenuItemId,
                CategoryId = item.CategoryId
            }));

            var orderPage = new PageOfZakaz();
            orderPage.SetOrderData(orderItems, TotalCartPrice);
            NavigationService.Navigate(orderPage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

