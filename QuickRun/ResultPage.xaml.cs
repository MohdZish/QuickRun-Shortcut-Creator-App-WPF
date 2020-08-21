using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        public string name = "";
        public string link = "";
        public string type = "";
        public string shortcut = "";

        public Window Owner { get; internal set; }
        public MainWindow AppMainWindow { get; internal set; }

        public ResultPage(string itemname, string itemlink, string itemtype, string itemshortcut)
        {
            InitializeComponent();
            titlename.Text = itemname;
            linktext.Text = itemlink;
            if(itemtype == "Website")
            {
                linktext.PreviewMouseDown += new MouseButtonEventHandler(gotolink);
            }
            shortcuttext.Text = itemshortcut;
            //shortcutbox.Width = itemshortcut.Length;
            name = itemname;
            link = itemlink;
            type = itemtype;
            shortcut = itemshortcut;
        }

        private void gotolink(object sender, MouseButtonEventArgs e)
        {
            Process.Start(linktext.Text);
        }

        public void Edit(object sender, RoutedEventArgs e)
        {
            EditPage newpagedashboard = new EditPage(name, link, type, shortcut);
            var myObject = this.Owner as MainWindow;
            newpagedashboard.Owner = this.Owner;
            myObject.EditPanel.Content = newpagedashboard;
            this.Visibility = Visibility.Hidden;
        }

        public void Delete(object sender, RoutedEventArgs e)
        {
            var myObject = this.Owner as MainWindow;
            myObject.itemname = name;
            buttondelete.Click += myObject.DeleteItem;
            this.Visibility = Visibility.Hidden;

        }

        private void Backbtn(object sender, RoutedEventArgs e)
        {
            //AddNewPage.Visi = Visibility.Hidden;
            this.Visibility = Visibility.Hidden;

        }

        private void Copylink(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(linktext.Text);

        }
    }
}
