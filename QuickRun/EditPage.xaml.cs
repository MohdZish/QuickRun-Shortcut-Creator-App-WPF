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
using Microsoft.Win32;

namespace QuickRun
{
    /// <summary>
    /// Interaction logic for AddNewPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        public EditPage(string itemname, string itemlink, string itemtype, string itemshortcut)
        {
            InitializeComponent();
            softwaretext.Visibility = Visibility.Hidden;
            softwarebox.Visibility = Visibility.Hidden;
            linktext.Visibility = Visibility.Hidden;
            linkbox.Visibility = Visibility.Hidden;
            test.Visibility = Visibility.Hidden;
            createbtn.Visibility = Visibility.Hidden;
            erasebtn.Visibility = Visibility.Hidden;
            shiftbtn.Visibility = Visibility.Hidden;
            ctrlbtn.Visibility = Visibility.Hidden;
            altbtn.Visibility = Visibility.Hidden;
            winbtn.Visibility = Visibility.Hidden;
            

            oldtitle = itemname;
            nameinput.Text = itemname;
            linkbox.Text = itemlink;
            test.Text = itemshortcut;
            
            int selecttype = 0;
            if (itemtype == "Website") { selecttype = 0; }
            if (itemtype == "Software") { selecttype = 1; }
            if (itemtype == "Folder") { selecttype = 2; }
            if (itemtype == "Other") { selecttype = 3; }
            Typebox.SelectedIndex = selecttype;
            Typebox.SelectedItem = itemtype;

            browsepath.Content = itemlink;
        }

