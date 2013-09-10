using System;
using System.Collections.Generic;
using System.Text;

namespace ComiKamiQ
{
    class Chapter
    {
        protected int index;
        protected string url;
        protected string name;
        protected IList<Page> pages;
        protected bool checked_;
        protected object tag;
        protected bool downloaded;
        protected bool started;

        #region 构造函数
        public Chapter(int index, string url, string name)
        {
            this.index = index;
            this.url = url;
            this.name = name;
            pages = new List<Page>();
        }
        #endregion

        #region 属性
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public IList<Page> Pages
        {
            get { return pages; }
            set
            {
                pages = value;
                foreach (Page page in pages)
                {
                    page.Chapter = this;
                }
            }
        }
        public bool Checked
        {
            get { return checked_; }
            set { checked_ = value; }
        }
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        public bool Downloaded
        {
            get { return downloaded; }
        }
        public bool Started
        {
            get { return started; }
            set { started = value; }
        }
        #endregion
#if DISABLED
        public override string ToString()
        {
            if (pages == null || pages.Count == 0)
            {
                if (url == null || url == "") return name;
                return name + "[" + url + "]";
            }
            if (Downloaded)
                return name + "(" + pages.Count + "P)[已完成]";
            if (!Started)
                return name + "(" + pages.Count + "P)";
            else
            {
                int counter = 0;
                foreach (Page p in pages)
                {
                    if (p.Downloaded) counter++;
                }
                if (counter >= pages.Count) downloaded = true;
                if (Downloaded)
                    return name + "(" + pages.Count + "P)[已完成]";
                return name + "(" + counter + "/" + pages.Count + ")";
            }
        }
#endif
        public override string ToString()
        {
            if (pages == null || pages.Count == 0)
            {
                if (url == null || url == "") return name;
                return name + "[" + url + "]";
            }
            if (Downloaded)
                return name + "(" + pages.Count + "P)[已完成]";
            else
            {
                int counter = 0;
                foreach (Page p in pages)
                {
                    if (p.Downloaded) counter++;
                }
                if (counter >= pages.Count) downloaded = true;
                if (Downloaded)
                    return name + "(" + pages.Count + "P)[已完成]";
                if (counter==0) return name + "(" + pages.Count + "P)";
                return name + "(" + counter + "/" + pages.Count + ")";
            }
        }
    }

}
