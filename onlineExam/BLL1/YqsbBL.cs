using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.DAL;
using WebApplication4.Models;

namespace WebApplication4.BLL
{
    public class YqsbBL:IDisposable
    {
        private IYqsbRepository yqsbRepository;
        public YqsbBL()
        {
            this.yqsbRepository = new YqsbRepository();
        }
        public YqsbBL(IYqsbRepository yqsbRepository)
        {
            this.yqsbRepository = yqsbRepository;
        }
        public IEnumerable<Yqsbb> GetYqsbbs()
        {
            
                return yqsbRepository.GetYqsbbs();
            //return yqsbRepository.GetYqsbbs().Where(s => s.yqbh == id).ToList();
        }
        public IEnumerable<Yqsbb> GetYqsbbs(string id)
        {
            if (id == "")
                return yqsbRepository.GetYqsbbs();
            return yqsbRepository.GetYqsbbs().Where(s=>s.yqbh==id).ToList();
        }
        public IEnumerable<Yqsbb> ImportYqsbbs(HttpPostedFile file)
        {
            if (file != null && file.ContentLength > 0)
            {
                using (ExcelPackage package = new ExcelPackage(file.InputStream))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                    List<Yqsbb> list = new List<Yqsbb>();
                    int length = sheet.Cells["A:A"].Count();
                    for (int i = 2; i <= length; i++)
                    {
                        string str = "A" + i + ":N" + i;
                        if (sheet.Cells[str].Count() < 14)
                        {
                            continue;
                        }
                        var arr = sheet.Cells[str].ToArray();
                        list.Add(new Yqsbb()
                        {
                            sxdm = (string)(arr[0].Value),
                            yqbh =(string)arr[1].Value,
                            flh = (string)arr[2].Value,
                            yqmc = (string)arr[3].Value,
                            xh = (string)arr[4].Value,
                            gg = (string)arr[5].Value,
                            yqly =(string)arr[6].Value,
                            gbm = (string)arr[7].Value,
                            dj = Convert.ToInt64(arr[8].Value),
                            gzrq =(string)arr[9].Value,
                            xzm = (string)arr[10].Value,
                            xyfx = (string)arr[11].Value,
                            dwbh = (string)arr[12].Value,
                            dwmc = (string)arr[13].Value
                        });
                    }
                    return list;
                }
                //string fname = Path.GetFileName(file.FileName);
                //file.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));
            }
            return null;
        }
        public void UpsertYqsbb(Yqsbb yqsbb)
        {
            yqsbRepository.SaveOrUpdate(yqsbb);
        }
        public void DeleteYqsbb(string id)
        {
            var item=yqsbRepository.GetYqsbbs().SingleOrDefault(x=>x.yqbh==id);
            DeleteYqsbb(item);
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
                    yqsbRepository.Dispose();
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

        public void InsertYqsbb(Yqsbb yqsbb)
        {
            try
            {
                yqsbRepository.InsertYqsbb(yqsbb);
            }
            catch (Exception ex)
            {

                throw new Exception("插入失败，可能由于设备编号重复或输入数据不符合要求") ;
            }
            
        }

        public void DeleteYqsbb(Yqsbb yqsbb)
        {
            try
            {
                yqsbRepository.DeleteYqsbb(yqsbb);
            }
            catch (Exception ex)
            {

                throw new Exception("删除失败，可能由于该设备已经不存在");
            }
        }

        public void UpdateYqsbb(Yqsbb yqsbb, Yqsbb origYqsbb)
        {
            try
            {
                yqsbRepository.UpdateYqsbb(yqsbb, origYqsbb);
            }
            catch (Exception)
            {

                throw new Exception("更新失败，可能由于设备不存在或数据不符合要求");
            }
        }
        #endregion

    }
}