using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ComiKamiQ
{
    class DUtilDmzj : DUtil
    {
        public static Regex reTitle = new Regex("(?<=[\" ]>).*?(?=</a>)", RegexOptions.Compiled);

        public DUtilDmzj(string url, string root = "", int chapterNameConfig = 3, int pageNameConfig = 2)
            : base(url, root, chapterNameConfig, pageNameConfig)
        { }

        public override IList<Chapter> SpiderChapters(string url = null)
        {
            IList<Chapter> chapters_ = new List<Chapter>();
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
            string[] htmlines = html.Split('\n');
            //分离地址与标题
            int counter = 0;
            foreach (string line in htmlines)
            {
                if (line.Contains("<li><a title="))
                {
                    chapters_.Add(SplitChapterInfo(line, ++counter));
                }
            }
            chapters = chapters_;
            return chapters_;
        }

        public override IList<Page> SpiderPages(Chapter chapter)
        {
            //throw new NotImplementedException();
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
            foreach (string line in htmlines)
            {
                if (line.Contains("var pages"))
                {
                    //分离这一行
                    alist = SplitPaths(line);
                    break;
                }
                if (line.Contains("eval("))
                {
                    string realine = DUtil.JScriptEval(line.Substring(4));
                    if (realine == null) continue;
                    alist = SplitPaths(realine);
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
                chapterUrl = "http://www.dmzj.com" + match.Value;
            match = reTitle.Match(line, 0);
            if (match.Success)
                chapterName = match.Value;
            return new Chapter(counter, chapterUrl, chapterName);
        }

        public virtual IList<Page> SplitPaths(string paths)
        {
            IList<Page> alist = new List<Page>();
            int restartat = 0;
            Match match = reQuote.Match(paths, restartat);

            int counter = 0;
            while (match.Success)
            {
            	//<img class="bigimgborder pic_link" alt="" style="border: 1px solid rgb(204, 204, 204); padding: 1px; cursor: pointer;" src="http://imgfast.dmzj.com/j/%E8%BF%9B%E5%87%BB%E7%9A%84%E5%B7%A8%E4%BA%BA/%E7%AC%AC01%E5%8D%B7/002.jpg" name="bigimg2" id="bigimg2" href="">
                string url = "http://imgfast.dmzj.com/";
                url += DUtil.JScriptEval("\"" + match.Value + "\"");
                Page page = new Page(++counter, url);
                alist.Add(page);
                restartat = match.Index + match.Length + 2;
                match = reQuote.Match(paths, restartat);
            }
            return alist;
        }

    }

}
