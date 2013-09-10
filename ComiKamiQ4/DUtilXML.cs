using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace ComiKamiQ
{
    class DUtilXML : DUtil
    {
        XmlDocument xmlDoc = new XmlDocument();
            
        public DUtilXML(string path)
            : base("", "", 0, 0)
        {
            loadFromXML(path);
        }

        protected void loadFromXML(string path)
        {
            if (!File.Exists(path)) return;
            xmlDoc.Load(path);
            XmlElement rootElement = xmlDoc.DocumentElement;
            root=rootElement.Attributes["Root"].Value;
            chapterNameConfig = Int32.Parse(rootElement.Attributes["ChapterNameConfig"].Value);
            pageNameConfig = Int32.Parse(rootElement.Attributes["PageNameConfig"].Value);
        }

        public override IList<Chapter> SpiderChapters(string url = null)
        {
            IList<Chapter> chapters = new List<Chapter>();
            XmlElement rootElement = xmlDoc.DocumentElement;
            foreach (XmlElement chapterNode in rootElement.ChildNodes)
            {
                int chapterIndex = Int32.Parse(chapterNode.Attributes["Index"].Value);
                string chapterName = chapterNode.Attributes["Name"].Value;
                Chapter chapter = new Chapter(chapterIndex, "", chapterName);
                chapter.Tag = chapterNode;
                chapter.Checked = true;
                chapters.Add(chapter);
            }
            this.chapters = chapters;
            return chapters;
        }

        public override IList<Page> SpiderPages(Chapter chapter)
        {
            IList<Page> pages = new List<Page>();
            XmlElement chapterElement = (XmlElement)chapter.Tag;
            //chapter.Tag = null;
            foreach (XmlElement pageNode in chapterElement.ChildNodes)
            {
                int pageIndex = Int32.Parse(pageNode.Attributes["Index"].Value);
                XmlNode urlNode = pageNode.ChildNodes.Item(0);
                XmlNode pathNode = pageNode.ChildNodes.Item(1);
                Page page =new Page( pageIndex, urlNode.InnerText);
                page.Path = pathNode.InnerText;
                pages.Add(page);
            }
            chapter.Pages = pages;
            return pages;
        }
    }
}
