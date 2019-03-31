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
using System.Net.Http; 
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Threading;
using System.ComponentModel;


namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
         public int SelectedIndex = -1;
         Timer timer;
   
        //public static string NonProfileImg = @"E:\Programming\VS\NETProjekt1\Lab01\Images\"; 
        public static string NonProfileImg = @"C:\Users\Waldemar\Desktop\Platormy Programistyczne .NET i JAVA\NETProjekt1\Lab01\Images\";

        //objects to JSON
        string NewsUri = @"https://newsapi.org/v2/everything?domains=bbc.co.uk&from=2019-03-25&to=2019-03-25&apiKey=55d4ae5d63ed4fafa486a113d6dbfae0";
        public int NewsCounter { get; set; } = 0;
        public News.RootObject Root { get; set; } 
        
        ObservableCollection<Person> people = new ObservableCollection<Person>
        {
            new Person { Name = "Jan", Surname = "Kowalski", Img = NonProfileImg + "Man2.jpeg" },
            new Person { Name = "Agata", Surname = "Nowak", Img = NonProfileImg + "Lady1.jpeg"}
        };

        public ObservableCollection<Person> Items
        {
            get => people;
        }
        public object ProgresChanged { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
           // GetJSON(); 
            this.MinWidth = 750;
            this.MinHeight = 500;
            CallAPI();
            timer = new Timer(3000);
            timer.Elapsed += FillRandomAsync;
            
            ImgPerson.Source = new BitmapImage(new Uri(NonProfileImg + "nonprofile.png"));

        }
        //methods created special to JSON newsAPI 
        private async void CallAPI()
        {
            Random rand = new Random();
            String randomDay = rand.Next(1, 26).ToString("D2");
            NewsUri = @"https://newsapi.org/v2/everything?domains=bbc.co.uk&from=2019-03-"+randomDay+"&to=2019-03-"+randomDay+"&apiKey=55d4ae5d63ed4fafa486a113d6dbfae0";
            NewsCounter = 0;
            Root = await Config.Deserialize(NewsUri);
            SetUpArticle();
        }
        private void SetUpArticle()
        {
            if (Root.articles.Length <= NewsCounter)
            {
                Article.AppendText("There are no more articles to be read!\n");
                return;
            }

            Article.AppendText("Title: " + Environment.NewLine + Root.articles[NewsCounter].title + Environment.NewLine );
            Article.AppendText("Description: " + Environment.NewLine + Root.articles[NewsCounter].description + Environment.NewLine + Environment.NewLine);
        }
        private void GetArticle_Click(object sender, RoutedEventArgs e)
        {
            CallAPI();
            Article.Text = string.Empty;
            int amount = Convert.ToInt32(Amount.Text);

            if ( amount < 20 && amount >0 )
            {
                int newsNumber = 0; 

                for (int i = 1; i < amount; i++)
                { newsNumber += 1;
                    Article.AppendText("News nr: " + newsNumber + Environment.NewLine);
                    this.NewsCounter++;
                    SetUpArticle();
                    Article.AppendText("____________________________"  + Environment.NewLine);
                }
            } else if(amount >=20 )
            {
                Article.Text = string.Empty;
                Article.Text = "You want too much!" + Environment.NewLine + "You can't get more than 19 news"; 
            }
           else if(amount <=0)
            {
                Article.Text = string.Empty;
                Article.Text = "Number must be more than 0";
            }
            NewsCounter = 0;
          
        }
        //private async void GetJSON()
        //{
        //    var json = await ConnectionAPI.GetAsync(NewsUri);
        //    Article.Text = json;
        //}
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
            if (openFile.ShowDialog() == true)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private async void FillRandomAsync(object sender, ElapsedEventArgs e)
        {
            Progress<ProgresReport> progress = new Progress<ProgresReport>();
            progress.ProgressChanged += ReportProgress; 

            Tuple<String, String, String> person = await GetRandomPersonAsync(progress);

            Application.Current.Dispatcher.Invoke(() =>
              people.Add(new Person { Surname = person.Item2, Name = person.Item1, Img = person.Item3 })
              );
        }

        private void ReportProgress(object sender, ProgresReport e)
        {
             try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    reportBar.Value = e.Percentage;
                    progressInfo.Text = e.progressInfo; 
                });
            }
            catch { } 
        }
        private int CountPercentageProgress(int level, int levelCount)
        {
            return (level * 100) / levelCount;
        }

        private async Task<Tuple<String,String,String>> GetRandomPersonAsync(IProgress<ProgresReport> progress)
         {
            ProgresReport report = new ProgresReport();
            int levelCount = 4;

            int startLevel = 1;
            String url = await GetImageWiki();
            report.Percentage = CountPercentageProgress(startLevel,levelCount);
            report.progressInfo = "Conneting with Wikipedia and getting url";
            progress.Report(report);

            startLevel += 1; 
            Tuple<String, String> name = GetNameFromPage(url);
            report.Percentage = CountPercentageProgress(startLevel, levelCount);
            report.progressInfo = "Getting name of random 20th century Chancellor of Germany";
            progress.Report(report);

            startLevel += 1;
            String image = GetImageFromPage(url);
            report.Percentage = CountPercentageProgress(startLevel, levelCount);
            report.progressInfo = "Getting image of random 20th century Chancellor of Germany";
            progress.Report(report);

            startLevel += 1;
            Tuple<String, String, String> randomPerson = new Tuple<String, String, String>(name.Item1, name.Item2, image);
            report.Percentage = CountPercentageProgress(startLevel, levelCount);
            report.progressInfo = "Saving data";
            progress.Report(report);

            Task.Delay(100).Wait();

            report.Percentage = 0;
            report.progressInfo = timer.Enabled ? "Ready" : "Random content stopped"; 
            progress.Report(report);  
             
                  
            return randomPerson;
        }
              
        private async Task<String> GetImageWiki()
        {
            String wiki = "https://en.wikipedia.org/wiki/Special:RandomInCategory/";
            String keyword = "20th-century_Chancellors_of_Germany";
            Match compare;
            HttpWebRequest request;
            WebResponse response;
           
            do
            {
                request = (HttpWebRequest)WebRequest.Create(wiki + keyword);
                request.AllowAutoRedirect = true;
                request.Timeout = 10000;
                response = await request.GetResponseAsync(); 
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
            System.Diagnostics.Debug.Write("\n" + result + "\n");
 
            return result;
        }
    
        String GetImageFromPage(String url)
        {
            HtmlDocument doc = new HtmlWeb().Load(@url);

            if (doc != null)
            {
               String imageUri = "https:" + doc.DocumentNode.SelectSingleNode("//tr//td[@colspan='2']//a//img").GetAttributeValue("src", "");

               return imageUri;
            }
            return String.Empty;
        }

        private Tuple<String, String> GetNameFromPage(String url)
        {
            HtmlDocument doc = new HtmlWeb().Load(@url);
            String name, surname;

            if (doc != null)
            {
                String text = doc.DocumentNode.SelectSingleNode("//h1").InnerText;
                String[] words = text.Split(' ');
                name = words[0];
                surname = String.Join(" ", words.Skip(1).ToList());

                return new Tuple<String, String>(name, surname);
            }
       
            return new Tuple<String, String>(String.Empty, String.Empty);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            if(progressInfo.Text == "Ready")
               progressInfo.Text = "Random content stopped";
        }

       
    }
}
