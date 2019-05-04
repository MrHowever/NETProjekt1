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
        int index = -1;

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
           
            if (index >= 0)
            {
                BusinessSpecifics updateBS = (from m in _db.BusinessSpecifics
                                   where m.id == index
                                   select m).Single();
                updateBS.name = nameTextBox.Text;
                updateBS.rating = Convert.ToInt32(ratingTextBox.Text);
                updateBS.price = priceTextBox.Text;
                updateBS.city = cityTextBox.Text;  

                _db.SaveChanges();
                dataGrid.ItemsSource = _db.BusinessSpecifics.ToList();

                insertBtn.Content = "Insert";
                index = -1; 
            }
            else
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

        private void MyDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int Id = (myDataGrid.SelectedItem as BusinessSpecifics).id;
            nameTextBox.Text = (myDataGrid.SelectedItem as BusinessSpecifics).name;
            ratingTextBox.Text = (myDataGrid.SelectedItem as BusinessSpecifics).rating.ToString();
            priceTextBox.Text = (myDataGrid.SelectedItem as BusinessSpecifics).price;
            cityTextBox.Text = (myDataGrid.SelectedItem as BusinessSpecifics).city;
            insertBtn.Content = "Udpdate";
            index = Id; 

        }
    }
}
