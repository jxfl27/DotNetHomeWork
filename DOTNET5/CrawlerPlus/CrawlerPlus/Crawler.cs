using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerPlus
{
    public class Crawler
    {
        private int count = 0;
        private int pagecount = 0;
        public string HostFilter { get; set; } //主机过滤规则
        public string FileFilter { get; set; } //文件过滤规则

        public Hashtable urls = new Hashtable();//页面爬取与否标记
        public int maxCount { get; set; }//爬取最大页面
        public string Url { get; set; }
    }
}