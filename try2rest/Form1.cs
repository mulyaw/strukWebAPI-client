using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using HtmlAgilityPack;
using System.Xml;
using System.Xml.Linq;

namespace try2rest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {

            if (cb1.Text == "PLN TAGIHAN")
                cb1.Text = "POSTPAID";
            else if (cb1.Text == "PLN TOKEN")
                cb1.Text = "PREPAID";
            else if (cb1.Text == "PLN NON TAGIHAN")
                cb1.Text = "NONTAG";
            else if (cb1.Text == "TELKOM GROUP")
                cb1.Text = "TELKOM";
            else if (cb1.Text == "MULTIFINANCE")
                cb1.Text = "MULTIFINANCE";
            else if (cb1.Text == "PDAM")
                cb1.Text = "PDAM";
            else if (cb1.Text == "TV TAGIHAN")
                cb1.Text = "TV";
            else if (cb1.Text == "TAGIHAN SELULER")
                cb1.Text = "HP";
            else if (cb1.Text == "BPJS KESEHATAN")
                cb1.Text = "BPJS";

            string result = string.Empty;
            string idpel = tb1.Text;
            string tgl = dtp1.Value.Date.ToString("yyyy-MM-dd");

            string tipe = cb1.Text;
            string uri = @"http://localhost:51092/api/getstruk/" + tipe + "/" + idpel + "/" + tgl;






            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.AutomaticDecompression = DecompressionMethods.GZip;

            req.ContentType = "application/x-www-form-urlencoded";

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            using (Stream stream = resp.GetResponseStream())
            using (StreamReader read = new StreamReader(stream, Encoding.UTF8))
            {
                result = read.ReadToEnd();
            }
            //var pageDoc = new HtmlAgilityPack.HtmlDocument();
            //pageDoc.LoadHtml(result);
            //var pageText = pageDoc.DocumentNode.InnerText;

            //richTextBox1.AppendText(pageText);


            /*HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(result);
            StringBuilder sb = new StringBuilder();
            IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants().Where(n =>
               n.NodeType == HtmlNodeType.Text &&
               n.ParentNode.Name != "script" &&
               n.ParentNode.Name != "style");
            foreach (HtmlNode node in nodes)
            {
                //Console.WriteLine(node.InnerText);
                richTextBox1.AppendText(node.InnerText);
            }*/


            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(result);
            var TempString = new StringBuilder();
            foreach (HtmlNode style in doc.DocumentNode.Descendants("style").ToArray())
            {
                style.Remove();
            }
            foreach (HtmlNode script in doc.DocumentNode.Descendants("script").ToArray())
            {
                script.Remove();
            }
            foreach (HtmlTextNode node in doc.DocumentNode.SelectNodes("//text()"))
            {
                TempString.ToString().Replace("\r", "").Replace("\n", "");
                richTextBox1.AppendText(node.InnerText);

            }
        }
    }
}