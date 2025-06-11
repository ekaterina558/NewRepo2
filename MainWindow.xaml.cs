using KP_Balashova_.ApplicationData;
using KP_Balashova_.PageMain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace KP_Balashova_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Инициализация фрейма
            AppFrame.FrameMain = FrmMain;
            AppFrame.FrameMain.Navigate(new PageMenu(null));
            
        }


        private void UpdateCartBadge()
        {
            // Обновляем UI (например, TextBlock с количеством товаров)
            Dispatcher.Invoke(() =>
            {
                // Здесь код обновления плюсика/значка корзины
            });
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageMenu((sender as Button).Content.ToString()));
        }

        private void Btn_Korzina_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageKorzina());
        }

        private void Btn_Sotrudnik_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Сейчас подойдёт к вам сотрудник кафе для помощи!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Btn_Admin_Click_1(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageAdmin());
        }
    }
}