using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    String input = richTextBox1.Text;
        // //   Regex regex = new Regex(@"av+\d{8}");
        //    String regex=@"av+\d{8}";
        //    String str = "";
        //    foreach (Match match in Regex.Matches(input, regex))
        //       // Console.WriteLine("https://www.bilibili.com/video/av" + match.Value);
        //        str += "https://www.bilibili.com/video/av" + match.Value + "\n";
        //    richTextBox2.Text = str;
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox2.Text);
        }
        private string GetWebClient(string url)
        {
            string strHTML = "";
            try
            {
                WebClient myWebClient = new WebClient();
                Stream myStream = myWebClient.OpenRead(url);
                StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
                strHTML = sr.ReadToEnd();
                myStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return strHTML;
        }
        private string GetWebRequest(string url)
        {
            String strHTML = "";
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                // WebRequest myReq = WebRequest.Create(uri);\
                myReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)";
                myReq.Method = "GET";
                WebResponse result = myReq.GetResponse();
                Stream receviceStream = result.GetResponseStream();
                StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
                strHTML = readerOfStream.ReadToEnd();
                readerOfStream.Close();
                receviceStream.Close();
                result.Close();
            }
            catch (Exception e)
            { 
                MessageBox.Show("输入错误");
            }
            
            return strHTML;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            String str = GetWebRequest(textBox1.Text);
           // Console.WriteLine(str);
            String regex = @"av+\d{8}";
            String str2 = "";
            foreach (Match match in Regex.Matches(str, regex))
                // Console.WriteLine("https://www.bilibili.com/video/av" + match.Value);
                str2 += "https://www.bilibili.com/video/" + match.Value + "\n";
            richTextBox2.Text = str2;
            //richTextBox1.Text = str;
            //button1_Click(null,null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.TabIndex = 0;
            button3.TabIndex = 1;
            richTextBox2.TabIndex = 2;
            button2.TabIndex = 3;
        }
    }
}
