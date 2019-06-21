using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using onlineExam.DAL;
using onlineExam.Models;
using System.ComponentModel;

namespace onlineExam.BLL
{
    [DataObjectAttribute]
    public class SheetBLL:IDisposable
    {
        private ISheetRepository sheetRepository;
        public SheetBLL()
        {
            this.sheetRepository = new SheetRepository();
        }
        public SheetBLL(ISheetRepository sheet)
        {
            this.sheetRepository = sheet;
        }
        public Sheet GetSheetByAssignmentId(int assId)
        {
            return this.sheetRepository.GetSheets().FirstOrDefault(x => x.Assignment.AssignmentId == assId);
        }
        public void UpdateSheetForReview(Sheet sheet)
        {
            using (OnlineExamContext context=new OnlineExamContext())
            {
                Sheet s = context.Sheets.FirstOrDefault(x => x.SheetId == sheet.SheetId);
                if (s != null)
                {
                    s.score1 = sheet.score1;
                    s.score2 = sheet.score2;
                    s.marker = sheet.marker;
                    context.SaveChanges();
                }
            }
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
                    sheetRepository.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~SheetBLL()
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