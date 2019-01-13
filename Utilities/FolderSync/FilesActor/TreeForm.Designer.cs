namespace FilesActor
{
    partial class TreeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.impactView = new System.Windows.Forms.TreeView();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.lblFind = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.panelControls = new System.Windows.Forms.Panel();
            this.contextMenuStripExp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExpandDiff = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCollapse = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControls.SuspendLayout();
            this.contextMenuStripExp.SuspendLayout();
            this.SuspendLayout();
            // 
            // impactView
            // 
            this.impactView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.impactView.Location = new System.Drawing.Point(12, 79);
            this.impactView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.impactView.Name = "impactView";
            this.impactView.Size = new System.Drawing.Size(539, 374);
            this.impactView.TabIndex = 0;
            this.impactView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.impactView_AfterSelect);
            this.impactView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.impactView_NodeMouseClick);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(3, 3);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(533, 22);
            this.txtFolder.TabIndex = 1;
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(46, 31);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(359, 22);
            this.txtFind.TabIndex = 2;
            // 
            // lblFind
            // 
            this.lblFind.AutoSize = true;
            this.lblFind.Location = new System.Drawing.Point(3, 34);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(37, 16);
            this.lblFind.TabIndex = 3;
            this.lblFind.Text = "Find:";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(411, 31);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 23);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(467, 31);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(69, 23);
            this.btnPrevious.TabIndex = 5;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.txtFolder);
            this.panelControls.Controls.Add(this.btnPrevious);
            this.panelControls.Controls.Add(this.txtFind);
            this.panelControls.Controls.Add(this.btnNext);
            this.panelControls.Controls.Add(this.lblFind);
            this.panelControls.Location = new System.Drawing.Point(12, 12);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(539, 60);
            this.panelControls.TabIndex = 6;
            // 
            // contextMenuStripExp
            // 
            this.contextMenuStripExp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExpandAll,
            this.toolStripMenuItemExpandDiff,
            this.toolStripMenuItemCollapse});
            this.contextMenuStripExp.Name = "contextMenuStripExp";
            this.contextMenuStripExp.Size = new System.Drawing.Size(153, 92);
            // 
            // toolStripMenuItemExpandAll
            // 
            this.toolStripMenuItemExpandAll.Name = "toolStripMenuItemExpandAll";
            this.toolStripMenuItemExpandAll.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemExpandAll.Text = "Expand All";
            this.toolStripMenuItemExpandAll.Click += new System.EventHandler(this.toolStripMenuItemExpandAll_Click);
            // 
            // toolStripMenuItemExpandDiff
            // 
            this.toolStripMenuItemExpandDiff.Name = "toolStripMenuItemExpandDiff";
            this.toolStripMenuItemExpandDiff.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemExpandDiff.Text = "Expand Diff";
            this.toolStripMenuItemExpandDiff.Click += new System.EventHandler(this.toolStripMenuItemExpandDiff_Click);
            // 
            // toolStripMenuItemCollapse
            // 
            this.toolStripMenuItemCollapse.Name = "toolStripMenuItemCollapse";
            this.toolStripMenuItemCollapse.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemCollapse.Text = "Collapse All";
            this.toolStripMenuItemCollapse.Click += new System.EventHandler(this.toolStripMenuItemCollapse_Click);
            // 
            // TreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 466);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.impactView);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TreeForm";
            this.Text = "TreeForm";
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.contextMenuStripExp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView impactView;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripExp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExpandAll;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExpandDiff;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCollapse;
    }
}