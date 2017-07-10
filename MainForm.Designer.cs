namespace AllegroFinder
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uxAreNewBtn = new System.Windows.Forms.Button();
            this.uxAddBtn = new System.Windows.Forms.Button();
            this.uxSearchNowBtn = new System.Windows.Forms.Button();
            this.uxTitleTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uxErrorLabel = new System.Windows.Forms.Label();
            this.uxAuctionsView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuctionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyNow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeLeft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Offers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuctionLink = new System.Windows.Forms.DataGridViewLinkColumn();
            this.searchTimer = new System.Windows.Forms.Timer(this.components);
            this.uxNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.uxGamesList = new System.Windows.Forms.CheckedListBox();
            this.uxShortlistContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxSearchSelectedBtn = new System.Windows.Forms.Button();
            this.uxWaitForNewBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.uxLoader = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxAuctionsView)).BeginInit();
            this.uxShortlistContextMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxLoader)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uxAreNewBtn);
            this.groupBox1.Controls.Add(this.uxAddBtn);
            this.groupBox1.Controls.Add(this.uxSearchNowBtn);
            this.groupBox1.Controls.Add(this.uxTitleTB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 140);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wyszukiwanie";
            // 
            // uxAreNewBtn
            // 
            this.uxAreNewBtn.Location = new System.Drawing.Point(9, 105);
            this.uxAreNewBtn.Name = "uxAreNewBtn";
            this.uxAreNewBtn.Size = new System.Drawing.Size(221, 23);
            this.uxAreNewBtn.TabIndex = 3;
            this.uxAreNewBtn.Text = "Są nowe aukcje - POKAŻ";
            this.uxAreNewBtn.UseVisualStyleBackColor = true;
            this.uxAreNewBtn.Visible = false;
            this.uxAreNewBtn.Click += new System.EventHandler(this.uxAreNewBtn_Click);
            // 
            // uxAddBtn
            // 
            this.uxAddBtn.Location = new System.Drawing.Point(9, 65);
            this.uxAddBtn.Name = "uxAddBtn";
            this.uxAddBtn.Size = new System.Drawing.Size(109, 23);
            this.uxAddBtn.TabIndex = 2;
            this.uxAddBtn.Text = "Dodaj do shortlisty";
            this.uxAddBtn.UseVisualStyleBackColor = true;
            this.uxAddBtn.Click += new System.EventHandler(this.uxAddBtn_Click);
            // 
            // uxSearchNowBtn
            // 
            this.uxSearchNowBtn.Location = new System.Drawing.Point(121, 65);
            this.uxSearchNowBtn.Name = "uxSearchNowBtn";
            this.uxSearchNowBtn.Size = new System.Drawing.Size(109, 23);
            this.uxSearchNowBtn.TabIndex = 1;
            this.uxSearchNowBtn.Text = "Szukaj teraz";
            this.uxSearchNowBtn.UseVisualStyleBackColor = true;
            this.uxSearchNowBtn.Click += new System.EventHandler(this.uxSearchNowBtn_Click);
            // 
            // uxTitleTB
            // 
            this.uxTitleTB.Location = new System.Drawing.Point(9, 39);
            this.uxTitleTB.Name = "uxTitleTB";
            this.uxTitleTB.Size = new System.Drawing.Size(221, 20);
            this.uxTitleTB.TabIndex = 0;
            this.uxTitleTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uxTitleTB_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Poszukiwana aukcja";
            // 
            // uxErrorLabel
            // 
            this.uxErrorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uxErrorLabel.Location = new System.Drawing.Point(12, 434);
            this.uxErrorLabel.Name = "uxErrorLabel";
            this.uxErrorLabel.Size = new System.Drawing.Size(781, 23);
            this.uxErrorLabel.TabIndex = 1;
            this.uxErrorLabel.Text = "errorLabel";
            this.uxErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uxAuctionsView
            // 
            this.uxAuctionsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxAuctionsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uxAuctionsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.AuctionName,
            this.CurrentPrice,
            this.BuyNow,
            this.TimeLeft,
            this.Offers,
            this.AuctionLink});
            this.uxAuctionsView.Location = new System.Drawing.Point(15, 158);
            this.uxAuctionsView.MultiSelect = false;
            this.uxAuctionsView.Name = "uxAuctionsView";
            this.uxAuctionsView.ReadOnly = true;
            this.uxAuctionsView.RowHeadersVisible = false;
            this.uxAuctionsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.uxAuctionsView.Size = new System.Drawing.Size(800, 270);
            this.uxAuctionsView.TabIndex = 2;
            this.uxAuctionsView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.uxAuctionsView_CellFormatting);
            this.uxAuctionsView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.uxAuctionsView_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // AuctionName
            // 
            this.AuctionName.HeaderText = "Nazwa Aukcji";
            this.AuctionName.Name = "AuctionName";
            this.AuctionName.ReadOnly = true;
            this.AuctionName.Width = 350;
            // 
            // CurrentPrice
            // 
            this.CurrentPrice.HeaderText = "Obecna cena";
            this.CurrentPrice.Name = "CurrentPrice";
            this.CurrentPrice.ReadOnly = true;
            this.CurrentPrice.Width = 95;
            // 
            // BuyNow
            // 
            this.BuyNow.HeaderText = "Kup teraz";
            this.BuyNow.Name = "BuyNow";
            this.BuyNow.ReadOnly = true;
            this.BuyNow.Width = 80;
            // 
            // TimeLeft
            // 
            this.TimeLeft.HeaderText = "Do końca";
            this.TimeLeft.Name = "TimeLeft";
            this.TimeLeft.ReadOnly = true;
            this.TimeLeft.Width = 80;
            // 
            // Offers
            // 
            this.Offers.HeaderText = "Oferty";
            this.Offers.Name = "Offers";
            this.Offers.ReadOnly = true;
            this.Offers.Width = 70;
            // 
            // AuctionLink
            // 
            this.AuctionLink.HeaderText = "Link";
            this.AuctionLink.Name = "AuctionLink";
            this.AuctionLink.ReadOnly = true;
            // 
            // searchTimer
            // 
            this.searchTimer.Interval = 60000;
            this.searchTimer.Tick += new System.EventHandler(this.searchTimer_Tick);
            // 
            // uxNotifyIcon
            // 
            this.uxNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("uxNotifyIcon.Icon")));
            this.uxNotifyIcon.Text = "Allegro finder";
            this.uxNotifyIcon.Visible = true;
            this.uxNotifyIcon.BalloonTipClicked += new System.EventHandler(this.uxNotifyIcon_BalloonTipClicked);
            this.uxNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.uxNotifyIcon_MouseDoubleClick);
            // 
            // uxGamesList
            // 
            this.uxGamesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxGamesList.CheckOnClick = true;
            this.uxGamesList.ContextMenuStrip = this.uxShortlistContextMenu;
            this.uxGamesList.FormattingEnabled = true;
            this.uxGamesList.Location = new System.Drawing.Point(6, 19);
            this.uxGamesList.Name = "uxGamesList";
            this.uxGamesList.Size = new System.Drawing.Size(396, 109);
            this.uxGamesList.TabIndex = 5;
            this.uxGamesList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.uxGamesList_ItemCheck);
            this.uxGamesList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.uxGamesList_MouseDown);
            this.uxGamesList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uxGamesList_KeyDown);
            // 
            // uxShortlistContextMenu
            // 
            this.uxShortlistContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.uxShortlistContextMenu.Name = "uxShortlistContextMenu";
            this.uxShortlistContextMenu.Size = new System.Drawing.Size(134, 48);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.searchToolStripMenuItem.Text = "Szukaj";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.deleteToolStripMenuItem.Text = "Usuń z listy";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // uxSearchSelectedBtn
            // 
            this.uxSearchSelectedBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxSearchSelectedBtn.Location = new System.Drawing.Point(408, 19);
            this.uxSearchSelectedBtn.Name = "uxSearchSelectedBtn";
            this.uxSearchSelectedBtn.Size = new System.Drawing.Size(140, 52);
            this.uxSearchSelectedBtn.TabIndex = 3;
            this.uxSearchSelectedBtn.Text = "Szukaj zaznaczonych";
            this.uxSearchSelectedBtn.UseVisualStyleBackColor = true;
            this.uxSearchSelectedBtn.Click += new System.EventHandler(this.uxSearchSelectedBtn_Click);
            // 
            // uxWaitForNewBtn
            // 
            this.uxWaitForNewBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxWaitForNewBtn.Location = new System.Drawing.Point(408, 76);
            this.uxWaitForNewBtn.Name = "uxWaitForNewBtn";
            this.uxWaitForNewBtn.Size = new System.Drawing.Size(140, 52);
            this.uxWaitForNewBtn.TabIndex = 4;
            this.uxWaitForNewBtn.Text = "Oczekuj na nowe wśród zaznaczonych";
            this.uxWaitForNewBtn.UseVisualStyleBackColor = true;
            this.uxWaitForNewBtn.Click += new System.EventHandler(this.uxWaitForNewBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.uxGamesList);
            this.groupBox2.Controls.Add(this.uxWaitForNewBtn);
            this.groupBox2.Controls.Add(this.uxSearchSelectedBtn);
            this.groupBox2.Location = new System.Drawing.Point(261, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(554, 140);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shortlista";
            // 
            // uxLoader
            // 
            this.uxLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uxLoader.Image = global::AllegroFinder.Properties.Resources.loader;
            this.uxLoader.InitialImage = null;
            this.uxLoader.Location = new System.Drawing.Point(799, 438);
            this.uxLoader.Name = "uxLoader";
            this.uxLoader.Size = new System.Drawing.Size(16, 16);
            this.uxLoader.TabIndex = 7;
            this.uxLoader.TabStop = false;
            this.uxLoader.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 463);
            this.Controls.Add(this.uxLoader);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.uxAuctionsView);
            this.Controls.Add(this.uxErrorLabel);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "AllegroFinder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxAuctionsView)).EndInit();
            this.uxShortlistContextMenu.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uxLoader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label uxErrorLabel;
        private System.Windows.Forms.TextBox uxTitleTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView uxAuctionsView;
        private System.Windows.Forms.Timer searchTimer;
        private System.Windows.Forms.Button uxSearchNowBtn;
        private System.Windows.Forms.NotifyIcon uxNotifyIcon;
        private System.Windows.Forms.Button uxAddBtn;
        private System.Windows.Forms.CheckedListBox uxGamesList;
        private System.Windows.Forms.Button uxSearchSelectedBtn;
        private System.Windows.Forms.Button uxWaitForNewBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button uxAreNewBtn;
        private System.Windows.Forms.PictureBox uxLoader;
        private System.Windows.Forms.ContextMenuStrip uxShortlistContextMenu;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuctionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyNow;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeLeft;
        private System.Windows.Forms.DataGridViewTextBoxColumn Offers;
        private System.Windows.Forms.DataGridViewLinkColumn AuctionLink;
    }
}

