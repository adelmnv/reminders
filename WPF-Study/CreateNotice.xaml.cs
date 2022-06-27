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
    /// Логика взаимодействия для CreateNotice.xaml
    /// </summary>
    public partial class CreateNotice : UserControl
    {
        public DateTime DateTime { get; set; }
        public string Date { get => datetbx.Text; set => datetbx.Text = value; }
        public string Time { get => timetbx.Text; set => timetbx.Text = value; }
        public string Description { get => desctbx.Text; set => desctbx.Text = value; }
        public string Theme { get; set; }
        public event EventHandler SaveValue;
        public CreateNotice()
        {
            InitializeComponent();
            calendar.SelectedDate = DateTime.Now;
        }
        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                TextBlock selectedItem = (TextBlock)comboBox.SelectedItem;
                Theme = selectedItem.Text.ToString();
            }
        }
        private void btn_Save(object sender, RoutedEventArgs e)
        {

            if (Date != "" && Description != "" && Theme != "" && CheckTime())
            {
                ClearCheck();
                SaveValue?.Invoke(this, e);
                calendar.SelectedDate = DateTime.Now;
                ClearText();
            }
            else
            {
                ClearCheck();
                istime.Text = " X";
                if (Date == "")
                    isdate.Text = " X";
                if (Description == "")
                    isdesc.Text = " X";
                if (Time == "")
                    istime.Text = " X";
                if (Theme == "" || Theme == null)
                    istype.Text = " X";
            }
        }

        public bool CheckTime()
        {
            if (Time != "")
            {
                if (Time.Contains(':'))
                {
                    int h = Int32.Parse(Time.Substring(0, Time.IndexOf(':')));
                    int m = Int32.Parse(Time.Substring(Time.IndexOf(':') + 1));
                    if (h >= 0 && h <= 23 && m >= 0 && m <= 59)
                        return true;
                    else
                        return false;
                        
                }
                return false; 
            }
            return false;
        }
        public void ClearText()
        {
            typecb.SelectedItem = null;
            desctbx.Text = "";
            timetbx.Text = "";
        }
        public void ClearCheck()
        {
            isdate.Text = "";
            istime.Text = "";
            isdesc.Text = "";
            istype.Text = "";
        }

        private void SelectedDate(object sender, SelectionChangedEventArgs e)
        {
            datetbx.Text = calendar.SelectedDate.Value.ToShortDateString();
        }
    }
}
