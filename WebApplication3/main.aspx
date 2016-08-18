<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="WebApplication3.main" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web</title>
    <script type="text/javascript" src="../JavaScript/jquery-1.3.2.min.js"></script>
    <script type="text/javascript">
        function mainmenu() {
            $(" #nav ul ").css({ display: "none" }); // Opera Fix

            $(" #nav li").hover(function () {
                $(this).find('ul:first').css({ visibility: "visible", display: "none" }).show(400);
            }
            , function () {
                $(this).find('ul:first').css({ visibility: "hidden" });
            });
        }

        $(document).ready(function () {
            mainmenu();
        });
    </script>
    <link rel="stylesheet" type="text/css" href="Css/StyleSheet1.css" />
</head>
<body>
    <form id="form1" runat="server">
      <div id="wrapper">
            <div id="banner">
            </div>
            <div id="navigation">
               <ul class="nav">
                   <li><asp:LinkButton ID="linkHome" runat="server" Text="Home" OnClick="linkHome_Click"/></li>
                   <li><asp:LinkButton ID="linkSignIn" runat="server" Text="Sign in" OnClick="linkSignIn_Click"/></li>
                   <li><asp:LinkButton ID="signUpUser" runat="server" Text="Sign up" OnClick="linkSignUp_Click"/></li>
                   <li><asp:LinkButton ID="linkUsersOnly" runat="server" Text="Users Only" OnClick="linkUsersOnly_Click"/></li>
                   <li><asp:LinkButton ID="linkAbout" runat="server" Text="About" OnClick="linkAbout_Click"/></li>

               </ul>
            </div>

            <div id="content_area" runat="server">
               <asp:PlaceHolder ID="PlaceHolder1" runat="server" EnableViewState="false"></asp:PlaceHolder>
                <asp:Panel runat="server" ID="homePanel">
                   <h2>Some on Checkers</h2>
                   <h3>
                       Draughts or checkers (American English) is a group of strategy board games for two players which involve diagonal moves of uniform game pieces and mandatory captures by jumping over opponent pieces. 
                       Draughts developed from alquerque.
                       The name derives from the verb to draw or to move.
                       The most popular forms are English draughts, also called American checkers, played on an 8×8 checkerboard;
                       Russian draughts, also played on an 8×8; and international draughts, played on a 10×10 board. 
                       There are many other variants played on an 8×8, and Canadian checkers is played on a 12×12 board.
                   </h3>
                </asp:Panel>

               <asp:Panel runat="server" ID="usersOnlyPanel">
                   What would you like to do?? &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:DropDownList ID="usersOnlyDropDownList" runat="server" 
                       OnSelectedIndexChanged="UsersOnlyDropDownList_OnSelectedChange" AutoPostBack="True">
                       <asp:ListItem Text="" />
                       <asp:ListItem Text="Ask Us" />
                       <asp:ListItem Text="Make Changes" />
                   </asp:DropDownList>

                   <br />
                   <br />

                   <asp:Panel runat="server" ID="askUsPanel">
                            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
                            <asp:Panel ID="pnlHeader1" runat="server" Width="750px" BackColor="#00CC99" Height="50px">
                                Show me all Users
                                <asp:Image ID="imgToggle1" runat="server" Height="19px" ImageUrl="~/Images/ic_expand_more_48px-128.png" style="margin-left: 637px" Width="28px" />
                            </asp:Panel>

                            <asp:Panel ID="pnlInfo1" runat="server" BackColor="Silver" Width="750px">
                               
                            </asp:Panel>
                            <ajaxToolkit:CollapsiblePanelExtender ID="pnlInfo1_CollapsiblePanelExtender" runat="server" BehaviorID="pnlInfo1_CollapsiblePanelExtender" CollapseControlID="pnlHeader1" Collapsed="True" CollapsedImage="ic_expand_more_48px-128.png" ExpandControlID="pnlHeader1" ExpandedImage="ic_collapse.png" ImageControlID="imgToggle1" TargetControlID="pnlInfo1" />

                            <br />
                            <br />
                            <asp:Panel ID="pnlHeader2" runat="server" Width="750px" BackColor="#00CC99" Height="50px">
                                Show me all Games
                                <asp:Image ID="imgToggle2" runat="server" Height="19px" ImageUrl="Images/ic_expand_more_48px-128.png" style="margin-left: 637px" Width="28px" />
                            </asp:Panel>

                            <asp:Panel ID="pnlInfo2" runat="server" BackColor="Silver" Width="750px">
                                


                            </asp:Panel>
                            <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" 
                                BehaviorID="pnlInfo2_CollapsiblePanelExtender" CollapseControlID="pnlHeader2"
                                Collapsed="True" CollapsedImage="ic_expand_more_48px-128.png" 
                                ExpandControlID="pnlHeader2" ExpandedImage="ic_collapse.png" 
                                ImageControlID="imgToggle2" TargetControlID="pnlInfo2" />
                
                            <br />
                            <br />

                             <asp:Panel ID="pnlHeader3" runat="server" Width="750px" BackColor="#00CC99" Height="50px">
                                Get all Games for user:  &nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:DropDownList ID="usersDropDownList" runat="server" Height="16px" Width="156px"
                                     OnSelectedIndexChanged="usersDropDownList_SelectedIndexChanged" AutoPostBack="true" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Image ID="imgToggle3" runat="server" Height="19px" ImageUrl="/Images/ic_expand_more_48px-128.png" 
                                     style="margin-left: 637px" Width="28px" />
                            </asp:Panel>

                            <asp:Panel ID="pnlInfo3" runat="server" BackColor="Silver" Width="750px" />

                            <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server"  
                                BehaviorID="pnlInfo3_CollapsiblePanelExtender" CollapseControlID="pnlHeader3"
                                Collapsed="True" CollapsedImage="ic_expand_more_48px-128.png" 
                                ExpandedImage="ic_collapse.png" ImageControlID="imgToggle3" TargetControlID="pnlInfo3" />
                   
                            <br />
                            <br />

                             <asp:Panel ID="pnlHeader4" runat="server" Width="750px" BackColor="#00CC99" Height="50px">
                                Get all Users for game:  &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="gamesDropDownList" runat="server" Height="16px" Width="156px">
                                 </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Image ID="imgToggle4" runat="server" Height="19px" ImageUrl="~/Images/ic_expand_more_48px-128.png" style="margin-left: 637px" Width="28px" />
                            </asp:Panel>

                            <asp:Panel ID="pnlInfo4" runat="server" BackColor="Silver" Width="750px">

                                <br />
                                <asp:Repeater ID="pnl" runat="server">
                                    <ItemTemplate>
                                        <table runat="server" style="color:white; background-color:lavender;" visible="true" id="headerTableUsers">
                                            <tr>
                                                <td style="width: 120px; background-color: #3A4F63; color: white;">Game ID</td>
                                                <td style="width: 120px; background-color: #3A4F63; color: white;">Game Name</td>
                                                <td style="width: 120px; background-color: #3A4F63; color: white;">First Player Name</td>
                                                <td style="width: 120px; background-color: #3A4F63; color: white;">Second Player Name</td>
                                            </tr>
                                        </table>
                                        <!--These are the actual data items -->
                                        <table>
                                            <tr>
                                                <td style="width: 200px;">
                                                    <asp:Label ID="lblGamesTableGameID" runat="server" Text='<%#Eval("GameID") %>'></asp:Label>
                                                </td>
                                                <td style="width: 200px;">
                                                    <asp:Label ID="lblGamesTableGameName" runat="server" Text='<%#Eval("GameName") %>'></asp:Label>
                                                </td>
                                                <td style="width: 200px;">
                                                    <asp:Label ID="lblGamesTableGamePlayer1" runat="server" Text='<%#Eval("Player1") %>'></asp:Label>
                                                </td>
                                                <td style="width: 200px;">
                                                    <asp:Label ID="lblGamesTableGamePlayer2" runat="server" Text='<%#Eval("Player2") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </asp:Panel>
                            <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" BehaviorID="pnlInfo4_CollapsiblePanelExtender" CollapseControlID="pnlHeader4" Collapsed="True" CollapsedImage="ic_expand_more_48px-128.png" ExpandControlID="pnlHeader4" ExpandedImage="ic_collapse.png" ImageControlID="imgToggle4" TargetControlID="pnlInfo4" />
                   

                             
                   </asp:Panel>
                            
                   <asp:Panel runat="server" ID="updatesPanel">
                   </asp:Panel>

               </asp:Panel>

                <asp:Panel runat="server" ID="signInPanel">
                    <div class="imgContainer">
                        <img src="Images/man.jpg" alt="Avatar" class="avatar" />
                    </div>

                    <asp:Label id="usersOnlyWarning" runat="server" Text="  You must Register before" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
                    <div class="container">
                        <label><b>Username</b></label>
                        <asp:TextBox  ID="textBoxSignInUname"  runat="server" />
                        <br />
                        <label><b>Password</b></label>
                        <asp:TextBox ID="textBoxSignInPsw" TextMode="Password" runat="server"  />
                        <br />
                        <asp:Button type="submit" runat="server" Text="Login" OnClick="btnSignIn_Click"/>
                    </div>
                </asp:Panel>


                <asp:Panel runat="server" ID="signUpPanel">
                    <div class="imgContainer">
                        <img src="Images/man.jpg" alt="Avatar" class="avatar" />
                    </div>
                    <b>many users to sign up?</b> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="textBoxNumberOfUsers" runat="server" Width="55px" Height="16px"></asp:TextBox>  
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNumberOfUsers" runat="server" Text="Start" OnClick="btnNumberOfUsers_Click" />
                    
                    <asp:Panel runat="server" ID="usersContainerPanel">
                    </asp:Panel>

                    <asp:Button ID="btnSignUp" runat="server" Text="Submit" OnClick="btnSignUp_Click" />
                </asp:Panel>


                <asp:Panel runat="server" ID="aboutPanel">
                   <label><b>Vitali Karabitski ID: 317721652</b></label>
                    <br />
                   <label><b>Dana Schvarzman ID:305699282</b></label>
                </asp:Panel> 

                <br />
                <br/>
                
            </div>


            <div id="sidebar">
            </div>
            <div id="footer">
                <p>Dotnet final project - August 2016</p>
            </div>
        </div>
    </form>
</body>
</html>
