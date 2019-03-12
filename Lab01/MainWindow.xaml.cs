﻿using Microsoft.Win32;
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
        ObservableCollection<Person> people = new ObservableCollection<Person>
        {
            new Person { Name = "Jan", Surname = "Kowalski", Img = "C:\\Users\\Waldemar\\Desktop\\Platormy Programistyczne .NET i JAVA\\NETProjekt1\\Lab01\\Images\\Man2.jpeg" },
            new Person { Name = "Agata", Surname = "Nowak", Img = "C:\\Users\\Waldemar\\Desktop\\Platormy Programistyczne .NET i JAVA\\NETProjekt1\\Lab01\\Images\\Lady1.jpeg"}
        };

        public ObservableCollection<Person> Items
        {
            get => people;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            people.Add(new Person {Surname = ageTextBox.Text, Name = nameTextBox.Text, Img = Path.Text});
            Path.Text = "";
            ImgPerson.Source = new BitmapImage(new Uri("C:\\Users\\Waldemar\\Desktop\\Platormy Programistyczne .NET i JAVA\\NETProjekt1\\Lab01\\Images\\nonprofile.png"));
            nameTextBox.Text = "";
            ageTextBox.Text = ""; 
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
            ImgPerson.Source = new BitmapImage(new Uri(selectedItem.Img));
        }

      
    }
}