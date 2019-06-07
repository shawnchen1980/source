using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using onlineExam.DAL;
using onlineExam.Models;
namespace onlineExam.BLL
{
    public class QTemplateBLL : IDisposable
    {
        private IQTemplateRepository qTemplateRepository;
        public QTemplateBLL()
        {
            this.qTemplateRepository = new QTemplateRepository();
        }
        public QTemplateBLL(IQTemplateRepository qTemplateRepository)
        {
            this.qTemplateRepository = qTemplateRepository;
        }
        public IEnumerable<QTemplate> GetQTemplates()
        {

            return qTemplateRepository.GetQTemplates();
            //return yqsbRepository.GetYqsbbs().Where(s => s.yqbh == id).ToList();
        }
        public IEnumerable<QTemplate> GetQTemplates(int id)
        {
            if (id == 0)
                return qTemplateRepository.GetQTemplates();
            return qTemplateRepository.GetQTemplates().Where(s => s.qid == id).ToList();
        }
        public QTemplate GetQTemplate(int id)
        {
            if (id == 0)
                return null;
            return qTemplateRepository.GetQTemplates().FirstOrDefault(s => s.qid == id);
        }
        public IEnumerable<QTemplate> ImportQTemplate(HttpPostedFile file)
        {
            if (file != null && file.ContentLength > 0)
            {
                using (ExcelPackage package = new ExcelPackage(file.InputStream))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                    List<QTemplate> list = new List<QTemplate>();
                    int length = sheet.Cells["A:A"].Count();
                    for (int i = 2; i <= length; i++)
                    {
                        string str = "A" + i + ":L" + i;
                        if (sheet.Cells[str].Count() < 12)
                        {
                            continue;
                        }
                        var arr = sheet.Cells[str].ToArray();
                        list.Add(new QTemplate()
                        {
                            //qid = Convert.ToInt32(arr[0].Value),
                            qtext1 = Convert.ToString(arr[1].Value),
                            qtext2 = Convert.ToString(arr[2].Value),
                            qType = Convert.ToInt32(arr[3].Value),
                            opLength = Convert.ToInt32(arr[4].Value),
                            op1 = Convert.ToString(arr[5].Value),
                            op2 = Convert.ToString(arr[6].Value),
                            op3 = Convert.ToString(arr[7].Value),
                            op4 = Convert.ToString(arr[8].Value),
                            op5 = Convert.ToString(arr[9].Value),
                            answer = Convert.ToString(arr[10].Value),
                            answer2 = Convert.ToString(arr[11].Value),
                            answer3 = Convert.ToString(arr[12].Value)
                        });
                    }
                    return list;
                }
                //string fname = Path.GetFileName(file.FileName);
                //file.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));
            }
            return null;
        }
        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    qTemplateRepository.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~YqsbBL()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        void IDisposable.Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }

        public void InsertQTemplate(QTemplate yqsbb)
        {
            try
            {
                qTemplateRepository.InsertQTemplate(yqsbb);
            }
            catch (Exception ex)
            {

                throw new Exception("插入失败，可能由于设备编号重复或输入数据不符合要求");
            }

        }

        public void DeleteQTemplate(QTemplate yqsbb)
        {
            try
            {
                qTemplateRepository.DeleteQTemplate(yqsbb);
            }
            catch (Exception ex)
            {

                throw new Exception("删除失败，可能由于该设备已经不存在");
            }
        }
        public void UpsertQTemplate(QTemplate yqsbb)
        {
            qTemplateRepository.SaveOrUpdate(yqsbb);
        }
        public void UpdateQTemplate(QTemplate yqsbb, QTemplate origYqsbb)
        {
            try
            {
                qTemplateRepository.UpdateQTemplate(yqsbb, origYqsbb);
            }
            catch (Exception)
            {

                throw new Exception("更新失败，可能由于设备不存在或数据不符合要求");
            }
        }
        #endregion

    }
}