using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Lab01
{
    /// <summary>
    /// Logika interakcji dla klasy DatabasePage.xaml
    /// </summary>
    public partial class DatabasePage : Window
    {
        PlaceEntities _db = new PlaceEntities();
        public static DataGrid dataGrid;

        public DatabasePage()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            myDataGrid.ItemsSource = _db.BusinessSpecifics.ToList();
            dataGrid = myDataGrid;
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            BusinessSpecifics newYelp = new BusinessSpecifics()
            {
                name = nameTextBox.Text,
                rating = Convert.ToInt32(ratingTextBox.Text),
                price = priceTextBox.Text,
                city = cityTextBox.Text
            };

            _db.BusinessSpecifics.Add(newYelp);
            _db.SaveChanges();

            dataGrid.ItemsSource = _db.BusinessSpecifics.ToList();
        }
    }
}
