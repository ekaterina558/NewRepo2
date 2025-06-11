using KP_Balashova_.ApplicationData;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        private Random random = new Random();
        public PageAdmin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


        private void Btn_Adm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = Balashova_DREntities.GetContext().User.FirstOrDefault(x => x.Login == TxbLogin.Text && x.Password == PsbPassword.Password);
                if (userObj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка авторизации!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch (userObj.ID)
                    {
                        case 1:
                            // Генерация случайного номера столика
                            int tableNumber = random.Next(1, 5);
                            MessageBox.Show($"Здравствуйте, Администратор {userObj.Name}! Вас вызывают к столику №{tableNumber} для разрешения вопроса.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.FrameMain.Navigate(new PageMenuAdd());
                            break;
                        case 2:
                            MessageBox.Show($"Здравствуйте, гость {userObj.Name}!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"Ошибка: {Ex.Message}\nКритическая ошибка приложения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
