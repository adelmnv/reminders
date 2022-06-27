using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WPF_Study
{
    class Model : Abstractions.IModel
    {
        public Notices notices = new Notices();
        //    public int firstValue;
        //    public int secondValue;
        //    public int result;
        //    public void ShowIn(string message)
        //    {
        //        Debug.WriteLine(message);
        //    }
        public void SaveIn(string s1, string s2, string s3, string s4)
        {
            notices.AddElement(new Notice(s1, s2, s3, s4));
        }
    }
        //    public string Operation()
        //    {
        //        result = firstValue + secondValue;
        //        return result.ToString();
        //    }

    
}
