using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, Abstractions.IView
    {
        Notices notices = new Notices();
        FriendsBirthdays birthdays = new FriendsBirthdays();
        CreateNotice cn = new CreateNotice();
        Calendar maincal = new Calendar();
        Birthday birthday = new Birthday();
        Work work = new Work();
        public MainWindow()
        {
            InitializeComponent();
            birthdays.Sort();
            birthday.fillFriends(birthdays);
            SetMainCalendar();
        }
        public void Save(object sender)
        {
            CreateNotice p = (CreateNotice)sender;
            notices.AddElement(new Notice(p.Date, p.Time, p.Description, p.Theme));
        }
        private void btn_Calendar(object s, RoutedEventArgs e)
        {
            SetMainCalendar();
        }
        public void SetMainCalendar()
        {
            CalendarType.Children.Clear();
            maincal = new Calendar();
            maincal.FillDay += (sender, EventArgs) => SetInformation(sender);
            SetDates();
            maincal.ColorDates();
            notices.Sort();
            CalendarType.Children.Add(maincal);
        }
        private void btn_Create(object s, RoutedEventArgs e)
        {
            CalendarType.Children.Clear();
            cn = new CreateNotice();
            cn.SaveValue += (sender, EventArgs) => Save(sender);
            CalendarType.Children.Add(cn);
        }
        private void btn_Work(object s, RoutedEventArgs e)
        {
            CalendarType.Children.Clear();
            notices.Sort();
            work.fillInf(notices);
            work.Done += (sender, EventArgs) => DoneDeals(sender);
            CalendarType.Children.Add(work);
        }

        private void btn_Birthday(object s, RoutedEventArgs e)
        {
            CalendarType.Children.Clear();
            birthday.AddPerson += (sender, EventArgs) => AddBirthday(sender);
            birthday.fillFriends(birthdays);
            CalendarType.Children.Add(birthday);
        }

        public void AddBirthday(object s)
        {
            Birthday p = (Birthday)s;
            if(!birthdays.Check(p.BDate, p.FName))
            {
                birthdays.AddElement(new FriendBirthday(p.BDate, p.FName, ""));
            }
            birthdays.Sort();
            birthday.fillFriends(birthdays);
        }
        public void SetDates()
        {
            if (notices.Count() > 0) 
            {
                notices.Sort();
                for (int i = 0; i < notices.Count(); i++)
                {
                    maincal.AddDates(notices.ElementAt(i).Date);
                }
            }
            if (birthdays.Count() > 0)
            {
                for (int i = 0; i < birthdays.Count(); i++)
                {
                    maincal.AddDates(birthdays.ElementAt(i).Date);
                }
            }
        }
        
        public void SetInformation(object sender)
        {
            Calendar dt = (Calendar)sender;
            string t = dt.CurrentDate;
            SetBirthdays(t);
            SetTasks(t);
        }

        public void SetBirthdays(string d)
        {
            maincal.Birthdays.Children.Clear();
            if (birthdays.Count() > 0 && birthdays.IsExist(d))
            {
                maincal.BInf.Visibility = Visibility.Visible;
                maincal.Birthdays.Children.Add(new TextBlock() { Text = " Birthdays: ", FontSize = 16 });
                birthdays.Sort();
                DateTime z = DateTime.Now;

                for (int i = 0; i < birthdays.Count(); i++)
                {
                    string bd = birthdays.ElementAt(i).Date + '.' + z.Year.ToString();
                    if (bd == d)
                    {
                        TextBlock t = new TextBlock();
                        t.FontSize = 16;
                        t.Text = birthdays.ElementAt(i).FriendName + '\t' + d;
                        maincal.Birthdays.Children.Add(t);
                    }
                }
            }
            else
                maincal.BInf.Visibility = Visibility.Hidden;
        }

        public void SetTasks(string d)
        {
            maincal.Work.Children.Clear();
            if (notices.Count() > 0 && notices.IsExist(d))
            {
                maincal.LInf.Visibility = Visibility.Visible;
                maincal.Work.Children.Add(new TextBlock() { Text = " To do list: ", FontSize = 16 });

                for (int i = 0; i < notices.Count(); i++)
                {
                    if (notices.ElementAt(i).IsDone == false && notices.ElementAt(i).Date == d)
                    {
                        TextBlock t = new TextBlock();
                        t.FontSize = 16;
                        t.Text = notices.ElementAt(i).Time + '\t' + notices.ElementAt(i).Description;
                        maincal.Work.Children.Add(t);
                    }
                }
            }
            else
                maincal.LInf.Visibility = Visibility.Hidden;

        }

        public void DoneDeals(object sender)
        {
            CheckBox cb = (CheckBox)sender;
            StackPanel x = (StackPanel)cb.Content;
            Notice notice = new Notice();
            int count = 0;
            foreach (object child in x.Children)
            {
                if (child is TextBlock)
                {
                    TextBlock temp = (TextBlock)child;
                    if(count == 0)
                    notice.Description = temp.Text;
                    if(count == 1)
                    notice.Time = temp.Text;
                    if(count == 2)
                    notice.Theme = temp.Text;
                    count++;
                }
                else if (child is Label)
                {
                    Label label = (Label)child;
                    notice.Date = label.Content.ToString();
                }
            }
            notices.Finish(notice);
        }
    }
    public class Notice
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public bool IsDone { get; set; }

        public Notice(string s1, string s2, string s3, string s4)
        {
            Date = s1; Time = s2; Description = s3; Theme = s4; IsDone = false;
        }
        public Notice() { }
    }

    public class FriendBirthday
    {
        public string FriendName { get; set; }
        public string Date { get; set; }
        public string Photo { get; set; }

        public FriendBirthday(string s1, string s2, string s3)
        {
            Date = s1; FriendName = s2;Photo = s3;
        }
    }

    public class Notices
    {
        ObservableCollection<Notice> notices = null;
        public Notices()
        {
            notices = new ObservableCollection<Notice>();
        }
        public void AddElement(Notice n)
        {
            notices.Add(n);
        }

        public void Sort()
        {
            notices = new ObservableCollection<Notice>(notices.OrderBy(i => i.Date).ThenBy(t => TimeSpan.Parse(t.Time)));
        }
        public bool IsExist(string d)
        {
            return notices.Any(i => i.Date == d && i.IsDone == false);
        }
        public int Count()
        {
            return notices.Count;
        }
        public Notice ElementAt(int n)
        {
            return notices[n];
        }

        public void Finish(Notice t)
        {
            //int index = -1;
            foreach(Notice n in notices)
            {
                if(n.Date == t.Date && n.Theme == t.Theme && n.Time == t.Time && n.Description == t.Description)
                    n.IsDone = true;
            }
        }
    }

    public class FriendsBirthdays
    {
        ObservableCollection<FriendBirthday> birthdays = null;
        public FriendsBirthdays()
        {
            birthdays = new ObservableCollection<FriendBirthday>();
            birthdays.Add(new FriendBirthday("07.06","Wilson", ""));
            birthdays.Add(new FriendBirthday("23.06", "Christopher", ""));
            birthdays.Add(new FriendBirthday("28.06","Edward", ""));
            birthdays.Add(new FriendBirthday("02.06", "Emy", ""));
            birthdays.Add(new FriendBirthday("03.06", "Chris", ""));
            birthdays.Add(new FriendBirthday("30.06", "Edee", ""));
        }
        public void AddElement(FriendBirthday n)
        {
            birthdays.Add(n);
        }

        public void Sort()
        {
            birthdays = new ObservableCollection<FriendBirthday>(birthdays.OrderBy(i => i.FriendName));
        }
        public bool IsExist(string d)
        {
            d = d.Remove(5);
            return birthdays.Any(db => db.Date == d);
        }

        public bool Check(string d, string n)
        {
            return birthdays.Any(i => i.Date == d && i.FriendName == n);
        }
        public int Count()
        {
            return birthdays.Count;
        }
        public FriendBirthday ElementAt(int n)
        {
            return birthdays[n];
        }
    }

}
