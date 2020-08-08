﻿using System;
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

        public static string itemname { get; private set; }
        public static string itemtype { get; private set; }
        public static string itemlink { get; private set; }
        public static string itemshortcut { get; private set; }

        private void itemnamemethod(object sender, RoutedEventArgs e)
        {
            string itemnametemp = (string)((Button)sender).Tag;

            string[] readText = File.ReadAllLines(@"C:\test\test.txt");
            foreach (string text in readText)
            {
                string myString = text;
                string[] mon = myString.Split('-');   //We're making array for mon[0] is name and mon[1] is shortcut

                if(mon[0] == itemnametemp)
                {
                    itemtype = mon[1];
                    itemlink = mon[2];
                    itemshortcut = mon[3];
                }
            }


            itemname = itemnametemp;
        }

        private void Resultpagemethod(object sender, RoutedEventArgs e)
        {
            ResultPage dashboard = new ResultPage(itemname, itemlink, itemtype, itemshortcut);

            ShowResultPanel.Content = null;
            ShowResultPanel.Content = dashboard;
        }

        public static bool IsEditOn = false; 
        private void LoadingScreen()
        {
            string[] readText = File.ReadAllLines(@"C:\test\test.txt");

            MyPanel.Children.Clear();
            foreach (string text in readText)
            {
                string myString = text;
                string[] mon = myString.Split('-');   //We're making array for mon[0] is name and mon[1] is shortcut

                StackPanel insidebutton = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                };

                Label itemname = new Label() //Item title
                {
                    Width = 120,
                    Content = mon[0], //text of name
                    Tag = mon[0],
                    HorizontalAlignment = HorizontalAlignment.Left,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                };

                Label itemname2 = new Label() //Item shortcut
                {
                    Width = 140,
                    Content = mon[3],
                    HorizontalAlignment = HorizontalAlignment.Right,
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                 };

                System.Windows.Shapes.Path editlogo = new System.Windows.Shapes.Path()
                {
                    Width = 13.866,
                    Height = 11.555
                };

                Canvas itemeditblock = new Canvas() { Width = 50 };
                //itemeditblock.Children.Add(editlogobox);

                Ellipse typecolor = new Ellipse()
                {
                    Height = 5,
                    Width = 5,
                    Fill = new SolidColorBrush(Color.FromArgb(255, 12, 255, 210)),
                 };


                Button namebutton = new Button()
                {
                    Content = insidebutton,
                    BorderThickness = new Thickness(0.5),
                    Background = new SolidColorBrush(Color.FromArgb(125, 250, 249, 247)),
                    BorderBrush = new SolidColorBrush(Color.FromArgb(255, 186, 186, 186)),
                    Margin = new Thickness(5),
                    Width = 280,
                    Height = 40,
                    Tag = mon[0],
                    Style = (Style)FindResource("RoundedButtonStyle")
                    
                 };
                namebutton.Click += itemnamemethod;
                namebutton.Click += Resultpagemethod;

                
                insidebutton.Children.Add(typecolor);
                insidebutton.Children.Add(itemname);
                insidebutton.Children.Add(itemname2);
                MyPanel.Children.Add(namebutton);

                recenttitle.Text = mon[0];
                recentshortcut.Text = mon[3];

                if (IsEditOn)
                {
                    namebutton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 52, 149, 235));
                    namebutton.BorderThickness = new Thickness(1);
                }
            }
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            AddNewPage newpagedashboard = new AddNewPage();
            AddNewPanel.Content = newpagedashboard;
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                if (!IsEditOn) {
                    buttonedit.Background = new SolidColorBrush(Color.FromArgb(255, 145, 220, 255));
                    IsEditOn = true;
                    LoadingScreen();
                    break;
                }
                if(IsEditOn)
                {
                    buttonedit.Background = new SolidColorBrush(Color.FromArgb(255, 250, 249, 247));
                    IsEditOn = false;
                    LoadingScreen();
                    break;
                }
            }

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
