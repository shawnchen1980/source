using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using onlineExam.Models;
namespace onlineExam.Pages
{
    public partial class DoReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string ShowStatus(object submitted, object firstLogin)
        {
            //Here you can place as many conditions as you like 
            //Provided you always return either true or false
            if (firstLogin == null && !Convert.ToBoolean(submitted))
                return "待考";
            else if (firstLogin != null && !Convert.ToBoolean(submitted))
                return "考试";
            else if (firstLogin != null && Convert.ToBoolean(submitted))
                return "完成";
            else
                return "异常，已交卷但无登陆记录";

        }
        protected int ShowScore(object submitted, object sheetQ)
        {
            if (!Convert.ToBoolean(submitted))
            {
                return 0;
            }
            var sheet = (Sheet)sheetQ;
            return sheet.SheetQs.Select(x => x.scored).Sum();
            //return 0;
        }
        protected void Button8_Click(object sender, EventArgs e)
        {
            GridView2.DataBind();
            Label2.Text = "查询结果：" + Convert.ToString(GridView2.Rows.Count) + "条记录";
        }
        protected void Button10_Click(object sender, EventArgs e)
        {
            var item = DropDownList2.Items[0];
            DropDownList2.Items.Clear();
            DropDownList2.Items.Add(item);
            DropDownList2.DataBind();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label3.Text=GridView2.SelectedValue.ToString();
        }

        protected void DropDownList2_DataBound(object sender, EventArgs e)
        {

        }

        protected void ObjectDataSource7_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            var a=e.ReturnValue;
        }
        protected int ShowScore(object answers,object ranswers, object scores)
        {
            string ans = Convert.ToString(answers);
            if (string.IsNullOrEmpty(ans)) return 0;
            Char dl = '|';
            string[] ansArr = ans.Split(dl).ToArray();
            string[] ransArr = Convert.ToString(ranswers).Split(dl).ToArray();
            int[] scoresArr = Convert.ToString(scores).Split(dl).Select(x => Convert.ToInt32(x)).ToArray();
            int finalScore = scoresArr.Select((x, i) => ansArr[i] == ransArr[i] ? x : 0).Sum();
            return finalScore;

            //return 0;
        }
    }
}