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
using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;

namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int SelectedIndex = -1;
        public static string NonProfileImg = "E:\\Programming\\VS\\NETProjekt1\\Lab01\\Images\\"; 

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

            this.MinWidth = 750;
            this.MinHeight = 500;

            GetImageWiki();

            ImgPerson.Source = new BitmapImage(new Uri(NonProfileImg + "nonprofile.png"));

        }
        
        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
           
            String path = Path.Text != string.Empty ? Path.Text : NonProfileImg + "nonprofile.png";

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
            }
            else
            {
                people.Add(new Person { Surname = ageTextBox.Text, Name = nameTextBox.Text, Img = path });
            }

            Path.Text = string.Empty;
            nameTextBox.Text = string.Empty;
            ageTextBox.Text = string.Empty;
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

        private String GetImageWiki()
        {
            String wiki = "https://en.wikipedia.org/wiki/Special:RandomInCategory/";
            String keyword = "People";
            Match compare;
            HttpWebRequest request;
            HttpWebResponse response;

            do
            {
                request = (HttpWebRequest)WebRequest.Create(wiki + keyword);
                request.AllowAutoRedirect = true;
                request.Timeout = 10000;
                response = (HttpWebResponse)request.GetResponse();
                String responseUri = response.ResponseUri.ToString();
                response.Close();

                String[] parts = responseUri.Split('/');
                compare = Regex.Match(parts.Last(), @"Category:.+");

                if (compare.Success)
                {
                    String category = compare.Value;
                    String[] words = category.Split(':');
                    keyword = words.Last();
                }
                else
                {
                    keyword = parts.Last();
                }
            } while (compare.Success);

           String result = "https://en.wikipedia.org/wiki/" + keyword;
            return result;
        }
    }
}
