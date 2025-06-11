using KP_Balashova_.ApplicationData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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

namespace KP_Balashova_.PageMain
{
    public partial class PageMenuAdds : Page
    {
        public class MenuItemData
        {
            public int ID { get; set; }
            public string DishesName { get; set; }
            public string Ingredients { get; set; }
            public int Price { get; set; }
            public int Calories { get; set; }
        }

        private MenuItemData _currentMenu = new MenuItemData();

        public PageMenuAdds(ApplicationData.Menu selectedMenu)
        {
            InitializeComponent();
            _currentMenu = new MenuItemData();
            if (selectedMenu != null)
            {
                _currentMenu = new MenuItemData
                {
                    ID = selectedMenu.MenuItemID,
                    DishesName = selectedMenu.DishesName,
                    Ingredients = selectedMenu.Ingredients,
                    Price = selectedMenu.Price,
                    Calories = selectedMenu.Calories // Если null, то 0
                };
            }

            DataContext = _currentMenu;
        }

        // Остальной код без изменений
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Проверка введенных данных
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentMenu.DishesName))
                errors.AppendLine("Укажите название товара");
            if (string.IsNullOrWhiteSpace(_currentMenu.Ingredients))
                errors.AppendLine("Укажите ингредиенты");
            if (_currentMenu.Price <= 0)
                errors.AppendLine("Цена не может быть меньше или равна 0");
            if (_currentMenu.Calories < 0)
                errors.AppendLine("Количество калорий не может быть отрицательным");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                using (var context = new Balashova_DREntities())
                {
                    ApplicationData.Menu menuItem;

                    if (_currentMenu.ID == 0)
                    {
                        // Создаем новый элемент
                        menuItem = new ApplicationData.Menu
                        {
                            DishesName = _currentMenu.DishesName,
                            Ingredients = _currentMenu.Ingredients,
                            Price = _currentMenu.Price,
                            Calories = _currentMenu.Calories,
                           
                        };
                        context.Menu.Add(menuItem);
                    }
                    else
                    {
                        // Обновляем существующий
                        menuItem = context.Menu.FirstOrDefault(m => m.MenuItemID == _currentMenu.ID);
                        if (menuItem == null)
                        {
                            MessageBox.Show("Элемент меню не найден в базе данных");
                            return;
                        }

                        menuItem.DishesName = _currentMenu.DishesName;
                        menuItem.Ingredients = _currentMenu.Ingredients;
                        menuItem.Price = _currentMenu.Price;
                        menuItem.Calories = _currentMenu.Calories;
                    }

                    context.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены");
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.ToString()}"); // Показывает полное исключение
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
