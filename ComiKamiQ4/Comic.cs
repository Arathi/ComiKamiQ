using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ComiKamiQ
{
    class Comic
    {
        protected string url;
        protected string root;
        protected int chapterNameConfig;
        protected int pageNameConfig;
        protected IList<Chapter> chapters;

        public const int ChapterRoot_None = 0;
        public const int ChapterRoot_Name = 1;
        public const int ChapterRoot_Index = 2;
        public const int ChapterRoot_IndexName = 3;

        public const int PageName_ChapterIndex = 0;
        public const int PageName_Name = 1;
        public const int PageName_Index = 2;
        public const int PageName_IndexName = 3;
        public const int PageName_ChapterIndexName = 4;

        #region 构造与初始化
        public Comic(string url, string root = "", int chapterNameConfig = 0, int pageNameConfig = 0)
        {
            Init(url, root, chapterNameConfig, pageNameConfig);
        }

        protected void Init(string url, string root, int chapterNameConfig, int pageNameConfig)
        {
            this.url = url;
            this.root = root;
            this.chapterNameConfig = chapterNameConfig;
            this.pageNameConfig = pageNameConfig;
            chapters = new List<Chapter>();
        }
        #endregion

        #region 属性
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public string Root
        {
            get { return root; }
            set { root = value; }
        }
        public int ChapterNameConfig
        {
            get { return chapterNameConfig; }
            set { chapterNameConfig = value; }
        }
        public int PageNameConfig
        {
            get { return pageNameConfig; }
            set { pageNameConfig = value; }
        }
        public IList<Chapter> Chapters
        {
            get { return chapters; }
            set { chapters = value; }
        }
        #endregion

        #region 方法
        public override string ToString()
        {
            return Url;
        }

        public IList<Page> SetPathForAllPage(bool checkedOnly = false)
        {
            IList<Page> pages = new List<Page>();
            int chapterIndexWidth = (int)Math.Log10(chapters.Count) + 1;
            foreach (Chapter chapter in chapters)
            {
                if (checkedOnly && !chapter.Checked) continue;
                string chapterName;// = chapter.Name;
                string chapterIndex = chapter.Index.ToString().PadLeft(chapterIndexWidth, '0');
                int pageIndexWidth = (int)Math.Log10(chapter.Pages.Count) + 1;
                switch (chapterNameConfig)
                {
                    case ChapterRoot_None:
                        chapterName = "";
                        break;
                    case ChapterRoot_Name:
                        chapterName = chapter.Name;
                        break;
                    case ChapterRoot_Index:
                        chapterName = chapterIndex;
                        break;
                    case ChapterRoot_IndexName:
                        chapterName = chapterIndex + "_" + chapter.Name;
                        break;
                    default:
                        chapterName = chapterIndex;
                        break;
                }
                foreach (Page page in chapter.Pages)
                {
                    if (page.Path != null && page.Path != "") continue;
                    string pageIndex = page.Index.ToString().PadLeft(pageIndexWidth, '0');
                    string pageName;
                    switch (pageNameConfig)
                    {
                        case PageName_ChapterIndex:
                            pageName = chapterIndex + "_" + pageIndex + page.FileExt;
                            break;
                        case PageName_Name:
                            pageName = page.FileName;
                            break;
                        case PageName_Index:
                            pageName = pageIndex + page.FileExt;
                            break;
                        case PageName_IndexName:
                            pageName = pageIndex + "_" + page.FileName;
                            break;
                        case PageName_ChapterIndexName:
                            pageName = chapterIndex + "_" + pageIndex + "_" + page.FileName;
                            break;
                        default:
                            pageName = pageIndex + page.FileExt;
                            break;
                    }
                    page.SetPath(root, chapterName, pageName);
                    pages.Add(page);
                }
            }
            return pages;
        }
        #endregion
    }
}
