using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ComiKamiQ
{
    class DUtilBengou : DUtil
    {
        public static Regex reSingleQuote = new Regex("(?<=\').*?(?=\')", RegexOptions.Compiled);

        public DUtilBengou(string url, string root = "", int chapterNameConfig = 1, int pageNameConfig = 3)
            : base(url, root, chapterNameConfig, pageNameConfig)
        { }

        public IList<Chapter> SpiderPartChapters(string html, int startIndex=1)
        {
            IList<Chapter> chapters_ = new List<Chapter>();
            IList<Chapter> chapters_reverse = new List<Chapter>();

            string[] htmlines = html.Split('\n');
            //分离地址与标题
            int counter = 0;
            foreach (string line in htmlines)
            {
                if (line.Contains("<a href=\""))
                {
                    chapters_.Add(SplitChapterInfo(line, ++counter));
                }
            }
            int index=startIndex;
            foreach (Chapter chapter in chapters_)
            {
                chapter.Index = index++;
                chapters_reverse.Insert(0, chapter);
            }

            return chapters_reverse;
        }

        public override IList<Chapter> SpiderChapters(string url = null)
        {
            IList<Chapter> chapters_, chapters_part;//= new List<Chapter>();
            chapters_ = new List<Chapter>();

            if (url == null) url = this.url;
            if (url == null) return null;
            //下载网页
            string html;
            try
            {
                html = LoadHtml(url);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //分离行
            string allComicHtml = null;
            int indexStart, indexEnd;

            indexStart = html.IndexOf("<!--全部漫画{-->");
            indexEnd = html.IndexOf("<!--}全部漫画-->");
            allComicHtml = html.Substring(indexStart, indexEnd - indexStart);
            chapters_part=SpiderPartChapters(allComicHtml);
            foreach (Chapter chapter in chapters_part)
                chapters_.Add(chapter);

            indexStart = html.IndexOf("<!--{外卷-->");
            indexEnd = html.IndexOf("<!--}外卷-->");
            allComicHtml = html.Substring(indexStart, indexEnd - indexStart);
            chapters_part = SpiderPartChapters(allComicHtml, 1000000001);
            foreach (Chapter chapter in chapters_part)
                chapters_.Add(chapter);

            chapters = chapters_;
            return chapters;
        }

        public override IList<Page> SpiderPages(Chapter chapter)
        {
            IList<Page> alist = null;
            string html;
            try
            {
                html = LoadHtml(chapter.Url);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            string[] htmlines = html.Split('\n');
            string urlStartWith=null;

            foreach (string line in htmlines)
                if (line.Contains("var pic_base = "))
                {
                    Match match = reSingleQuote.Match(line);
                    if (match.Success)
                    {
                        urlStartWith = match.Value;
                    }
                }
            foreach (string line in htmlines)
            {
                if (line.Contains("var picTree = ["))
                {
                    //分离这一行
                    alist = SplitPaths(line, urlStartWith);
                    break;
                }
            }
            chapter.Pages = alist;
            return alist;
        }
        
        public Chapter SplitChapterInfo(string line, int counter)
        {
            string chapterUrl = "", chapterName = "";
            Match match;
            match = reHref.Match(line, 0);
            if (match.Success)
                chapterUrl = match.Value;
            match = reTitle.Match(line, 0);
            if (match.Success)
                chapterName = match.Value;
            return new Chapter(counter, chapterUrl, chapterName);
        }
        
        public virtual IList<Page> SplitPaths(string paths, string urlStartWith)
        {
            IList<Page> alist = new List<Page>();
            int restartat = 0;
            Match match = reQuote.Match(paths, restartat);

            int counter = 0;
            while (match.Success)
            {
                string url = urlStartWith;
                url += DUtil.JScriptEval("\"" + match.Value + "\"");
                Page page = new Page(++counter, url);
                alist.Add(page);
                restartat = match.Index + match.Length + 2;
                match = reQuote.Match(paths, restartat);
            }
            return alist;
        }
        
        public override string GetEncoding()
        {
            return "UTF-8";
            //throw new NotImplementedException();
        }
    }
}
