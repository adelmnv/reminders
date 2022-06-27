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
    /// Логика взаимодействия для Work.xaml
    /// </summary>
    public partial class Work : UserControl
    {
        StackPanel stackPanel = new StackPanel();
        public event EventHandler Done;
        public Work()
        {
            InitializeComponent();
            TextBlock heading = new TextBlock();
            heading.Text = " To Do List: ";
            heading.HorizontalAlignment = HorizontalAlignment.Center;
            heading.FontSize = 20;
            heading.FontFamily = new FontFamily("Bahnschrift");
            WorkSP.Children.Add(heading);
            WorkSP.Children.Add(stackPanel);
        }
        public void fillInf(Notices t)
        {
            int count = 0;
            stackPanel.Children.Clear();
            for (int i = 0; i < t.Count(); i++)
            {
                if (stackPanel.Children.OfType<TextBlock>().Any(x => x.Text == t.ElementAt(i).Date))
                {
                    TextBlock temp = new TextBlock(); temp = stackPanel.Children.OfType<TextBlock>().First(x => x.Text == t.ElementAt(i).Date);
                    stackPanel.Children.Insert(i + count, CreateStackPanel(t.ElementAt(i)));

                }
                else
                {
                    TextBlock date = new TextBlock();
                    date.FontSize = 20;date.MinWidth = 250; date.HorizontalAlignment = HorizontalAlignment.Left;
                    date.FontFamily = new FontFamily("Bahnschrift");
                    date.Text = t.ElementAt(i).Date;
                    stackPanel.Children.Add(date);
                    stackPanel.Children.Add(CreateStackPanel(t.ElementAt(i)));
                    count++;
                }
            }
        }

        public StackPanel CreateStackPanel(Notice n)
        {
            StackPanel f = new StackPanel();
            if (n.IsDone == true)
            {
                f.Children.Add(NotDeal(n));
            }
            else
            {
                StackPanel c = new StackPanel();
                CheckBox done = new CheckBox();
                done.Margin = new Thickness(0, 0, 20, 0);
                done.Checked += (sender, e) => Deal(sender, e);
                TextBlock desc = new TextBlock();
                TextBlock time = new TextBlock();
                TextBlock type = new TextBlock();
                Label date = new Label() { Content = n.Date, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE8EDF2")), FontSize = 6 };
                StyleT(desc);
                StyleT(time);
                StyleT(type);
                desc.Text = n.Description; time.Text = n.Time; type.Text = n.Theme;
                c.Orientation = Orientation.Horizontal;
                c.Children.Add(desc); c.Children.Add(time); c.Children.Add(type); c.Children.Add(date);
                done.Content = c;
                f.Orientation = Orientation.Horizontal;
                f.Children.Add(done);
            }
            return f;
        }

        public CheckBox NotDeal(Notice n)
        {
            StackPanel c = new StackPanel();
            CheckBox done = new CheckBox();
            done.Margin = new Thickness(0, 0, 20, 0);
            done.IsChecked = true; done.IsEnabled = false;
            TextBlock desc = new TextBlock();
            TextBlock time = new TextBlock();
            TextBlock type = new TextBlock();
            DoneStyle(desc);
            DoneStyle(time);
            DoneStyle(type);
            desc.Text = n.Description; time.Text = n.Time; type.Text = n.Theme;
            c.Orientation = Orientation.Horizontal;
            c.Children.Add(desc); c.Children.Add(time); c.Children.Add(type);
            done.Content = c;
            return done;
        }

        public void DoneStyle(TextBlock desc)
        {
            StyleT(desc);
            desc.TextDecorations = TextDecorations.Strikethrough;
            desc.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF52585D"));
        }
        public void StyleT(TextBlock t)
        {
            t.FontSize = 18;
            t.MinWidth = 50;
            t.FontFamily = new FontFamily("Bahnschrift Light");
        }

        public void Deal(object sender, RoutedEventArgs e)
        {
            CheckBox t = (CheckBox)sender;
            StackPanel x = (StackPanel)t.Content;
            foreach(object child in x.Children)
            {
                if(child is TextBlock)
                {
                    TextBlock temp = (TextBlock)child;
                    temp.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF52585D"));
                    temp.TextDecorations = TextDecorations.Strikethrough;
                }
            }
            t.IsEnabled = false;
            Done?.Invoke(sender, e);
        }
    }
}
