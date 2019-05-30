<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="onlineExam.Pages.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div>
            
            
            <asp:Menu ID="Menu1" runat="server" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal">
                <Items>
                    <asp:MenuItem Text="试题导入" Value="1"></asp:MenuItem>
                    <asp:MenuItem Text="创建试卷模板" Value="2"></asp:MenuItem>
                    <asp:MenuItem Text="阅卷评分" Value="3"></asp:MenuItem>
                </Items>
            </asp:Menu>

            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
                <asp:View ID="View1" runat="server">
                    <div class="custom-file">
    <input type="file" class="custom-file-input" id="myFile" name="myFile" lang="cn">
    
  </div>
              <div class="input-group-append">
   
      <asp:LinkButton ID="btnUpload" CssClass="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm" runat="server" OnClick="btnUploadClick"><i class="fas fa-upload fa-sm text-white-50"></i> 列表上传</asp:LinkButton>

  </div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetQTemplates" TypeName="onlineExam.DAL.QTemplateRepository" UpdateMethod="UpdateQTemplate" OnDataBinding="ObjectDataSource1_DataBinding" ConflictDetection="CompareAllValues" DataObjectTypeName="onlineExam.Models.QTemplate" OldValuesParameterFormatString="orig{0}">
                <UpdateParameters>
                    <asp:Parameter Name="yqsbb" Type="Object" />
                    <asp:Parameter Name="origYqsbb" Type="Object" />
                </UpdateParameters>
            </asp:ObjectDataSource>

            
            
                <asp:FormView ID="FormView1" runat="server" AllowPaging="True" DataSourceID="ObjectDataSource1" DefaultMode="Edit" OnDataBinding="FormView1_DataBinding" OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating">
                <EditItemTemplate>
                     qid:
                    <asp:Label ID="qidLabel" runat="server" Text='<%# Bind("qid") %>' />
                    <br />
                    qtext1:
                    <asp:Label ID="qtext1Label" runat="server" Text='<%# Bind("qtext1") %>' />
                    <br />
                    qtext2:
                    <asp:Label ID="qtext2Label" runat="server" Text='<%# Bind("qtext2") %>' />
                    <br />
                    <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Bind("qType") %>' />
                    answer:
                    <asp:TextBox ID="answerTextBox" runat="server" Text='<%# Bind("answer") %>' />
                    <br />
                    <asp:Panel ID="Panel1" runat="server">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnDataBinding="RadioButtonList1_DataBinding" >
                        
                    </asp:RadioButtonList>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
                    </asp:Panel>
                    <asp:Panel ID="Panel4" runat="server">
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("answer") %>' TextMode="MultiLine" />
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("answer2") %>' TextMode="MultiLine"/>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("answer3") %>' TextMode="MultiLine"/>
                    </asp:Panel>
                    <br />
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="更新" />
                    &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" />
                </EditItemTemplate>
                
                
            </asp:FormView>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <asp:ListBox ID="ListBox1" runat="server" DataSourceID="ObjectDataSource1" DataTextField="qid" DataValueField="qid" Height="85px" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" Width="88px"></asp:ListBox>
            

            <asp:Button ID="Button1" runat="server" style="margin-top: 0px" Text="加入" OnClick="Button1_Click" />

            <asp:Button ID="Button2" runat="server" Height="22px" Text="删除" OnClick="Button2_Click" />
            <asp:ListBox ID="ListBox2" runat="server" Height="85px" Width="81px"></asp:ListBox>
            
            <br />
            <asp:TextBox ID="TextBox4" runat="server" type="Number"></asp:TextBox>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="生成试卷方案" />
            
            <br />
            <asp:TextBox ID="TextBox5" runat="server" type="Number"></asp:TextBox>
            <asp:Button ID="Button4" runat="server" Text="生成试卷" OnClick="Button4_Click" Width="119px" />

                </asp:View>
                <asp:View ID="View3" runat="server">
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetSheetQs" TypeName="onlineExam.DAL.SheetQRepository" UpdateMethod="UpdateSheetQ" ConflictDetection="CompareAllValues" DataObjectTypeName="onlineExam.Models.SheetQ" OldValuesParameterFormatString="orig{0}">
                <UpdateParameters>
                    <asp:Parameter Name="yqsbb" Type="Object" />
                    <asp:Parameter Name="origYqsbb" Type="Object" />
                </UpdateParameters>
            </asp:ObjectDataSource>

            <br />
            <asp:FormView ID="FormView2" runat="server" AllowPaging="True" DataSourceID="ObjectDataSource2" DefaultMode="Edit" OnDataBound="FormView2_DataBound" OnItemUpdating="FormView2_ItemUpdating">
                <EditItemTemplate>
                    SheetQId:
                    <asp:TextBox ID="SheetQIdTextBox" runat="server" Text='<%# Bind("SheetQId") %>' />
                    <br />
                   qid:
                    <asp:Label ID="qidLabel2" runat="server" Text='<%# Eval("QTemplate.qid") %>' />
                    <br />
                    qtext1:
                    <asp:Label ID="qtext1Label2" runat="server" Text='<%# Eval("QTemplate.qtext1") %>' />
                    <br />
                    qtext2:
                    <asp:Label ID="qtext2Label2" runat="server" Text='<%# Eval("QTemplate.qtext2") %>' />
                    <br />
                    <asp:Label ID="Label12" runat="server" Visible="false" Text='<%# Eval("QTemplate.qType") %>' />
                    
                    <br />
                    <asp:Panel ID="Panel12" runat="server">
                    <asp:RadioButtonList ID="RadioButtonList12" runat="server" OnDataBinding="RadioButtonList1_DataBinding" >
                        
                    </asp:RadioButtonList>
                    </asp:Panel>
                    <asp:Panel ID="Panel22" runat="server">
                        <asp:CheckBoxList ID="CheckBoxList12" runat="server"></asp:CheckBoxList>
                    </asp:Panel>
                    <asp:Panel ID="Panel42" runat="server">
                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("answer") %>' TextMode="MultiLine" />
                        <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("answer2") %>' TextMode="MultiLine"/>
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
                    answer:
                    <asp:TextBox ID="answerTextBox" runat="server" Text='<%# Bind("answer") %>' />
                    <br />
                    answer2:
                    <asp:TextBox ID="answer2TextBox" runat="server" Text='<%# Bind("answer2") %>' />
                    <br />
                    answer3:
                    <asp:TextBox ID="answer3TextBox" runat="server" Text='<%# Bind("answer3") %>' />
                    <br />
                                        <br />
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="更新" />
                    &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" />
                </EditItemTemplate>
                
            </asp:FormView>
                </asp:View>
            </asp:MultiView>

        </div>
    </form>
</body>
</html>
