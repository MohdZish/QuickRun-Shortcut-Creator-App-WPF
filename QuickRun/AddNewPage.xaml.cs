using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
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
    /// Interaction logic for AddNewPage.xaml
    /// </summary>
    public partial class AddNewPage : Page
    {
        public AddNewPage()
        {
            InitializeComponent();
            softwaretext.Visibility = Visibility.Hidden;
            softwarebox.Visibility = Visibility.Hidden;
            linktext.Visibility = Visibility.Hidden;
            linkbox.Visibility = Visibility.Hidden;
            test.Visibility = Visibility.Hidden;
            listenbtn.Visibility = Visibility.Hidden;
            createbtn.Visibility = Visibility.Hidden;
            erasebtn.Visibility = Visibility.Hidden;
        }

        private bool handle = true;

        private void ComboBox_DropDownClosed(object sender, EventArgs e) //for combobox
        {
            if (handle) Handle();
            handle = true;
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e) //for combobox
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        private void ListenerVisible()
        {
            test.Visibility = Visibility.Visible;
            listenbtn.Visibility = Visibility.Visible;
            erasebtn.Visibility = Visibility.Visible;
            createbtn.Visibility = Visibility.Visible;
        }

        private void Handle()
        {
            switch (Typebox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Website":
                case "Email":
                    softwaretext.Visibility = Visibility.Hidden;
                    softwarebox.Visibility = Visibility.Hidden;
                    linktext.Visibility = Visibility.Visible;
                    linkbox.Visibility = Visibility.Visible;
                    ListenerVisible();
                    break;
                case "Software":
                    softwaretext.Visibility = Visibility.Visible;
                    softwarebox.Visibility = Visibility.Visible;
                    linktext.Visibility = Visibility.Hidden;
                    linkbox.Visibility = Visibility.Hidden;
                    ListenerVisible();
                    break;
                case "Folder":
                    softwaretext.Visibility = Visibility.Hidden;
                    softwarebox.Visibility = Visibility.Hidden;
                    linktext.Visibility = Visibility.Hidden;
                    linkbox.Visibility = Visibility.Hidden;
                    ListenerVisible();
                    break;
            }


        }

        private void Backbtn(object sender, RoutedEventArgs e)
        {
            //AddNewPage.Visi = Visibility.Hidden;
            this.Visibility = Visibility.Hidden;
            
        }

        private void Erase(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            test.Clear();
        }

        public bool keydebugging = false;
        private void ListenToKeys(object sender, RoutedEventArgs e)
        {
            //AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)ListenToKeys
            if (test.Text.Contains("Click Listen to enter shortcut!"))
            {
                test.Text = "";
            }
            
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

            string[] defaultkeys = {"LeftShift","RightShift"}; //default ones!
            string[] correctkeys = { "LShift", "RShift" }; //to display!

            int b = 0;
            foreach(string a in defaultkeys)
            {
                if ((displaykey == a) && (test.Text.Contains(correctkeys[b]) == false))
                { test.Text += correctkeys[b] + "+"; }
                b++;
            }
            test.CaretIndex = test.Text.Length;
        }

        private void CreateShortcut(object sender, RoutedEventArgs e)
        {
            string Name = nameinput.Text;
            string Type = Typebox.Text;
            string Value = ""; //the link/software/folder for example
            if (softwarebox.Text != "") { Value = softwarebox.Text; }
            if (linkbox.Text != "") { Value = linkbox.Text; }
            string Shortcut = test.Text;
            string Finaltext = Name +"-" + Type + "-" + Value + "-" + Shortcut + Environment.NewLine;
            File.AppendAllText(@"C:\test\test.txt", Finaltext);
        }



    }
}
