using System;
using System.Collections.Generic;
using System.Text;

namespace ComiKamiQ
{
    class Page
    {
        protected int index;
        protected string url;
        protected string path;
        protected string filename;
        protected string fileext;
        protected object tag;
        protected bool downloaded;
        protected Chapter chapter;

        #region 构造函数
        public Page(int index, string url)
        {
            this.index = index;
            this.url = url;
            SplitFileName(url);
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
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }
        public string FileExt
        {
            get { return fileext; }
        }
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        public bool Downloaded
        {
            get { return downloaded; }
            set { downloaded = value; }
        }
        public Chapter Chapter
        {
            get { return chapter; }
            set { chapter = value; }
        }
        #endregion

        #region 方法
        public void SetPath(string rootPath, string chapterPath, string newFileName)
        {
            path = rootPath + "\\" + chapterPath + "\\" + newFileName;
        }
        public string GetPathWithoutFileName()
        {
            if (path == null || path == "") return null;
            int lastSlashIndex = path.LastIndexOf('\\');
            return path.Substring(0, lastSlashIndex);
            // string pathWithoutFileName;
        }

        public override string ToString()
        {
            if (path == null || path == "") return url;
            if (downloaded) return "第" + index + "页" + "[已完成]";
            return url + ";" + path;
        }

        protected void SplitFileName(string url)
        {
            int lastSlashIndex = url.LastIndexOf('/');
            filename = url.Substring(lastSlashIndex + 1);
            int lastDotIndex = url.LastIndexOf('.');
            fileext = url.Substring(lastDotIndex);
        }
        #endregion
    }

}
