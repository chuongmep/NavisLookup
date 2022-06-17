namespace AppInfo
{
   partial class AppInfoControl
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

      #region Component Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Fully Qualified Class Name", System.Windows.Forms.HorizontalAlignment.Left);
         System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Value", System.Windows.Forms.HorizontalAlignment.Left);
         System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Properties", System.Windows.Forms.HorizontalAlignment.Left);
         System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Methods", System.Windows.Forms.HorizontalAlignment.Left);
         System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Dynamic Members", System.Windows.Forms.HorizontalAlignment.Left);
         this.splitContainer3 = new System.Windows.Forms.SplitContainer();
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.appView = new System.Windows.Forms.TreeView();
         this.splitContainer2 = new System.Windows.Forms.SplitContainer();
         this.propertiesView = new System.Windows.Forms.ListView();
         this._methodsCol = new System.Windows.Forms.ColumnHeader("(none)");
         this._methodsCol2 = new System.Windows.Forms.ColumnHeader();
         this.eventsView = new System.Windows.Forms.ListView();
         this.columnHeader1 = new System.Windows.Forms.ColumnHeader("(none)");
         this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
         this.output = new System.Windows.Forms.RichTextBox();
         this.outputContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.clearOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.detachFromEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.attachToEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
         ((System.ComponentModel.ISupportInitialize) (this.splitContainer3)).BeginInit();
         this.splitContainer3.Panel1.SuspendLayout();
         this.splitContainer3.Panel2.SuspendLayout();
         this.splitContainer3.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize) (this.splitContainer2)).BeginInit();
         this.splitContainer2.Panel1.SuspendLayout();
         this.splitContainer2.Panel2.SuspendLayout();
         this.splitContainer2.SuspendLayout();
         this.outputContextMenuStrip.SuspendLayout();
         this.contextMenuStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // splitContainer3
         // 
         this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer3.Location = new System.Drawing.Point(0, 0);
         this.splitContainer3.Name = "splitContainer3";
         this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // splitContainer3.Panel1
         // 
         this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
         // 
         // splitContainer3.Panel2
         // 
         this.splitContainer3.Panel2.Controls.Add(this.output);
         this.splitContainer3.Size = new System.Drawing.Size(780, 552);
         this.splitContainer3.SplitterDistance = 490;
         this.splitContainer3.TabIndex = 3;
         // 
         // splitContainer1
         // 
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer1.Location = new System.Drawing.Point(0, 0);
         this.splitContainer1.Name = "splitContainer1";
         // 
         // splitContainer1.Panel1
         // 
         this.splitContainer1.Panel1.Controls.Add(this.appView);
         // 
         // splitContainer1.Panel2
         // 
         this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
         this.splitContainer1.Size = new System.Drawing.Size(780, 490);
         this.splitContainer1.SplitterDistance = 225;
         this.splitContainer1.TabIndex = 1;
         // 
         // appView
         // 
         this.appView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.appView.Location = new System.Drawing.Point(0, 0);
         this.appView.Name = "appView";
         this.appView.Size = new System.Drawing.Size(225, 490);
         this.appView.TabIndex = 0;
         this.appView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.appView_BeforeCollapse);
         this.appView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.appView_BeforeExpand);
         this.appView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.appView_AfterSelect);
         // 
         // splitContainer2
         // 
         this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer2.Location = new System.Drawing.Point(0, 0);
         this.splitContainer2.Name = "splitContainer2";
         this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // splitContainer2.Panel1
         // 
         this.splitContainer2.Panel1.Controls.Add(this.propertiesView);
         // 
         // splitContainer2.Panel2
         // 
         this.splitContainer2.Panel2.Controls.Add(this.eventsView);
         this.splitContainer2.Size = new System.Drawing.Size(551, 490);
         this.splitContainer2.SplitterDistance = 394;
         this.splitContainer2.TabIndex = 1;
         // 
         // propertiesView
         // 
         this.propertiesView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this._methodsCol, this._methodsCol2});
         this.propertiesView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.propertiesView.FullRowSelect = true;
         listViewGroup1.Header = "Fully Qualified Class Name";
         listViewGroup1.Name = "_objectClassInfo";
         listViewGroup2.Header = "Value";
         listViewGroup2.Name = "_objectValue";
         listViewGroup3.Header = "Properties";
         listViewGroup3.Name = "_objectProperties";
         listViewGroup4.Header = "Methods";
         listViewGroup4.Name = "_objectMethods";
         listViewGroup5.Header = "Dynamic Members";
         listViewGroup5.Name = "_objectDynMembers";
         this.propertiesView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {listViewGroup1, listViewGroup2, listViewGroup3, listViewGroup4, listViewGroup5});
         this.propertiesView.HideSelection = false;
         this.propertiesView.Location = new System.Drawing.Point(0, 0);
         this.propertiesView.MultiSelect = false;
         this.propertiesView.Name = "propertiesView";
         this.propertiesView.Size = new System.Drawing.Size(551, 394);
         this.propertiesView.TabIndex = 0;
         this.propertiesView.UseCompatibleStateImageBehavior = false;
         this.propertiesView.View = System.Windows.Forms.View.Details;
         // 
         // _methodsCol
         // 
         this._methodsCol.Text = "Name";
         this._methodsCol.Width = 202;
         // 
         // _methodsCol2
         // 
         this._methodsCol2.Text = "Value";
         this._methodsCol2.Width = 345;
         // 
         // eventsView
         // 
         this.eventsView.CheckBoxes = true;
         this.eventsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.columnHeader1, this.columnHeader2});
         this.eventsView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.eventsView.FullRowSelect = true;
         this.eventsView.HideSelection = false;
         this.eventsView.Location = new System.Drawing.Point(0, 0);
         this.eventsView.MultiSelect = false;
         this.eventsView.Name = "eventsView";
         this.eventsView.Size = new System.Drawing.Size(551, 92);
         this.eventsView.TabIndex = 1;
         this.eventsView.UseCompatibleStateImageBehavior = false;
         this.eventsView.View = System.Windows.Forms.View.Details;
         this.eventsView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.eventsView_ItemCheck);
         this.eventsView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.eventsView_MouseClick);
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "Event";
         this.columnHeader1.Width = 202;
         // 
         // columnHeader2
         // 
         this.columnHeader2.Text = "Last Occurrence";
         this.columnHeader2.Width = 345;
         // 
         // output
         // 
         this.output.Dock = System.Windows.Forms.DockStyle.Fill;
         this.output.Location = new System.Drawing.Point(0, 0);
         this.output.Name = "output";
         this.output.ReadOnly = true;
         this.output.Size = new System.Drawing.Size(780, 58);
         this.output.TabIndex = 0;
         this.output.Text = "";
         // 
         // outputContextMenuStrip
         // 
         this.outputContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.clearOutputToolStripMenuItem, this.toolStripSeparator1, this.selectAllToolStripMenuItem, this.copyToolStripMenuItem});
         this.outputContextMenuStrip.Name = "outputContextMenuStrip";
         this.outputContextMenuStrip.Size = new System.Drawing.Size(143, 76);
         // 
         // clearOutputToolStripMenuItem
         // 
         this.clearOutputToolStripMenuItem.Name = "clearOutputToolStripMenuItem";
         this.clearOutputToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
         this.clearOutputToolStripMenuItem.Text = "Clear Output";
         this.clearOutputToolStripMenuItem.Click += new System.EventHandler(this.clearOutputToolStripMenuItem_Click);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
         // 
         // selectAllToolStripMenuItem
         // 
         this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
         this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
         this.selectAllToolStripMenuItem.Text = "Select All";
         this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
         // 
         // copyToolStripMenuItem
         // 
         this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
         this.copyToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
         this.copyToolStripMenuItem.Text = "Copy";
         this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
         // 
         // detachFromEventToolStripMenuItem
         // 
         this.detachFromEventToolStripMenuItem.Name = "detachFromEventToolStripMenuItem";
         this.detachFromEventToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
         this.detachFromEventToolStripMenuItem.Text = "Detach from Event";
         this.detachFromEventToolStripMenuItem.Click += new System.EventHandler(this.detachFromEventToolStripMenuItem_Click);
         // 
         // attachToEventToolStripMenuItem
         // 
         this.attachToEventToolStripMenuItem.Name = "attachToEventToolStripMenuItem";
         this.attachToEventToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
         this.attachToEventToolStripMenuItem.Text = "Attach to Event";
         this.attachToEventToolStripMenuItem.Click += new System.EventHandler(this.attachToEventToolStripMenuItem_Click);
         // 
         // contextMenuStrip
         // 
         this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.attachToEventToolStripMenuItem, this.detachFromEventToolStripMenuItem});
         this.contextMenuStrip.Name = "contextMenuStrip";
         this.contextMenuStrip.Size = new System.Drawing.Size(173, 48);
         // 
         // AppInfoControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.AutoSize = true;
         this.Controls.Add(this.splitContainer3);
         this.MinimumSize = new System.Drawing.Size(585, 280);
         this.Name = "AppInfoControl";
         this.Size = new System.Drawing.Size(780, 552);
         this.Load += new System.EventHandler(this.AppInfoControl_Load);
         this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AppInfoControl_KeyDown);
         this.splitContainer3.Panel1.ResumeLayout(false);
         this.splitContainer3.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize) (this.splitContainer3)).EndInit();
         this.splitContainer3.ResumeLayout(false);
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         this.splitContainer2.Panel1.ResumeLayout(false);
         this.splitContainer2.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize) (this.splitContainer2)).EndInit();
         this.splitContainer2.ResumeLayout(false);
         this.outputContextMenuStrip.ResumeLayout(false);
         this.contextMenuStrip.ResumeLayout(false);
         this.ResumeLayout(false);
      }

      #endregion

      private System.Windows.Forms.SplitContainer splitContainer3;
      private System.Windows.Forms.SplitContainer splitContainer1;
      private System.Windows.Forms.TreeView appView;
      private System.Windows.Forms.SplitContainer splitContainer2;
      private System.Windows.Forms.ListView propertiesView;
      private System.Windows.Forms.ColumnHeader _methodsCol;
      private System.Windows.Forms.ColumnHeader _methodsCol2;
      private System.Windows.Forms.ListView eventsView;
      private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.ColumnHeader columnHeader2;
      private System.Windows.Forms.RichTextBox output;
      private System.Windows.Forms.ContextMenuStrip outputContextMenuStrip;
      private System.Windows.Forms.ToolStripMenuItem clearOutputToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem detachFromEventToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem attachToEventToolStripMenuItem;
      private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
   }
}
