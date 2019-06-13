<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoReview.aspx.cs" Inherits="onlineExam.Pages.DoReview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <style>
        .answerBlock{
            width:80%;
        }
        .formView{
            width:100%;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
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
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetSheetByAssignmentId" TypeName="onlineExam.BLL.SheetBLL">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="GridView2" Name="assId" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
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
                    <asp:TextBox ID="TextBox9" placeholder="试卷编号" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox10" placeholder="班级编号" runat="server"></asp:TextBox>
                    <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="查询" />
                    <asp:Button ID="Button10" runat="server" Text="刷新" OnClick="Button10_Click" />
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource8" CellPadding="4" DataKeyNames="AssignmentId" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="AssignmentId" HeaderText="任务编号" SortExpression="AssignmentId" />
                            <asp:BoundField DataField="Student.StudentId" HeaderText="学号" SortExpression="Student" />
                            <asp:BoundField DataField="Student.name" HeaderText="姓名" SortExpression="Student" />
                            <asp:BoundField DataField="Exam.name" HeaderText="科目" />
                            <asp:BoundField DataField="ipAddress" HeaderText="登陆IP地址" SortExpression="ipAddress" />
                            <asp:TemplateField HeaderText="考试状态" SortExpression="sheetSubmited">
                                
                                <ItemTemplate>
                                    <%# ShowStatus(Eval("sheetSubmited"),Eval("firstLogin")) %>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="firstLogin" HeaderText="初次登陆时间" SortExpression="firstLogin" />
                            <asp:BoundField DataField="lastLogin" HeaderText="最近登陆时间" SortExpression="lastLogin" />
                            <asp:BoundField DataField="SheetSchema.SheetSchemaId" HeaderText="试卷编号" />
                            <asp:TemplateField HeaderText="当前得分">
                                <ItemTemplate>
                                    <%# ShowScore(Eval("Sheet.answers"),Eval("Sheet.qAns"),Eval("Sheet.qScores")) %>
                                    
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:BoundField DataField="Student.classId" HeaderText="班级" SortExpression="Student" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
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
            </asp:MultiView>
        </div>
        <br />
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
          <br />
        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" DefaultMode="Edit" CssClass="formView">
            <EditItemTemplate>
                
                <asp:TextBox ID="SheetIdTextBox" Visible="false" runat="server" Text='<%# Bind("SheetId") %>'  />
                
                <asp:TextBox ID="timestampTextBox" Visible="false" runat="server" Text='<%# Bind("timestamp") %>' />

                <asp:TextBox ID="answersTextBox" Visible="false" runat="server" Text='<%# Bind("answers") %>'  />

                第一小题回答:
                <asp:TextBox ID="answer1TextBox" Enabled="false" runat="server" Text='<%# Eval("answer1") %>' Rows="10" TextMode="MultiLine" CssClass="answerBlock"/>
                <br />
                第二小题回答:
                <asp:TextBox ID="answer2TextBox" Enabled="false" runat="server" Text='<%# Bind("answer2") %>' Rows="10" TextMode="MultiLine"  CssClass="answerBlock" />
                <br />
                第三小题回答:
                <asp:TextBox ID="answer3TextBox" Enabled="false" runat="server" Text='<%# Bind("answer3") %>' Rows="10" TextMode="MultiLine" CssClass="answerBlock"/>

                <asp:TextBox ID="qOrdersTextBox" Visible="false" runat="server" Text='<%# Bind("qOrders") %>' />

                <asp:TextBox ID="qOffsTextBox" Visible="false" runat="server" Text='<%# Bind("qOffs") %>' />

                <asp:TextBox ID="qAnsTextBox" Visible="false" runat="server" Text='<%# Bind("qAns") %>' />

                <asp:TextBox ID="qScoresTextBox" Visible="false" runat="server" Text='<%# Bind("qScores") %>' />
                <br />
                客观题评分:
                <asp:TextBox ID="score1TextBox"  Enabled="false" runat="server" Text='<%# ShowScore(Eval("answers"),Eval("qAns"),Eval("qScores")) %>' />
                <br />
                主观题评分:
                <asp:TextBox ID="score2TextBox" runat="server" Text='<%# Bind("score2") %>' />
                <br />
                阅卷人:
                <asp:TextBox ID="markerTextBox"  Enabled="false" runat="server" Text='<%# Bind("marker") %>' />
                
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="更新" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" />
            </EditItemTemplate>
           
            <ItemTemplate>
                SheetId:
                <asp:Label ID="SheetIdLabel" runat="server" Text='<%# Bind("SheetId") %>' />
                <br />
                timestamp:
                <asp:Label ID="timestampLabel" runat="server" Text='<%# Bind("timestamp") %>' />
                <br />
                answers:
                <asp:Label ID="answersLabel" runat="server" Text='<%# Bind("answers") %>' />
                <br />
                answer1:
                <asp:Label ID="answer1Label" runat="server" Text='<%# Bind("answer1") %>' />
                <br />
                answer2:
                <asp:Label ID="answer2Label" runat="server" Text='<%# Bind("answer2") %>' />
                <br />
                answer3:
                <asp:Label ID="answer3Label" runat="server" Text='<%# Bind("answer3") %>' />
                <br />
                qOrders:
                <asp:Label ID="qOrdersLabel" runat="server" Text='<%# Bind("qOrders") %>' />
                <br />
                qOffs:
                <asp:Label ID="qOffsLabel" runat="server" Text='<%# Bind("qOffs") %>' />
                <br />
                qAns:
                <asp:Label ID="qAnsLabel" runat="server" Text='<%# Bind("qAns") %>' />
                <br />
                qScores:
                <asp:Label ID="qScoresLabel" runat="server" Text='<%# Bind("qScores") %>' />
                <br />
                score1:
                <asp:Label ID="score1Label" runat="server" Text='<%# Bind("score1") %>' />
                <br />
                score2:
                <asp:Label ID="score2Label" runat="server" Text='<%# Bind("score2") %>' />
                <br />
                marker:
                <asp:Label ID="markerLabel" runat="server" Text='<%# Bind("marker") %>' />
                <br />
                SheetQs:
                <asp:Label ID="SheetQsLabel" runat="server" Text='<%# Bind("SheetQs") %>' />
                <br />
                Assignment:
                <asp:Label ID="AssignmentLabel" runat="server" Text='<%# Bind("Assignment") %>' />
                <br />

            </ItemTemplate>
        </asp:FormView>
        <br />
    </form>
</body>
</html>
