using KP_Balashova_.ApplicationData;
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
using static KP_Balashova_.PageMain.PageMenu;

namespace KP_Balashova_.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageMenuAdd.xaml
    /// </summary>
    public partial class PageMenuAdd : Page
    {
        public PageMenuAdd()
        {
            InitializeComponent();
            DtGridTovar.ItemsSource = Balashova_DREntities.GetContext().Menu.ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Balashova_DREntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DtGridTovar.ItemsSource = Balashova_DREntities.GetContext().Menu.ToList();
        }

        private void BtnRd_Click(object sender, RoutedEventArgs e)
        {
            // Используем правильный тип ApplicationData.Menu
            var selectedItem = (sender as Button).DataContext as ApplicationData.Menu;
            AppFrame.FrameMain.Navigate(new PageMenuAdds(selectedItem));
        }

        private void BtnDel_Click_1(object sender, RoutedEventArgs e)
        {
            var itemsForRemoving = DtGridTovar.SelectedItems.Cast<ApplicationData.Menu>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {itemsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var context = Balashova_DREntities.GetContext();
                    context.Menu.RemoveRange(itemsForRemoving);
                    context.SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DtGridTovar.ItemsSource = context.Menu.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnDb_Click_1(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageMenuAdds(null));
        }

        private void BtnVix_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageMenu(null));
        }
    }
}
