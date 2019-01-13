namespace KaraokeQueuer
{
	partial class QueuerForm
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
			this.lblWelcome = new System.Windows.Forms.Label();
			this.tabSingingClub = new System.Windows.Forms.TabControl();
			this.tabRound1 = new System.Windows.Forms.TabPage();
			this.queueControl1 = new KaraokeQueuer.QueueControl();
			this.tabRound2 = new System.Windows.Forms.TabPage();
			this.queueControl2 = new KaraokeQueuer.QueueControl();
			this.tabRound3 = new System.Windows.Forms.TabPage();
			this.queueControl3 = new KaraokeQueuer.QueueControl();
			this.tabRound4 = new System.Windows.Forms.TabPage();
			this.queueControl4 = new KaraokeQueuer.QueueControl();
			this.tabRound5 = new System.Windows.Forms.TabPage();
			this.queueControl5 = new KaraokeQueuer.QueueControl();
			this.tabRound6 = new System.Windows.Forms.TabPage();
			this.queueControl6 = new KaraokeQueuer.QueueControl();
			this.tabSingers = new System.Windows.Forms.TabPage();
			this.singersControl1 = new KaraokeQueuer.SingersControl();
			this.tabEvents = new System.Windows.Forms.TabPage();
			this.eventsControl1 = new KaraokeQueuer.EventsControl();
			this.tabVenues = new System.Windows.Forms.TabPage();
			this.venuesControl1 = new KaraokeQueuer.VenuesControl();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnNotHere = new System.Windows.Forms.Button();
			this.lblRound = new System.Windows.Forms.Label();
			this.btnRun = new System.Windows.Forms.Button();
			this.txtInvisible = new System.Windows.Forms.TextBox();
			this.txtOnDeckRound = new System.Windows.Forms.TextBox();
			this.txtNowSingingRound = new System.Windows.Forms.TextBox();
			this.btnCompleted = new System.Windows.Forms.Button();
			this.txtOnDeck = new System.Windows.Forms.TextBox();
			this.txtNowSinging = new System.Windows.Forms.TextBox();
			this.txtStaged = new System.Windows.Forms.TextBox();
			this.lblStage = new System.Windows.Forms.Label();
			this.lblOnDeck = new System.Windows.Forms.Label();
			this.lblNowSinging = new System.Windows.Forms.Label();
			this.lblEvent = new System.Windows.Forms.Label();
			this.cmbEvent = new System.Windows.Forms.ComboBox();
			this.btnHistory = new System.Windows.Forms.Button();
			this.tabSingingClub.SuspendLayout();
			this.tabRound1.SuspendLayout();
			this.tabRound2.SuspendLayout();
			this.tabRound3.SuspendLayout();
			this.tabRound4.SuspendLayout();
			this.tabRound5.SuspendLayout();
			this.tabRound6.SuspendLayout();
			this.tabSingers.SuspendLayout();
			this.tabEvents.SuspendLayout();
			this.tabVenues.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblWelcome
			// 
			this.lblWelcome.AutoSize = true;
			this.lblWelcome.Font = new System.Drawing.Font("Goudy Stout", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWelcome.ForeColor = System.Drawing.Color.Red;
			this.lblWelcome.Location = new System.Drawing.Point(1, 0);
			this.lblWelcome.Name = "lblWelcome";
			this.lblWelcome.Size = new System.Drawing.Size(1047, 32);
			this.lblWelcome.TabIndex = 3;
			this.lblWelcome.Text = "W e l c o m e   T o   T h e   S i n g i n g   C l u b";
			// 
			// tabSingingClub
			// 
			this.tabSingingClub.Controls.Add(this.tabRound1);
			this.tabSingingClub.Controls.Add(this.tabRound2);
			this.tabSingingClub.Controls.Add(this.tabRound3);
			this.tabSingingClub.Controls.Add(this.tabRound4);
			this.tabSingingClub.Controls.Add(this.tabRound5);
			this.tabSingingClub.Controls.Add(this.tabRound6);
			this.tabSingingClub.Controls.Add(this.tabSingers);
			this.tabSingingClub.Controls.Add(this.tabEvents);
			this.tabSingingClub.Controls.Add(this.tabVenues);
			this.tabSingingClub.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabSingingClub.Location = new System.Drawing.Point(0, 0);
			this.tabSingingClub.Name = "tabSingingClub";
			this.tabSingingClub.SelectedIndex = 0;
			this.tabSingingClub.Size = new System.Drawing.Size(1048, 320);
			this.tabSingingClub.TabIndex = 4;
			// 
			// tabRound1
			// 
			this.tabRound1.Controls.Add(this.queueControl1);
			this.tabRound1.Location = new System.Drawing.Point(4, 22);
			this.tabRound1.Name = "tabRound1";
			this.tabRound1.Padding = new System.Windows.Forms.Padding(3);
			this.tabRound1.Size = new System.Drawing.Size(1040, 294);
			this.tabRound1.TabIndex = 4;
			this.tabRound1.Text = "Round 1";
			this.tabRound1.UseVisualStyleBackColor = true;
			// 
			// queueControl1
			// 
			this.queueControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.queueControl1.Location = new System.Drawing.Point(3, 3);
			this.queueControl1.Name = "queueControl1";
			this.queueControl1.Size = new System.Drawing.Size(1034, 288);
			this.queueControl1.TabIndex = 0;
			// 
			// tabRound2
			// 
			this.tabRound2.Controls.Add(this.queueControl2);
			this.tabRound2.Location = new System.Drawing.Point(4, 22);
			this.tabRound2.Name = "tabRound2";
			this.tabRound2.Padding = new System.Windows.Forms.Padding(3);
			this.tabRound2.Size = new System.Drawing.Size(1040, 294);
			this.tabRound2.TabIndex = 5;
			this.tabRound2.Text = "Round 2";
			this.tabRound2.UseVisualStyleBackColor = true;
			// 
			// queueControl2
			// 
			this.queueControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.queueControl2.Location = new System.Drawing.Point(3, 3);
			this.queueControl2.Name = "queueControl2";
			this.queueControl2.Size = new System.Drawing.Size(1034, 288);
			this.queueControl2.TabIndex = 0;
			// 
			// tabRound3
			// 
			this.tabRound3.Controls.Add(this.queueControl3);
			this.tabRound3.Location = new System.Drawing.Point(4, 22);
			this.tabRound3.Name = "tabRound3";
			this.tabRound3.Padding = new System.Windows.Forms.Padding(3);
			this.tabRound3.Size = new System.Drawing.Size(1040, 294);
			this.tabRound3.TabIndex = 6;
			this.tabRound3.Text = "Round 3";
			this.tabRound3.UseVisualStyleBackColor = true;
			// 
			// queueControl3
			// 
			this.queueControl3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.queueControl3.Location = new System.Drawing.Point(3, 3);
			this.queueControl3.Name = "queueControl3";
			this.queueControl3.Size = new System.Drawing.Size(1034, 288);
			this.queueControl3.TabIndex = 0;
			// 
			// tabRound4
			// 
			this.tabRound4.Controls.Add(this.queueControl4);
			this.tabRound4.Location = new System.Drawing.Point(4, 22);
			this.tabRound4.Name = "tabRound4";
			this.tabRound4.Padding = new System.Windows.Forms.Padding(3);
			this.tabRound4.Size = new System.Drawing.Size(1040, 294);
			this.tabRound4.TabIndex = 7;
			this.tabRound4.Text = "Round 4";
			this.tabRound4.UseVisualStyleBackColor = true;
			// 
			// queueControl4
			// 
			this.queueControl4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.queueControl4.Location = new System.Drawing.Point(3, 3);
			this.queueControl4.Name = "queueControl4";
			this.queueControl4.Size = new System.Drawing.Size(1034, 288);
			this.queueControl4.TabIndex = 0;
			// 
			// tabRound5
			// 
			this.tabRound5.Controls.Add(this.queueControl5);
			this.tabRound5.Location = new System.Drawing.Point(4, 22);
			this.tabRound5.Name = "tabRound5";
			this.tabRound5.Padding = new System.Windows.Forms.Padding(3);
			this.tabRound5.Size = new System.Drawing.Size(1040, 294);
			this.tabRound5.TabIndex = 8;
			this.tabRound5.Text = "Round 5";
			this.tabRound5.UseVisualStyleBackColor = true;
			// 
			// queueControl5
			// 
			this.queueControl5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.queueControl5.Location = new System.Drawing.Point(3, 3);
			this.queueControl5.Name = "queueControl5";
			this.queueControl5.Size = new System.Drawing.Size(1034, 288);
			this.queueControl5.TabIndex = 0;
			// 
			// tabRound6
			// 
			this.tabRound6.Controls.Add(this.queueControl6);
			this.tabRound6.Location = new System.Drawing.Point(4, 22);
			this.tabRound6.Name = "tabRound6";
			this.tabRound6.Padding = new System.Windows.Forms.Padding(3);
			this.tabRound6.Size = new System.Drawing.Size(1040, 294);
			this.tabRound6.TabIndex = 0;
			this.tabRound6.Text = "Round 6";
			this.tabRound6.UseVisualStyleBackColor = true;
			// 
			// queueControl6
			// 
			this.queueControl6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.queueControl6.Location = new System.Drawing.Point(3, 3);
			this.queueControl6.Name = "queueControl6";
			this.queueControl6.Size = new System.Drawing.Size(1034, 288);
			this.queueControl6.TabIndex = 0;
			// 
			// tabSingers
			// 
			this.tabSingers.Controls.Add(this.singersControl1);
			this.tabSingers.Location = new System.Drawing.Point(4, 22);
			this.tabSingers.Name = "tabSingers";
			this.tabSingers.Padding = new System.Windows.Forms.Padding(3);
			this.tabSingers.Size = new System.Drawing.Size(1040, 294);
			this.tabSingers.TabIndex = 1;
			this.tabSingers.Text = "Singers";
			this.tabSingers.UseVisualStyleBackColor = true;
			// 
			// singersControl1
			// 
			this.singersControl1.AutoSize = true;
			this.singersControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.singersControl1.Location = new System.Drawing.Point(3, 3);
			this.singersControl1.Name = "singersControl1";
			this.singersControl1.Size = new System.Drawing.Size(1034, 288);
			this.singersControl1.TabIndex = 0;
			// 
			// tabEvents
			// 
			this.tabEvents.Controls.Add(this.eventsControl1);
			this.tabEvents.Location = new System.Drawing.Point(4, 22);
			this.tabEvents.Name = "tabEvents";
			this.tabEvents.Padding = new System.Windows.Forms.Padding(3);
			this.tabEvents.Size = new System.Drawing.Size(1040, 294);
			this.tabEvents.TabIndex = 2;
			this.tabEvents.Text = "Events";
			this.tabEvents.UseVisualStyleBackColor = true;
			// 
			// eventsControl1
			// 
			this.eventsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.eventsControl1.Location = new System.Drawing.Point(3, 3);
			this.eventsControl1.Name = "eventsControl1";
			this.eventsControl1.Size = new System.Drawing.Size(1034, 288);
			this.eventsControl1.TabIndex = 0;
			// 
			// tabVenues
			// 
			this.tabVenues.Controls.Add(this.venuesControl1);
			this.tabVenues.Location = new System.Drawing.Point(4, 22);
			this.tabVenues.Name = "tabVenues";
			this.tabVenues.Padding = new System.Windows.Forms.Padding(3);
			this.tabVenues.Size = new System.Drawing.Size(1040, 294);
			this.tabVenues.TabIndex = 3;
			this.tabVenues.Text = "Venues";
			this.tabVenues.UseVisualStyleBackColor = true;
			// 
			// venuesControl1
			// 
			this.venuesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.venuesControl1.Location = new System.Drawing.Point(3, 3);
			this.venuesControl1.Name = "venuesControl1";
			this.venuesControl1.Size = new System.Drawing.Size(1034, 288);
			this.venuesControl1.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btnHistory);
			this.splitContainer1.Panel1.Controls.Add(this.btnNotHere);
			this.splitContainer1.Panel1.Controls.Add(this.lblRound);
			this.splitContainer1.Panel1.Controls.Add(this.btnRun);
			this.splitContainer1.Panel1.Controls.Add(this.txtInvisible);
			this.splitContainer1.Panel1.Controls.Add(this.txtOnDeckRound);
			this.splitContainer1.Panel1.Controls.Add(this.txtNowSingingRound);
			this.splitContainer1.Panel1.Controls.Add(this.btnCompleted);
			this.splitContainer1.Panel1.Controls.Add(this.txtOnDeck);
			this.splitContainer1.Panel1.Controls.Add(this.txtNowSinging);
			this.splitContainer1.Panel1.Controls.Add(this.txtStaged);
			this.splitContainer1.Panel1.Controls.Add(this.lblStage);
			this.splitContainer1.Panel1.Controls.Add(this.lblOnDeck);
			this.splitContainer1.Panel1.Controls.Add(this.lblNowSinging);
			this.splitContainer1.Panel1.Controls.Add(this.lblEvent);
			this.splitContainer1.Panel1.Controls.Add(this.cmbEvent);
			this.splitContainer1.Panel1.Controls.Add(this.lblWelcome);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabSingingClub);
			this.splitContainer1.Size = new System.Drawing.Size(1048, 413);
			this.splitContainer1.SplitterDistance = 89;
			this.splitContainer1.TabIndex = 5;
			// 
			// btnNotHere
			// 
			this.btnNotHere.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnNotHere.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNotHere.Location = new System.Drawing.Point(658, 32);
			this.btnNotHere.Name = "btnNotHere";
			this.btnNotHere.Size = new System.Drawing.Size(60, 23);
			this.btnNotHere.TabIndex = 23;
			this.btnNotHere.Text = "Not Here";
			this.btnNotHere.UseVisualStyleBackColor = false;
			this.btnNotHere.Click += new System.EventHandler(this.btnNotHere_Click);
			// 
			// lblRound
			// 
			this.lblRound.AutoSize = true;
			this.lblRound.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRound.Location = new System.Drawing.Point(424, 35);
			this.lblRound.Name = "lblRound";
			this.lblRound.Size = new System.Drawing.Size(49, 16);
			this.lblRound.TabIndex = 22;
			this.lblRound.Text = "Round:";
			// 
			// btnRun
			// 
			this.btnRun.Location = new System.Drawing.Point(527, 32);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(44, 23);
			this.btnRun.TabIndex = 21;
			this.btnRun.Text = "Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// txtInvisible
			// 
			this.txtInvisible.Location = new System.Drawing.Point(5, 65);
			this.txtInvisible.Name = "txtInvisible";
			this.txtInvisible.Size = new System.Drawing.Size(29, 20);
			this.txtInvisible.TabIndex = 20;
			this.txtInvisible.Visible = false;
			// 
			// txtOnDeckRound
			// 
			this.txtOnDeckRound.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOnDeckRound.Location = new System.Drawing.Point(483, 64);
			this.txtOnDeckRound.Name = "txtOnDeckRound";
			this.txtOnDeckRound.Size = new System.Drawing.Size(38, 22);
			this.txtOnDeckRound.TabIndex = 19;
			// 
			// txtNowSingingRound
			// 
			this.txtNowSingingRound.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNowSingingRound.Location = new System.Drawing.Point(483, 33);
			this.txtNowSingingRound.Name = "txtNowSingingRound";
			this.txtNowSingingRound.Size = new System.Drawing.Size(38, 22);
			this.txtNowSingingRound.TabIndex = 18;
			// 
			// btnCompleted
			// 
			this.btnCompleted.Location = new System.Drawing.Point(577, 32);
			this.btnCompleted.Name = "btnCompleted";
			this.btnCompleted.Size = new System.Drawing.Size(60, 23);
			this.btnCompleted.TabIndex = 16;
			this.btnCompleted.Text = "Finished";
			this.btnCompleted.UseVisualStyleBackColor = true;
			this.btnCompleted.Click += new System.EventHandler(this.btnCompleted_Click);
			// 
			// txtOnDeck
			// 
			this.txtOnDeck.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOnDeck.Location = new System.Drawing.Point(111, 65);
			this.txtOnDeck.Name = "txtOnDeck";
			this.txtOnDeck.Size = new System.Drawing.Size(294, 22);
			this.txtOnDeck.TabIndex = 13;
			// 
			// txtNowSinging
			// 
			this.txtNowSinging.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNowSinging.Location = new System.Drawing.Point(111, 34);
			this.txtNowSinging.Name = "txtNowSinging";
			this.txtNowSinging.Size = new System.Drawing.Size(294, 22);
			this.txtNowSinging.TabIndex = 12;
			// 
			// txtStaged
			// 
			this.txtStaged.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtStaged.Location = new System.Drawing.Point(790, 62);
			this.txtStaged.Name = "txtStaged";
			this.txtStaged.Size = new System.Drawing.Size(246, 22);
			this.txtStaged.TabIndex = 11;
			// 
			// lblStage
			// 
			this.lblStage.AutoSize = true;
			this.lblStage.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStage.Location = new System.Drawing.Point(727, 65);
			this.lblStage.Name = "lblStage";
			this.lblStage.Size = new System.Drawing.Size(57, 16);
			this.lblStage.TabIndex = 10;
			this.lblStage.Text = "Staged:";
			// 
			// lblOnDeck
			// 
			this.lblOnDeck.AutoSize = true;
			this.lblOnDeck.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblOnDeck.Location = new System.Drawing.Point(40, 68);
			this.lblOnDeck.Name = "lblOnDeck";
			this.lblOnDeck.Size = new System.Drawing.Size(65, 16);
			this.lblOnDeck.TabIndex = 9;
			this.lblOnDeck.Text = "On Deck:";
			// 
			// lblNowSinging
			// 
			this.lblNowSinging.AutoSize = true;
			this.lblNowSinging.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNowSinging.Location = new System.Drawing.Point(12, 37);
			this.lblNowSinging.Name = "lblNowSinging";
			this.lblNowSinging.Size = new System.Drawing.Size(93, 16);
			this.lblNowSinging.TabIndex = 8;
			this.lblNowSinging.Text = "Now Singing:";
			// 
			// lblEvent
			// 
			this.lblEvent.AutoSize = true;
			this.lblEvent.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEvent.ForeColor = System.Drawing.Color.Blue;
			this.lblEvent.Location = new System.Drawing.Point(733, 37);
			this.lblEvent.Name = "lblEvent";
			this.lblEvent.Size = new System.Drawing.Size(51, 18);
			this.lblEvent.TabIndex = 5;
			this.lblEvent.Text = "Event:";
			// 
			// cmbEvent
			// 
			this.cmbEvent.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbEvent.FormattingEnabled = true;
			this.cmbEvent.Location = new System.Drawing.Point(790, 34);
			this.cmbEvent.Name = "cmbEvent";
			this.cmbEvent.Size = new System.Drawing.Size(185, 24);
			this.cmbEvent.TabIndex = 4;
			this.cmbEvent.SelectedIndexChanged += new System.EventHandler(this.cmbEvent_SelectedIndexChanged);
			// 
			// btnHistory
			// 
			this.btnHistory.Location = new System.Drawing.Point(981, 35);
			this.btnHistory.Name = "btnHistory";
			this.btnHistory.Size = new System.Drawing.Size(55, 23);
			this.btnHistory.TabIndex = 24;
			this.btnHistory.Text = "History";
			this.btnHistory.UseVisualStyleBackColor = true;
			this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
			// 
			// QueuerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1048, 413);
			this.Controls.Add(this.splitContainer1);
			this.MinimumSize = new System.Drawing.Size(1064, 451);
			this.Name = "QueuerForm";
			this.Text = "Queue Manager";
			this.Load += new System.EventHandler(this.QueuerForm_Load);
			this.tabSingingClub.ResumeLayout(false);
			this.tabRound1.ResumeLayout(false);
			this.tabRound2.ResumeLayout(false);
			this.tabRound3.ResumeLayout(false);
			this.tabRound4.ResumeLayout(false);
			this.tabRound5.ResumeLayout(false);
			this.tabRound6.ResumeLayout(false);
			this.tabSingers.ResumeLayout(false);
			this.tabSingers.PerformLayout();
			this.tabEvents.ResumeLayout(false);
			this.tabVenues.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblWelcome;
		private System.Windows.Forms.TabControl tabSingingClub;
		private System.Windows.Forms.TabPage tabRound6;
		private System.Windows.Forms.TabPage tabSingers;
		private System.Windows.Forms.TabPage tabEvents;
		private System.Windows.Forms.TabPage tabVenues;
		private System.Windows.Forms.TabPage tabRound1;
		private System.Windows.Forms.TabPage tabRound2;
		private System.Windows.Forms.TabPage tabRound3;
		private System.Windows.Forms.TabPage tabRound4;
		private System.Windows.Forms.TabPage tabRound5;
		private SingersControl singersControl1;
		private EventsControl eventsControl1;
		private VenuesControl venuesControl1;
		private QueueControl queueControl1;
		private QueueControl queueControl2;
		private QueueControl queueControl3;
		private QueueControl queueControl4;
		private QueueControl queueControl5;
		private QueueControl queueControl6;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label lblEvent;
		private System.Windows.Forms.ComboBox cmbEvent;
		private System.Windows.Forms.Label lblStage;
		private System.Windows.Forms.Label lblOnDeck;
		private System.Windows.Forms.Label lblNowSinging;
		private System.Windows.Forms.TextBox txtOnDeck;
		private System.Windows.Forms.TextBox txtNowSinging;
		private System.Windows.Forms.TextBox txtStaged;
		private System.Windows.Forms.Button btnCompleted;
		private System.Windows.Forms.TextBox txtOnDeckRound;
		private System.Windows.Forms.TextBox txtNowSingingRound;
		private System.Windows.Forms.TextBox txtInvisible;
		private System.Windows.Forms.Label lblRound;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.Button btnNotHere;
		private System.Windows.Forms.Button btnHistory;
	}
}

