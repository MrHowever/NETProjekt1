using System;
using System.Diagnostics;
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

                try
                {
                    _db.SaveChanges();
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("The id is not unique!");
                    throw; 
                }

                dataGrid.ItemsSource = _db.BusinessSpecifics.ToList();

                insertBtn.Content = "Insert";
                index = -1; 
            }
            else
            {
                BusinessSpecifics newYelp = new BusinessSpecifics()
                {
                    name = nameTextBox.Text,
                    rating = int.Parse(ratingTextBox.Text),
                    price = priceTextBox.Text,
                    city = cityTextBox.Text
                };

                
                _db.BusinessSpecifics.Add(newYelp);

                try
                {
                    _db.SaveChanges();
                }
                catch(Exception ex)
                {
                    _db.BusinessSpecifics.Remove(newYelp);
                    Debug.WriteLine("The id is not unique");
                    throw; 
                }

                dataGrid.ItemsSource = _db.BusinessSpecifics.ToList();
                nameTextBox.Text = string.Empty;
                ratingTextBox.Text = string.Empty;
                priceTextBox.Text = string.Empty;
                cityTextBox.Text = string.Empty;

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

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int Id = (myDataGrid.SelectedItem as BusinessSpecifics).id;
            var deleteBS = _db.BusinessSpecifics.Where(m => m.id == Id).Single();
            _db.BusinessSpecifics.Remove(deleteBS);

            try
            {
                _db.SaveChanges();
            }
            catch(Exception ex)
            {
               Debug.WriteLine("Cannot delete");
                throw;
            }
            

            myDataGrid.ItemsSource = _db.BusinessSpecifics.ToList();

            insertBtn.Content = "Insert";
            nameTextBox.Text = string.Empty;
            ratingTextBox.Text = string.Empty;
            priceTextBox.Text = string.Empty;
            cityTextBox.Text = string.Empty;

        }

        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            string startLetter = FirstLetter.Text;
            string costs = priceCheck.Text;

            var result = _db.BusinessSpecifics.AsQueryable();

            if (!string.IsNullOrWhiteSpace(startLetter))
            {
                result = result.Where(x => x.name.StartsWith(startLetter));
            }

            myDataGrid.ItemsSource = result.ToList();

           
          if (!string.IsNullOrWhiteSpace(costs))
            {
                result = result.Where(x => x.price.Equals(costs));
            }
            myDataGrid.ItemsSource = result.ToList();
        }

       
    }
}
