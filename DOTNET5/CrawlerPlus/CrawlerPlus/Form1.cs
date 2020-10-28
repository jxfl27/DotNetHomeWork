using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlerPlus
{
    public partial class Form1 : Form
    {
        private int count = 0;  //爬取页面数    
        private int pagecount = 0;

        public Queue<string> pending = new Queue<string>();//存贮待爬取对象的队列

        public static  string urlParseRegex =
            @"^(?<site>https?://(?<host>[\w\d.]+)(:\d+)?($|/))([\w\d]+/)*(?<file>[^#?]*)";
        public static string strRef = @"(href|HREF)\s*=\s*[""'](?<url>[^""'#>]+)[""']";

        private int _Error = 0;//成功数
        private int _Complete = 0;//失败数

        public Crawler crawler = new Crawler();
        public string crawlMessage { get; set; }//显示爬行信息
        Thread thread = null;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            textBox1.Text = "http://www.cnblogs.com/dstang2000/";
            crawlMessage = "";
            textBox3.DataBindings.Add("Text",crawler,"maxPage");
        }
        private void Crawl()
        {
            crawlMessage += "开始爬行了.... \r\n";
            textBox2.Text = crawlMessage;
            while (pending.Count > 0&&count< crawler.maxCount)//队列里有元素及未达最大页面时
            {
                string current = null;
                current = pending.Dequeue(); //出队

                if (current == null) break;
                crawlMessage += ("页面\r\n" + current + "爬行中...\r\n");
                textBox2.Text = crawlMessage;//信息

                string html = DownLoad(current); 
                count++;//下载

                if (html == "")//判断
                { 
                    crawler.urls[current] = true; crawlMessage += "爬行失败\r\n"; 
                    textBox2.Text = crawlMessage; 
                }
                else
                { 
                    crawler.urls[current] = true; //已爬取标记
                        bool isHtml;

                    if (html.Length >= 15)//判断是否为html格式
                    {
                        isHtml = html.Substring(0, 15).ToLower() == "<!doctype html>";
                    }
                    else
                    {
                        isHtml = false;
                        if (isHtml)
                        {
                            Parse(html, current);//转换为要求的格式并添加到队列中
                            crawlMessage += "爬行结束\r\n";
                            textBox2.Text = crawlMessage;
                        }
                        else
                        {
                            crawlMessage += "未能爬取到HTML\r\n";
                            textBox2.Text = crawlMessage;
                        }
                    }
                }
            }
            label4.Text = "起始网站爬取完成！\r\n" + $"成功:{_Complete},失败：{_Error}";
            crawlMessage += "爬行完成";
            textBox2.Text = crawlMessage;
        }

        public string DownLoad(string url)  
        {
            try
            {
                System.Net.WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                _Complete++;
                return html;
            }
            catch (Exception ex)
            {
                crawlMessage += ex.Message;
                crawlMessage += "\r\n";
                _Error++;
                return "";
            }
        }

        private void Parse(string html, string current)
        { 
            var matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                string linkUrl = match.Groups["url"].Value;
                if (linkUrl == null || linkUrl == "") continue;
                linkUrl = FixUrl(linkUrl, current);//转绝对路径
                                                   //解析出host和file两个部分，进行过滤
                Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
                string host = linkUrlMatch.Groups["host"].Value;
                string file = linkUrlMatch.Groups["file"].Value;
                if (file == "") file = "index.html";

                if (Regex.IsMatch(host, crawler.HostFilter) && Regex.IsMatch(file, crawler.FileFilter)
                  && !crawler.urls.ContainsKey(linkUrl))
                {
                    pending.Enqueue(linkUrl);
                    crawler.urls.Add(linkUrl, false);
                }
            }
        }
        static private string FixUrl(string url, string baseUrl)
        {
            if (url.Contains("://"))
            {
                return url;
            }
            if (url.StartsWith("//"))
            {
                return "http:" + url;
            }
            if (url.StartsWith("/"))
            {
                Match urlMatch = Regex.Match(baseUrl, urlParseRegex);
                String site = urlMatch.Groups["site"].Value;
                return site.EndsWith("/") ? site + url.Substring(1) : site + url;
            }

            if (url.StartsWith("../"))
            {
                url = url.Substring(3);
                int idx = baseUrl.LastIndexOf('/');
                return FixUrl(url, baseUrl.Substring(0, idx));
            }

            if (url.StartsWith("./"))
            {
                return FixUrl(url.Substring(2), baseUrl);
            }

            int end = baseUrl.LastIndexOf("/");
            return baseUrl.Substring(0, end) + "/" + url;
        }

        private void startCrawl(object sender, EventArgs e)
        { 
            string urlForm = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";//检查起始url格式
            crawler.Url = textBox1.Text;
            if (crawler.Url != null && crawler.Url != "" && Regex.IsMatch(crawler.Url, urlForm))
            {
                Match match = Regex.Match(crawler.Url, urlParseRegex);
                if (match.Length == 0) return;
                string host = match.Groups["host"].Value;
                crawler.HostFilter = "^" + host + "$";
                crawler.FileFilter = ".html?$";
                _Complete = 0;
                _Error = 0;
                pagecount = 0;
                count = 0;
                if (thread != null)
                {
                    thread.Abort();
                }
                pending.Clear();
                pending.Enqueue(crawler.Url);
                crawlMessage = "";
                textBox2.Text = crawlMessage;
                label4.Text = "正在爬取...";
                crawler.urls.Clear();
                crawler.urls.Add(crawler.Url, false);
                thread = new Thread(this.Crawl);
                thread.Start();
            }
            else
            {
                MessageBox.Show("URL格式错误");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.SelectionStart = this.textBox2.Text.Length;
            this.textBox2.SelectionLength = 0;
            this.textBox2.ScrollToCaret();
        }
    }
}
