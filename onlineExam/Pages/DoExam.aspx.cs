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
            MultiView1.ActiveViewIndex = 1;
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
            ObjectDataSource1.Update();
            
            MultiView1.ActiveViewIndex = 2;
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
            if (item.qType == 1 || item.qType == 2)
            {
                list = list.GetRange(sheetQ.optionOffset, 4);
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
                CheckBoxList cbl = (CheckBoxList)FormView1.FindControl("CheckBoxList12");
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
            FormViewRow pagerRow = FormView1.BottomPagerRow;
            pagerRow = FormView1.TopPagerRow;
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
            TextBox1.Text = "";
            TextBox2.Text = "";
            MultiView1.ActiveViewIndex = 0;
            ViewState["submitted"] = true;
            ObjectDataSource1.Update();
        }
    }
}