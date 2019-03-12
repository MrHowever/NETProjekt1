using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int SelectedIndex = -1;
        public static string NonProfileImg = "C:\\Users\\Waldemar\\Desktop\\Platormy Programistyczne .NET i JAVA\\NETProjekt1\\Lab01\\Images\\"; 

        ObservableCollection<Person> people = new ObservableCollection<Person>
        {
            new Person { Name = "Jan", Surname = "Kowalski", Img = NonProfileImg + "Man2.jpeg" },
            new Person { Name = "Agata", Surname = "Nowak", Img = NonProfileImg + "Lady1.jpeg"}
        };

        public ObservableCollection<Person> Items
        {
            get => people;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ImgPerson.Source = new BitmapImage(new Uri(NonProfileImg + "nonprofile.png"));
        }
        
        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedIndex >= 0)
            {
                people[SelectedIndex] = new Person
                {
                    Surname = ageTextBox.Text,
                    Name = nameTextBox.Text,
                    Img = Path.Text
                };  
                
                addNewPersonButton.Content = "Add new person";
                btnImg.Content = "Search your picture...";
                SelectedIndex = -1;
                Path.Text = "";
            }
            else
            {
                String path = Path.Text != "" ? Path.Text : NonProfileImg + "nonprofile.png";
                people.Add(new Person { Surname = ageTextBox.Text, Name = nameTextBox.Text, Img = path });
                Path.Text = "";
                ImgPerson.Source = new BitmapImage(new Uri(NonProfileImg + "nonprofile.png"));
            }

            nameTextBox.Text = "";
            ageTextBox.Text = "";
            ImgPerson.Source = new BitmapImage(new Uri(NonProfileImg + "nonprofile.png"));
        }

        private void BtnImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Select your picture...";
            openFile.Filter = "All files|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*jpg;*jpeg|" + "Portable Network Graphic (*.png)|*.png";
            if(openFile.ShowDialog()==true)
            {
                ImgPerson.Source = new BitmapImage(new Uri(openFile.FileName));
                Path.Text = openFile.FileName;
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listbox = sender as ListBox;
            Person selectedItem = (Person)listbox.SelectedItem;
            nameTextBox.Text = selectedItem.Name;
            ageTextBox.Text = selectedItem.Surname;
            Path.Text = selectedItem.Img;
            ImgPerson.Source = new BitmapImage(new Uri(selectedItem.Img));
            addNewPersonButton.Content = "Change";
            btnImg.Content = "Change your picture...";
            SelectedIndex = listbox.SelectedIndex;
        }
    }
}