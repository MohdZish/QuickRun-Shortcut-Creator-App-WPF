using System;
using System.IO;
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

namespace QuickRun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Test();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Buttonclose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Buttonminimise(object sender, RoutedEventArgs e)
        {
            MainPage.WindowState = WindowState.Minimized;
        }

        private void Test()
        {
            string[] readText = File.ReadAllLines(@"C:\test\test.txt");
            foreach (string text in readText)
            {
                string myString = text;
                string[] mon = myString.Split('-');   //We're making array for mon[0] is name and mon[1] is shortcut

                Button namebutton = new Button();
                StackPanel insidebutton = new StackPanel();
                insidebutton.HorizontalAlignment = HorizontalAlignment.Left;
                insidebutton.VerticalAlignment = VerticalAlignment.Top;
                insidebutton.Height = 80;

                Label itemname = new Label();
                Label itemname2 = new Label();
                TextBlock itemnameblock = new TextBlock();
                TextBlock itemnameblock2 = new TextBlock();
                itemname.Content = itemnameblock;
                itemname2.Content = itemnameblock2;
                itemnameblock.Text = mon[0]; //text of name
                itemnameblock2.Text = mon[1];
                itemnameblock.Tag = mon[0];
                itemnameblock.TextWrapping = TextWrapping.Wrap;
                /*Maybe useless   itemname.Foreground = Brushes.White;
                itemname.HorizontalAlignment = HorizontalAlignment.Center;
                itemname.VerticalContentAlignment = VerticalAlignment.Center;
                itemname.HorizontalContentAlignment = HorizontalAlignment.Center;*/

                Ellipse typecolor = new Ellipse();
                //typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 255, 210));
                typecolor.Height = 5;
                typecolor.Width = 5;

                namebutton.Content = insidebutton;
                namebutton.BorderThickness = new Thickness(0.5);
                namebutton.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                namebutton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 76, 50, 168));
                namebutton.Margin = new Thickness(5);
                namebutton.Width = 280;
                namebutton.Height = 40;
                namebutton.Tag = mon[0];
                itemname.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                itemname2.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                insidebutton.Children.Add(typecolor);
                insidebutton.Children.Add(itemname);
                insidebutton.Children.Add(itemname2);
                MyPanel.Children.Add(namebutton);
            }
        }

    }
}
