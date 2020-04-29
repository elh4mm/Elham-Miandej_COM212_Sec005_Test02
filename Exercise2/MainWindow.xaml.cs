using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Exercise2
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource playerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("playerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // playerViewSource.Source = [generic data source]
        }
        private BaseBall.BaseballEntities dbcontext =
         new BaseBall.BaseballEntities();

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            playerDataGrid.DataContext = dbcontext.Players.Local.Where(a => a.LastName.ToLower().Contains(txtSearch.Text.ToLower()));

        }

       

        private void DisplayAuthorsTable_Load(object sender, RoutedEventArgs e)
        {
            dbcontext.Players.OrderBy(a => a.FirstName).ThenBy(a => a.LastName).Load();

            playerDataGrid.DataContext = dbcontext.Players.Local;


        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            txtId.Clear();
            playerDataGrid.DataContext = dbcontext.Players.Local;
            
        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {
            playerDataGrid.DataContext = dbcontext.Players.Local.Where(a => a.PlayerID.ToString().Equals( (txtId.Text)));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lblAvg.Content = $" { dbcontext.Players.Local.Where(a => a.PlayerID == a.PlayerID).Average(a => a.BattingAverage):00.00}";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            playerDataGrid.DataContext =dbcontext.Players.Local.Where(a=>a.BattingAverage == dbcontext.Players.Local.Where(p => p.PlayerID == p.PlayerID).Max(p => p.BattingAverage));
        }
    }
}
