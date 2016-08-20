using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        const int MAX_TO_SIGN_UP = 5;
        const int MIN_TO_SIGN_UP = 1;

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
        }


        private void updateGameRepater()
        {
            
            Repeater1.DataSource =
                from g in Games
                select new FormatedGame {
                    Id = g.Id.ToString(),
                    CreatedDateTime = g.CreatedDateTime,
                    GameStatus = g.GameStatus,
                    Player1 = g.Player1 != null ? g.Player1.Name : null,
                    Player2 = g.Player2 != null ? g.Player2.Name : null,
                    WinnerPlayerNum = g.WinnerPlayerNum.ToString()};
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
            lblValideNumberOfUsers.Visible = false;
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
                txtUpdateInfoUserName.Text = Session["userName"].ToString();
                txtUpdateInfoUserLastName.Text = Session["Family"].ToString();
                txtUpdateInfoUserPassword.Text = Session["password"].ToString();
                lblUpdates.Visible = false;
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
                updateGameRepater();
                if (Repeater1.Items.Count > 0) { lblGamePanel.Visible = false; }
                else
                {
                    lblGamePanel.Text = "No games to show";
                    lblGamePanel.Visible = true;
                }
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

        protected bool isOldContentEqualNew(string oldContent, string newContent)
        {
            if (string.IsNullOrEmpty(newContent) || newContent.Equals("null")) return false;
            return newContent.Equals(oldContent);
        }

        private void CongigSignUp(int signUpCount)
        {
            int visiableDisable = MAX_TO_SIGN_UP - signUpCount;
            textBoxNumberOfUsers.Text = "";
            lblUserContainerNumberOfUsers.Text = signUpCount.ToString();
            switch(signUpCount)
            {
                case 1:
                    enableUser1(true);
                    enableUser2(false);
                    enableUser3(false);
                    enableUser4(false);
                    enableUser5(false);
                    break;
                case 2:
                    enableUser1(true);
                    enableUser2(true);
                    enableUser3(false);
                    enableUser4(false);
                    enableUser5(false);
                    break;
                case 3:
                    enableUser1(true);
                    enableUser2(true);
                    enableUser3(true);
                    enableUser4(false);
                    enableUser5(false);
                    break;
                case 4:
                    enableUser1(true);
                    enableUser2(true);
                    enableUser3(true);
                    enableUser4(true);
                    enableUser5(false);
                    break;
                case 5:
                    enableUser1(true);
                    enableUser2(true);
                    enableUser3(true);
                    enableUser4(true);
                    enableUser5(true);
                    break;
            }
            
        }

        private void enableUser5(bool enable)
        {
            lblSignUpUserName5.Visible = enable;
            txtSignUpUserName5.Visible = enable;
            lblSignUpPassword5.Visible = enable;
            txtSignUpPassword5.Visible = enable;
            ruvUserName5.Enabled = enable;
            ruvPasswordUser5.Enabled = enable;
        }

        private void enableUser4(bool enable)
        {
            lblSignUpUserName4.Visible = enable;
            txtSignUpUserName4.Visible = enable;
            lblSignUpPassword4.Visible = enable;
            txtSignUpUserName4.Visible = enable;
            ruvUserName4.Enabled = enable;
            ruvPasswordUser4.Enabled = enable;
        }

        private void enableUser3(bool enable)
        {
            lblSignUpUserName3.Visible = enable;
            txtSignUpUserName3.Visible = enable;
            lblSignUpPassword3.Visible = enable;
            txtSignUpPassword3.Visible = enable;
            ruvUserName3.Enabled = enable;
            ruvPasswordUser3.Enabled = enable;
        }

        private void enableUser2(bool enable)
        {
            lblSignUpUserName2.Visible = enable;
            txtSignUpUserName2.Visible = enable;
            lblSignUpPassword2.Visible = enable;
            txtSignUpPassword2.Visible = enable;
            ruvUserName2.Enabled = enable;
            ruvPasswordUser2.Enabled = enable;
        }

        private void enableUser1(bool enable)
        {
            lblSignUpUserName1.Visible = enable;
            txtSignUpUserName1.Visible = enable;
            lblSignUpPassword1.Visible = enable;
            txtSignUpPassword1.Visible = enable;
            ruvUserName1.Enabled = enable;
            ruvPasswordUser1.Enabled = enable;
        }
        
        /* Events */
        protected void UpdatePanel_UnLoad(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string userName = textBoxSignInUname.Text;
            string password = textBoxSignInPsw.Text;

            Player player = restCalls.LoginWeb(userName, password);
            if(player == null)
            {
                textBoxSignInUname.Text = "";
                textBoxSignInPsw.Text = "";
                lblLoginFailed.Text = "Failed to Login";
                showPanel("login failed");
            } else
            {
                Session["UserID"] = player.Id;
                Session["password"] = password;
                Session["userName"] = player.Name;
                Session["Family"] = player.Family.Name;
                Session["numberOfVisiting"] = (int)Session["numberOfVisiting"] + 1;
                lblLoginSuccess.Text = "Successfully Login";
                showPanel("login susscess");
            }
        }
        
        protected void btnNumberOfUsers_Click(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(textBoxNumberOfUsers.Text);
            if(count > MAX_TO_SIGN_UP || count < MIN_TO_SIGN_UP)
            {
                lblValideNumberOfUsers.Text = "Invalid number of users";
                lblValideNumberOfUsers.ForeColor = Color.Red;
                lblValideNumberOfUsers.Visible = true;

                textBoxNumberOfUsers.Text = "";
                showPanel("signUp");
            }
            CongigSignUp(count);
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
            gamesDropDownList.DataTextField = "CreatedDateTime";
            gamesDropDownList.DataValueField = "ID";
            gamesDropDownList.DataSource = Games;
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

                    lblUpdates.Text = "Changes saved Successfully :)";
                    lblUpdates.ForeColor = Color.Green;
                    lblUpdates.Visible = true;

                    Session["password"] = updatePassword;
                    Session["userName"] = updateUserName;
                    Session["Family"] = updateFamily;
                } else
                {
                    lblUpdates.Text = "Something Went Worng :(";
                    lblUpdates.ForeColor = Color.Red;
                    lblUpdates.Visible = true;
                }
            }
            else
            {
                lblUpdates.Text = "No changes has made :|";
                lblUpdates.ForeColor = Color.Blue;
                lblUpdates.Visible = true;
            }

            txtUpdateInfoUserName.Text = updateUserName;
            txtUpdateInfoUserLastName.Text = updateFamily;
            txtUpdateInfoUserPassword.Text = updatePassword;
            showPanel("updates");
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
                lblUpdates.Text = "Something Went Worng :(";
                lblUpdates.ForeColor = Color.Red;
                lblUpdates.Visible = true;
                showPanel("updates");
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            int numberOfUsers = Convert.ToInt32(lblUserContainerNumberOfUsers.Text);
            String result = String.Empty;
            List<Player> players = new List<Player>(); 
            string lastName = txtSignUpFamilyName.Text;
            Family family = new Family { Name = lastName };
            bool isAddedFamily = restCalls.AddFamily(family);
            if(isAddedFamily)
            {
                if (lblSignUpUserName1.Visible) { players.Add(new Player { Name = txtSignUpUserName1.Text, Password = txtSignUpPassword1.Text, Family = family }); }
                if (lblSignUpUserName2.Visible) { players.Add(new Player { Name = txtSignUpUserName2.Text, Password = txtSignUpPassword2.Text, Family = family }); }
                if (lblSignUpUserName3.Visible) { players.Add(new Player { Name = txtSignUpUserName3.Text, Password = txtSignUpPassword3.Text, Family = family }); }
                if (lblSignUpUserName4.Visible) { players.Add(new Player { Name = txtSignUpUserName4.Text, Password = txtSignUpPassword4.Text, Family = family }); }
                if (lblSignUpUserName5.Visible) { players.Add(new Player { Name = txtSignUpUserName5.Text, Password = txtSignUpPassword5.Text, Family = family }); }
                foreach(Player p in players)
                {
                    bool isAddedPlayer = restCalls.AddPlayer(p);
                    if (!isAddedPlayer)
                    {
                        result += "Problem occur while adding " + p.Name + "\n";
                    }
                }
            }


        }
        
        private void registerToGame(String gameID)
        {
            bool registerSuccess = false;
            bool addPlayer = false;
            Game g = (Game)Games.Where(game => game.Id.Equals(Convert.ToInt32(gameID)));
            Player p = restCalls.GetPlayerById(Session["UserID"].ToString());
            if (g.Player1 == null)
            {
                g.Player1 = p;
                addPlayer = true;
            }
            else if (g.Player1 != null && g.Player2 == null)
            {
                g.Player2 = p;
                addPlayer = true;
            }
            if (addPlayer)
            {
                registerSuccess = restCalls.UpdateGame(g);
                if (registerSuccess)
                {
                    lblGamePanel.ForeColor = Color.Green;
                    lblGamePanel.Text = "Successfully Register to game" + gameID;
                }
                else
                {
                    lblGamePanel.ForeColor = Color.Red;
                    lblGamePanel.Text = "Something went worng. please try again";
                }
            }
            else
            {
                lblGamePanel.ForeColor = Color.Red;
                lblGamePanel.Text = "Cannot Register to game";
            }
            lblGamePanel.Visible = true;
            showPanel("games");
        }

        protected void btnCreateGame_Click(object sender, EventArgs e)
        {
            Game game = new Game();
            game.GameStatus = Status.NEW_GAME;
            game.CreatedDateTime = DateTime.Now;
            Player p = restCalls.GetPlayerById(Session["UserID"].ToString());
            game.Player1 = p;
            bool addGameSuccess = restCalls.AddGame(game);
            txtGameID.Text = "";
            if (addGameSuccess)
            {
                Games = restCalls.GetGames();
                updateGameRepater();
                lblGamePanel.ForeColor = Color.Green;
                lblGamePanel.Text = "Successfully created new game";
            }
            else
            {
                lblGamePanel.ForeColor = Color.Red;
                lblGamePanel.Text = "Something went worng. please try again";
            }
            lblGamePanel.Visible = true;
            showPanel("games");
            
        }

        protected void btnRegisterGame_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                registerToGame(txtGameID.Text);
            }
        }

        protected void btnDeleteGame_Click(object sender, EventArgs e)
        {
            bool deletedSuccess = restCalls.RemoveGame(txtGameID.Text);
            if (deletedSuccess)
            {
                Games = restCalls.GetGames();
                updateGameRepater();
                lblGamePanel.ForeColor = Color.Green;
                lblGamePanel.Text = "Successfully delete game "+ txtGameID.Text;
            }
            else
            {
                lblGamePanel.ForeColor = Color.Red;
                lblGamePanel.Text = "Something went worng. please try again";
            }
            txtGameID.Text = "";
            lblGamePanel.Visible = true;
            showPanel("games");
            
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddPlayer_Click(object sender, EventArgs e)
        {

        }
    }

    public class FormatedGame
    {
        public String Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Status GameStatus { get; set; }
        public String Player1 { get; set; }
        public String Player2 { get; set; }
        public String WinnerPlayerNum { get; set; }
    }
}
