using onlineExam.BLL;
using onlineExam.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace onlineExam.Pages
{
    public partial class DoExam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetNoStore();
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            //if (!IsPostBack)
            //{
            //    string cacheKey = GetIPAddress() + DateTime.Now;
            //    ViewState["cacheKey"] = cacheKey;
            //    ObjectDataSource2.CacheKeyDependency = cacheKey;
            //    Cache[ObjectDataSource2.CacheKeyDependency] = cacheKey;
            //}
            if (Convert.ToBoolean(Session["submitted"]))
            {
                MultiView1.ActiveViewIndex=3;
            }
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
            Label lb1 = (Label)MultiView1.Views[2].FindControl("Label1");
            Label lb2= (Label)MultiView1.Views[2].FindControl("Label2");
            ViewState["id"] = TextBox1.Text.Trim();
            ViewState["name"] = TextBox2.Text.Trim() ;
            ViewState["arrChecked"] = null;
            if (Convert.ToBoolean(Session["submitted"]))
            {
                return;
            }
            MultiView1.ActiveViewIndex = 1;
            //Response.Redirect("/pages/doexam");
        }

        protected void GridView1_DataBinding(object sender, EventArgs e)
        {
           
        }

        protected void ObjectDataSource1_DataBinding(object sender, EventArgs e)
        {
            
        }

        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            //var arguments = e.InputParameters;
            e.InputParameters["id"] = TextBox1.Text.Trim();
            e.InputParameters["name"] = TextBox2.Text.Trim();

        }

        protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            int id = Convert.ToInt32(ViewState["assId"]);
            using (AssignmentBLL bll = new AssignmentBLL())
            {
                Assignment origItem = bll.GetAssignment(id);
                Assignment item = origItem.ShallowCopy();
                if (Convert.ToBoolean(ViewState["submitted"]))
                {
                    item.sheetSubmited = true;
                    ViewState["submitted"] = false;
                    Session["submitted"] = true;
                }
                else
                {
                    item.lastLogin = DateTime.Now;
                    item.firstLogin = item.firstLogin ?? DateTime.Now;
                    item.ipAddress = GetIPAddress();
                }
                
                e.InputParameters["item"] = item ;
                e.InputParameters["origItem"] = origItem;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            GridView1.SelectedIndex = clickedRow.RowIndex;
            ViewState["assId"] = Convert.ToString(GridView1.SelectedDataKey.Value);
            HiddenField hanswers= (HiddenField)GridView1.SelectedRow.FindControl("HiddenField1");
            HiddenField hanswer1 = (HiddenField)GridView1.SelectedRow.FindControl("HiddenField2");
            HiddenField hanswer2 = (HiddenField)GridView1.SelectedRow.FindControl("HiddenField3");
            HiddenField hanswer3 = (HiddenField)GridView1.SelectedRow.FindControl("HiddenField4");
            Char dl = '|';
            ViewState["answers"] = string.IsNullOrEmpty(hanswers.Value) ? null : hanswers.Value.Split(dl) ;
            ViewState["answer1"] = hanswer1.Value;
            ViewState["answer2"] = hanswer2.Value;
            ViewState["answer3"] = hanswer3.Value;
            if (Convert.ToBoolean(Session["submitted"]))
            {
                return;
            }
            MultiView1.ActiveViewIndex = 2;
            ObjectDataSource1.Update();
            //string cacheKey = ViewState["cacheKey"] as string;
            //Cache.Remove(ObjectDataSource2.CacheKeyDependency);
            //Cache[ObjectDataSource2.CacheKeyDependency] = cacheKey;
            FormView1.DataBind();


        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //ObjectDataSource1.Update();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                var i = e.Row.FindControl("CompareValidator1") as CompareValidator;
                var j = e.Row.FindControl("RequiredFieldValidator3") as RequiredFieldValidator;
                var k = e.Row.FindControl("TextBox3") as TextBox;
                var l = e.Row.FindControl("Label13") as Label;
                var m = e.Row.FindControl("LinkButton1") as LinkButton;
                if (((Assignment)e.Row.DataItem).sheetSubmited)
                {
                    i.Enabled = false;
                    j.Enabled = false;
                    k.Visible = false;
                    m.Visible = false;
                    l.Visible = true;
                }
                else if (((Assignment)e.Row.DataItem).ipAddress == GetIPAddress()|| string.IsNullOrEmpty(((Assignment)e.Row.DataItem).ipAddress))
                {
                    
                    i.Enabled = false;
                    j.Enabled = false;
                    k.Visible = false;
                    l.Visible = false;
                }
            }
        }

        protected void ObjectDataSource2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["assId"] = Convert.ToInt32(ViewState["assId"]);
            
        }
        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            SheetQ sheetQ = (SheetQ)FormView1.DataItem;
            if (sheetQ == null) return;
            if (ViewState["assId"] == null) Response.Redirect("/pages/Submitted.html");
            if(Convert.ToInt32(ViewState["assId"])!=sheetQ.Sheet.Assignment.AssignmentId) Response.Redirect("/pages/Submitted.html");
            QTemplate item = sheetQ.QTemplate;
            //QTemplate item = ((SheetQ)FormView1.DataItem).QTemplate;
            ArrayList arr = new ArrayList();
            ArrayList list = new ArrayList();
            list.Add(new { Code = item.op1, Name = "1" });
            list.Add(new { Code = item.op2, Name = "2" });
            list.Add(new { Code = item.op3, Name = "3" });
            list.Add(new { Code = item.op4, Name = "4" });
            list.Add(new { Code = item.op1, Name = "1" });
            list.Add(new { Code = item.op2, Name = "2" });
            list.Add(new { Code = item.op3, Name = "3" });

            ArrayList list2 = new ArrayList();
            list2.Add(new { Code = item.op1, Name = "1" });
            list2.Add(new { Code = item.op2, Name = "2" });
            list2.Add(new { Code = item.op3, Name = "3" });
            list2.Add(new { Code = item.op4, Name = "4" });
            list2.Add(new { Code = item.op5, Name = "5" });
            list2.Add(new { Code = item.op1, Name = "1" });
            list2.Add(new { Code = item.op2, Name = "2" });
            list2.Add(new { Code = item.op3, Name = "3" });
            list2.Add(new { Code = item.op4, Name = "4" });

            if (item.qType == 1 || item.qType == 2)
            {
                list = item.qType == 1?list.GetRange(sheetQ.optionOffset, 4): list2.GetRange(sheetQ.optionOffset, 5);
            }
            else
            {
                list = list.GetRange(0, 4);
            }

            var panel1 = FormView1.FindControl("Panel12");
            var panel2 = FormView1.FindControl("Panel22");
            var panel4 = FormView1.FindControl("Panel42");
            var lb1 = (Label)FormView1.TopPagerRow.FindControl("Label1");
            var lb2 = (Label)FormView1.TopPagerRow.FindControl("Label2");
            lb1.Text = Convert.ToString(ViewState["id"]);
            lb2.Text = Convert.ToString(ViewState["name"]);


            if (item.qType == 1 || item.qType == 3)
            {
                //单选和是非题
                panel1.Visible = true;
                panel2.Visible = false;
                panel4.Visible = false;
                RadioButtonList rbl = (RadioButtonList)FormView1.FindControl("RadioButtonList12");
                if (item.qType == 3)
                {
                    list.RemoveRange(2, 2);
                }
                rbl.DataSource = list;
                rbl.DataTextField = "Code";
                rbl.DataValueField = "Name";
                if (ViewState["answers"]!=null)
                {
                    string[] answers = ViewState["answers"] as string[];
                    if (!string.IsNullOrEmpty(answers[FormView1.PageIndex]))
                    {
                        rbl.SelectedValue = answers[FormView1.PageIndex];
                    }
                    
                }
                rbl.DataBind();
            }
            else if (item.qType == 2)
            {
                //多选题
                panel1.Visible = false;
                panel2.Visible = true;
                panel4.Visible = false;
                CheckBoxList cbl = (CheckBoxList)FormView1.FindControl("CheckBoxList12");
                cbl.DataSource = list;
                cbl.DataTextField = "Code";
                cbl.DataValueField = "Name";

                cbl.DataBind();
                if (ViewState["answers"]!=null)
                {
                    string[] answers = ViewState["answers"] as string[];
                    char[] charSeparators = new char[] { ',' };
                    if (!string.IsNullOrEmpty(answers[FormView1.PageIndex])) {
                        string[] arr1 = answers[FormView1.PageIndex].Split(charSeparators);
                        cbl.Items.Cast<ListItem>().OrderBy(x => x.Value).Where(x => arr1.Contains(x.Value)).ToList().ForEach(x => x.Selected = true);
                    }
                    

                }
            }
            else
            {
                //简答题
                panel1.Visible = false;
                panel2.Visible = false;
                panel4.Visible = true;
                TextBox t1 = (TextBox)FormView1.FindControl("TextBox12");
                TextBox t2 = (TextBox)FormView1.FindControl("TextBox22");
                TextBox t3 = (TextBox)FormView1.FindControl("TextBox32");
                t1.Text = ViewState["answer1"] as string;
                t2.Text = ViewState["answer2"] as string;
                t3.Text = ViewState["answer3"] as string;
            }
            FormViewRow pagerRow = FormView1.TopPagerRow;
            //pagerRow = FormView1.TopPagerRow;
            Repeater rpt =(Repeater)pagerRow.FindControl("rptPagesHistory");
            if (rpt.Items.Count == 0) {
                rpt.DataSource = Enumerable.Range(1, FormView1.PageCount);
                rpt.DataBind();
            }
            
        }

        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            //QTemplate item = (QTemplate)e.OldValues["QTemplate"];
            Label lb = (Label)FormView1.FindControl("Label12");
            if (lb.Text == "1" || lb.Text == "3")
            {
                RadioButtonList rbl = (RadioButtonList)FormView1.FindControl("RadioButtonList12");
                if (!String.IsNullOrEmpty(rbl.SelectedValue))
                {
                    e.NewValues["answer"] = rbl.SelectedValue;
                }
            }
            else if (lb.Text == "2")
            {
                CheckBoxList cbl = (CheckBoxList)FormView1.FindControl("CheckBoxList12");
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
        protected void rptPagesHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lnkPageNumber = new LinkButton();
            System.Int32 pageNumber = (System.Int32)e.Item.DataItem;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                lnkPageNumber = (LinkButton)e.Item.FindControl("lnkPageNumber");
                lnkPageNumber.Text = Convert.ToString(pageNumber);
                lnkPageNumber.CommandArgument = Convert.ToString(pageNumber - 1);
                if (e.Item.ItemIndex == FormView1.PageIndex)
                {
                    lnkPageNumber.CssClass = "cItem";
                }
                var arr = ViewState["arrChecked"] as bool[];
                if (arr != null)
                {
                    if (arr[e.Item.ItemIndex])
                    {
                        lnkPageNumber.CssClass += " checked";
                    }
                }
            }
            
        }

        protected void rptPagesHistory_Load(object sender, EventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            if (rpt.Items.Count != FormView1.PageCount)
            {
                rpt.DataSource = Enumerable.Range(1, FormView1.PageCount);
                rpt.DataBind();
            }
            //rpt.DataSource = Enumerable.Range(1, FormView1.PageCount);
            //rpt.DataBind();
        }

        protected void lnkPageNumberHistory_Click(object sender, EventArgs e)
        {
            CollectData();
            LinkButton btn = (LinkButton)sender;
            FormView1.PageIndex =Convert.ToInt32(btn.CommandArgument);
            //FormView1.DataBind();
        }

        protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            var arr = ViewState["arrChecked"] as bool[]??new bool[FormView1.PageCount];
            arr[FormView1.PageIndex] = true;
            ViewState["arrChecked"] = arr;
            FormViewRow pagerRow = FormView1.TopPagerRow;
            Repeater rpt = (Repeater)pagerRow.FindControl("rptPagesHistory");
            rpt.DataBind();


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //CollectData();
            SaveData();
            TextBox1.Text = "";
            TextBox2.Text = "";
            MultiView1.ActiveViewIndex = 3;
            ViewState["submitted"] = true;
            ObjectDataSource1.Update();
            
        }

        protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

            CollectData();

        }

        protected void CollectData()
        {
            string[] answers;
            if (ViewState["answers"] == null)
            {
                answers = new String[FormView1.PageCount];
            }
            else
            {
                answers = ViewState["answers"] as string[];
            }
            RadioButtonList rbl = (RadioButtonList)FormView1.FindControl("RadioButtonList12");
            CheckBoxList cbl = (CheckBoxList)FormView1.FindControl("CheckBoxList12");
            TextBox t1 = (TextBox)FormView1.FindControl("TextBox12");
            TextBox t2 = (TextBox)FormView1.FindControl("TextBox22");
            TextBox t3 = (TextBox)FormView1.FindControl("TextBox32");
            if (rbl.Items.Count > 0)
            {
                answers[FormView1.PageIndex] = rbl.SelectedValue;
                ViewState["answers"] = answers;
            }
            else if (cbl.Items.Count > 0)
            {
                answers[FormView1.PageIndex] = string.Join(",", cbl.Items.Cast<ListItem>().Where(x => x.Selected).OrderBy(x => x.Value).Select(x => x.Value).ToArray());
                ViewState["answers"] = answers;
            }
            else
            {
                ViewState["answer1"] = t1.Text;
                ViewState["answer2"] = t2.Text;
                ViewState["answer3"] = t3.Text;
            }
        }

        protected void ObjectDataSource2_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            
            
            //e.InputParameters.Add("answers", answers);
            //e.InputParameters.Add("answer1", answer1);
            //e.InputParameters.Add("answer2", answer2);
            //e.InputParameters.Add("answer3", answer3);
        }
        private void SaveData()
        {
            try
            {
                CollectData();
                string[] vAnswers = (ViewState["answers"] as string[]);
                string answers = ViewState["answers"] == null ? "" : string.Join("|", vAnswers);
                string answer1 = ViewState["answer1"] as string;
                string answer2 = ViewState["answer2"] as string;
                string answer3 = ViewState["answer3"] as string;
                Label lbs = (Label)FormView1.FindControl("Label3");
                using (OnlineExamContext context = new OnlineExamContext())
                {
                    int sheetId = Convert.ToInt32(lbs.Text);
                    Sheet sheet = context.Sheets.SingleOrDefault(x => x.SheetId == sheetId);
                    if (sheet != null)
                    {
                        sheet.answers = answers;
                        sheet.answer1 = answer1;
                        sheet.answer2 = answer2;
                        sheet.answer3 = answer3;
                        context.SaveChanges();
                        var arr = ViewState["arrChecked"] as bool[] ?? new bool[FormView1.PageCount];
                        for (int i = 0; i < vAnswers.Length; i++)
                        {
                            arr[i] = !string.IsNullOrEmpty(vAnswers[i]);
                        }
                        arr[FormView1.PageCount - 1] = !(string.IsNullOrEmpty(answer1) && string.IsNullOrEmpty(answer2) && string.IsNullOrEmpty(answer3));
                        ViewState["arrChecked"] = arr;
                        FormViewRow pagerRow = FormView1.TopPagerRow;
                        Repeater rpt = (Repeater)pagerRow.FindControl("rptPagesHistory");
                        rpt.DataSource = Enumerable.Range(1, FormView1.PageCount);
                        rpt.DataBind();


                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("/pages/submitError.html");
            }
        }
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            SaveData();
        }
    }
}