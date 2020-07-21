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

        private void Test(object sender, RoutedEventArgs e)
        {
            string[] readText = File.ReadAllLines(@"C:\test\test.txt");
            foreach (string mon in readText)
            {
                Button namebutton = new Button();
                StackPanel insidebutton = new StackPanel();
                insidebutton.HorizontalAlignment = HorizontalAlignment.Left;
                insidebutton.VerticalAlignment = VerticalAlignment.Top;
                insidebutton.Height = 80;

                Label itemname = new Label();
                TextBlock itemnameblock = new TextBlock();
                itemname.Content = itemnameblock;
                itemnameblock.Text = mon;
                itemnameblock.Tag = mon;
                itemnameblock.TextWrapping = TextWrapping.Wrap;
                itemname.Foreground = Brushes.White;
                itemname.HorizontalAlignment = HorizontalAlignment.Center;
                itemname.VerticalContentAlignment = VerticalAlignment.Center;
                itemname.HorizontalContentAlignment = HorizontalAlignment.Center;
                itemname.Height = 70;
                itemname.Width = 80;
                Ellipse typecolor = new Ellipse();
                typecolor.Height = 5;
                typecolor.Width = 5;

                namebutton.Content = insidebutton;
                namebutton.BorderThickness = new Thickness(1);
                namebutton.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                namebutton.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 255));
                namebutton.Margin = new Thickness(5);
                namebutton.Width = 80;
                namebutton.Height = 80;
                namebutton.Tag = mon;
                namebutton.Width = 80;
                namebutton.Height = 80;
                insidebutton.Children.Add(typecolor);
                insidebutton.Children.Add(itemname);
                MyPanel.Children.Add(namebutton);

            }
        }

    }
}
