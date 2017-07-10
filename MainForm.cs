using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllegroFinder.Data;
using AllegroFinder.Searcher;
using System.Globalization;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AllegroFinder
{
    public partial class MainForm : Form
    {
        #region private fields

        private LoginManager lm = null;
        private AuctionsContainer container = new AuctionsContainer();
        private Shortlist shortList = new Shortlist();
        
        #endregion

        #region public fields
        
        public bool IsInitialized { get; set; }

        #endregion

        #region private methods

        #region background worker methods

        private void BackgroundWorkerSearchSelected(bool clearBeforeSearch, bool showBaloonAfterSearch)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(SearchSelectedAsync);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_SearchSelectedCompleted);
            bw.RunWorkerAsync(new SearchCriteria() { ClearBeforeSearch = clearBeforeSearch, ShowBaloon = showBaloonAfterSearch });
        }

        private void BackgroundWorkerLogin()
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(Login);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_LoginCompleted);
            bgWorker.RunWorkerAsync();
        }

        private void BackgroundWorkerSearchSelected(string title)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(SearchAsync);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_SearchCompleted);
            bgWorker.RunWorkerAsync(title);
        }

        #endregion

        #region login methods

        private void Login(object sender, DoWorkEventArgs e)
        {
            e.Result = lm.Login();
        }

        private void Login()
        {
            if (lm.UnsuccessfulLoginCounter == Consts.MaxConnectTries)
            {
                uxErrorLabel.Text =
                    string.Format("Błąd komunikacji z serwerem Allegro. Nieudanych prób: {0}. Uruchom program ponownie za jakiś czas.", lm.UnsuccessfulLoginCounter);
            }
            else
            {
                EnableLoader();
                SetAllButtonsState(false);
                uxErrorLabel.Text = string.Format("Łączenie do serwera Allegro... Próba nr {0}", lm.UnsuccessfulLoginCounter + 1);

                BackgroundWorkerLogin();
            }
        }

        #endregion

        #region loader
        
        private void EnableLoader()
        {
            uxLoader.Visible = true;
        }

        private void DisableLoader()
        {
            uxLoader.Visible = false;
        }

        #endregion

        #region buttons helpers

        private void SetAllButtonsState(bool enabled)
        {
            uxSearchNowBtn.Enabled = enabled;
            uxSearchSelectedBtn.Enabled = enabled;
            uxWaitForNewBtn.Enabled = enabled;
        }

        #endregion

        #region shortlist helpers

        private void ShortlistDeleteSelected()
        {
            if (uxGamesList.SelectedIndex != -1)
            {
                string selected = uxGamesList.Items[uxGamesList.SelectedIndex] as string;
                shortList.Delete(selected);
                BindShortList();
            }
        }

        private void BindShortList()
        {
            uxGamesList.Items.Clear();
            foreach (GameTitle title in shortList.Games)
            {
                uxGamesList.Items.Add(title.Title, title.Checked);
            }
        }

        #endregion

        #region event handlers

        private void SearchAsync(object sender, DoWorkEventArgs e)
        {
            container.Clear();

            SearchCriteria criteria = new SearchCriteria() { ClearBeforeSearch = true, ShowBaloon = false };

            AuctionSearcher searcher = new AuctionSearcher(new AllegroServiceProvider(), lm.SessionKey);
            searcher.Title = e.Argument as string;
            searcher.OnlyNew = false;

            List<Auction> auctionsFound = null;
            MethodResult methodResult = new MethodResult();
            methodResult = searcher.DoSearch(out auctionsFound);

            SearchResult result = new SearchResult() { Auctions = auctionsFound, ShowBaloon = criteria.ShowBaloon, AllegroMethodResult = methodResult };
            e.Result = result;
        }

        private void SearchSelectedAsync(object sender, DoWorkEventArgs e)
        {
            SearchCriteria criteria = e.Argument as SearchCriteria;
            bool clearBeforeSearch = criteria.ClearBeforeSearch;

            if (clearBeforeSearch)
            {
                container.Clear();
            }

            List<Auction> result = new List<Auction>();

            AuctionSearcher searcher = new AuctionSearcher(new AllegroServiceProvider(), lm.SessionKey);

            MethodResult methodResult = new MethodResult();
            foreach (GameTitle title in shortList.Games)
            {
                if (title.Checked)
                {
                    searcher.Title = title.Title;
                    searcher.OnlyNew = false;
                    List<Auction> auctionsFound = new List<Auction>();
                    methodResult = searcher.DoSearch(out auctionsFound);
                    result.AddRange(auctionsFound);
                }
            }

            container.Add(result);
            SearchResult searchResult = new SearchResult() { Auctions = container.Auctions, ShowBaloon = criteria.ShowBaloon, AllegroMethodResult = methodResult };
            e.Result = searchResult;
        }

        private void searcher_OnSearchFinished(object sender, AuctionEventArgs e)
        {
            List<Auction> auctions = e.Auctions;
            foreach (Auction a in auctions)
            {
                container.Add(a);
            }

            FillView(container.Auctions);
        }

        private void bw_SearchSelectedCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SearchResult result = e.Result as SearchResult;

            if (!result.AllegroMethodResult.Success)
            {
                uxErrorLabel.Text = result.AllegroMethodResult.ErrorMessage;
                if (result.AllegroMethodResult.ErrorCode == "ERR_NO_SESSION" ||
                    result.AllegroMethodResult.ErrorCode == "ERR_SESSION_EXPIRED")
                {
                    Login();
                }
                DisableLoader();
                return;
            }

            List<Auction> auctions = result.Auctions;

            FillView(auctions);
            uxSearchSelectedBtn.Enabled = true;
            DisableLoader();
            SetAllButtonsState(true);

            if (result.ShowBaloon)
            {
                List<Auction> newAuctions = container.GetNewAuctions();
                if (newAuctions.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Auction auction in newAuctions)
                    {
                        sb.AppendLine(auction.Name);
                        sb.AppendFormat("Cena: {0}", auction.Price.ToString("0.00", CultureInfo.InvariantCulture));
                        if (auction.BuyNowPrice.HasValue)
                        {
                            sb.AppendFormat(", Kup teraz: {0}", auction.BuyNowPrice.Value.ToString("0.00", CultureInfo.InvariantCulture));
                        }
                        sb.AppendLine(string.Empty);
                        sb.AppendLine(string.Empty);
                    }

                    uxAreNewBtn.Visible = true;
                    uxNotifyIcon.ShowBalloonTip(5000, "Nowe aukcje spełniające kryteria", sb.ToString(), ToolTipIcon.Info);
                    System.Media.SystemSounds.Asterisk.Play();
                }
            }
        }

        private void bgWorker_LoginCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MethodResult result = (MethodResult)e.Result;
            bool logged = result.Success;
            if (logged)
            {
                uxErrorLabel.Text = "Połączenie OK";
            }
            else
            {
                uxErrorLabel.Text = "Połączenie z Allegro nieudane. " + result.ErrorMessage;
            }
            SetAllButtonsState(logged);
            DisableLoader();
        }

        private void container_OnAuctionAdded(object sender, AuctionEventArgs e)
        {
            if (InvokeRequired)
            {
                EventHandler<AuctionEventArgs> handler = new EventHandler<AuctionEventArgs>(container_OnAuctionAdded);
                this.Invoke(handler, new object[] { sender, e });
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (Auction auction in e.Auctions)
                {
                    sb.AppendLine(auction.Name);
                    sb.AppendFormat("Cena: {0}", auction.Price.ToString("0.00", CultureInfo.InvariantCulture));
                    if (auction.BuyNowPrice.HasValue)
                    {
                        sb.AppendFormat(", Kup teraz: {0}", auction.BuyNowPrice.Value.ToString("0.00", CultureInfo.InvariantCulture));
                    }
                    sb.AppendLine(string.Empty);
                    sb.AppendLine(string.Empty);
                }

                uxAreNewBtn.Visible = true;
                uxNotifyIcon.ShowBalloonTip(Consts.BalloonTipTime, "Nowe aukcje spełniające kryteria", sb.ToString(), ToolTipIcon.Info);
                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        private void bgWorker_SearchCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SearchResult result = e.Result as SearchResult;

            if (!result.AllegroMethodResult.Success)
            {
                uxErrorLabel.Text = result.AllegroMethodResult.ErrorMessage;
                if (result.AllegroMethodResult.ErrorCode == "ERR_NO_SESSION" ||
                    result.AllegroMethodResult.ErrorCode == "ERR_SESSION_EXPIRED")
                {
                    Login();
                }
                DisableLoader();
                return;
            }

            FillView(result.Auctions);
            uxSearchNowBtn.Enabled = true;
            DisableLoader();
            SetAllButtonsState(true);
        }

        private void searchTimer_Tick(object sender, EventArgs e)
        {
            SearchSelected(false, true);
        }

        private void uxSearchNowBtn_Click(object sender, EventArgs e)
        {
            if (uxSearchNowBtn.Enabled)
            {
                RunWorkerForTitle(uxTitleTB.Text);
                uxTitleTB.SelectAll();
            }
        }

        private void uxAuctionsView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.Value != null)
            {
                e.FormattingApplied = true;
                TimeSpan val = (TimeSpan)e.Value;
                e.Value = Auction.GetTimeLeftAsString(val);
            }
            if ((e.ColumnIndex == 2 || e.ColumnIndex == 3) && e.Value != null)
            {
                decimal val = (decimal)e.Value;
                e.Value = val.ToString("0.00");
                e.FormattingApplied = true;
            }
        }

        private void uxAuctionsView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 && e.RowIndex != -1)
            {
                System.Diagnostics.Process.Start(uxAuctionsView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }

        private void uxTitleTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                uxSearchNowBtn_Click(uxSearchNowBtn, new EventArgs());
                e.SuppressKeyPress = true;
            }
        }

        private void uxAddBtn_Click(object sender, EventArgs e)
        {
            shortList.Add(uxTitleTB.Text);
            BindShortList();
        }

        private void uxGamesList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string gameTitle = uxGamesList.Items[e.Index] as string;
            if (e.NewValue == CheckState.Checked)
            {
                shortList.SetChecked(gameTitle);
            }
            else
            {
                shortList.SetUnchecked(gameTitle);
            }
        }

        private void uxSearchSelectedBtn_Click(object sender, EventArgs e)
        {
            SearchSelected(true, false);
            uxSearchSelectedBtn.Enabled = false;
            uxAreNewBtn.Visible = false;
        }

        private void uxWaitForNewBtn_Click(object sender, EventArgs e)
        {
            uxAreNewBtn.Visible = false;

            SearchSelected(true, false);

            Hide();
            searchTimer.Start();
        }

        private void uxNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            searchTimer.Stop();
        }

        private void uxNotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Show();
            searchTimer.Stop();
            FillView(container.GetNewAuctions());
        }

        private void uxGamesList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ShortlistDeleteSelected();
                e.SuppressKeyPress = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            XmlWriter writer = XmlWriter.Create(new FileStream(Shortlist.FileName, FileMode.Create, FileAccess.Write));
            XmlSerializer serializer = new XmlSerializer(typeof(Shortlist));
            serializer.Serialize(writer, shortList);
            writer.Close();
        }

        private void uxAreNewBtn_Click(object sender, EventArgs e)
        {
            FillView(container.GetNewAuctions());
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShortlistDeleteSelected();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uxSearchNowBtn.Enabled)
            {
                if (uxGamesList.SelectedIndex != -1)
                {
                    string selected = uxGamesList.Items[uxGamesList.SelectedIndex] as string;
                    RunWorkerForTitle(selected);
                }
            }
        }

        private void uxGamesList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < uxGamesList.Items.Count; i++)
                {
                    Rectangle rect = uxGamesList.GetItemRectangle(i);
                    if ((e.Y >= rect.Y && e.Y < (rect.Y + rect.Height)))
                    {
                        uxGamesList.SelectedItem = uxGamesList.Items[i];
                        return;
                    }
                }
            }
        }

        #endregion

        private void SearchSelected(bool clearBeforeSearch, bool showBaloonAfterSearch)
        {
            BackgroundWorkerSearchSelected(clearBeforeSearch, showBaloonAfterSearch);
            EnableLoader();
            SetAllButtonsState(false);
        }

        private void RunWorkerForTitle(string title)
        {
            BackgroundWorkerSearchSelected(title);

            uxSearchNowBtn.Enabled = false;
            EnableLoader();
            SetAllButtonsState(false);
        }

        private void FillView(List<Auction> list)
        {
            uxAuctionsView.Rows.Clear();
            list.Sort(new TimeLeftComparer());
            foreach (Auction auction in list)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(uxAuctionsView);

                row.SetValues(new object[] { auction.Id, 
                    auction.Name, 
                    auction.Price, 
                    (auction.BuyNowPrice.HasValue) ? auction.BuyNowPrice.Value : default(decimal), 
                    auction.TimeLeft,
                    auction.Offers,
                    auction.AllegroLink});

                if (auction.TimeLeft <= TimeSpan.FromHours(5))
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.BackColor = Color.Pink;
                    }
                }
                uxAuctionsView.Rows.Add(row);
            }
        }

        #endregion

        #region constructors

        public MainForm()
        {
            InitializeComponent();

            try
            {
                lm = new LoginManager(new AllegroServiceProvider(),
                            AppSettings.Default.ApiKey, AppSettings.Default.Login, AppSettings.Default.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsInitialized = false;
                return;
            }

            Login();

            if (File.Exists(Shortlist.FileName))
            {
                using(Stream stream = new FileStream(Shortlist.FileName, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Shortlist));
                    shortList = serializer.Deserialize(stream) as Shortlist;
                    BindShortList();
                }
            }

            searchTimer.Interval = 1000 * 60 * AppSettings.Default.CheckInterval;

            IsInitialized = true;
        }
        
        #endregion
    }
}
