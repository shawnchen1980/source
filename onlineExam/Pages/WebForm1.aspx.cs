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
                    for(int j = 0; j < arr1.Length - 1; j++)
                    {
                        cbl.Items[System.Convert.ToInt32(arr1[j])-1].Selected = true;
                    }
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
                for (int i = 0; i < cbl.Items.Count; i++)
                {
                    
                    if (cbl.Items[i].Selected)
                    {
                        res += (i+1) + ",";
                    }
                }
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

            using (QTemplateBLL bl = new QTemplateBLL())
            {
                var items = bl.ImportQTemplate(file);
                foreach (var item in items)
                {
                    bl.UpsertQTemplate(item);
                }

            }
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
                    for (int j = 0; j < arr1.Length - 1; j++)
                    {
                        cbl.Items.FindByValue(arr1[j]).Selected = true;
                    }
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
                for (int i = 0; i < cbl.Items.Count; i++)
                {

                    if (cbl.Items[i].Selected)
                    {
                        res += cbl.Items[i].Value + ",";
                    }
                }
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
    }

    
}