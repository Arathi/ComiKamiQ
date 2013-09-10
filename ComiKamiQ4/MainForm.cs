/*
 * 由SharpDevelop创建。
 * 用户： Arathi
 * 日期: 2013/4/15
 * 时间: 10:28
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using System.ComponentModel;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using ComiKamiQ;

namespace ComiKamiQ4
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
        private DUtil dutil;
        private WebClient client=new WebClient();
        private IList<Page> taskList;
        private IEnumerator<Page> iterator=null;

        private string version;
        private int subversion=17;
        private string ApplicationTitle;
        private bool paused;

        private Size lastSize;

        private string LastDownloadList = Application.StartupPath + "\\lastlist.ckt";
        
		#region 窗体事件
        public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			int lastDotIndex = Application.ProductVersion.LastIndexOf('.');
            this.version = "v" + Application.ProductVersion.Substring(0,lastDotIndex);

            string version = this.version;
#if DEBUG
            version += "(" + Application.ProductVersion.Substring(lastDotIndex + 1) + ")";
#endif
#if IsSvnEdition
            version += " SVN" + subversion;
#endif
            this.Text += " "+version;
            ApplicationTitle = this.Text;

            paused = true;
		}
        
        private void ComiDownForm_Load(object sender, EventArgs e)
        {
            comboRootRule.Items.Add("没有章节目录");
            comboRootRule.Items.Add("章节名");
            comboRootRule.Items.Add("章节序号");
            comboRootRule.Items.Add("章节序号_章节名(*)");
            comboRootRule.SelectedIndex = 3;

            comboFileRule.Items.Add("章节序号_页码");
            comboFileRule.Items.Add("原文件名");
            comboFileRule.Items.Add("页码(*)");
            comboFileRule.Items.Add("页码_原文件名");
            comboFileRule.Items.Add("章节序号_页码_原文件名");
            comboFileRule.SelectedIndex = 2;
            
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);

            //txtURL.Text = "http://www.bengou.com/080819/hyrz008081909/";
            //txtRoot.Text = @"D:\comic\ckqtest\KasokuSekai\naruto";

            if (File.Exists(LastDownloadList))
            {
                DialogResult dr = MessageBox.Show("是否载入上次未完成的任务？", "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    LoadDownloadList(LastDownloadList);
                    btnAnalysis_Click(null, null);
                }
                File.Delete(LastDownloadList);
            }

            lastSize = Size;
        }

        private void ComiDownForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dutil == null) return;
            if (taskList == null || taskList.Count <= 0) return;
            DialogResult dr = MessageBox.Show("是否保存正在下载的列表？", "提示", MessageBoxButtons.YesNoCancel);
            if (dr == DialogResult.Yes)
            {
                //处理保存
                SaveDownloadList(LastDownloadList);
            }
            else if (dr != DialogResult.No)
            {
                //不能退出
                e.Cancel = true;
            }
        }
        private void ComiDownForm_SizeChanged(object sender, EventArgs e)
        {
            int deltaWidth = Width - lastSize.Width, deltaHeight = Height - lastSize.Height;
            //lblURL; //不需要调整
            txtURL.Width += deltaWidth; //调整长度 Size.Width
            btnOpen.Left += deltaWidth; //仅左右移动
            btnAnalysis.Left += deltaWidth; //仅左右移动

            //lblRoot; //不需要调整
            txtRoot.Width += deltaWidth; //调整长度
            btnChooseDir.Left += deltaWidth; //仅左右移动
            btnDownload.Left += deltaWidth; //仅左右移动

            //lblRenameRule; //不需要调整
            //comboRootRulel; //不需要调整
            //lblRulesSplit; //不需要调整
            //comboFileRule; //不需要调整

            tabControl.Width += deltaWidth;//只调整大小，容器内部的也一样
            tabControl.Height += deltaHeight;
            checkedListChapters.Width += deltaWidth;
            checkedListChapters.Height += deltaHeight;
            taskTree.Width += deltaWidth;
            taskTree.Height += deltaHeight;

            lblDownloadingFileName.Top += deltaHeight; //仅上下移动(Location.Y)
            btnExport.Top += deltaHeight; //移动(Location)
            btnExport.Left += deltaWidth;
            btnImport.Top += deltaHeight; //移动
            btnImport.Left += deltaWidth;
            btnSelectAll.Top += deltaHeight; //移动
            btnSelectAll.Left += deltaWidth;
            btnSelectReverse.Top += deltaHeight; //移动
            btnSelectReverse.Left += deltaWidth;
            progressBar.Top += deltaHeight; //上下移动，调整长度(Size.Width)
            progressBar.Width += deltaWidth;

            lastSize = this.Size;
        }

        private void SaveDownloadList(string path)
        {
            if (dutil == null) return;
            //XmlDocument xmlist;
            dutil.SaveToXML(path);
        }

        private void LoadDownloadList(string path)
        {
            dutil = new DUtilXML(path);
            comboRootRule.SelectedIndex = dutil.ChapterNameConfig;
            comboFileRule.SelectedIndex = dutil.PageNameConfig;
            try
            {
                dutil.SpiderChapters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
            taskTree.Nodes.Clear();
            checkedListChapters.Items.Clear();
            foreach (Chapter chapter in dutil.Chapters)
            {
                checkedListChapters.Items.Add(chapter, true);
            }
            tabControl.SelectedIndex = 0;
        }
        #endregion

        #region 按钮事件
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (txtURL.Text == null || txtURL.Text == "") return;
            dutil = DUtilFactory.CreateDUtil(txtURL.Text);
            if (dutil == null)
            {
                MessageBox.Show("错误的URL！","提示");
            }
            try
            {
                dutil.SpiderChapters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"错误");
                return;
            }
            taskTree.Nodes.Clear();
            checkedListChapters.Items.Clear();
            foreach (Chapter chapter in dutil.Chapters)
            {
                checkedListChapters.Items.Add(chapter);
            }
            tabControl.SelectedIndex = 0;

            comboRootRule.SelectedIndex = dutil.ChapterNameConfig;
            comboFileRule.SelectedIndex = dutil.PageNameConfig;
        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            if (dutil == null) return;
            if (dutil.Root != null && dutil.Root != "") txtRoot.Text = dutil.Root;
            if (txtRoot.Text == "")
            {
                MessageBox.Show("下载目录未设置","错误");
                return;
            }
            dutil.Root = txtRoot.Text;
            dutil.ChapterNameConfig = comboRootRule.SelectedIndex;
            dutil.PageNameConfig = comboFileRule.SelectedIndex;
            //进度条设置
            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = checkedListChapters.CheckedItems.Count;
            lblDownloadingFileName.Text = "正在分析...";
            //设置选取
            foreach (Chapter chapter in dutil.Chapters)
            {
                chapter.Checked = false;
            }
            foreach (Chapter chapter in checkedListChapters.CheckedItems)
            {
                //if (chapter.Checked)
                //{
                //    progressBar1.Value++;
                //    continue;
                //}
                try
                {
                    dutil.SpiderPages(chapter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    continue;
                }
                progressBar.Value++;
                chapter.Checked = true;
                //this.OnPaint(null);
            }
            dutil.SetPathForAllPage(true);
            //清空原有的节点映射
            taskTree.Nodes.Clear();
            foreach (Chapter chapter in dutil.Chapters)
            {
                chapter.Tag = null;
                foreach (Page page in chapter.Pages)
                    page.Tag = null;
            }
            taskList = new List<Page>();
            //重新建立节点树
            foreach (Chapter chapter in dutil.Chapters)
            {
                if (!chapter.Checked) continue;
                TreeNode node = taskTree.Nodes.Add(chapter.ToString());
                node.Tag = chapter;
                chapter.Tag = node;
                foreach (Page page in chapter.Pages)
                {
                    TreeNode subNode = node.Nodes.Add(page.ToString());
                    subNode.Tag = page;
                    page.Tag = subNode;
                    taskList.Add(page);
                }
            }
            preDownloadPages();
            tabControl.SelectedIndex=1;
            //ControlsEnable = false;
            progressBar.Value = 0;
            lblDownloadingFileName.Text = "分析完成！";
        }

        private void btnSelectReverse_Click(object sender, EventArgs e)
        {
            int i, count = checkedListChapters.Items.Count;
            for (i = 0; i < count; i++)
                checkedListChapters.SetItemChecked(i, !checkedListChapters.GetItemChecked(i));
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            int i, count = checkedListChapters.Items.Count;
            for (i = 0; i < count; i++)
                checkedListChapters.SetItemChecked(i, true);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (taskList == null || taskList.Count == 0) return;
            //if (iterator == null) return;
            if (ControlsEnable)//btnDownload.Text == "开始下载"
            {
                //其他按钮全部禁用掉
                ControlsEnable = false;
                iterator = taskList.GetEnumerator();
                downloadNextPage();
            }
            else
            {
                //启用按钮
                ControlsEnable = true;
                //停止通信
                if (client.IsBusy)
                    client.CancelAsync();
            }
        }

        private void btnChooseDir_Click(object sender, EventArgs e)
        {
            string defaultPath=txtRoot.Text;//
            if (defaultPath=="")
                defaultPath=Application.StartupPath;
            try
            {
                Directory.CreateDirectory(defaultPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("目录不合法: \n"+ex.Message,"错误");
                defaultPath = "";
            }
            folderBrowserDialog1.SelectedPath = defaultPath;
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr != DialogResult.OK)
                return;
            txtRoot.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "ComiKami任务列表 (*.ckt)|*.ckt|所有文件 (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.FileName = "";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr != DialogResult.OK)
                return;
            string path = openFileDialog1.FileName;
            LoadDownloadList(path);
            btnAnalysis_Click(null, null);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dutil == null || taskList == null || taskList.Count <= 0)
            {
                MessageBox.Show("没有正在下载的任务","错误");
                return;
            }
            saveFileDialog1.FileName = "";
            saveFileDialog1.DefaultExt = "ckt";
            saveFileDialog1.Filter = "ComiKami任务列表 (*.ckt)|*.ckt|所有文件 (*.*)|*.*";
            saveFileDialog1.FilterIndex = 0;
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //处理保存
                string path = saveFileDialog1.FileName;
                SaveDownloadList(path);
            }
        }

        #endregion

        #region 属性
        /// <summary>
        /// 所有控件的可访问属性
        /// </summary>
        public bool ControlsEnable
        {
            get 
            {
                //return client.IsBusy;
                return btnDownload.Text=="开始下载"; 
            }
            set
            {
                if (value) //结束或者暂停
                {
                    btnAnalysis.Enabled = true;
                    btnChooseDir.Enabled = true;
                    btnOpen.Enabled = true;
                    btnSelectAll.Enabled = true;
                    btnSelectReverse.Enabled = true;
                    btnImport.Enabled = true;
                    btnExport.Enabled = true;
                    comboRootRule.Enabled = true;
                    comboFileRule.Enabled = true;
                    btnDownload.Enabled = true;
                    paused = true;
                    btnDownload.Text = "开始下载";
                    lblDownloadingFileName.Text = "任务结束";
                    this.Text = ApplicationTitle;
                    foreach (Chapter chapter in dutil.Chapters)
                    {
                        if (chapter.Tag != null)
                        {
                            TreeNode node = (TreeNode)chapter.Tag;
                            node.Text = chapter.ToString();
                        }
                    }
                }
                else
                {
                    btnAnalysis.Enabled = false;
                    btnChooseDir.Enabled = false;
                    btnOpen.Enabled = false;
                    btnSelectAll.Enabled = false;
                    btnSelectReverse.Enabled = false;
                    btnImport.Enabled = false;
                    btnExport.Enabled = false;
                    comboRootRule.Enabled = false;
                    comboFileRule.Enabled = false;
                    paused = false;
                    btnDownload.Text = "停止下载";
                    lblDownloadingFileName.Text = "正在开始下载...";
                    progressBar.Value = 0;
                    progressBar.Minimum = 0;
                    progressBar.Maximum = 100;
                }
            }
        }
        #endregion// 属性

        #region 下载部分实现
        private bool downloadNextPage()
        {
            bool endOfList;
            do
            {
                endOfList=!iterator.MoveNext();
                if (endOfList)
                {
                    ControlsEnable = true;
                    taskList.Clear();
                    return false;
                }
            }
            while (!downloadPage(iterator.Current));
            return true;
        }

        //当前任务下载失败，则返回false，下载下一个任务
        private bool downloadPage(Page page)
        {
            //4种情况下跳到下个任务: 
            //0.任务失败 
            //1.任务已完成（已经标记为下载完毕或者文件已存在） 
            //2.Page不存在 
            //3.引发了无法建立目录异常
            //Page不存在
            if (page == null)
            {
                return false;
            }
            //Page已经标记为Downloaded
            page.Chapter.Started = true;
            if (page.Downloaded)
            {
                return false;
            }
            //文件已存在
            if (File.Exists(page.Path))
            {
                pageDownloadCompleted(page);//标记下载完成
                return false;
            }
            //建立下载目录
            string pathWithoutFileName = page.GetPathWithoutFileName();
            if (!Directory.Exists(pathWithoutFileName))
            {
                try
                {
                    Directory.CreateDirectory(pathWithoutFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误");
                    return false;
                }
            }
            //开始异步下载文件
            try
            {
                client.DownloadFileAsync(new Uri(page.Url), page.Path + ".tmp");
            }
            catch (Exception ex)
            {
                File.Delete(page.Path + ".tmp");
                MessageBox.Show("任务停止：" + ex.Message);
                return false;
            }
            progressBar.Value = 0;
            return true; //文件正确开始下载，则downloadNextPage()函数也返回，控制权回到窗体
        }

        private void preDownloadPages()
        {
            if (taskList == null || taskList.Count <= 0) return;
            foreach (Page page in taskList)
            {
                if (page.Downloaded) continue;
                if (File.Exists(page.Path))
                {
                    page.Downloaded = true;
                }
            }
            repaintTaskTree();
        }

        #endregion

        /// <summary>
        /// 刷新任务树
        /// </summary>
        private void repaintTaskTree()
        {
            foreach (TreeNode chapterNode in taskTree.Nodes)
            {
                Chapter chapter=(Chapter)(chapterNode.Tag);
                chapterNode.Text = chapter.ToString();
                foreach (TreeNode pageNode in chapterNode.Nodes)
                {
                    pageNode.Text = pageNode.Tag.ToString();
                }
            }
        }

        #region 下载事件
        private void pageDownloadCompleted(Page page)
        {
            page.Downloaded = true;
            //更新页节点
            ((TreeNode)page.Tag).Text = page.ToString();
            //更新章节点
            Chapter chapter = page.Chapter;
            ((TreeNode)chapter.Tag).Text = chapter.ToString();
        }

        /// <summary>
        /// 当前正在下载的文件完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void client_DownloadFileCompleted(Object sender, AsyncCompletedEventArgs e)
        {
            Page downloadingPage = iterator.Current;
            //正常的下载完成，有临时文件存在
            string fileTempPath = downloadingPage.Path + ".tmp";
            if (!e.Cancelled)
            {
                File.Copy(fileTempPath, downloadingPage.Path, true);
                pageDownloadCompleted(downloadingPage);
            }
            File.Delete(fileTempPath);
            if (!paused)
                downloadNextPage();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Page downloadingPage = iterator.Current;
            Chapter downloadingChapter = downloadingPage.Chapter;
            lblDownloadingFileName.Text = "正在下载 " + downloadingChapter.Name + "第" + downloadingPage.Index+ "页：" + e.ProgressPercentage + "%";
            progressBar.Value = e.ProgressPercentage;
            int counter = 0;
            foreach (Page page in taskList)
                if (page.Downloaded) counter++;
            double progress = counter * 100.0 / taskList.Count;
            double eachFileProgress = e.ProgressPercentage * 1.0 / taskList.Count;
            progress += eachFileProgress;
            this.Text = ApplicationTitle + " " + "已下载" + progress.ToString("F") + "%";
        }
        #endregion
	}
}
