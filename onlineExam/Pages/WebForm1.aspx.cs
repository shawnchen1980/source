using onlineExam.BLL;
using onlineExam.DAL;
using onlineExam.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using onlineExam.Utilities;
namespace onlineExam.Pages
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string add = GetIPAddress();
            lblIpAddress.Text = add;

        }

        protected void FormView1_DataBinding(object sender, EventArgs e)
        {
            
        }

        protected void ObjectDataSource1_DataBinding(object sender, EventArgs e)
        {

        }
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            Console.Write(FormView1.DataItem);
            QTemplate item = (QTemplate)FormView1.DataItem;
            ArrayList arr = new ArrayList();
            ArrayList list = new ArrayList();
            list.Add(new { Code = item.op1, Name = "1" });
            list.Add(new { Code = item.op2, Name = "2" });
            list.Add(new { Code = item.op3, Name = "3" });
            list.Add(new { Code = item.op4, Name = "4" });
            var panel1 = FormView1.FindControl("Panel1");
            var panel2 = FormView1.FindControl("Panel2");
            var panel4 = FormView1.FindControl("Panel4");


            if (item.qType == 1||item.qType==3)
            {
                panel1.Visible = true;
                panel2.Visible = false;
                panel4.Visible = false;
                RadioButtonList rbl = (RadioButtonList)FormView1.FindControl("RadioButtonList1");
                if (item.qType == 3)
                {
                    list.RemoveRange(2, 2);
                }
                rbl.DataSource = list;
                rbl.DataTextField = "Code";
                rbl.DataValueField = "Name";
                if (!String.IsNullOrEmpty(item.answer))
                {
                    rbl.SelectedValue = item.answer;
                }
                rbl.DataBind();
            }
            else if (item.qType == 2 )
            {
                panel1.Visible = false;
                panel2.Visible = true;
                panel4.Visible = false;
                CheckBoxList cbl = (CheckBoxList)FormView1.FindControl("CheckBoxList1");
                cbl.DataSource = list;
                cbl.DataTextField = "Code";
                cbl.DataValueField = "Name";
               
                cbl.DataBind();
                if (!String.IsNullOrEmpty(item.answer))
                {
                    char[] charSeparators = new char[] { ',' };
                    string[] arr1=item.answer.Split(charSeparators);
                    cbl.Items.Cast<ListItem>().Where(x => arr1.Contains(x.Value)).ToList().ForEach(x=>x.Selected=true);
                    //for(int j = 0; j < arr1.Length - 1; j++)
                    //{
                    //    cbl.Items[System.Convert.ToInt32(arr1[j])-1].Selected = true;
                    //}
                    //Console.Write(arr1);
                }
            }
            else
            {
                panel1.Visible = false;
                panel2.Visible = false;
                panel4.Visible = true;
            }
        }

        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            QTemplate item = (QTemplate)FormView1.DataItem;
            if (e.OldValues["qType"].ToString() == "1"|| e.OldValues["qType"].ToString() == "3") {
                RadioButtonList rbl = (RadioButtonList)FormView1.FindControl("RadioButtonList1");
                if (!String.IsNullOrEmpty(rbl.SelectedValue))
                {
                    e.NewValues["answer"] = rbl.SelectedValue;
                }
            }
            else if (e.OldValues["qType"].ToString() == "2")
            {
                CheckBoxList cbl = (CheckBoxList)FormView1.FindControl("CheckBoxList1");
                string res = "";
                res = string.Join(",", cbl.Items.Cast<ListItem>().OrderBy(x=>x.Value).Where(x => x.Selected).Select(x => x.Value));
                //for (int i = 0; i < cbl.Items.Count; i++)
                //{
                    
                //    if (cbl.Items[i].Selected)
                //    {
                //        res += (i+1) + ",";
                //    }
                //}
                if (!String.IsNullOrEmpty(res))
                {
                    e.NewValues["answer"] = res;
                    //cbl.
                }
            }

        }

        protected void RadioButtonList1_DataBinding(object sender, EventArgs e)
        {
            try { base.OnDataBinding(e); }
            catch(ArgumentOutOfRangeException ex)
            {
                return;
            }
        }

        protected void btnUploadClick(object sender, EventArgs e)
        {
            HttpPostedFile file = Request.Files["myFile"];
            using (SheetSchemaBLL bl=new SheetSchemaBLL())
            {
                bl.ImportSheetSchema(file, TextBox34.Text);
            }

            //using (QTemplateBLL bl = new QTemplateBLL())
            //{
            //    var items = bl.ImportQTemplate(file);
            //    foreach (var item in items)
            //    {
            //        bl.UpsertQTemplate(item);
            //    }

            //}
            //using (OnlineExamContext context = new OnlineExamContext())
            //{

            //    int i = 1;
            //    var sheetSchema = context.SheetSchemas.FirstOrDefault(x => x.name == TextBox34.Text);
            //    if (sheetSchema == null)
            //    {
            //        sheetSchema = new SheetSchema { name = TextBox34.Text };
            //        context.SheetSchemas.Add(sheetSchema);
            //    }
            //    foreach (ListItem item in ListBox2.Items)
            //    {
            //        int index = Convert.ToInt32(item.Value);
            //        var qTemplate = context.QTemplates.FirstOrDefault(x => x.qid == index);
            //        var sheetSchemaQ = new SheetSchemaQ { SheetSchema = sheetSchema, qOrder = i++, QTemplate = qTemplate };
            //        context.SheetSchemaQs.Add(sheetSchemaQ);


            //    }
            //    context.SaveChanges();


            //}

            FormView1.DataBind();
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FormView1.PageIndex = Convert.ToInt32(ListBox1.SelectedValue)-1;
            ListBox2.Items.Add(ListBox1.SelectedValue);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ListBox2.SelectedValue))
            {
                ListBox2.Items.Remove(ListBox2.SelectedValue);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            using (OnlineExamContext context = new OnlineExamContext())
            {
                
                    int i = 1;
                var sheetSchema = context.SheetSchemas.FirstOrDefault(x => x.name == TextBox4.Text);
                if (sheetSchema == null)
                {
                    sheetSchema = new SheetSchema { name = TextBox4.Text };
                    context.SheetSchemas.Add(sheetSchema);
                }
                    foreach (ListItem item in ListBox2.Items)
                    {
                    int index = Convert.ToInt32(item.Value);
                        var qTemplate = context.QTemplates.FirstOrDefault(x => x.qid == index);
                    var sheetSchemaQ = new SheetSchemaQ { SheetSchema= sheetSchema, qOrder = i++, QTemplate = qTemplate };
                    context.SheetSchemaQs.Add(sheetSchemaQ);
                    
                        
                    }
                context.SaveChanges();


            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            using (OnlineExamContext context = new OnlineExamContext())
            {
                var seq = SeqGenerator.GenerateRandom(10);
               
                int i = 1;
                var sheetSchema = context.SheetSchemas.Include("SheetSchemaQs.QTemplate").FirstOrDefault(x => x.name == TextBox4.Text);
                if (sheetSchema == null)
                {
                    return;

                }
                var sheet = new Sheet { timestamp = DateTime.Now };
                context.Sheets.Add(sheet);
                var qs = sheetSchema.SheetSchemaQs.OrderBy(x=>x.qOrder).ToArray();
                int count = sheetSchema.SheetSchemaQs.Count();
                var qts = SeqGenerator.GenerateRandom(count).Select(x => qs[x].QTemplate).ToArray();
                foreach (var item in qts)
                {
                    var sheetQ = new SheetQ { qOrder = i++, QTemplate = item, correctAnswer = item.answer, optionOffset =item.qType==4 ? 0 : SeqGenerator.GenerateRandomNum(item.opLength), Sheet=sheet };
                    context.SheetQs.Add(sheetQ);
                    


                }
                context.SaveChanges();


            }
        }

        protected void FormView2_DataBound(object sender, EventArgs e)
        {
            SheetQ sheetQ = (SheetQ)FormView2.DataItem;
            if (sheetQ == null) return;
            QTemplate item = sheetQ.QTemplate;
            //QTemplate item = ((SheetQ)FormView2.DataItem).QTemplate;
            ArrayList arr = new ArrayList();
            ArrayList list = new ArrayList();
            list.Add(new { Code = item.op1, Name = "1" });
            list.Add(new { Code = item.op2, Name = "2" });
            list.Add(new { Code = item.op3, Name = "3" });
            list.Add(new { Code = item.op4, Name = "4" });
            list.Add(new { Code = item.op1, Name = "1" });
            list.Add(new { Code = item.op2, Name = "2" });
            list.Add(new { Code = item.op3, Name = "3" });
            if (item.qType == 1 || item.qType == 2)
            {
                list = list.GetRange(sheetQ.optionOffset, 4);
            }
            else
            {
                list = list.GetRange(0, 4);
            }
            
            var panel1 = FormView2.FindControl("Panel12");
            var panel2 = FormView2.FindControl("Panel22");
            var panel4 = FormView2.FindControl("Panel42");


            if (item.qType == 1 || item.qType == 3)
            {
                panel1.Visible = true;
                panel2.Visible = false;
                panel4.Visible = false;
                RadioButtonList rbl = (RadioButtonList)FormView2.FindControl("RadioButtonList12");
                if (item.qType == 3)
                {
                    list.RemoveRange(2, 2);
                }
                rbl.DataSource = list;
                rbl.DataTextField = "Code";
                rbl.DataValueField = "Name";
                if (!String.IsNullOrEmpty(sheetQ.answer))
                {
                    rbl.SelectedValue = sheetQ.answer;
                }
                rbl.DataBind();
            }
            else if (item.qType == 2)
            {
                panel1.Visible = false;
                panel2.Visible = true;
                panel4.Visible = false;
                CheckBoxList cbl = (CheckBoxList)FormView2.FindControl("CheckBoxList12");
                cbl.DataSource = list;
                cbl.DataTextField = "Code";
                cbl.DataValueField = "Name";

                cbl.DataBind();
                if (!String.IsNullOrEmpty(sheetQ.answer))
                {
                    char[] charSeparators = new char[] { ',' };
                    string[] arr1 = sheetQ.answer.Split(charSeparators);
                    cbl.Items.Cast<ListItem>().OrderBy(x => x.Value).Where(x => arr1.Contains(x.Value)).ToList().ForEach(x => x.Selected = true);
                    //for (int j = 0; j < arr1.Length - 1; j++)
                    //{
                    //    cbl.Items.FindByValue(arr1[j]).Selected = true;
                    //}
                    //Console.Write(arr1);
                }
            }
            else
            {
                panel1.Visible = false;
                panel2.Visible = false;
                panel4.Visible = true;
            }
        }

        protected void FormView2_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            //QTemplate item = (QTemplate)e.OldValues["QTemplate"];
            Label lb = (Label)FormView2.FindControl("Label12");
            if (lb.Text == "1" || lb.Text == "3")
            {
                RadioButtonList rbl = (RadioButtonList)FormView2.FindControl("RadioButtonList12");
                if (!String.IsNullOrEmpty(rbl.SelectedValue))
                {
                    e.NewValues["answer"] = rbl.SelectedValue;
                }
            }
            else if (lb.Text == "2")
            {
                CheckBoxList cbl = (CheckBoxList)FormView2.FindControl("CheckBoxList12");
                string res = "";
                res = string.Join(",", cbl.Items.Cast<ListItem>().OrderBy(x => x.Value).Where(x => x.Selected).Select(x => x.Value));
                //for (int i = 0; i < cbl.Items.Count; i++)
                //{

                //    if (cbl.Items[i].Selected)
                //    {
                //        res += cbl.Items[i].Value + ",";
                //    }
                //}
                if (!String.IsNullOrEmpty(res))
                {
                    e.NewValues["answer"] = res;
                    //cbl.
                }
            }
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            
            MultiView1.ActiveViewIndex = Convert.ToInt32(e.Item.Value)-1;
            
        }

        protected void LinkButton1Stu_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = Request.Files["myFileStu"];
            if (file.ContentLength == 0) return;
            using (StudentBLL bl = new StudentBLL())
            {
                var items = bl.ImportStudent(file);
                foreach (var item in items)
                {
                    bl.SaveOrUpdate(item);
                }

            }
            GridView1.DataBind();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox33.Text)) return;
            string exName = TextBox33.Text;
            int stuCount;
            using (OnlineExamContext context=new OnlineExamContext())
            {
                var list1 = context.Assignments.Include("Student").Where(x => x.name == exName).Select(x => x.Student.StudentId).ToList();
                var list2 = context.Students.Where(x => !list1.Contains(x.StudentId)).ToList();
                stuCount = list2.Count();
                Exam exam = context.Exams.FirstOrDefault(x => x.name == exName) ?? new Exam { name = exName, open=true };
                
                context.Exams.Add(exam);
                
                foreach (var item in list2)
                {
                    var ass = new Assignment { name = exName, sheetSubmited = false, Student = item,Exam=exam };
                    context.Assignments.Add(ass);

                }
                context.SaveChanges();
                
            }
            Label13.Text = "考试-" + exName + "-新增考生" + stuCount + "人";
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            ClearCheckedRecords();
            DropDownList1.DataBind();
            GridView3.DataBind();
        }

        protected void CollectCheckedRecords()
        {
            List<string> list = new List<string>();
            if (ViewState["SelectedRecords"] != null)
            {
                list = (List<string>)ViewState["SelectedRecords"];
            }
            foreach (GridViewRow row in GridView3.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                var selectedKey =
                (GridView3.DataKeys[row.RowIndex].Value.ToString());
                if (chk.Checked)
                {
                    if (!list.Contains(selectedKey))
                    {
                        list.Add(selectedKey);
                    }
                }
                else
                {
                    if (list.Contains(selectedKey))
                    {
                        list.Remove(selectedKey);
                    }
                }
            }
            ViewState["SelectedRecords"] = list;
        }
        protected void ClearCheckedRecords()
        {
            if (ViewState["SelectedRecords"] != null)
            {
                List<string> list = (List<string>)ViewState["SelectedRecords"];
                list.Clear();
            }
        }
        protected void PaginateTheData(object sender, GridViewPageEventArgs e)
        {
            CollectCheckedRecords();
            //GridView1.PageIndex = e.NewPageIndex;
            //this.GetData();
        }
        protected void ReSelectSelectedRecords(object sender, GridViewRowEventArgs e)
        {
            List<string> list = ViewState["SelectedRecords"] as List<string>;
            if (e.Row.RowType == DataControlRowType.DataRow && list != null)
            {
                var autoId = (GridView3.DataKeys[e.Row.RowIndex].Value.ToString());
                if (list.Contains(autoId))
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkSelect");
                    chk.Checked = true;
                }
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            CollectCheckedRecords();
            using (OnlineExamContext context=new OnlineExamContext())
            {
                //Exam exam = context.Exams.Include("Assignment.Student").FirstOrDefault(x => x.ExamId == Convert.ToInt32(DropDownList1.SelectedValue));
                if (string.IsNullOrEmpty(DropDownList1.SelectedValue)) return;
                int examId = Convert.ToInt32(DropDownList1.SelectedValue);
                var assignments = context.Assignments.Include("Exam").Include("Sheet").Where(x => x.Exam.ExamId ==examId ).ToList();
                var list=(List<string>)ViewState["SelectedRecords"];
                var arr = list.Select(x => Convert.ToInt32(x)).ToArray();
                var schemas = context.SheetSchemas.Include("SheetSchemaQs.QTemplate").Where(x => arr.Contains(x.SheetSchemaId)).ToArray();
                int schemaCount = schemas.Count();
                int index = 0, assCount = 0;
                foreach (var item in assignments)
                {
                    if (item.SheetSchema != null) continue;
                    index = index % schemaCount;
                    
                    //先分配试卷模板再分配试卷，如果当前任务已经包含试卷则不再分配，直接进行下一任务分配
                    assCount++;
                    item.SheetSchema = schemas[index];
                    int qCount = schemas[index].SheetSchemaQs.Count();
                    var qts = schemas[index].SheetSchemaQs.ToArray();
                    Sheet st = new Sheet { timestamp = DateTime.Now };
                    item.Sheet = st;
                    context.Sheets.Add(st);
                    var seqArr = Utilities.SeqGenerator.GenerateRandom(qCount);
                    int j = 0;
                    foreach (var qitem in qts)
                    {
                        SheetQ sq = new SheetQ { QTemplate = qitem.QTemplate, Sheet = st, optionOffset = qitem.QTemplate.qType == 1 || qitem.QTemplate.qType == 2 ? Utilities.SeqGenerator.GenerateRandomNum(4) : 0, qOrder = seqArr[j++], score=qitem.score, correctAnswer=qitem.QTemplate.answer };
                        context.SheetQs.Add(sq);
                    }
                    index++;

                }
                context.SaveChanges();
                Label15.Text = "当前完成" + assCount + "份试卷分配任务";
            }
        }
    }

    
}