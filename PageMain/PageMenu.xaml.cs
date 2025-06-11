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
using static KP_Balashova_.MainWindow;

namespace KP_Balashova_.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageMenu.xaml
    /// </summary>
    /// 
    public partial class PageMenu : Page
    {
        public static ObservableCollection<CartMenu> CartItems { get; set; } = new ObservableCollection<CartMenu>();
        private string _currentCategory;

        public PageMenu(string selectedCategory)
        {
            InitializeComponent();
            _currentCategory = selectedCategory;
            DataContext = this;

            // Добавляем обработчик изменений коллекции
            CartItems.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(TotalCartPrice));
                OnPropertyChanged(nameof(FormattedTotalCartPrice));
            };
        }

        public decimal TotalCartPrice => CalculateTotalPrice();

        public string FormattedTotalCartPrice
        {
            get
            {
                return TotalCartPrice % 1 == 0
                    ? $"{TotalCartPrice:0} ₽"
                    : $"{TotalCartPrice:0.##} ₽";
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_currentCategory == null)
            {
                DtGridTovar.ItemsSource = Balashova_DREntities.GetContext().Menu.ToList();
            }
            else
            {
                DtGridTovar.ItemsSource = Balashova_DREntities.GetContext().Menu
                    .Where(x => x.CategoryMenu.CategoryName == _currentCategory).ToList();
            }
            Balashova_DREntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
        }

        public class CartMenu : INotifyPropertyChanged
        {
            public int MenuItemId { get; set; }
            public string DishesName { get; set; }

            private int _quantity;
            public int Quantity
            {
                get => _quantity;
                set
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                    OnPropertyChanged(nameof(TotalPrice));
                    OnPropertyChanged(nameof(FormattedTotalPrice));
                }
            }

            private decimal _price;
            public decimal Price
            {
                get => _price;
                set
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                    OnPropertyChanged(nameof(TotalPrice));
                    OnPropertyChanged(nameof(FormattedTotalPrice));
                }
            }

            public decimal TotalPrice => Price * Quantity;

            public string FormattedTotalPrice
            {
                get
                {
                    return TotalPrice % 1 == 0
                        ? $"{TotalPrice:0} ₽"
                        : $"{TotalPrice:0.##} ₽";
                }
            }

            public int CategoryId { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void BtnDb_Click(object sender, RoutedEventArgs e)
        {
            var selectedDish = ((sender as Button).DataContext as ApplicationData.Menu);
            var existingItem = CartItems.FirstOrDefault(item => item.DishesName == selectedDish.DishesName);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                CartItems.Add(new CartMenu
                {
                    DishesName = selectedDish.DishesName,
                    Price = selectedDish.Price,
                    Quantity = 1,
                    //MenuItemId = selectedDish.Id,
                    //CategoryId = selectedDish.CategoryId
                });
            }

            OnPropertyChanged(nameof(TotalCartPrice));
            OnPropertyChanged(nameof(FormattedTotalCartPrice));
            MessageBox.Show($"{selectedDish.DishesName} добавлен в корзину!");
        }

        private decimal CalculateTotalPrice()
        {
            return CartItems.Sum(item => item.TotalPrice);
        }

        private void BtnCheckout_Click(object sender, RoutedEventArgs e)
        {
            if (!CartItems.Any())
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }

            MessageBox.Show($"Заказ оформлен! Сумма: {FormattedTotalCartPrice}");
            CartItems.Clear();
            OnPropertyChanged(nameof(TotalCartPrice));
            OnPropertyChanged(nameof(FormattedTotalCartPrice));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }   }
