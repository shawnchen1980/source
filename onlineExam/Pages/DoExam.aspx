<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoExam.aspx.cs" Inherits="onlineExam.Pages.DoExam" ViewStateEncryptionMode="Never" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .container{
            position:relative;
            width:100%;

        }
        .left{
            position:absolute;
            width:20%;
        }
        .main{
            position:absolute;
            width:80%;
            left:20%;
            padding:20px 50px 10px 50px;
            border-left:2px solid black;
            min-height:100vh;
            max-width:700px;

        }
        .qListZone{
            display:flex;
            flex-wrap: wrap;
        }
        .qListItem{
            flex:1 1 33%;
        }
        .cItem{
            border:1px solid black;
        }
        .checked::after{
            content:"\2713";
        }
        body{
            width:100vw;
            height:100vh;
            position:relative;
            background-color: #FBAB7E;
            background-image: linear-gradient(62deg, #FBAB7E 0%, #F7CE68 100%);

            
        }
        .centerBlock{
            position:absolute;
            left:50%;
            top:50%;
            transform:translate(-50%,-50%);
        }
        .answerBlock{
            width:100%;
            height:200px;
        }
        .submitButton{
            display:block;
            width:50%;
            margin:auto;
        }
        .myButton {
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

.myButton2 {
	-moz-box-shadow:inset 0px 1px 0px 0px #cf866c;
	-webkit-box-shadow:inset 0px 1px 0px 0px #cf866c;
	box-shadow:inset 0px 1px 0px 0px #cf866c;
	background-color:#d0451b;
	-moz-border-radius:3px;
	-webkit-border-radius:3px;
	border-radius:3px;
	border:1px solid #942911;
	display:inline-block;
	cursor:pointer;
	color:#ffffff;
	font-family:Arial;
	font-size:13px;
	padding:6px 24px;
	text-decoration:none;
	text-shadow:0px 1px 0px #854629;
    margin-top:40px;
}
.myButton2:hover {
	background-color:#bc3315;
}
.myButton2:active {
	position:relative;
	top:1px;
}
    </style>
    <script>
        function openMaxWin() {

            //let numberOfEntries = window.history.length;
            //window.history.go(-1 * numberOfEntries + 1);
            //window.location.href = "http://www.mozilla.org";

            var params = [
                'height=' + screen.height,
                'width=' + screen.width,
                'fullscreen=yes' // only works in IE, but here for completeness
            ].join(',');
            // and any other options from
            // https://developer.mozilla.org/en/DOM/window.open

            var popup = window.open('/pages/submitted.html', 'popup_window', params);
            popup.moveTo(0, 0);
            
        }
        
    </script>
</head>
<body>
    
    <form id="form1" runat="server">
        <div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAssignmentsForLogin" TypeName="onlineExam.BLL.AssignmentBLL" OnDataBinding="ObjectDataSource1_DataBinding" OnSelecting="ObjectDataSource1_Selecting" ConflictDetection="OverwriteChanges" OnUpdating="ObjectDataSource1_Updating" UpdateMethod="UpdateAssignment" >
                <SelectParameters>
                    <asp:ControlParameter ControlID="TextBox1" DefaultValue="&quot;&quot;" Name="id" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="TextBox2" DefaultValue="&quot;&quot;" Name="name" PropertyName="Text" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="item" Type="Object" />
                    <asp:Parameter Name="origItem" Type="Object" />
                </UpdateParameters>
            </asp:ObjectDataSource>

            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" OnSelecting="ObjectDataSource2_Selecting" SelectMethod="GetSheetQsForExam" TypeName="onlineExam.BLL.SheetQBLL" UpdateMethod="UpdateSheetForExam"  OnUpdating="ObjectDataSource2_Updating" DataObjectTypeName="onlineExam.Models.SheetQ" >
                
                <UpdateParameters>
                    <asp:Parameter Name="item" Type="Object" />
                </UpdateParameters>
            </asp:ObjectDataSource>
<!--<SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="assId" Type="Int32" />
                </SelectParameters>-->
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <div class="centerBlock">
                <h1>华东政法大学军事理论考试</h1>
            <asp:TextBox ID="TextBox1" placeholder="输入学号" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:TextBox ID="TextBox2" placeholder="输入姓名" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:Button ID="Button1" runat="server" Text="登录" OnClick="Button1_Click" />
                <p style="color:red;">登录考试前请确认设备可以正常使用，登录后不得随意换机！</p>
                <p style="color:red;">
登陆时必须输入正确的学号和姓名，输入时使用中文半角输入，输入错误将无法进入考试系统。如果姓名中带有分隔点或空格的，只需输入分隔点或空格之前的部分即可，比如：恩卡尔·叶尔波，只需要输入 恩卡尔 即可，姓名中间，前后都不要有空格</p>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" OnDataBinding="GridView1_DataBinding" DataKeyNames="AssignmentId" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" EnableViewState="False" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="centerBlock">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="AssignmentId" HeaderText="任务编号" SortExpression="AssignmentId" />
                    <asp:TemplateField HeaderText="考试科目" SortExpression="name">
                        
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Exam.name") %>'></asp:Label>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ipAddress" HeaderText="登录IP地址" SortExpression="ipAddress" />
                    <asp:BoundField DataField="firstLogin" HeaderText="首次登录时间" SortExpression="firstLogin" />
                    <asp:BoundField DataField="lastLogin" HeaderText="最近登录时间" SortExpression="lastLogin" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Sheet.answers") %>' />
                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("Sheet.answer1") %>' />
                            <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Eval("Sheet.answer2") %>' />
                            <asp:HiddenField ID="HiddenField4" runat="server" Value='<%# Eval("Sheet.answer3") %>' />
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("AssignmentId", "{0}") %>' OnClick="LinkButton1_Click">进入考试</asp:LinkButton>
                            <asp:TextBox ID="TextBox3" runat="server" TextMode="Password" placeholder="当前ip地址与上一次登录时不同，请输入换机密码"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBox3" ErrorMessage="换机密码错误，请重试" ForeColor="Red" ValueToCompare="88888888"></asp:CompareValidator>
                            <asp:Label ID="Label13" runat="server" Text="已交卷" Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataTemplate>
                    <p>没有找到你的考试任务，请确认用户名和密码是否输入正确并再次登录</p>
                    <button onclick="window.history.go(-1);return false;">返回登录</button>
                </EmptyDataTemplate>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="container">
            <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource2" AllowPaging="True" DefaultMode="Edit" OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating" OnItemUpdated="FormView1_ItemUpdated" OnPageIndexChanging="FormView1_PageIndexChanging">
                <EditItemTemplate>
                    <div class="main">
                    <p style="color:red;">注意事项：所有题目回答完毕前必须点击题目底部的保存按钮,答题过程中可以分多次保存,题目编号打勾的为保存过的答题，没有保存过的答题不得分</p>
                    <h2>
                    问题区
                    </h2>
                    
                    <h3>
                    
                    
                    <asp:Label ID="qtext1Label2" runat="server" Text='<%# Eval("QTemplate.qtext1") %>' EnableViewState="true" />
                    </h3>
                    
                    <asp:Label ID="qtext2Label2" runat="server" Text='<%# Eval("QTemplate.qtext2") %>' EnableViewState="true" />
                    <br />
                    <!--以下隐藏信息不可去掉，否则答案将无法正常修改 -->
                    <asp:Label ID="SheetQIdTextBox" Visible="false" runat="server" Text='<%# Bind("SheetQId") %>' />
                    <asp:Label ID="Label12" runat="server" Visible="false" Text='<%# Eval("QTemplate.qType") %>' />
                    <asp:Label ID="Label3" runat="server" Visible="false" Text='<%# Eval("Sheet.SheetId") %>' />
                    
                    <h3>
                    回答区:
                    </h3>
                    
                    <asp:Panel ID="Panel12" runat="server">
                    <asp:RadioButtonList ID="RadioButtonList12" runat="server" >
                        
                    </asp:RadioButtonList>
                    </asp:Panel>
                    <asp:Panel ID="Panel22" runat="server">
                        <asp:CheckBoxList ID="CheckBoxList12" runat="server"></asp:CheckBoxList>
                    </asp:Panel>
                    <asp:Panel ID="Panel42" runat="server">
                        第一小题：
                        <br />
                        <asp:TextBox ID="TextBox12" CssClass="answerBlock" runat="server"  TextMode="MultiLine" EnableViewState="True" />
                        <br />
                        第二小题：
                        <br />
                        <asp:TextBox ID="TextBox22" CssClass="answerBlock" runat="server"  TextMode="MultiLine" EnableViewState="True" />
                        <br />
                        第三小题：
                        <br />
                        <asp:TextBox ID="TextBox32" CssClass="answerBlock" runat="server"  TextMode="MultiLine" EnableViewState="True" />
                    </asp:Panel>
                    

                    
                    
                    <br />
                                        <br />
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CssClass="myButton" OnClick="UpdateButton_Click"  Text="保存" />
                        <asp:linkbutton id="Linkbutton2" text="上一题"  commandname="Page" CssClass="myButton1"   commandargument="Prev"   runat="Server"/> 
                        <asp:linkbutton id="NextButton" text="下一题"  commandname="Page" CssClass="myButton1"  commandargument="Next"   runat="Server"/> 
                 </div>
                    
                </EditItemTemplate>
                
                <PagerSettings Position="Top" />
                <PagerTemplate>
                    <div class="left">
                        <h3>学号：
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
                        <h3>姓名：<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h3>
                        <hr />
                        <h4>答题列表</h4>
                        <div class="qListZone">
                        <asp:Repeater ID="rptPagesHistory" runat="server" OnItemDataBound="rptPagesHistory_ItemDataBound" OnLoad="rptPagesHistory_Load">
                            <ItemTemplate>
                                <div class="qListItem">
                                <asp:LinkButton ID="lnkPageNumber"  runat="server" CommandName="Page" OnClick="lnkPageNumberHistory_Click"  /><!--OnClick="lnkPageNumberHistory_Click"-->
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        </div>
                        <asp:Button ID="Button2" class="myButton2" runat="server" Text="交卷" OnClick="Button2_Click" OnClientClick="if(confirm('确认要交卷码？')) { openMaxWin(); return true;} return false;" />
                        
                    </div>
                    
                </PagerTemplate>
                
            </asp:FormView>
          </div>
            

        </asp:View>
        <asp:View ID="View4" runat="server">
            <h1 style="text-align:center;">考试结束，请离开考场！</h1>
        </asp:View>
    </asp:MultiView>
            
        </div>
    </form>
</body>
</html>
