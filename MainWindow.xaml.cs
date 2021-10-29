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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace FileBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ImageBrush folderImage = new ImageBrush();
        public List<Button> buttonlist = new List<Button>();
        public double btnH = 100;
        public double btnW = 100;
        private void renderButtons(string direct)
        {
            
            titlebar.Text = direct;
            var dirs = from dir in Directory.EnumerateDirectories(direct) select dir;
            foreach (int y in Enumerable.Range(0, dirs.Count()))
            {
                Button tempbutton = Button_creator();
                tempbutton.Width = 100;
                tempbutton.Height = 100;
                tempbutton.Click += button_click;
                tempbutton.Background = folderImage;
                tempbutton.Foreground = Brushes.White;
                tempbutton.Content = dirs.ElementAt(y);
                buttonlist.Add(tempbutton);
                LayoutRoot.Children.Add(buttonlist.Last());
            }
        }
        private void RerenderButtons(double _h,double _w)
        {
            double jump = btnW;
            double down = 0;
            var dirs = from dir in Directory.EnumerateDirectories(direct) select dir;
            foreach (Button tempbutton in buttonlist)
            {
                button_customizer(tempbutton);
                if (jump+2*btnW > _w)
                {
                    jump = btnW;
                    down += btnH;
                }
                Canvas.SetLeft(tempbutton, jump);
                Canvas.SetTop(tempbutton, down);
                jump += btnW;
            }
            scrollingbar.Height = _h;
        }
        private void button_click(object sender, RoutedEventArgs e)
        {
            
            unrenderButtons();
            direct=(sender as Button).Content.ToString();
            renderButtons((sender as Button).Content.ToString());
        }

        private void unrenderButtons()
        {
            foreach (Button butt in buttonlist)
            {
                LayoutRoot.Children.Remove(butt);
            }
            buttonlist.Clear();
        }
        private Button Button_creator()
        {
            return button_customizer(new Button());
        }
        private Button button_customizer(Button tempbutton)
        {
            tempbutton.Width = 90;
            tempbutton.Height = 90;
            tempbutton.Click += button_click;
            tempbutton.Background = folderImage;
            tempbutton.Foreground = Brushes.White;
            tempbutton.BorderThickness = new Thickness(0);
            tempbutton.ContextMenu = RightClickMenu(new ContextMenu());
            return tempbutton;
        }

        public string direct = "D:";
        private ContextMenu RightClickMenu(ContextMenu tempContext)
        {
            MenuItem Cpybtn = new MenuItem();
            Cpybtn.Header = "Copy";
            Cpybtn.Click += Cpybtn_Click;
            tempContext.Items.Add(Cpybtn);
            return tempContext;
        }

        private void Cpybtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        public MainWindow()
        {
            ContextMenu buttonContext = new ContextMenu();
            InitializeComponent();
            titlebar.Text = direct;
            var dirs = from dir in Directory.EnumerateFileSystemEntries(direct) select dir;
            Button[] x = new Button[dirs.Count()];
            folderImage.ImageSource = new BitmapImage(new Uri("C:/Users\\Nandan Varma\\Desktop\\csharpfilebrowser\\FileBrowser\\FileBrowser\\images\\folder.png"));
            int jump = 30;
            int down = 0;
            foreach (int y in Enumerable.Range(0, dirs.Count()))
            {
                if (jump > 700)
                {
                    jump = 30;
                    down += 100;
                }
                Button tempbutton = Button_creator();
                Canvas.SetLeft(tempbutton, jump);
                Canvas.SetTop(tempbutton, down);
                tempbutton.Content = dirs.ElementAt(y);
                buttonlist.Add(tempbutton);
                LayoutRoot.Children.Add(buttonlist.Last());
                jump += 100;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RerenderButtons((sender as Window).Height,(sender as Window).Width);
            
        }
    }
}
