using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheOneParser
{
    public partial class Form1 : Form
    {

        private String[] picks = new WebClient().DownloadString("https://s3.amazonaws.com/pacesettertech/practical/pool_picks.txt").Split('\n');
        private String[] leader = new WebClient().DownloadString("https://s3.amazonaws.com/pacesettertech/practical/event_leaderboard.txt").Split('\n');

        public Form1()
        {
            InitializeComponent();
            Hashtable h = new Hashtable();
            Hashtable otherHash = new Hashtable();

            for (int i = 1; i < leader.Length; i = i + 2)
            {
                String[] temp = leader[i].Split('\t');
                h.Add(temp[0].Trim(), temp[1].Replace("E", "0").Trim());
            }

            foreach (String line in picks)
            {
                String[] temp = line.Split('=');
                int score = 0;
                foreach (String name in temp[1].Split(','))
                {
                    score += Convert.ToInt32(h[name]);
                }
                otherHash.Add(temp[0], score);
            }

            foreach (DictionaryEntry pair in otherHash)
                richTextBox1.Text += pair.Key.ToString() + ": " + pair.Value.ToString() + "\n";
        }
    }
}
