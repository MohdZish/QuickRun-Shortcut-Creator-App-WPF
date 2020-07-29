using System;
using System.IO;
using Microsoft.Win32;
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
            LoadingScreen();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
            Keyboard.ClearFocus();
        }

        private void Buttonclose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Buttonminimise(object sender, RoutedEventArgs e)
        {
            MainPage.WindowState = WindowState.Minimized;
        }

        private void LoadingScreen()
        {
            string[] readText = File.ReadAllLines(@"C:\test\test.txt");
            foreach (string text in readText)
            {
                string myString = text;
                string[] mon = myString.Split('-');   //We're making array for mon[0] is name and mon[1] is shortcut

                Button namebutton = new Button();
                StackPanel insidebutton = new StackPanel();
                insidebutton.Orientation = Orientation.Horizontal;
                insidebutton.HorizontalAlignment = HorizontalAlignment.Left;
                insidebutton.VerticalAlignment = VerticalAlignment.Top;

                Label itemname = new Label();
                Label itemname2 = new Label();
                TextBlock itemnameblock = new TextBlock();
                TextBlock itemnameblock2 = new TextBlock();
                itemnameblock.Width = 100;
                itemnameblock2.Width = 140;
                itemname.Content = itemnameblock;
                itemname2.Content = itemnameblock2;
                itemnameblock2.TextAlignment = TextAlignment.Right;
                itemnameblock.Text = mon[0]; //text of name
                itemnameblock2.Text = mon[3];
                itemnameblock.Tag = mon[0];
                itemname.HorizontalAlignment = HorizontalAlignment.Left;
                itemname.HorizontalContentAlignment = HorizontalAlignment.Left;
                itemname2.HorizontalAlignment = HorizontalAlignment.Right;
                itemname2.HorizontalContentAlignment = HorizontalAlignment.Right;

                Ellipse typecolor = new Ellipse();
                //typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 255, 210));
                typecolor.Height = 5;
                typecolor.Width = 5;

                namebutton.Content = insidebutton;
                namebutton.BorderThickness = new Thickness(0.5);
                namebutton.Background = new SolidColorBrush(Color.FromArgb(125, 250, 249, 247));
                namebutton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 186, 186, 186));
                namebutton.Margin = new Thickness(5);
                namebutton.Width = 280;
                namebutton.Height = 40;
                namebutton.Tag = mon[0];
                namebutton.Style = (Style)FindResource("RoundedButtonStyle");
                itemname.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                itemname2.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                insidebutton.Children.Add(typecolor);
                insidebutton.Children.Add(itemname);
                insidebutton.Children.Add(itemname2);
                MyPanel.Children.Add(namebutton);
            }
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            AddNewPage newpagedashboard = new AddNewPage();
            AddNewPanel.Content = newpagedashboard;
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            AddNewPage newpagedashboard = new AddNewPage();
            AddNewPanel.Content = newpagedashboard;
        }

        public void GetInstalledApps()
        {
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            Console.WriteLine(sk.GetValue("DisplayName"));
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }
        }

        private void OpenMini(object sender, RoutedEventArgs e)
        {
            MiniAppWindow opennewmini = new MiniAppWindow();
            opennewmini.ShowDialog();

        }

    }
}
