<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoReview.aspx.cs" Inherits="onlineExam.Pages.DoReview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <style>
        .loginCover{
            position:fixed;
            left:0;
            right:0;
            bottom:0;
            top:0;
            background-color:white;
        }
         .centerBlock{
            position:absolute;
            left:50%;
            top:50%;
            transform:translate(-50%,-50%);
        }
        .answerBlock{
            width:80%;
        }
        .formView{
            width:100%;
        }
        .flexContainer{
            display:flex;
            justify-content:center;
            width:100%;
        }
        .flexLeft{
            flex:0 0 auto;
            width:300px;
            height:80vh;
            overflow:auto;
        }
        .flexRight{
            flex:1 1 auto;

        }
        .flexItems{
            display:flex;
            width:100%;
            flex-wrap:wrap;
            align-content:center;
            justify-content:center;
        }
        .flexItemLeft{
            flex:0 0 auto;
            width:20%;
        }
        .flexItemRight{
            flex:1 1 auto;
            width:75%;
        }
        .header5v{
            height:5vh;
        }
        .header{
            height:10vh;
            overflow:auto;
        }
        .myButton {
            margin-top:10px;
	background-color:#44c767;
	-moz-border-radius:28px;
	-webkit-border-radius:28px;
	border-radius:28px;
	border:1px solid #18ab29;
	display:inline-block;
	cursor:pointer;
	color:#ffffff;
	font-family:Arial;
	font-size:17px;
	padding:16px 31px;
	text-decoration:none;
	text-shadow:0px 1px 0px #2f6627;
}
.myButton:hover {
	background-color:#5cbf2a;
}
.myButton:active {
	position:relative;
	top:1px;
}
.myButton1 {
	-moz-box-shadow: 0px 0px 0px 2px #9fb4f2;
	-webkit-box-shadow: 0px 0px 0px 2px #9fb4f2;
	box-shadow: 0px 0px 0px 2px #9fb4f2;
	background-color:#7892c2;
	-moz-border-radius:10px;
	-webkit-border-radius:10px;
	border-radius:10px;
	border:1px solid #4e6096;
	display:inline-block;
	cursor:pointer;
	color:#ffffff;
	font-family:Arial;
	font-size:19px;
	padding:12px 37px;
	text-decoration:none;
	text-shadow:0px 1px 0px #283966;
}
.myButton1:hover {
	background-color:#476e9e;
}
.myButton1:active {
	position:relative;
	top:1px;
}
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ObjectDataSource ID="ObjectDataSource7" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllExams" TypeName="onlineExam.BLL.ExamBLL" OnSelected="ObjectDataSource7_Selected"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource8" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAssignmentsByExam" TypeName="onlineExam.BLL.ExamBLL">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList2" DefaultValue="0" Name="exId" PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="TextBox7" DefaultValue="" Name="sId" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="TextBox8" Name="sName" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="DropDownList3" DefaultValue="" Name="status" PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="TextBox9" Name="sheetId" PropertyName="Text" Type="Int32" DefaultValue="" />
                            <asp:ControlParameter ControlID="TextBox10" Name="classId" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetSheetByAssignmentId" TypeName="onlineExam.BLL.SheetBLL" DataObjectTypeName="onlineExam.Models.Sheet" OnUpdating="ObjectDataSource1_Updating" UpdateMethod="UpdateSheetForReview">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="GridView2" Name="assId" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
             <div style="background-color :sandybrown">第一步：点顶部阅卷评分；第二步：选择考试场次，输入班级代码，点“查询试卷列表”；第三步：点击左侧试卷项，在右侧打分，在下方点保存按钮保存评分；第四步：所有试卷评分结束后点导出下载试卷按钮</div>

            <div class="header5v">
            <asp:Menu ID="Menu1" runat="server" BackColor="#B5C7DE" RenderingMode="List" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal" StaticSubMenuIndent="10px">
                <DynamicHoverStyle BackColor="#284E98" ForeColor="White" />
                <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <DynamicMenuStyle BackColor="#B5C7DE" />
                <DynamicSelectedStyle BackColor="#507CD1" />
                <Items>
                    <asp:MenuItem Text="阅卷评分" Value="0"></asp:MenuItem>
                    <asp:MenuItem Text="成绩列表" Value="1"></asp:MenuItem>
                </Items>
                <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <StaticSelectedStyle BackColor="#507CD1" />
            </asp:Menu>
                
            </div>
            <div class="header">
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="ObjectDataSource7" DataTextField="name" DataValueField="ExamId" AppendDataBoundItems="True" OnDataBound="DropDownList2_DataBound">
                        <asp:ListItem Value="0">请选择考试场次</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList3" runat="server">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="1">待考</asp:ListItem>
                        <asp:ListItem Value="2">考试</asp:ListItem>
                        <asp:ListItem Value="3">完成</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TextBox7" placeholder="学生学号" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox8" placeholder="学生姓名" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox9" type="number" placeholder="试卷编号" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox10" placeholder="班级编号" runat="server"></asp:TextBox>
                    <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="查询试卷列表" />
                    <asp:Button ID="Button10" runat="server" Text="刷新考试信息" OnClick="Button10_Click" />
            <asp:Button ID="Button11" runat="server" OnClick="Button11_Click" Text="导出下载试卷" />
            <asp:Button ID="Button2" runat="server"  Text="导出本人批阅的试卷" OnClick="Button2_Click" />
            <br />
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            </div>            
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="2">
                <asp:View ID="View1" runat="server">
                    
                    <div class="flexContainer">
                        <div class="flexLeft">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource8" CellPadding="4" DataKeyNames="AssignmentId" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField  HeaderText="试卷列表">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text='<%# Convert.ToString(Eval("Student.StudentId"))+" "+Convert.ToString(Eval("Student.name")) %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                           
                            
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <EmptyDataTemplate>
                            请先选择考试场次，输入相关查询条件，点查询按钮获取阅卷列表，如果查询后依旧看到本信息，说明没有满足查询条件的试卷
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                            </div>
                        <div class="flexRight">
                        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" DefaultMode="Edit" CssClass="formView" OnItemCommand="FormView1_ItemCommand">
            <EditItemTemplate>
                
                <asp:TextBox ID="SheetIdTextBox" Visible="false" runat="server" Text='<%# Bind("SheetId") %>'  />
                
                <asp:TextBox ID="timestampTextBox" Visible="false" runat="server" Text='<%# Bind("timestamp") %>' />

                <asp:TextBox ID="answersTextBox" Visible="false" runat="server" Text='<%# Bind("answers") %>'  />

                <asp:TextBox ID="qOrdersTextBox" Visible="false" runat="server" Text='<%# Bind("qOrders") %>' />

                <asp:TextBox ID="qOffsTextBox" Visible="false" runat="server" Text='<%# Bind("qOffs") %>' />

                <asp:TextBox ID="qAnsTextBox" Visible="false" runat="server" Text='<%# Bind("qAns") %>' />

                <asp:TextBox ID="qScoresTextBox" Visible="false" runat="server" Text='<%# Bind("qScores") %>' />
                <div class="flexItems">
                    <div class="flexItemLeft">
                第一小题回答:<asp:TextBox ID="TextBox1" type="number" value="0" min="0" max="30" runat="server"></asp:TextBox>
                    </div>
                <div class="flexItemRight">
                <asp:TextBox ID="answer1TextBox" Enabled="false" runat="server" Text='<%# Eval("answer1") %>' Rows="10" TextMode="MultiLine" CssClass="answerBlock"/>
                </div>
                <div class="flexItemLeft">
                第二小题回答:<asp:TextBox ID="TextBox2" type="number" value="0" min="0" max="30" runat="server"></asp:TextBox>
                </div>
                <div class="flexItemRight">
                <asp:TextBox ID="answer2TextBox" Enabled="false" runat="server" Text='<%# Bind("answer2") %>' Rows="10" TextMode="MultiLine"  CssClass="answerBlock" />
                </div>
                <div class="flexItemLeft">
                第三小题回答:<asp:TextBox ID="TextBox3" type="number" value="0" min="0" max="30" runat="server"></asp:TextBox>
                </div>
                <div class="flexItemRight">
                <asp:TextBox ID="answer3TextBox" Enabled="false" runat="server" Text='<%# Bind("answer3") %>' Rows="10" TextMode="MultiLine" CssClass="answerBlock"/>
                </div>
                
                <div class="flexItemLeft">
                客观题得分:
                    </div>
                    <div class="flexItemRight">
                <asp:TextBox ID="score1TextBox"  Enabled="false" runat="server" Text='<%# ShowScore(Eval("answers"),Eval("qAns"),Eval("qScores")) %>' />
                </div>
                        <div class="flexItemLeft">
                主观题得分:
                            </div>
                    <div class="flexItemRight">
                <asp:TextBox ID="score2TextBox" Enabled="false" runat="server" Text='<%# Bind("score2") %>' />
                        </div>
                <div class="flexItemLeft">
                阅卷人:
                    </div>
                    <div class="flexItemRight">
                <asp:TextBox ID="markerTextBox"  Enabled="false" runat="server" Text='<%# Bind("marker") %>' />
                        </div>
              </div>
                
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" CssClass="myButton" Text="保存" />
                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="myButton1" CommandName="PrevEntry">上一条</asp:LinkButton>
                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="myButton1" CommandName="NextEntry">下一条</asp:LinkButton>
            </EditItemTemplate>
           
           
        </asp:FormView>
                            </div>
                        </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="ObjectDataSource8" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="AssignmentId" HeaderText="试卷编号" SortExpression="AssignmentId" />
                            <asp:BoundField DataField="Student.StudentId" HeaderText="学号" />
                            <asp:BoundField DataField="Student.name" HeaderText="姓名" />
                            <asp:BoundField DataField="Sheet.score1" HeaderText="客观题总分" SortExpression="Sheet" />
                            <asp:BoundField DataField="Sheet.score2" HeaderText="主观题总分" SortExpression="Sheet" />
                            <asp:BoundField DataField="Sheet.marker" HeaderText="阅卷人" />
                            <asp:BoundField DataField="ipAddress" HeaderText="IP地址" SortExpression="ipAddress" />
                            <asp:CheckBoxField DataField="sheetSubmited" HeaderText="是否交卷" SortExpression="sheetSubmited" />
                            <asp:BoundField DataField="firstLogin" HeaderText="初次登陆时间" SortExpression="firstLogin" />
                            <asp:BoundField DataField="lastLogin" HeaderText="最后登陆时间" SortExpression="lastLogin" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <EmptyDataTemplate>
                            请先选择考试场次，输入相关查询条件，点查询按钮获取阅卷列表，如果查询后依旧看到本信息，说明没有满足查询条件的试卷
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <div class="loginCover">
                        <div class="centerBlock">
                            <h1>阅卷入口,请输入阅卷人姓名和阅卷密码</h1>
                        <asp:TextBox ID="TextBox4" placeholder="阅卷人" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox4" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="TextBox5" placeholder="密码" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox5" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:Button ID="Button1" runat="server" Text="登陆" OnClick="Button1_Click" />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBox5" ErrorMessage="登陆密码错误" ForeColor="Red" ValueToCompare="7777777"></asp:CompareValidator>
                        </div>
                        
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
        <br />
        <asp:Label ID="Label3" runat="server" Visible="false" Text=""></asp:Label>
          <br />
        
        <br />
    </form>
</body>
</html>
