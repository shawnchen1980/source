using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using onlineExam.DAL;
using onlineExam.Models;

namespace onlineExam.BLL
{
    public class SheetSchemaBLL:IDisposable
    {
        private ISheetSchemaRepository sheetSchemaRepository;

        public SheetSchemaBLL()
        {
            this.sheetSchemaRepository = new SheetSchemaRepository();
        }
        public SheetSchemaBLL(ISheetSchemaRepository sheetSchemaRepository)
        {
            this.sheetSchemaRepository = sheetSchemaRepository;
        }
        public SheetSchema GetSheetSchemaByName(string schemaName)
        {
            var res = this.sheetSchemaRepository.GetSheetSchemas().FirstOrDefault(x => x.name == schemaName);
            if (res == null)
            {
                res = new SheetSchema { name = schemaName };
                this.sheetSchemaRepository.InsertSheetSchema(res);
            }
            return res;
            
        }

        public SheetSchema ImportSheetSchema(HttpPostedFile file,string schemaName)
        {
            //var schema = GetSheetSchemaByName(schemaName);
            //schema.SheetSchemaQs= 
            using (OnlineExamContext context=new OnlineExamContext())
            {
                var schema = context.SheetSchemas.Include("SheetSchemaQs.QTemplate").FirstOrDefault(x => x.name == schemaName);
                if (schema == null)
                {
                    schema = new SheetSchema { name = schemaName };
                    context.SheetSchemas.Add(schema);
                }
                else
                {
                    //schema.SheetSchemaQs = null;
                    schema.SheetSchemaQs.ToList().ForEach(x => context.QTemplates.Remove(x.QTemplate));
                    context.SheetSchemaQs.RemoveRange(schema.SheetSchemaQs);

                    //foreach (var item in schema.SheetSchemaQs)
                    //{
                    //    context.SheetSchemaQs.re
                    //}
                }
                if (file != null && file.ContentLength > 0)
                {
                    using (ExcelPackage package = new ExcelPackage(file.InputStream))
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                        List<QTemplate> list = new List<QTemplate>();
                        int length = sheet.Cells["A:A"].Count();
                        for (int i = 2,j=0; i <= length; i++,j++)
                        {
                            string str = "A" + i + ":N" + i;
                            if (sheet.Cells[str].Count() < 14)
                            {
                                continue;
                            }
                            var arr = sheet.Cells[str].ToArray();
                            var qt=new QTemplate()
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
                                op5=Convert.ToString(arr[9].Value),
                                answer = Convert.ToString(arr[10].Value),
                                answer2 = Convert.ToString(arr[11].Value),
                                answer3 = Convert.ToString(arr[12].Value)
                            };
                            var schemaQ = new SheetSchemaQ { qOrder = j, score = Convert.ToInt32(arr[13].Value), QTemplate=qt, SheetSchema=schema };
                            context.SheetSchemaQs.Add(schemaQ);
                        }
                        context.SaveChanges();
                        return schema;
                    }
                    //string fname = Path.GetFileName(file.FileName);
                    //file.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));
                }
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
                    sheetSchemaRepository.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~SheetSchemaBLL()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}