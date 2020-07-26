using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuickRun
{
    /// <summary>
    /// Interaction logic for MiniAppWindow.xaml
    /// </summary>
    public partial class MiniAppWindow : Window
    {
        public MiniAppWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = System.Windows.SystemParameters.WorkArea.Width - this.Width;
            this.Top = System.Windows.SystemParameters.WorkArea.Height - this.Height;
            test.Focusable = true;
            Keyboard.Focus(test);
            test.CaretIndex = test.Text.Length;
            ListenToKeys();
            //textreading();
        }
        private void Buttonclose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Buttonminimise(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
            Keyboard.ClearFocus();
        }

        public bool keydebugging = false;
        private void ListenToKeys()
        {
            test.Focusable = true;
            Keyboard.Focus(test);
            test.CaretIndex = test.Text.Length;

            //AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)ListenToKeys
            test.Focusable = true;
            Keyboard.Focus(test);
            test.CaretIndex = test.Text.Length;

            var window = Window.GetWindow(this);
            if (keydebugging == false)
            {
                window.KeyDown += new KeyEventHandler(Keys);
            }
            keydebugging = true;
            return;
        }

        private void Keys(object sender, KeyEventArgs e)
        {
            KeyConverter kc = new KeyConverter();
            var displaykey = kc.ConvertToString(e.Key);

            string[] defaultkeys = { "LeftShift", "RightShift", "Alt"}; //default ones!
            string[] correctkeys = { "Shift", "Shift" }; //to display!

            int b = 0;
            foreach (string a in defaultkeys)
            {
                if ((displaykey == a) && (test.Text.Contains(correctkeys[b]) == false))
                { test.Text += correctkeys[b] + "+"; }
                b++;
            }
            test.CaretIndex = test.Text.Length;
        }

        string[] mon;

        private void test_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            string[] readText = File.ReadAllLines(@"C:\test\test.txt");
            foreach (string text in readText)
            {
                string myString = text;
                mon = myString.Split('-');   //We're making array for mon[0] is name and mon[1] is shortcut
                Console.WriteLine(mon[0]);


                bool result = test.Text.Equals(mon[3], StringComparison.OrdinalIgnoreCase);
                if (result) { 
                    
                    if(mon[1] == "Website" || mon[1] == "Email")
                    {
                        gotolink(mon[2]);
                    }

                    if (mon[1] == "Folder")
                    {
                        gotofolder(mon[2]);
                    }

                    if (mon[1] == "Software")
                    {
                        gotofolder(mon[2]);
                    }
                }
            }
        }

        private void gotolink(string link)
        {
            Process.Start(link);
            test.Clear();
        }

        private void gotofolder(string folderurl)
        {
            Process.Start(folderurl);
            test.Clear();
        }

        private void gotosoftware(string softwareurl)
        {
            Process.Start(softwareurl);
            test.Clear();
        }



    }
}
