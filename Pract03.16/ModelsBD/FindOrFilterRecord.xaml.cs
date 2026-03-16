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
using Pract03._16;

namespace Pract03._16.ModelsBD
{
    /// <summary>
    /// Логика взаимодействия для FindOrFilterRecord.xaml
    /// </summary>
    public partial class FindOrFilterRecord : Window
    {
        public FindOrFilterRecord()
        {
            InitializeComponent();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            string criterion = tbCriterion.Text;
            MainWindow.FindRecordInDG(criterion);
            wFindOrFilter.Close();
        }
    }
}