        public string oldtitle = "";

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
            erasebtn.Visibility = Visibility.Visible;
            createbtn.Visibility = Visibility.Visible;
            shiftbtn.Visibility = Visibility.Visible;
            ctrlbtn.Visibility = Visibility.Visible;
            altbtn.Visibility = Visibility.Visible;
            winbtn.Visibility = Visibility.Visible;
        }

        private void Handle()
        {
            switch (Typebox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Website":
                case "Email":
                case "Other":
                    softwaretext.Visibility = Visibility.Hidden;
                    linktext.Visibility = Visibility.Visible;
                    linkbox.Visibility = Visibility.Visible;
                    browsebtn.Visibility = Visibility.Hidden;
                    browsebtn2.Visibility = Visibility.Hidden;
                    browsebtn.Visibility = Visibility.Hidden;
                    browsebtn2.Visibility = Visibility.Hidden;
                    browsesoftware.Visibility = Visibility.Hidden;
                    browsepath.Visibility = Visibility.Hidden;
                    ListenerVisible();
                    break;
                case "Software":
                    softwaretext.Visibility = Visibility.Hidden;
                    linktext.Visibility = Visibility.Hidden;
                    linkbox.Visibility = Visibility.Hidden;
                    browsebtn.Visibility = Visibility.Hidden;
                    browsebtn2.Visibility = Visibility.Hidden;
                    browsesoftware.Visibility = Visibility.Visible;
                    browsepath.Visibility = Visibility.Visible;
                    ListenerVisible();
                    break;
                case "Folder":
                case "File/Folder":
                    softwaretext.Visibility = Visibility.Hidden;
                    linktext.Visibility = Visibility.Hidden;
                    linkbox.Visibility = Visibility.Hidden;
                    browsebtn.Visibility = Visibility.Visible;
                    browsebtn2.Visibility = Visibility.Visible;
                    browsepath.Visibility = Visibility.Visible;
                    browsesoftware.Visibility = Visibility.Hidden;
                    browsepath.Visibility = Visibility.Hidden;
                    ListenerVisible();
                    break;
            }
        }

        private void Browseexe(object sender, RoutedEventArgs e)
        {
            OpenFileDialog browse = new OpenFileDialog();
            browse.Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*";
            browse.ShowDialog();
            string sFileName = browse.FileName;
            browselink = sFileName;
            browsepath.Content = sFileName;
            browsepath.Visibility = Visibility.Visible;
        }

        private void Backbtn(object sender, RoutedEventArgs e)
        {
            //AddNewPage.Visi = Visibility.Hidden;
            this.Visibility = Visibility.Hidden;
        }

        private void Reckey(object sender, RoutedEventArgs e)
        {
            Button tempbtn = sender as Button;
            test.Text += tempbtn.Content + "+";

            test.Focusable = true;
            Keyboard.Focus(test);

            test.CaretIndex = test.Text.Length;
        }

        private void Erase(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            test.Clear();

        }

        private void test_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (test.Text.Contains("Shift")) { shiftbtn.IsEnabled = false; }
            if (test.Text.Contains("Ctrl")) { ctrlbtn.IsEnabled = false; }
            if (test.Text.Contains("Alt")) { altbtn.IsEnabled = false; }
            if (test.Text.Contains("Win")) { winbtn.IsEnabled = false; }

            if (test.Text.Contains("Shift") == false) { shiftbtn.IsEnabled = true; }
            if (test.Text.Contains("Ctrl") == false) { ctrlbtn.IsEnabled = true; }
            if (test.Text.Contains("Alt") == false) { altbtn.IsEnabled = true; }
            if (test.Text.Contains("Win") == false) { winbtn.IsEnabled = true; }
        }





        public bool keydebugging = false;

        public Window Owner { get; internal set; }
        public MainWindow AppMainWindow { get; internal set; }

        private string LocateEXE(String filename)
        {
            String path = Environment.GetEnvironmentVariable("path");
            String[] folders = path.Split(';');
            foreach (String folder in folders)
            {
                if (File.Exists(folder + filename))
                {
                    return folder + filename;
                }
                else if (File.Exists(folder + "\\" + filename))
                {
                    return folder + "\\" + filename;
                }
            }

            return String.Empty;
        }



        private void ListenToKeys(object sender, RoutedEventArgs e)
        {
            string pathToExe = LocateEXE("Adobe Premiere Pro.exe");
            Console.WriteLine(pathToExe);
            //Activator.CreateInstance(Type.GetTypeFromProgID("Notepad.Application"));
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
            Console.WriteLine(displaykey);

            string[] defaultkeys = { "LeftShift", "RightShift" }; //default ones!
            string[] correctkeys = { "LShift", "RShift" }; //to display!

            int b = 0;
            foreach (string a in defaultkeys)
            {
                if ((displaykey == a) && (test.Text.Contains(correctkeys[b]) == false))
                { Console.WriteLine(defaultkeys[b]); }
                b++;
            }
            test.CaretIndex = test.Text.Length;
        }

        public string browselink = "";
        private void Browsefile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog browse = new OpenFileDialog();
            browse.ShowDialog();
            string sFileName = browse.FileName;
            browselink = sFileName;
            browsepath.Content = sFileName;
            browsepath.Visibility = Visibility.Visible;
        }

        private void Browsefolder(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                browselink = dialog.SelectedPath;
                browsepath.Content = browselink;
                browsepath.Visibility = Visibility.Visible;
            }
        }

        private void EditShortcut(object sender, RoutedEventArgs e)
        {

            string[] readText = File.ReadAllLines(@"C:\test\test.txt");
            for(int i = 0; i <readText.Length; i++)
            {
                string[] mon = readText[i].Split('=');   //We're making array for mon[0] is name and mon[1] is shortcut

                string Name = nameinput.Text;
                string Type = Typebox.Text;

                string Value = "";
                if (Typebox.Text == "File/Folder")
                {
                    Type = "Folder"; //Just to keep File/Folder as Folder simply
                }

                if (Type == "Folder")
                {
                    Value = browselink;
                }
                if (Type == "Software")
                {
                    Value = browselink;
                }
                if (Type == "Website" || Type == "Other")
                {
                    Value = linkbox.Text;
                }

                string Shortcut = test.Text;

                if (mon[0] == oldtitle)
                {
                    readText[i] = Name + "=" + Type + "=" + Value + "=" + Shortcut;
                }
                
            }
            File.WriteAllLines(@"C:\test\test.txt", readText);

            //CALLING FUNCTION FROM ANOTHER WINDOW !!!!!!!! Change function you call to PUBLIC (not PRIVATE)
            //You can also access strings or items if public! Dont use STATIC!
            Window mainWindow = this.Owner;
            var myObject = this.Owner as MainWindow;
            myObject.MenuPopUp(oldtitle + " modified!");
            myObject.IsEditOn = false;
            myObject.buttonedit.Background = new SolidColorBrush(Color.FromArgb(255, 250, 249, 247));
            myObject.LoadingScreen();

            this.Visibility = Visibility.Hidden;


        }

        

    }
}
