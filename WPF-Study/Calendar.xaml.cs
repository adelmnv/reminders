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
    /// Логика взаимодействия для Calendar.xaml
    /// </summary>
    public partial class Calendar : UserControl
    {

        public List<string> dates = new List<string>();
        public string CurrentDate { get; set; }
        public Calendar()
        {
            InitializeComponent();
            calendar.SelectedDate = DateTime.Now;
            //ColorDates();
        }

        public void AddDates(string s)
        {
            if(!dates.Contains(s))
                dates.Add(s);
        }
        public event EventHandler FillDay;
        public void ColorDates()
        {
            foreach(string s in dates)
            {
                DateTime dt;
                dt = DateTime.Parse(s);
                if(!calendar.SelectedDates.Contains(dt))
                   calendar.SelectedDates.Add(dt);
            }
        }

        private void ShowDay(object sender, SelectionChangedEventArgs e)
        {
            CurrentDate = calendar.SelectedDate.Value.ToShortDateString();
            FillDay?.Invoke(this, e);
            ColorDates();
        }
    }
}
