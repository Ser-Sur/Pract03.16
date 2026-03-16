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
using System.Windows.Shapes;

namespace Pract03._16.ModelsBD
{
    /// <summary>
    /// Логика взаимодействия для TableInteraction.xaml
    /// </summary>
    public partial class TableInteraction : Window
    {
        AeroFlotContext _db = new();
        Flight _flight;

        public TableInteraction()
        {
            InitializeComponent();
        }

        private void AddEditTI_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new();
            if (tbDestination.Text == null) errors.AppendLine("Заполните поле Пункт назначения.");
            if (tbDepartureTime.Text == null) errors.AppendLine("Заполните поле Время вылета.");
            if (tbArrivalTime.Text == null) errors.AppendLine("Заполните поле Время прибытия.");
            if (tbCountFreePlaces.Text == null) errors.AppendLine("Заполните поле Кол-во свободных мест.");
            else if (!Int32.TryParse(tbCountFreePlaces.Text, out int countFP)) errors.AppendLine("Введите корректное Кол-во свободных мест.");
            if (tbTypePlane.Text == null) errors.AppendLine("Заполните поле Пункт назначения.");
            if (tbCapacity.Text == null) errors.AppendLine("Заполните поле Кол-во посадочных мест.");
            else if (!Int32.TryParse(tbCountFreePlaces.Text, out int countFP)) errors.AppendLine("Введите корректное Кол-во посадочных мест.");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (Flights.fly == null)
                {
                    Random rnd = new();
                    int id = 21326 + rnd.Next(1000, 9999);
                    _flight.FlightId = id;
                    _db.Flights.Add(_flight);
                    _db.SaveChanges();
                }
                else _db.SaveChanges();
                this.Close();
            }
            catch (Exception ex) 
            {
                _db.Flights.Remove(_flight);
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CancelTi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TableInteraction_Loaded(object sender, RoutedEventArgs e)
        {
            if (Flights.fly == null)
            {
                wTableInteraction.Title = "Добавление записи";
                btnAddEditTI.Content = "Добавить";
                _flight = new();
            }
            else
            {
                wTableInteraction.Title = "Изменение записи";
                btnAddEditTI.Content = "Изменить";
                _flight = _db.Flights.Find(Flights.fly.FlightId);
            }
            wTableInteraction.DataContext = _flight;
        }
    }
}
