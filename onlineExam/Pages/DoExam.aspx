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
            background-image: radial-gradient( circle farthest-corner at 12.3% 19.3%,  rgba(85,88,218,1) 0%, rgba(95,209,249,1) 100.2% );
        }
        .centerBlock{
            position:absolute;
            left:50%;
            top:50%;
            transform:translate(-50%,-50%);
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

            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" ConflictDetection="CompareAllValues" OldValuesParameterFormatString="orig{0}" OnSelecting="ObjectDataSource2_Selecting" SelectMethod="GetSheetQsForExam" TypeName="onlineExam.BLL.SheetQBLL" UpdateMethod="UpdateSheetQForExam" DataObjectTypeName="onlineExam.Models.SheetQ">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="assId" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="item" Type="Object" />
                    <asp:Parameter Name="origItem" Type="Object" />
                </UpdateParameters>
            </asp:ObjectDataSource>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <div class="centerBlock">
            <asp:TextBox ID="TextBox1" placeholder="输入学号" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:TextBox ID="TextBox2" placeholder="输入姓名" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:Button ID="Button1" runat="server" Text="登录" OnClick="Button1_Click" />
                <p>登录考试前请确认设备可以正常使用，登录后不得随意换机！</p>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" OnDataBinding="GridView1_DataBinding" DataKeyNames="AssignmentId" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" EnableViewState="False" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="centerBlock">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="AssignmentId" HeaderText="任务编号" SortExpression="AssignmentId" />
                    <asp:BoundField DataField="name" HeaderText="考试科目" SortExpression="name" />
                    <asp:BoundField DataField="ipAddress" HeaderText="登录IP地址" SortExpression="ipAddress" />
                    <asp:BoundField DataField="firstLogin" HeaderText="首次登录时间" SortExpression="firstLogin" />
                    <asp:BoundField DataField="lastLogin" HeaderText="最近登录时间" SortExpression="lastLogin" />
                    <asp:TemplateField>
                        <ItemTemplate>
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
            <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource2" AllowPaging="True" DefaultMode="Edit" OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating" OnItemUpdated="FormView1_ItemUpdated" EnableViewState="False">
                <EditItemTemplate>
                    <div class="main">
                    
                    <br />
                   
                    <br />
                    问题区
                    <br />
                    题目标题:
                    <asp:Label ID="qtext1Label2" runat="server" Text='<%# Eval("QTemplate.qtext1") %>' EnableViewState="False" />
                    <br />
                    题干:
                    <asp:Label ID="qtext2Label2" runat="server" Text='<%# Eval("QTemplate.qtext2") %>' EnableViewState="False" />
                    <br />
                    <!--以下隐藏信息不可去掉，否则答案将无法正常修改 -->
                    <asp:Label ID="SheetQIdTextBox" Visible="false" runat="server" Text='<%# Bind("SheetQId") %>' />
                    <asp:Label ID="Label12" runat="server" Visible="false" Text='<%# Eval("QTemplate.qType") %>' />
                    
                    <br />
                    回答区:
                    <br />
                    <asp:Panel ID="Panel12" runat="server">
                    <asp:RadioButtonList ID="RadioButtonList12" runat="server" >
                        
                    </asp:RadioButtonList>
                    </asp:Panel>
                    <asp:Panel ID="Panel22" runat="server">
                        <asp:CheckBoxList ID="CheckBoxList12" runat="server"></asp:CheckBoxList>
                    </asp:Panel>
                    <asp:Panel ID="Panel42" runat="server">
                        
                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("answer") %>' TextMode="MultiLine" EnableViewState="False" />
                        <br />
                        <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("answer2") %>' TextMode="MultiLine" EnableViewState="False" />
                        <br />
                        <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("answer3") %>' TextMode="MultiLine" EnableViewState="False" />
                    </asp:Panel>
                    

                    
                    
                    <br />
                                        <br />
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="保存" />
                        <asp:linkbutton id="Linkbutton2" text="上一题"  commandname="Page"   commandargument="Prev"   runat="Server"/> 
                        <asp:linkbutton id="NextButton" text="下一题"  commandname="Page"   commandargument="Next"   runat="Server"/> 
                 </div>
                    
                </EditItemTemplate>
                
                <PagerSettings Position="Top" />
                <PagerTemplate>
                    <div class="left">
                        <h3>
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
                        <h3><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h3>
                        <hr />
                        <h4>答题列表</h4>
                        <div class="qListZone">
                        <asp:Repeater ID="rptPagesHistory" runat="server" OnItemDataBound="rptPagesHistory_ItemDataBound" OnLoad="rptPagesHistory_Load">
                            <ItemTemplate>
                                <div class="qListItem">
                                <asp:LinkButton ID="lnkPageNumber"  runat="server" CommandName="Page" OnClick="lnkPageNumberHistory_Click" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        </div>
                        <asp:Button ID="Button2" runat="server" Text="交卷" OnClick="Button2_Click" OnClientClick="if(confirm('确认要交卷码？')) { openMaxWin(); return true;} return false;" />
                        <button onclick="let numberOfEntries = window.history.length;window.history.go(-1 * numberOfEntries + 1);window.location.href='http://www.baidu.com';return false;">haha</button>
                    </div>
                    
                </PagerTemplate>
                
            </asp:FormView>
          </div>
            

        </asp:View>
        <asp:View ID="View4" runat="server">
            <h1 style="text-align:center;">考试结束，请关闭浏览器，离开考场！</h1>
        </asp:View>
    </asp:MultiView>
            
        </div>
    </form>
</body>
</html>
