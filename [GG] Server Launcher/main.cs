﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace _GG__Server_Launcher
{
    public partial class main : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        private const int SET_FEATURE_ON_THREAD = 0x00000001;
        private const int SET_FEATURE_ON_PROCESS = 0x00000002;
        private const int SET_FEATURE_IN_REGISTRY = 0x00000004;
        private const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 0x00000008;
        private const int SET_FEATURE_ON_THREAD_INTRANET = 0x00000010;
        private const int SET_FEATURE_ON_THREAD_TRUSTED = 0x00000020;
        private const int SET_FEATURE_ON_THREAD_INTERNET = 0x00000040;
        private const int SET_FEATURE_ON_THREAD_RESTRICTED = 0x00000080;

        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            run_ie9();
            webBrowser1.Navigate("http://ghostzgamerz.com/forums/breaking-news.118/");

            try
            {
                StreamReader inStream;
                WebRequest webRequest = WebRequest.Create("http://192.99.38.184/launcher/search_server.php");
                WebResponse webresponse = webRequest.GetResponse();

                inStream = new StreamReader(webresponse.GetResponseStream());
                textBox1.Text = inStream.ReadToEnd();

            }
            catch
            {
                //MessageBox.Show("No internet connection detected");
            }

            StringBuilder lineInfo = new StringBuilder();
            lineInfo.Append(textBox1.Lines.Length.ToString() + "\n");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void run_ie9()
        {
            try
            {
                string executablePath = Environment.GetCommandLineArgs()[0];
                string executableName = System.IO.Path.GetFileName(executablePath);

                RegistryKey registrybrowser = Registry.CurrentUser.OpenSubKey
                   (@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);

                if (registrybrowser == null)
                {
                    RegistryKey registryFolder = Registry.CurrentUser.OpenSubKey
                        (@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl", true);
                    registrybrowser = registryFolder.CreateSubKey("FEATURE_BROWSER_EMULATION");
                }
                registrybrowser.SetValue(executableName, 11000, RegistryValueKind.DWord);
                registrybrowser.Close();
            }
            catch { }
        }

        string servername = "[GG] Overpoch Taviana 1.0.5.1 |Coins|SlowZs|Group";
        private void get_servername()
        {
            //WebClient wc = new WebClient();
            //string htmlString = wc.DownloadString("http://www.gametracker.com/server_info/us.ghostzgamerz.com:3302/");
            //Match mTitle = Regex.Match(htmlString, @"</span>(.*?)&nbsp;<span class=");
            //if (mTitle.Success)
            //{
            //    servername = mTitle.Groups[1].Value;

                view_listview();

        }

        private void view_listview()
        {
            string[] items = { servername, "?", "A2-OP" /*, ........... */};
            ListViewItem lvi = new ListViewItem(items);
            listView1.Items.Add(lvi);

        }

        private void btn_server_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            get_servername();

            webBrowser1.Visible = false;
        }

        class WebPostRequest
        {
            WebRequest theRequest;
            HttpWebResponse theResponse;
            ArrayList theQueryData;

            public WebPostRequest(string url)
            {
                theRequest = WebRequest.Create(url);
                theRequest.Method = "POST";
                theQueryData = new ArrayList();
            }

            public void Add(string key, string value)
            {
                theQueryData.Add(String.Format("{0}={1}", key, HttpUtility.UrlEncode(value)));
            }

            public string GetResponse()
            {

                theRequest.ContentType = "application/x-www-form-urlencoded";

                string Parameters = String.Join("&", (String[])theQueryData.ToArray(typeof(string)));
                theRequest.ContentLength = Parameters.Length;

                StreamWriter sw = new StreamWriter(theRequest.GetRequestStream());
                sw.Write(Parameters);
                sw.Close();

                theResponse = (HttpWebResponse)theRequest.GetResponse();
                StreamReader sr = new StreamReader(theResponse.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            webBrowser1.Visible = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        private void btn_off_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel4.Height > 475) timer1.Enabled = false;
            else panel4.Height += 10;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (panel4.Height <10) timer2.Enabled = false;
            else panel4.Height -= 10;
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void btn_path_Click(object sender, EventArgs e)
        {
            Process.Start(@"c:\windows\");
        }
    }
}