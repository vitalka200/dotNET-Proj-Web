using System;
using System.Collections.Generic;
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

        public Repeater rpt { get; private set; }
        public RestApiCalls restCalls = new RestApiCalls();

        protected void Page_Load(object sender, EventArgs e)
        {
            allPanels.Add(homePanel);
            allPanels.Add(signInPanel);
            allPanels.Add(signUpPanel);
            allPanels.Add(usersContainerPanel);
            allPanels.Add(usersOnlyPanel);
            allPanels.Add(askUsPanel);
            allPanels.Add(updatesPanel);
            allPanels.Add(aboutPanel);

            disablePanelVisiable();
            createTableUsers();
            createTableGames();
            createUsersDropDownList();

            if (!IsPostBack)
            {
                Session["userName"] = "";
                Session["numberOfVisiting"] = 0;
            }
            showPanel("homePanel");
        }

        public void createUsersDropDownList()
        {
            List<Player> players = restCalls.GetPlayers();
            usersDropDownList.Items.Add(new ListItem("", ""));

            foreach (Player p in players)
            {
                usersDropDownList.Items.Add(new ListItem(p.Name, p.Id.ToString()));
            }
        }

        public void createTableUsers()
        {
            List<Player> players = restCalls.GetPlayers();
            Table tblAllUser = createPlayersTable(players);
            pnlInfo1.Controls.Add(tblAllUser);
        }

        public void createTableGames()
        {
            List<Game> games = restCalls.GetGames();
            Table tblAllGames = createGamesTable(games);
            pnlInfo2.Controls.Add(tblAllGames);
        }

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

        protected void linkUsersOnly_Click(object sender, EventArgs e)
        {
          //  if(String.IsNullOrEmpty(Session["userName"].ToString()))
            if(Session["userName"] == null)
            {
                usersOnlyWarning.Visible = true;
                showPanel("signIn");
            }
            else
            {
                usersOnlyDropDownList.ClearSelection();
                showPanel("usersOnly");
            }
        }

        protected void linkAbout_Click(object sender, EventArgs e)
        {
            showPanel("about");
        }

        private void showPanel(String panelName)
        {
            disablePanelVisiable();
            Panel p = new Panel();
            switch (panelName)
            {
                case "homePanel":
                    p = homePanel;
                    break;
                case "signIn":
                    p = signInPanel;
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
                case "usersOnly":
                    p = usersOnlyPanel;
                    break;
                case "userControl":
                    p = usersContainerPanel;
                    break;
                case "updates":
                    p = updatesPanel;
                    break;
            }
            p.Visible = true;
            PlaceHolder1.Controls.Add(p);
        }

        private void disablePanelVisiable()
        {
            foreach (Panel p in allPanels)
            {
                p.Visible = false;
            }
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Panel p = new Panel();
            Label l1 = new Label();
            Label l2 = new Label();
           // p.Visible = false;

            string userName = textBoxSignInUname.Text;
            string password = textBoxSignInPsw.Text;


            l1.Text = "Hello" + userName;
            l2.Text ="You can start play now";

            p.Controls.Add(l1);
            p.Controls.Add(l2);

            disablePanelVisiable();
            PlaceHolder1.Controls.Add(p);
            p.Visible = true;

            Session["userName"] = userName;
            Session["numberOfVisiting"] = (int)Session["numberOfVisiting"] + 1;
        }

        protected void UsersOnlyDropDownList_OnSelectedChange(object sender, EventArgs e)
        {
            int index = usersOnlyDropDownList.SelectedIndex;
            switch (index)
            {
                case 1:
                    askUsPanel.Visible = true;
                    updatesPanel.Visible = false;
                    showPanel("askUs");
                    break;
                case 2:
                    askUsPanel.Visible = false;
                    updatesPanel.Visible = true;
                    showPanel("updates");
                    break;
                default:
                    askUsPanel.Visible = false;
                    updatesPanel.Visible = false;
                    break;
            }
            usersOnlyDropDownList.ClearSelection();
           
        }

        protected void usersDropDownList_OnSelectedChange(object sender, EventArgs e)
        {
            int index = usersOnlyDropDownList.SelectedIndex;
            switch (index)
            {
                case 1:
                    askUsPanel.Visible = true;
                    updatesPanel.Visible = false;
                    break;
                case 2:
                    askUsPanel.Visible = false;
                    updatesPanel.Visible = true;
                    break;
                default:
                    askUsPanel.Visible = false;
                    updatesPanel.Visible = false;
                    break;
            }
            usersOnlyDropDownList.ClearSelection();
        }

        protected void rptUsers_RowDataBound(object sender, RepeaterItemEventArgs e)
        {
         /*   String current = String.Empty;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Binding to Game object.
                if (current != (e.Item.DataItem as Game).GameId)
                {
                    current = (e.Item.DataItem as Game).Id;
                    e.Item.FindControl("headerTable").Visible = true;
                    (e.Item.FindControl("headerTitle") as Label).Text = (e.Item.DataItem as Employee).Department;
                }
                else
                {
                    e.Item.FindControl("headerTable").Visible = false;
                }
            }*/
        }

        private void bindGridView()
        {
           /* List<Game> games = new List<Game>();
            rpt.DataSource = games.OrderBy(x => x.Id);
            rpt.DataBind();*/
        }

        protected void gamesDropDownList_OnSelectedChange(object sender, EventArgs e)
        {
            int index = usersOnlyDropDownList.SelectedIndex;
            switch (index)
            {
                case 1:
                    askUsPanel.Visible = true;
                    updatesPanel.Visible = false;
                    break;
                case 2:
                    askUsPanel.Visible = false;
                    updatesPanel.Visible = true;
                    break;
                default:
                    askUsPanel.Visible = false;
                    updatesPanel.Visible = false;
                    break;
            }
            usersOnlyDropDownList.ClearSelection();
        }

        protected void rptGames_RowDataBound(object sender, RepeaterItemEventArgs e)
        {
         /*   String current = String.Empty;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Binding to Game object.
                if (current != (e.Item.DataItem as Game).GameId)
                {
                    current = (e.Item.DataItem as Game).Id;
                    e.Item.FindControl("headerTable").Visible = true;
                    (e.Item.FindControl("headerTitle") as Label).Text = (e.Item.DataItem as Employee).Department;
                }
                else
                {
                    e.Item.FindControl("headerTable").Visible = false;
                }
            }*/
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

        protected void btnSignUp_Click(object sender, EventArgs e)
        {

        }
        
        public void usersDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem item = usersDropDownList.SelectedItem;
            if(item != null && !String.IsNullOrEmpty(item.Value))
            {
                List<Game> games = restCalls.GetGameByPlayerId(item.Value);
                Table gamesByPlayerID = createGamesTable(games);
                pnlInfo3.Controls.Add(gamesByPlayerID);
            }
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

    }
}