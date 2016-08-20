using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Web;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class main : System.Web.UI.Page
    {
        List<Panel> allPanels = new List<Panel>();
        public RestApiCalls restCalls = new RestApiCalls();
        public List<Game> Games { get; set; }
        public List<Player> Players { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            initialPanel();
            disablePanelVisiable();

            Games = restCalls.GetGames();
            Players = restCalls.GetPlayers();

            createTableUsers();
            createTableGames();
            createNumOfGamesPerPlayer();
            

            if (!IsPostBack)
            {
                Session["userName"] = null;
                Session["UserID"] = null;
                Session["password"] = null;
                Session["Family"] = null;
                Session["numberOfVisiting"] = 0;
                showPanel("homePanel");
                updateGameRepater();
            }

            foreach(RepeaterItem  ritem in Repeater1.Items)
            {
                Button btn = ritem.FindControl("btnRegisterGame") as Button;
                btn.Click += new EventHandler(btnRegisterGame_Click);
                btn = ritem.FindControl("btnDeleteGame") as Button;
                btn.Click += new EventHandler(btnDeleteGame_Click);
            }

        }


        private void updateGameRepater()
        {
            Repeater1.DataSource = Games;
            Repeater1.DataBind();
        }

        /* Events of menu options */
        protected void linkHome_Click(object sender, EventArgs e)
        {
            showPanel("homePanel");
        }

        protected void linkSignIn_Click(object sender, EventArgs e)
        {
            showPanel("signIn");
        }

        protected void linkSignUp_Click(object sender, EventArgs e)
        {
            showPanel("signUp");
        }

        protected void linkSignOut_Click(object sender, EventArgs e)
        {
            Session["UserID"] = null;
            Session["password"] = null;
            Session["userName"] = null;
            Session["Family"] = null;
            Session["numberOfVisiting"] = 0;
            showPanel("homePanel");
        }

        protected void linkAskUs_Click(object sender, EventArgs e)
        {
            if (Session["userName"] == null)
            {
                usersOnlyWarning.Visible = true;
                showPanel("signIn");
            }
            else
            {
                showPanel("askUs");
            }
        }

        protected void linkUpdateInfo_Click(object sender, EventArgs e)
        {
            if (Session["userName"] == null)
            {
                usersOnlyWarning.Visible = true;
                showPanel("signIn");
            }
            else
            {
                txtIDLabel.Text = Session["UserID"].ToString();
                lblUpdateSavedSuccess.Visible = false;
                lblUpdateSavedFailed.Visible = false;
                lblUpdateNoChanges.Visible = false;
                showPanel("updates");
            }
        }

        protected void linkGames_Click(object sender, EventArgs e)
        {
            if (Session["userName"] == null)
            {
                usersOnlyWarning.Visible = true;
                showPanel("signIn");
            }
            else
            {
                showPanel("games");
            }
        }

        protected void linkAbout_Click(object sender, EventArgs e)
        {
            showPanel("about");
        }

        /* Infrastructure */

        private void showPanel(String panelName)
        {
            disablePanelVisiable();
            Panel p = new Panel();
            UpdatePanel up = new UpdatePanel();
            switch (panelName)
            {
                case "homePanel":
                    p = homePanel;
                    break;
                case "signIn":
                    p = signInPanel;
                    break;
                case "login susscess":
                    p = LoginSuccessPanel;
                    break;
                case "login failed":
                    p = LoginFailedPanel;
                    break;
                case "signUp":
                    p = signUpPanel;
                    break;
                case "askUs":
                    p = askUsPanel;
                    break;
                case "about":
                    p = aboutPanel;
                    break;
                case "userControl":
                    p = usersContainerPanel;
                    break;
                case "updates":
                    up = updatesPanel;
                    break;
                case "games":
                    p = gamesPanel;
                    break;
            }
            if (panelName.Equals("updates"))
           {
                up.Visible = true;
                PlaceHolder1.Controls.Add(up);
            } else
            {
                p.Visible = true;
                PlaceHolder1.Controls.Add(p);
            }
        }

        private void disablePanelVisiable()
        {
            foreach (Panel p in allPanels)
            {
                p.Visible = false;
            }
        }

        public void initialPanel()
        {
            allPanels.Add(homePanel);
            allPanels.Add(signInPanel);
            allPanels.Add(LoginSuccessPanel);
            allPanels.Add(LoginFailedPanel);
            allPanels.Add(signUpPanel);
            allPanels.Add(usersContainerPanel);
            allPanels.Add(askUsPanel);
          //  allPanels.Add(updatesPanel);
            allPanels.Add(gamesPanel);
            allPanels.Add(aboutPanel);

            updatesPanel.Visible = false;

        }

        public void createNumOfGamesPerPlayer()
        {
            Table tblGamesCount = new Table();

            TableRow tRow = new TableRow();
            TableCell tCell = new TableCell();
            tCell.Text = "Id";
            tRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "Name";
            tRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "Number of games";
            tRow.Cells.Add(tCell);
            tCell = new TableCell();

            tblGamesCount.Rows.Add(tRow);

            foreach (Player p in Players)
            {
                int count = restCalls.GetTotalGamesCountForPlayer(p.Id.ToString());
                tRow = new TableRow();
                tCell = new TableCell();
                tCell.Text = p.Id.ToString();
                tRow.Cells.Add(tCell);
                tCell = new TableCell();
                tCell.Text = p.Name;
                tRow.Cells.Add(tCell);
                tCell = new TableCell();
                tCell.Text = count.ToString();
                tRow.Cells.Add(tCell);
                tblGamesCount.Rows.Add(tRow);
            }

            pnlInfo5.Controls.Add(tblGamesCount);
        }

        public void createTableUsers()
        {
            Table tblAllUser = createPlayersTable(Players);
            pnlInfo1.Controls.Add(tblAllUser);
        }

        public void createTableGames()
        {
            Table tblAllGames = createGamesTable(Games);
            pnlInfo2.Controls.Add(tblAllGames);
        }

        public Table createGamesTable(List<Game> games)
        {
            Table tblAllGames = new Table();
            TableRow tRow = new TableRow();
            TableCell tCell = new TableCell();
            tCell.Text = "Id";
            tRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "Creation Time";
            tRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "Status";
            tRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "Player1";
            tRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "Player2";
            tRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "Winner";
            tRow.Cells.Add(tCell);
            tblAllGames.Rows.Add(tRow);

            foreach (Game g in games)
            {
                tRow = new TableRow();
                tCell = new TableCell();
                tCell.Text = g.Id.ToString();
                tRow.Cells.Add(tCell);
                tCell = new TableCell();
                tCell.Text = g.CreatedDateTime.ToShortTimeString();
                tRow.Cells.Add(tCell);
                tCell = new TableCell();
                tCell.Text = g.GameStatus.ToString();
                tRow.Cells.Add(tCell);
                tCell = new TableCell();
                tCell.Text = g.Player1.Name;
                tRow.Cells.Add(tCell);
                tCell = new TableCell();
                tCell.Text = g.Player2.Name;
                tRow.Cells.Add(tCell);
                tCell = new TableCell();
                if (g.WinnerPlayerNum == 1) { tCell.Text = g.Player1.Name; }
                else if (g.WinnerPlayerNum == 2) { tCell.Text = g.Player2.Name; }
                else { tCell.Text = "No Winner"; }
                tRow.Cells.Add(tCell);
                tblAllGames.Rows.Add(tRow);
            }

            return tblAllGames;
        }

        public Table createPlayersTable (List<Player> players)
        {
            Table tblAllUser = new Table();
            TableRow tRow = new TableRow();
            TableCell tCell = new TableCell();
            tCell.Text = "Id";
            tRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "Name";
            tRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "Family Name";
            tRow.Cells.Add(tCell);
            tblAllUser.Rows.Add(tRow);

            foreach (Player p in players)
            {
                tRow = new TableRow();
                tCell = new TableCell();
                tCell.Text = p.Id.ToString();
                tRow.Cells.Add(tCell);
                tCell = new TableCell();
                tCell.Text = p.Name;
                tRow.Cells.Add(tCell);
                tCell = new TableCell();
                tCell.Text = p.Family.Name;
                tRow.Cells.Add(tCell);
                tblAllUser.Rows.Add(tRow);
            }

            return tblAllUser;
        }

        /* Events */
        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string userName = textBoxSignInUname.Text;
            string password = textBoxSignInPsw.Text;

            Player player = restCalls.LoginWeb(userName, password);
            if(player == null)
            {
                textBoxSignInUname.Text = "";
                textBoxSignInPsw.Text = "";
                showPanel("login failed");
            } else
            {
                Session["UserID"] = player.Id;
                Session["password"] = player.Password;
                Session["userName"] = player.Name;
                Session["Family"] = player.Family.Name;
                Session["numberOfVisiting"] = (int)Session["numberOfVisiting"] + 1;
                showPanel("login susscess");
            }
        }

        protected void UpdatePanel_UnLoad(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });

        }

        protected void btnNumberOfUsers_Click(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(textBoxNumberOfUsers.Text);
            for (int i = 0; i < count; i++)
            {
                Label lblUserName = new Label();
                lblUserName.ID = "lblUserName" + i.ToString();
                lblUserName.Text = "User Name:";
                Label lblUserLastName = new Label();
                lblUserLastName.ID = "lblUserLastName" + i.ToString();
                lblUserLastName.Text = "Last Name:";
                TextBox txtUserName = new TextBox();
                txtUserName.ID = "txtUserName" + i.ToString();
                TextBox txtUserLastName = new TextBox();
                txtUserLastName.ID = "txtUseLastrName" + i.ToString();

                Label lblUserPassword = new Label();
                lblUserLastName.ID = "lblUserPassword" + i.ToString();
                lblUserLastName.Text = "Password:";
                TextBox txtUserPassword = new TextBox();
                txtUserName.ID = "txtUserPassword" + i.ToString();
                txtUserPassword.TextMode = TextBoxMode.Password;

                Label lblColorChecker = new Label();
                lblColorChecker.Text = "please choose checker color: ";
                RadioButtonList rbl = new RadioButtonList();
                rbl.ID = "RadioButtonList";
                rbl.RepeatDirection = RepeatDirection.Horizontal;
                rbl.Items.Add(new ListItem("Black"));
                rbl.Items.Add(new ListItem("White"));

                usersContainerPanel.Controls.Add(lblUserName);
                usersContainerPanel.Controls.Add(new LiteralControl("&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;"));
                usersContainerPanel.Controls.Add(txtUserName);
                usersContainerPanel.Controls.Add(new LiteralControl("<br />"));

                usersContainerPanel.Controls.Add(lblUserLastName);
                usersContainerPanel.Controls.Add(new LiteralControl("&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;"));
                usersContainerPanel.Controls.Add(txtUserLastName);
                usersContainerPanel.Controls.Add(new LiteralControl("<br />"));

                usersContainerPanel.Controls.Add(lblUserPassword);
                usersContainerPanel.Controls.Add(new LiteralControl("&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;"));
                usersContainerPanel.Controls.Add(txtUserPassword);
                usersContainerPanel.Controls.Add(new LiteralControl("<br />"));

                usersContainerPanel.Controls.Add(lblColorChecker);
                usersContainerPanel.Controls.Add(new LiteralControl("<br />"));
                usersContainerPanel.Controls.Add(rbl);
                usersContainerPanel.Controls.Add(new LiteralControl("<br /><br /><br />"));
            }
            //usersContainerPanel.Visible = true;
            showPanel("userControl");
        }

        protected void UsersDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem item = UsersDropDownList.SelectedItem;
            if (item != null && !String.IsNullOrEmpty(item.Value))
            {
                List<Game> games = restCalls.GetGameByPlayerId(item.Value);
                Table gamesByPlayerID = createGamesTable(games);
                pnlInfo3.Controls.Add(gamesByPlayerID);
            }
         
        }

        protected void UsersDropDownList_Init(object sender, EventArgs e)
        {
            List<Player> players = restCalls.GetPlayers();
            UsersDropDownList.DataTextField = "Name";
            UsersDropDownList.DataValueField = "ID";
            UsersDropDownList.DataSource = players;
            UsersDropDownList.DataBind();
        }

        protected void gamesDropDownList_Init(object sender, EventArgs e)
        {
            List<Game> games = restCalls.GetGames();
            gamesDropDownList.DataTextField = "CreatedDateTime";
            gamesDropDownList.DataValueField = "ID";
            gamesDropDownList.DataSource = games;
            gamesDropDownList.DataBind();
        }

        protected void gamesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem item = gamesDropDownList.SelectedItem;
            if (item != null && !String.IsNullOrEmpty(item.Value))
            {
                List<Player> players = restCalls.GetPlayersByGameId(item.Value);
                Table playersByGameID = createPlayersTable(players);
                pnlInfo4.Controls.Add(playersByGameID);
            }
        }

        protected bool isOldContentEqualNew(string oldContent, string newContent)
        {
            if (string.IsNullOrEmpty(newContent) || newContent.Equals("null")) return false;
            return newContent.Equals(oldContent);
        }

        protected void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            string updateUserName = txtUpdateInfoUserName.Text;
            string updateFamily = txtUpdateInfoUserLastName.Text;
            string updatePassword = txtUpdateInfoUserPassword.Text;
            string stored_userName = string.Format("{0}", Session["userName"]);
            string stored_Family = string.Format("{0}", Session["Family"]);
            string stored_password = string.Format("{0}", Session["password"]);
            if (!isOldContentEqualNew(stored_userName, updateUserName)
                || !isOldContentEqualNew(stored_Family, updateFamily)
                || !isOldContentEqualNew(stored_password, updatePassword)
                )
            {
                Player p = restCalls.GetPlayerById(Session["UserID"].ToString());
                p.Name = updateUserName;
                p.Family.Name = updateFamily;
                p.Password = updatePassword;
                bool saved = restCalls.UpdatePlayer(p);
                if(saved)
                {

                    lblUpdateSavedSuccess.Visible = true;
                    lblUpdateSavedFailed.Visible = false;
                    lblUpdateNoChanges.Visible = false;

                    Session["password"] = updatePassword;
                    Session["userName"] = updateUserName;
                    Session["Family"] = updateFamily;
                } else
                {
                    lblUpdateSavedSuccess.Visible = false;
                    lblUpdateSavedFailed.Visible = true;
                   lblUpdateNoChanges.Visible = false;
                    
                }
            }
            else
            {
                lblUpdateSavedSuccess.Visible = false;
                lblUpdateSavedFailed.Visible = false;
                lblUpdateNoChanges.Visible = true;
            }
           // txtUpdateInfoUserName.Text = updateUserName;
          //  txtUpdateInfoUserLastName.Text = updateFamily;
          //  txtUpdateInfoUserPassword.Text = updatePassword;
           // showPanel("updates");
        }

        protected void deleteUser_Click(object sender, EventArgs e)
        {
            bool removed = restCalls.RemovePlayer(Session["UserID"].ToString());
            if (removed)
            {
                Session["UserID"] = null;
                Session["password"] = null;
                Session["userName"] = null;
                Session["Family"] = null;
                Session["numberOfVisiting"] = 0;
                showPanel("homePanel");
            }
            else
            {
                lblUpdateSavedSuccess.Visible = false;
                lblUpdateSavedFailed.Visible = true;
                lblUpdateNoChanges.Visible = false;
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {

        }
        
        private void btnDeleteGame_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnRegisterGame_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
        }
    }
}