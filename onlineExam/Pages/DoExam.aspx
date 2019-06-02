<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoExam.aspx.cs" Inherits="onlineExam.Pages.DoExam" %>

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
    </style>
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
            <asp:TextBox ID="TextBox1" placeholder="输入学号" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:TextBox ID="TextBox2" placeholder="输入姓名" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:Button ID="Button1" runat="server" Text="登录" OnClick="Button1_Click" />
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" OnDataBinding="GridView1_DataBinding" DataKeyNames="AssignmentId" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="AssignmentId" HeaderText="AssignmentId" SortExpression="AssignmentId" />
                    <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                    <asp:BoundField DataField="ipAddress" HeaderText="ipAddress" SortExpression="ipAddress" />
                    <asp:CheckBoxField DataField="sheetSubmited" HeaderText="sheetSubmited" SortExpression="sheetSubmited" />
                    <asp:BoundField DataField="firstLogin" HeaderText="firstLogin" SortExpression="firstLogin" />
                    <asp:BoundField DataField="lastLogin" HeaderText="lastLogin" SortExpression="lastLogin" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("AssignmentId", "{0}") %>' OnClick="LinkButton1_Click">进入考试</asp:LinkButton>
                            <asp:TextBox ID="TextBox3" runat="server" TextMode="Password" placeholder="当前ip地址与上一次登录时不同，请输入换机密码"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBox3" ErrorMessage="换机密码错误，请重试" ForeColor="Red" ValueToCompare="88888888"></asp:CompareValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    当前没有为你分配考试任务
                </EmptyDataTemplate>
            </asp:GridView>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="container">
            <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource2" AllowPaging="True" DefaultMode="Edit" OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating" OnItemUpdated="FormView1_ItemUpdated">
                <EditItemTemplate>
                    <div class="main">
                    SheetQId:
                    <asp:Label ID="SheetQIdTextBox" runat="server" Text='<%# Bind("SheetQId") %>' />
                    <br />
                   qid:
                    <asp:Label ID="qidLabel2" runat="server" Text='<%# Eval("QTemplate.qid") %>' />
                    <br />
                    问题区
                    <br />
                    题目标题:
                    <asp:Label ID="qtext1Label2" runat="server" Text='<%# Eval("QTemplate.qtext1") %>' />
                    <br />
                    题干:
                    <asp:Label ID="qtext2Label2" runat="server" Text='<%# Eval("QTemplate.qtext2") %>' />
                    <br />
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
                        
                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("answer") %>' TextMode="MultiLine" />
                        <br />
                        <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("answer2") %>' TextMode="MultiLine"/>
                        <br />
                        <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("answer3") %>' TextMode="MultiLine"/>
                    </asp:Panel>
                    
                    qOrder:
                    <asp:TextBox ID="qOrderTextBox" runat="server" Text='<%# Bind("qOrder") %>' />
                    <br />
                    optionOffset:
                    <asp:TextBox ID="optionOffsetTextBox" runat="server" Text='<%# Bind("optionOffset") %>' />
                    <br />
                    correctAnswer:
                    <asp:TextBox ID="correctAnswerTextBox" runat="server" Text='<%# Bind("correctAnswer") %>' />
                    
                    <br />
                                        <br />
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="保存" />
                        <asp:linkbutton id="NextButton"
                  text="下一题"
                  commandname="Page"
                  commandargument="Next"
                  runat="Server"/> 
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
                    </div>
                </PagerTemplate>
                
            </asp:FormView>
          </div>

        </asp:View>
    </asp:MultiView>
        </div>
    </form>
</body>
</html>
