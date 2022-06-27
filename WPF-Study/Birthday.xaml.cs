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

namespace WPF_Study
{
    /// <summary>
    /// Логика взаимодействия для Birthday.xaml
    /// </summary>
    public partial class Birthday : UserControl
    {
        public string FName { get; set; }
        public string BDate { get; set; }
        StackPanel stackPanel = new StackPanel();
        public event EventHandler AddPerson;
        public Birthday()
        {
            InitializeComponent();
            TextBlock heading = new TextBlock();
            heading.Text = " Friend's Birthdays: ";
            heading.HorizontalAlignment = HorizontalAlignment.Center;
            heading.FontSize = 20;
            heading.FontFamily = new FontFamily("Bahnschrift");
            Friends.Children.Add(heading);
            Friends.Children.Add(stackPanel);
        }
        public void fillFriends(FriendsBirthdays t)
        {
            stackPanel.Children.Clear();
            for (int i = 0; i < t.Count(); i++)
            {
                stackPanel.Children.Add(CreateStackPanel(t.ElementAt(i).FriendName, t.ElementAt(i).Date));
            }
        }

        public StackPanel CreateStackPanel(string n, string d)
        {
            TextBlock nametb = new TextBlock();
            nametb.FontSize = 20;
            nametb.MinWidth = 250;
            nametb.FontFamily = new FontFamily("Bahnschrift");
            nametb.Text = n;
            TextBlock datetb = new TextBlock();
            datetb.FontSize = 20;
            datetb.Width = 100;
            datetb.FontFamily = new FontFamily("Bahnschrift");
            datetb.Text = d;
            StackPanel f = new StackPanel();
            f.Orientation = Orientation.Horizontal;
            f.Children.Add(nametb);
            f.Children.Add(datetb);
            return f;
        }
        private void AddFriend(object sender, MouseButtonEventArgs e)
        {
            SPaddfriend.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Hidden;
        }

        private void btn_Save(object sender, RoutedEventArgs e)
        {
            FName = Nametbx.Text;
            BDate = Datetbx.Text;
            if(DateCheck())
                AddPerson?.Invoke(this, new EventArgs());
            ClearText();
            SPaddfriend.Visibility = Visibility.Hidden;
            btnAdd.Visibility = Visibility.Visible;
        }

        public bool DateCheck()
        {
            if (BDate != null && BDate != "")
            {
                if (BDate.Contains('.'))
                {
                    if (Int32.Parse(BDate.Substring(0, BDate.IndexOf('.'))) > 0 && Int32.Parse(BDate.Substring(0, BDate.IndexOf('.'))) <= 31 && Int32.Parse(BDate.Substring(BDate.IndexOf('.') + 1)) > 0 && Int32.Parse(BDate.Substring(BDate.IndexOf('.') + 1)) <= 12)
                        return true;
                }
            }
            return false;
        }

        public void ClearText()
        {
            Nametbx.Text = "";
            Datetbx.Text = "";
        }
    }
}
