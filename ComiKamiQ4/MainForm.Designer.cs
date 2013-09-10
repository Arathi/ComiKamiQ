/*
 * 由SharpDevelop创建。
 * 用户： Arathi
 * 日期: 2013/4/15
 * 时间: 10:28
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace ComiKamiQ4
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			
            this.lblURL = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnAnalysis = new System.Windows.Forms.Button();
            this.lblRoot = new System.Windows.Forms.Label();
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.taskTree = new System.Windows.Forms.TreeView();
            this.btnChooseDir = new System.Windows.Forms.Button();
            this.checkedListChapters = new System.Windows.Forms.CheckedListBox();
            this.comboRootRule = new System.Windows.Forms.ComboBox();
            this.comboFileRule = new System.Windows.Forms.ComboBox();
            this.lblRulesSplit = new System.Windows.Forms.Label();
            this.lblRenameRule = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectReverse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblDownloadingFileName = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(39, 17);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(35, 12);
            this.lblURL.TabIndex = 0;
            this.lblURL.Text = "URL：";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(75, 12);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(217, 21);
            this.txtURL.TabIndex = 1;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(298, 11);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(50, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnAnalysis
            // 
            this.btnAnalysis.Location = new System.Drawing.Point(352, 11);
            this.btnAnalysis.Name = "btnAnalysis";
            this.btnAnalysis.Size = new System.Drawing.Size(50, 23);
            this.btnAnalysis.TabIndex = 3;
            this.btnAnalysis.Text = "分析";
            this.btnAnalysis.UseVisualStyleBackColor = true;
            this.btnAnalysis.Click += new System.EventHandler(this.btnAnalysis_Click);
            // 
            // lblRoot
            // 
            this.lblRoot.AutoSize = true;
            this.lblRoot.Location = new System.Drawing.Point(9, 43);
            this.lblRoot.Name = "lblRoot";
            this.lblRoot.Size = new System.Drawing.Size(65, 12);
            this.lblRoot.TabIndex = 4;
            this.lblRoot.Text = "保存目录：";
            // 
            // txtRoot
            // 
            this.txtRoot.Location = new System.Drawing.Point(75, 42);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.Size = new System.Drawing.Size(217, 21);
            this.txtRoot.TabIndex = 5;
            // 
            // taskTree
            // 
            this.taskTree.Location = new System.Drawing.Point(1, 3);
            this.taskTree.Name = "taskTree";
            this.taskTree.Size = new System.Drawing.Size(386, 132);
            this.taskTree.TabIndex = 8;
            // 
            // btnChooseDir
            // 
            this.btnChooseDir.Location = new System.Drawing.Point(298, 40);
            this.btnChooseDir.Name = "btnChooseDir";
            this.btnChooseDir.Size = new System.Drawing.Size(32, 23);
            this.btnChooseDir.TabIndex = 9;
            this.btnChooseDir.Text = "...";
            this.btnChooseDir.UseVisualStyleBackColor = true;
            this.btnChooseDir.Click += new System.EventHandler(this.btnChooseDir_Click);
            // 
            // checkedListChapters
            // 
            this.checkedListChapters.CheckOnClick = true;
            this.checkedListChapters.FormattingEnabled = true;
            this.checkedListChapters.HorizontalScrollbar = true;
            this.checkedListChapters.Location = new System.Drawing.Point(1, 3);
            this.checkedListChapters.Name = "checkedListChapters";
            this.checkedListChapters.Size = new System.Drawing.Size(386, 132);
            this.checkedListChapters.TabIndex = 10;
            // 
            // comboRootRule
            // 
            this.comboRootRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRootRule.FormattingEnabled = true;
            this.comboRootRule.Location = new System.Drawing.Point(75, 72);
            this.comboRootRule.Name = "comboRootRule";
            this.comboRootRule.Size = new System.Drawing.Size(133, 20);
            this.comboRootRule.TabIndex = 11;
            // 
            // comboFileRule
            // 
            this.comboFileRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFileRule.FormattingEnabled = true;
            this.comboFileRule.Location = new System.Drawing.Point(235, 72);
            this.comboFileRule.Name = "comboFileRule";
            this.comboFileRule.Size = new System.Drawing.Size(158, 20);
            this.comboFileRule.TabIndex = 12;
            // 
            // lblRulesSplit
            // 
            this.lblRulesSplit.AutoSize = true;
            this.lblRulesSplit.Location = new System.Drawing.Point(217, 76);
            this.lblRulesSplit.Name = "lblRulesSplit";
            this.lblRulesSplit.Size = new System.Drawing.Size(11, 12);
            this.lblRulesSplit.TabIndex = 13;
            this.lblRulesSplit.Text = "/";
            // 
            // lblRenameRule
            // 
            this.lblRenameRule.AutoSize = true;
            this.lblRenameRule.Location = new System.Drawing.Point(9, 76);
            this.lblRenameRule.Name = "lblRenameRule";
            this.lblRenameRule.Size = new System.Drawing.Size(65, 12);
            this.lblRenameRule.TabIndex = 14;
            this.lblRenameRule.Text = "命名规则：";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(336, 40);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(66, 23);
            this.btnDownload.TabIndex = 15;
            this.btnDownload.Text = "开始下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(304, 263);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(47, 23);
            this.btnSelectAll.TabIndex = 16;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectReverse
            // 
            this.btnSelectReverse.Location = new System.Drawing.Point(356, 263);
            this.btnSelectReverse.Name = "btnSelectReverse";
            this.btnSelectReverse.Size = new System.Drawing.Size(47, 23);
            this.btnSelectReverse.TabIndex = 17;
            this.btnSelectReverse.Text = "反选";
            this.btnSelectReverse.UseVisualStyleBackColor = true;
            this.btnSelectReverse.Click += new System.EventHandler(this.btnSelectReverse_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(8, 98);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(396, 163);
            this.tabControl.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkedListChapters);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(388, 137);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "章节选取";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.taskTree);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(388, 137);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "任务结构";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(9, 289);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(394, 23);
            this.progressBar.TabIndex = 19;
            // 
            // lblDownloadingFileName
            // 
            this.lblDownloadingFileName.AutoSize = true;
            this.lblDownloadingFileName.Location = new System.Drawing.Point(11, 268);
            this.lblDownloadingFileName.Name = "lblDownloadingFileName";
            this.lblDownloadingFileName.Size = new System.Drawing.Size(0, 12);
            this.lblDownloadingFileName.TabIndex = 20;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(252, 263);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(47, 23);
            this.btnImport.TabIndex = 21;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(200, 263);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(47, 23);
            this.btnExport.TabIndex = 22;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            
			//this.SuspendLayout();
			
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(411, 317);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.lblDownloadingFileName);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnSelectReverse);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.lblRenameRule);
            this.Controls.Add(this.lblRulesSplit);
            this.Controls.Add(this.comboFileRule);
            this.Controls.Add(this.comboRootRule);
            this.Controls.Add(this.btnChooseDir);
            this.Controls.Add(this.txtRoot);
            this.Controls.Add(this.lblRoot);
            this.Controls.Add(this.btnAnalysis);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.lblURL);
            this.MinimumSize = new System.Drawing.Size(427, 355);
            //this.Name = "ComiDownForm";
            this.Opacity = 0.98D;
            this.Text = "ComiKami[Q]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComiDownForm_FormClosing);
            this.Load += new System.EventHandler(this.ComiDownForm_Load);
            //this.SizeChanged += new System.EventHandler(this.ComiDownForm_SizeChanged);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            //this.ResumeLayout(false);
            //this.PerformLayout();
            
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(332, 262);
			this.Name = "MainForm";
			//this.Text = "ComiKamiQ4";
			this.ResumeLayout(false);
		}
		
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnAnalysis;
        private System.Windows.Forms.Label lblRoot;
        private System.Windows.Forms.TextBox txtRoot;
        private System.Windows.Forms.TreeView taskTree;
        private System.Windows.Forms.Button btnChooseDir;
        private System.Windows.Forms.CheckedListBox checkedListChapters;
        private System.Windows.Forms.ComboBox comboRootRule;
        private System.Windows.Forms.ComboBox comboFileRule;
        private System.Windows.Forms.Label lblRulesSplit;
        private System.Windows.Forms.Label lblRenameRule;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectReverse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblDownloadingFileName;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
	}
}
