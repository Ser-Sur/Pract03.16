using Pract03._16.ModelsBD;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Вариант 2.
// Сведения о рейсах Аэрофлота. База данных должна содержать следующую информацию: 
// номер рейса, пункт назначения, время вылета, 
// время прибытия, количество свободных мест,
// тип самолета и его вместимость.

namespace Pract03._16
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            Flights.fly = null;
            TableInteraction f = new();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInDataGrid();
        }
        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultMB;
            resultMB = MessageBox.Show("Удалить запись?","Удаление записи",MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (resultMB == MessageBoxResult.Yes)
            {
                try
                {
                    Flight row = (Flight)dg_Flishts.SelectedItem;
                    if (row != null)
                    {
                        using (AeroFlotContext _db = new())
                        {
                            _db.Flights.Remove(row);
                            _db.SaveChanges();
                        }
                        LoadDBInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления");
                }
            }
            else dg_Flishts.Focus();
        }
        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            if (dg_Flishts.SelectedItem != null)
            {
                Flights.fly = (Flight)dg_Flishts.SelectedItem;
                TableInteraction f = new();
                f.Owner = this;
                f.ShowDialog();
                LoadDBInDataGrid();
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
        }

        void LoadDBInDataGrid()
        {
            using(AeroFlotContext  _db = new())
            {
                int selectedIndex = dg_Flishts.SelectedIndex;
                dg_Flishts.ItemsSource = _db.Flights.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex >= dg_Flishts.Items.Count) selectedIndex = dg_Flishts.Items.Count - 1;
                    dg_Flishts.SelectedIndex = selectedIndex;
                    dg_Flishts.ScrollIntoView(dg_Flishts.SelectedItem);
                }
                dg_Flishts.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        public void FindRecordInDG(string criterion)
        {
            List<Flight> listItem = (List<Flight>)dg_Flishts.ItemsSource;
            var filtered = listItem.Where(p => p.Destination.Contains(criterion) ||
                (!string.IsNullOrEmpty(p.DepartureTime) ? p.DepartureTime.Contains(criterion) : false) ||
                (!string.IsNullOrEmpty(p.ArrivalTime) ? p.ArrivalTime.Contains(criterion) : false));
            if (filtered.Count() > 0)
            {
                var item = filtered.First();
                dg_Flishts.SelectedItem = item;
                dg_Flishts.ScrollIntoView(item);
                dg_Flishts.Focus();
            }
        }
    }
}