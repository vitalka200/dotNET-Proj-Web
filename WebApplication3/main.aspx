<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="WebApplication3.main" EnableEventValidation="false" ValidateRequest="false"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web</title>
    <script type="text/javascript" src="../JavaScript/jquery-1.3.2.min.js"></script>
    <link rel="stylesheet" type="text/css" href="Css/StyleSheet1.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
      <div id="wrapper">
            <div id="banner">
            </div>
            <div id="navigation">
               <ul class="nav">
                   <li><asp:LinkButton ID="linkHome" runat="server" Text="Home" OnClick="linkHome_Click" CausesValidation="false"/></li>
                   <li><asp:LinkButton ID="linkaskUs" runat="server" Text="Ask us" OnClick="linkAskUs_Click" CausesValidation="false"/></li>
                   <li><asp:LinkButton ID="linkUpdateInfo" runat="server" Text="Updates" OnClick="linkUpdateInfo_Click" CausesValidation="false"/></li>
                   <li><asp:LinkButton ID="linkGames" runat="server" Text="Games" OnClick="linkGames_Click" CausesValidation="false"/></li>
                   <li><asp:LinkButton ID="linkAbout" runat="server" Text="About" OnClick="linkAbout_Click" CausesValidation="false"/></li>

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

                
                <asp:Panel runat="server" ID="askUsPanel">
                   
                    <asp:Panel ID="pnlHeader1" runat="server" Width="750px" BackColor="#00CC99" Height="50px">
                        Show me all Users
                        <asp:Image ID="imgToggle1" runat="server" Height="19px" ImageUrl="~/Images/ic_expand_more_48px-128.png" 
                            style="margin-left: 637px" Width="28px" />
                    </asp:Panel>

                    <asp:Panel ID="pnlInfo1" runat="server" BackColor="Silver" Width="750px">
                               
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="pnlInfo1_CollapsiblePanelExtender" runat="server"
                            BehaviorID="pnlInfo1_CollapsiblePanelExtender" CollapseControlID="pnlHeader1"
                            Collapsed="True" CollapsedImage="~/images/ic_expand_more_48px-128.png"
                            ExpandControlID="pnlHeader1" ExpandedImage="~/images/ic_collapse.png"
                            ImageControlID="imgToggle1" TargetControlID="pnlInfo1" />

                    <br />
                    <br />
                    <asp:Panel ID="pnlHeader2" runat="server" Width="750px" BackColor="#00CC99" Height="50px">
                        Show me all Games
                        <asp:Image ID="imgToggle2" runat="server" Height="19px" ImageUrl="Images/ic_expand_more_48px-128.png" 
                            style="margin-left: 637px" Width="28px" />
                    </asp:Panel>

                    <asp:Panel ID="pnlInfo2" runat="server" BackColor="Silver" Width="750px">
                                


                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" 
                        BehaviorID="pnlInfo2_CollapsiblePanelExtender" CollapseControlID="pnlHeader2"
                        Collapsed="True" CollapsedImage="~/images/ic_expand_more_48px-128.png" 
                        ExpandControlID="pnlHeader2" ExpandedImage="~/images/ic_collapse.png" 
                        ImageControlID="imgToggle2" TargetControlID="pnlInfo2" />
                
                    <br />
                    <br />                               
                       
                        <asp:Panel ID="pnlHeader3" runat="server" Width="750px" BackColor="#00CC99" Height="50px">
                        Get all Games for user:
                            <asp:DropDownList ID="UsersDropDownList" runat="server" AutoPostBack="true" 
                                EnableViewState="true" OnInit="UsersDropDownList_Init" 
                                OnSelectedIndexChanged="UsersDropDownList_SelectedIndexChanged" ViewStateMode="Enabled">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Image ID="imgToggle3" runat="server" Height="19px" ImageUrl="/Images/ic_expand_more_48px-128.png" 
                                style="margin-left: 637px" Width="28px" />
                        </asp:Panel>
                            
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" OnUnload="UpdatePanel_UnLoad">
                            <ContentTemplate>
                            <asp:Panel ID="pnlInfo3" runat="server" BackColor="Silver" Width="750px">
                                <br />
                            </asp:Panel>   
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="UsersDropDownList" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>    
                   
                    <br />
                    <br />

                        <asp:Panel ID="pnlHeader4" runat="server" Width="750px" BackColor="#00CC99" Height="50px">
                        Get all Users for game:  &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="gamesDropDownList" runat="server" AutoPostBack="true" EnableViewState="true" Height="16px" OnInit="gamesDropDownList_Init" OnSelectedIndexChanged="gamesDropDownList_SelectedIndexChanged" ViewStateMode="Enabled" Width="156px">
                            </asp:DropDownList>
                            <asp:Image ID="imgToggle4" runat="server" Height="19px" ImageUrl="/Images/ic_expand_more_48px-128.png" style="margin-left: 637px" Width="28px" />
                        </asp:Panel>

                        <asp:UpdatePanel ID="UpdatePanel" runat="server" OnUnload="UpdatePanel_UnLoad">
                            <ContentTemplate>
                            <asp:Panel ID="pnlInfo4" runat="server" BackColor="Silver" Width="750px">
                                <br />
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gamesDropDownList" EventName="SelectedIndexChanged" />
                        </Triggers>
                        </asp:UpdatePanel>                
                       
                    <br />
                    <br />
                          
                        <asp:Panel ID="pnlHeader5" runat="server" Width="750px" BackColor="#00CC99" Height="50px">
                            Show how many games for each player
                            <asp:Image ID="ingToggle5" runat="server" Height="19px" 
                            ImageUrl="Images/ic_expand_more_48px-128.png" style="margin-left: 637px" Width="28px" />
                        </asp:Panel>

                        <asp:Panel ID="pnlInfo5" runat="server" BackColor="Silver" Width="750px">
                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="server" 
                            BehaviorID="pnlInfo5_CollapsiblePanelExtender" CollapseControlID="pnlHeader5"
                            Collapsed="True" CollapsedImage="~/images/ic_expand_more_48px-128.png" 
                            ExpandControlID="pnlHeader5" ExpandedImage="~/images/ic_collapse.png" 
                            ImageControlID="imgToggle5" TargetControlID="pnlInfo5" />
                    <br />
            </asp:Panel>

                <asp:UpdatePanel runat="server" ID="updatesPanel" UpdateMode="Conditional" OnUnload="UpdatePanel_UnLoad">
                    <ContentTemplate>
                         <table>
                            <tr>
                                <td><asp:Label ID="userIDLabel" runat="server" Text="userID: "></asp:Label></td>
                                <td><asp:TextBox ID="txtIDLabel" runat="server" Enabled="false"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="lblUpdateInfoUserName" runat="server" Text="Name: "></asp:Label></td>
                                <td><asp:TextBox ID="txtUpdateInfoUserName" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="lblUpdateInfoUserLastName" runat="server" Text="Last name: "></asp:Label></td>
                                <td><asp:TextBox ID="txtUpdateInfoUserLastName" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="lblUpdateInfoUserPassowrd" runat="server" Text="Password"></asp:Label>:</td>
                                <td><asp:TextBox ID="txtUpdateInfoUserPassword" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                        <asp:Label ID="lblUpdates" runat="server" Font-Bold="True" 
                            Font-Names="Comic Sans MS" Font-Size="X-Large" Visible="false"/><br />
                         <br />

                         <br />
                        <asp:Button ID="btnUpdateInfo" runat="server" Text ="Save Changes" OnClick="btnUpdateInfo_Click"/>&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="deleteUser" runat="server" Text="Delete User" Width="118px" OnClick="deleteUser_Click" />
                
                        <br /><br />
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:Panel ID="gamesPanel" runat="server">
                    <asp:Label ID="lblGamePanel" runat="server" Font-Bold="True" 
                            Font-Names="Comic Sans MS" Font-Size="X-Large" Visible="false"/> <br /><br />
                    <asp:Repeater ID="Repeater1" runat="server" >
                        <ItemTemplate>
                            <div>
                                <table border="1">
                                    <tr><th colspan="2">Game <%#Eval("Id")%></th></tr>
                                    <tr><td>Game Id: </td><td><%#Eval("Id")%></td></tr>
                                    <tr><td>Crated at: </td><td><%#Eval("CreatedDateTime")%></td></tr>
                                    <tr><td>Status: </td><td><%#Eval("GameStatus")%></td></tr>
                                    <tr><td>Player1: </td><td><%#Eval("Player1")%></td></tr>
                                    <tr><td>Player2: </td><td><%#Eval("Player2")%></td></tr>
                                    <tr><td>Winner: </td><td><%#Eval("WinnerPlayerNum")%></td></tr>
                                </table>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Label ID="lblGameID" Text="GameID" Font-Bold="true" Font-Size="Large" Font-Names="Comic Sans MS" runat="server" />&nbsp;&nbsp;
                    <asp:TextBox ID="txtGameID" runat="server" Height="5px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorGameID" runat="server" ErrorMessage="Game id is required" ControlToValidate="txtGameID">
                    </asp:RequiredFieldValidator>
                    <br />
                    <asp:Button ID="btnCreateGame" runat="server" Text="New game" OnClick="btnCreateGame_Click" CausesValidation="false"/>&nbsp;&nbsp;
                    <asp:Button ID="btnRegisterGame" runat="server" Text="Register" OnClick="btnRegisterGame_Click" /> &nbsp;&nbsp;
                    <asp:Button ID="btnDeleteGame" runat="server" Text="Delete" OnClick="btnDeleteGame_Click" />
                    <br /><br />
                    <asp:Label ID="lblControlGame" runat="server" Font-Bold="true" />
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
                        <asp:Button ID="btnSignIn" runat="server" Text="Login" OnClick="btnSignIn_Click"/>
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
                    
                </asp:Panel>

                <asp:Panel runat="server" ID="usersContainerPanel">
                     
                     <asp:Button ID="btnSignUp" runat="server" Text="Submit" OnClick="btnSignUp_Click" />
                </asp:Panel>

                <asp:Panel runat="server" ID="LoginSuccessPanel">
                      <div class="imgContainer">
                        <img src="Images/success.png" alt="Avatar" class="avatar" />
                      </div>
                      <asp:Label runat="server" Text="Successfully Login" Font-Bold="True" Font-Italic="True" Font-Names="Comic Sans MS" Font-Size="X-Large" ForeColor="#00CC66" />
                </asp:Panel>

                <asp:Panel runat="server" ID="LoginFailedPanel">
                      <div class="imgContainer">
                        <img src="Images/error.png" alt="Avatar" class="avatar" />
                      </div>
                      <asp:Label runat="server" Text="Failed to Login" Font-Bold="True" Font-Italic="True" Font-Names="Comic Sans MS" Font-Size="X-Large" ForeColor="Red" />
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
                <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <asp:LinkButton ID="linksignUp" runat="server" Text="Sign up" OnClick="linkSignUp_Click" CausesValidation="false"/><br />
                <asp:LinkButton ID="linkSignIn" runat="server" Text="Log in" OnClick="linkSignIn_Click" CausesValidation="false"/><br />
                <asp:LinkButton ID="linkSignOut" runat="server" Text="Log Out" OnClick="linkSignOut_Click" CausesValidation="false" />
            </div>
            <div id="footer">
                <p>Dotnet final project - August 2016</p>
            </div>
        </div>
    </form>
</body>
</html>
