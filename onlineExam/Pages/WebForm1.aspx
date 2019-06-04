<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="onlineExam.Pages.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<asp:label runat="server" id="lblIpAddress" text="Label"></asp:label>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div>
            
            
            <asp:Menu ID="Menu1" runat="server" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal">
                <Items>
                    <asp:MenuItem Text="试题导入" Value="1"></asp:MenuItem>
                    <asp:MenuItem Text="创建试卷模板" Value="2"></asp:MenuItem>
                    <asp:MenuItem Text="阅卷评分" Value="3"></asp:MenuItem>
                    <asp:MenuItem Text="创建考试名单" Value="4"></asp:MenuItem>
                    <asp:MenuItem Text="分配任务" Value="5"></asp:MenuItem>
                    <asp:MenuItem Text="考试管理" Value="6"></asp:MenuItem>
                </Items>
            </asp:Menu>

            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
                <asp:View ID="View1" runat="server">
                    <div class="custom-file">
    <input type="file" class="custom-file-input" id="myFile" name="myFile" lang="cn">
    
                        <asp:TextBox ID="TextBox34" placeholder="输入试卷模板名称，例如：科目一第一套试卷" runat="server"></asp:TextBox>
    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox34" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
    
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
                <asp:View ID="View4" runat="server">
                    <div class="custom-file">
    <input type="file" class="custom-file-input" id="myFileStu" name="myFileStu" lang="cn">
    
  </div>
              <div class="input-group-append">
   
      <asp:LinkButton ID="LinkButton1Stu" CssClass="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm" runat="server" OnClick="LinkButton1Stu_Click" >学生名单上传</asp:LinkButton>
                  <p>可以通过上传学生名单的方式新增学生，或者修改已有学生的信息，如果要修改信息，必须保证前后id一致，注意id不是学号</p>

  </div>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetStudents" TypeName="onlineExam.DAL.StudentRepository" UpdateMethod="UpdateStudent"  ConflictDetection="CompareAllValues" DataObjectTypeName="onlineExam.Models.Student" OldValuesParameterFormatString="orig{0}">
                <UpdateParameters>
                    <asp:Parameter Name="yqsbb" Type="Object" />
                    <asp:Parameter Name="origYqsbb" Type="Object" />
                </UpdateParameters>
            </asp:ObjectDataSource>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="ObjectDataSource3">
                        <Columns>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:BoundField DataField="StudentId" HeaderText="StudentId" SortExpression="StudentId" />
                            <asp:BoundField DataField="xhId" HeaderText="xhId" SortExpression="xhId" />
                            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                            <asp:BoundField DataField="classId" HeaderText="classId" SortExpression="classId" />
                        </Columns>
                    </asp:GridView>
                    <asp:TextBox ID="TextBox33" placeholder="请输入考试名称" runat="server"></asp:TextBox>
                    <asp:Button ID="Button6" runat="server" Text="生成考试名单" OnClick="Button6_Click" />
                    <p>此处为数据库内的所有考生分配考试任务，但还未分配试卷，因此此时的考试尚不能开放，每次分配试卷，都是针对那些尚未分配此次考试任务的学生，如果有新加入的学生，需要再次分配一遍同名考试任务</p>
                    <br />
                    <asp:Label ID="Label13" runat="server"></asp:Label>
                </asp:View>
                <asp:View ID="View5" runat="server">
                    <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="GetExams" TypeName="onlineExam.DAL.ExamRepository"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" SelectMethod="GetSheetSchemas" TypeName="onlineExam.DAL.SheetSchemaRepository"></asp:ObjectDataSource>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSource4" DataTextField="name" DataValueField="ExamId" Height="16px">
                    </asp:DropDownList>
                    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="刷新" />
                    <asp:Label ID="Label14" runat="server" Text="选择考试名称和试卷方案后组卷"></asp:Label>
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="SheetSchemaId" DataSourceID="ObjectDataSource5" OnPageIndexChanging="PaginateTheData" OnRowDataBound="ReSelectSelectedRecords">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SheetSchemaId" HeaderText="SheetSchemaId" SortExpression="SheetSchemaId" />
                            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                        </Columns>
                    </asp:GridView>
                    <p>组卷前必须要保证至少有一套试卷方案被选中，只有组卷后的考试才能被开启</p>
                    <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="组卷并分配试卷" />
                    <asp:Label ID="Label15" runat="server"></asp:Label>
                </asp:View>
                <asp:View ID="View6" runat="server">


                    <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" DataObjectTypeName="OnlineExam.Models.Exam" OldValuesParameterFormatString="orig{0}" SelectMethod="GetExams" TypeName="onlineExam.BLL.ExamBLL" UpdateMethod="UpdateExam" ConflictDetection="CompareAllValues">
                        <UpdateParameters>
                            <asp:Parameter Name="qt" Type="Object" />
                            <asp:Parameter Name="origqt" Type="Object" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource6">
                        <Columns>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:BoundField DataField="ExamId" HeaderText="ExamId" SortExpression="ExamId" />
                            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                            <asp:CheckBoxField DataField="open" HeaderText="open" SortExpression="open" />
                        </Columns>
                    </asp:GridView>

                    <p>只有分配过试卷的考试会出现在这里，才能被开启</p>
                </asp:View>
            </asp:MultiView>

        </div>
        
    </form>
</body>
</html>
