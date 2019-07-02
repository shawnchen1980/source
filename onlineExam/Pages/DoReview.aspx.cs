using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using onlineExam.Models;
using onlineExam.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using onlineExam.BLL;
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
            if (MultiView1.ActiveViewIndex == 0)
            {
                GridView2.DataBind();
            }
            else if (MultiView1.ActiveViewIndex == 1)
            {
                GridView1.DataBind();
            }
            
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
            return GradeHelper.CalScore(answers, ranswers, scores);
            //string ans = Convert.ToString(answers);
            //if (string.IsNullOrEmpty(ans)) return 0;
            //Char dl = '|';
            //string[] ansArr = ans.Split(dl).ToArray();
            //string[] ransArr = Convert.ToString(ranswers).Split(dl).ToArray();
            //int[] scoresArr = Convert.ToString(scores).Split(dl).Select(x => Convert.ToInt32(x)).ToArray();
            //int finalScore = scoresArr.Select((x, i) => ansArr[i] == ransArr[i] ? x : 0).Sum();
            //return finalScore;

            //return 0;
        }

        protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            Sheet sheet = (Sheet)e.InputParameters["sheet"];
            if (sheet == null)
            {
                sheet = new Sheet { };
            }
            TextBox t1 =(TextBox)FormView1.FindControl("TextBox1");
            TextBox t2 = (TextBox)FormView1.FindControl("TextBox2");
            TextBox t3 = (TextBox)FormView1.FindControl("TextBox3");
            TextBox tscore = (TextBox)FormView1.FindControl("score1TextBox");
            sheet.score2 = Convert.ToInt32(t1.Text) + Convert.ToInt32(t2.Text) + Convert.ToInt32(t3.Text);
            sheet.score1 = Convert.ToInt32(tscore.Text);
            sheet.marker = ViewState["Reviewer"] as string??"unknown";
            //e.InputParameters["sheet"] = sheet;
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            MultiView1.ActiveViewIndex = Convert.ToInt32(e.Item.Value);
        }
        public void RunSample1()
        {
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Grades");
                using (var data = new ExamBLL())
                {
                    int exId =Convert.ToInt32(DropDownList2.SelectedValue);
                    string sId = TextBox7.Text;
                    string sName = TextBox8.Text;
                    int status = Convert.ToInt32(DropDownList3.SelectedValue);
                    int sheetId =string.IsNullOrEmpty(TextBox9.Text)?0: Convert.ToInt32(TextBox9.Text);
                    string classId = TextBox10.Text;
                    worksheet.Cells["A2"].LoadFromCollection(data.GetSheetExportByExam(exId,sId,sName,status,sheetId,classId), false, TableStyles.Medium9);
                    //Add the headers
                    worksheet.Cells[1, 1].Value = "考试编号";
                    worksheet.Cells[1, 2].Value = "考试名称";
                    worksheet.Cells[1, 3].Value = "试卷编号";
                    worksheet.Cells[1, 4].Value = "学号";
                    worksheet.Cells[1, 5].Value = "姓名";
                    worksheet.Cells[1, 6].Value = "班级";
                    worksheet.Cells[1, 7].Value = "单选题得分";
                    worksheet.Cells[1, 8].Value = "多选题得分";
                    worksheet.Cells[1, 9].Value = "是非题得分";
                    worksheet.Cells[1, 10].Value = "简答题得分";
                    worksheet.Cells[1, 11].Value = "总分";
                    worksheet.Cells[1, 12].Value = "阅卷人";
                    worksheet.Cells[1, 13].Value = "第一小题回答";
                    worksheet.Cells[1, 14].Value = "第二小题回答";
                    worksheet.Cells[1, 15].Value = "第三小题回答";
                }
                //ExamId	ExamName	SheetId	StuId	StuName	StuClass	score1	score2	scoreSum	marker

                //Add the headers
                //worksheet.Cells[1, 1].Value = "ID";
                //worksheet.Cells[1, 2].Value = "Product";
                //worksheet.Cells[1, 3].Value = "Quantity";
                //worksheet.Cells[1, 4].Value = "Price";
                //worksheet.Cells[1, 5].Value = "Value";

                ////Add some items...
                //worksheet.Cells["A2"].Value = 12001;
                //worksheet.Cells["B2"].Value = "Nails";
                //worksheet.Cells["C2"].Value = 37;
                //worksheet.Cells["D2"].Value = 3.99;

                //worksheet.Cells["A3"].Value = 12002;
                //worksheet.Cells["B3"].Value = "Hammer";
                //worksheet.Cells["C3"].Value = 5;
                //worksheet.Cells["D3"].Value = 12.10;

                //worksheet.Cells["A4"].Value = 12003;
                //worksheet.Cells["B4"].Value = "Saw";
                //worksheet.Cells["C4"].Value = 12;
                //worksheet.Cells["D4"].Value = 15.37;

                ////Add a formula for the value-column
                //worksheet.Cells["E2:E4"].Formula = "C2*D2";

                ////Ok now format the values;
                //using (var range = worksheet.Cells[1, 1, 1, 5])
                //{
                //    range.Style.Font.Bold = true;
                //    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                //    range.Style.Font.Color.SetColor(Color.White);
                //}

                //worksheet.Cells["A5:E5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //worksheet.Cells["A5:E5"].Style.Font.Bold = true;

                //worksheet.Cells[5, 3, 5, 5].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2, 3, 4, 3).Address);
                //worksheet.Cells["C2:C5"].Style.Numberformat.Format = "#,##0";
                //worksheet.Cells["D2:E5"].Style.Numberformat.Format = "#,##0.00";

                ////Create an autofilter for the range
                //worksheet.Cells["A1:E4"].AutoFilter = true;

                //worksheet.Cells["A2:A4"].Style.Numberformat.Format = "@";   //Format as text

                ////There is actually no need to calculate, Excel will do it for you, but in some cases it might be useful. 
                ////For example if you link to this workbook from another workbook or you will open the workbook in a program that hasn't a calculation engine or 
                ////you want to use the result of a formula in your program.
                //worksheet.Calculate();

                //worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                // lets set the header text 
                // worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Inventory";
                // add the page number to the footer plus the total number of pages
                // worksheet.HeaderFooter.OddFooter.RightAlignedText =
                //    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                // add the sheet name to the footer
                // worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                // add the file path to the footer
                // worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                // worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                // worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                // Change the sheet view to show it in page layout mode
                //worksheet.View.PageLayoutView = true;

                // set some document properties
                //package.Workbook.Properties.Title = "Invertory";
                //package.Workbook.Properties.Author = "Jan Källman";
                //package.Workbook.Properties.Comments = "This sample demonstrates how to create an Excel 2007 workbook using EPPlus";

                // set some extended property values
                //package.Workbook.Properties.Company = "AdventureWorks Inc.";

                // set some custom property values
                //package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                //package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

                //var xlFile = Utils.GetFileInfo("sample1.xlsx");
                // save our new workbook in the output directory and we are done!
                package.SaveAs(Response.OutputStream);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=download.xlsx");

                //return xlFile.FullName;
            }
        }
        public void RunSample2()
        {
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Grades");
                using (var data = new ExamBLL())
                {
                    int exId = Convert.ToInt32(DropDownList2.SelectedValue);
                    string reviewer = ViewState["Reviewer"] as string;


                    worksheet.Cells["A2"].LoadFromCollection(data.GetSheetExportByExamAndReviewer(exId, reviewer), false, TableStyles.Medium9);
                    //Add the headers
                    worksheet.Cells[1, 1].Value = "考试编号";
                    worksheet.Cells[1, 2].Value = "考试名称";
                    worksheet.Cells[1, 3].Value = "试卷编号";
                    worksheet.Cells[1, 4].Value = "学号";
                    worksheet.Cells[1, 5].Value = "姓名";
                    worksheet.Cells[1, 6].Value = "班级";
                    worksheet.Cells[1, 7].Value = "单选题得分";
                    worksheet.Cells[1, 8].Value = "多选题得分";
                    worksheet.Cells[1, 9].Value = "是非题得分";
                    worksheet.Cells[1, 10].Value = "简答题得分";
                    worksheet.Cells[1, 11].Value = "总分";
                    worksheet.Cells[1, 12].Value = "阅卷人";
                    worksheet.Cells[1, 13].Value = "第一小题回答";
                    worksheet.Cells[1, 14].Value = "第二小题回答";
                    worksheet.Cells[1, 15].Value = "第三小题回答";
                }
                
                package.SaveAs(Response.OutputStream);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=download.xlsx");

                //return xlFile.FullName;
            }
        }
        protected void Button11_Click(object sender, EventArgs e)
        {
            RunSample1();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ViewState["Reviewer"] = TextBox4.Text;
            MultiView1.ActiveViewIndex = 0;
        }

        protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == "NextEntry")
            {
                GridView2.SelectedIndex++;
            }
            else if (e.CommandName == "PrevEntry")
            {
                GridView2.SelectedIndex--;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            RunSample2();
        }
    }
}