using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Xml;

namespace ComiKamiQ
{
    abstract class DUtil : Comic
    {
        public abstract IList<Chapter> SpiderChapters(string url=null);
        public abstract IList<Page> SpiderPages(Chapter chapter);

        public static Regex reQuote = new Regex("(?<=\").*?(?=\")", RegexOptions.Compiled);
        public static Regex reHref = new Regex("(?<=href=\").*?(?=\")", RegexOptions.Compiled);
        public static Regex reCharset = new Regex("<meta([^<]*)charset=\"([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        public static Regex reTitle = new Regex("(?<=[\" ]>).*?(?=</a>)", RegexOptions.Compiled);

        protected static Microsoft.JScript.Vsa.VsaEngine jsEngine = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();

        public DUtil(string url, string root = "", int chapterNameConfig = 0, int pageNameConfig = 0)
            : base(url, root, chapterNameConfig, pageNameConfig)
        { }

        public virtual string GetEncoding()
        {
            return "UTF-8";
        }

        public string LoadHtml(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                string html = "";
                // Set some reasonable limits on resources used by this request
                request.MaximumAutomaticRedirections = 4;
                request.MaximumResponseHeadersLength = 4;
                // Set credentials to use for this request.
                HttpWebResponse response;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream;
                StreamReader readStream;
                receiveStream = response.GetResponseStream();
                if (response.ContentEncoding != "")
                {
                    if (response.ContentEncoding.ToLower().Contains("gzip"))
                        receiveStream = new GZipStream(receiveStream, CompressionMode.Decompress);
                    else return null;//if (response.ContentEncoding.ToLower().Contains("gzip"))
                }
                Encoding encoding;
                encoding= Encoding.GetEncoding(GetEncoding()); //response.CharacterSet);
                readStream = new StreamReader(receiveStream, encoding);
                html = readStream.ReadToEnd();
                //Match charSetMatch = reCharset.Match(html);
                //if (charSetMatch.Success)
                //{
                //    string webCharSet = charSetMatch.Groups[2].Value;
                //    if (webCharSet != response.CharacterSet)
                //    {
                //        encoding = Encoding.GetEncoding(webCharSet);
                //        readStream = new StreamReader(receiveStream, encoding);
                //        html = readStream.ReadToEnd();
                //    }
                //}
                response.Close();
                readStream.Close();
                return html;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string JScriptEval(string expression)
        {
            string jsResultStr;
            object jsResultObj;
            try
            {
                jsResultObj = Microsoft.JScript.Eval.JScriptEvaluate(expression, jsEngine);
                jsResultStr = jsResultObj.ToString();
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
            return jsResultStr;
        }

        public void SaveToXML(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(dec);
            //创建根节点
            XmlElement root = xmlDoc.CreateElement("ComiKamiTaskList");
            XmlAttribute rootPath = xmlDoc.CreateAttribute("Root");
            rootPath.Value = Root;
            root.Attributes.Append(rootPath);
            XmlAttribute configChapterName = xmlDoc.CreateAttribute("ChapterNameConfig");
            configChapterName.Value = chapterNameConfig.ToString();
            root.Attributes.Append(configChapterName);
            XmlAttribute configPageName = xmlDoc.CreateAttribute("PageNameConfig");
            configPageName.Value = pageNameConfig.ToString();
            root.Attributes.Append(configPageName);
            //chapterNameConfig;
            //pageNameConfig;
            foreach (Chapter chapter in chapters)
            {
                if (!chapter.Checked) continue;
                if (chapter.Downloaded) continue;
                XmlNode nodeChapter = xmlDoc.CreateElement("Chapter");

                XmlAttribute chapterIndex = xmlDoc.CreateAttribute("Index");
                chapterIndex.Value = chapter.Index.ToString();
                nodeChapter.Attributes.Append(chapterIndex);

                XmlAttribute chapterName = xmlDoc.CreateAttribute("Name");
                chapterName.Value = chapter.Name;
                nodeChapter.Attributes.Append(chapterName);

                foreach (Page page in chapter.Pages)
                {
                    //if (page.Downloaded) continue;
                    XmlNode nodePage = xmlDoc.CreateElement("Page");

                    XmlAttribute pageIndex = xmlDoc.CreateAttribute("Index");
                    pageIndex.Value = page.Index.ToString();
                    nodePage.Attributes.Append(pageIndex);

                    XmlElement pageUrl = xmlDoc.CreateElement("Url");
                    pageUrl.InnerText = page.Url;
                    nodePage.AppendChild(pageUrl);

                    XmlElement pagePath = xmlDoc.CreateElement("Path");
                    pagePath.InnerText = page.Path;
                    nodePage.AppendChild(pagePath);

                    nodeChapter.AppendChild(nodePage);
                }
                root.AppendChild(nodeChapter);
            }
            xmlDoc.AppendChild(root);
            xmlDoc.Save(xmlPath);
        }

    }

}
