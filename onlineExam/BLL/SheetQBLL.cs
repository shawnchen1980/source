using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using onlineExam.DAL;
using onlineExam.Models;

namespace onlineExam.BLL
{
    [DataObjectAttribute]
    public class SheetQBLL:IDisposable
    {
        private ISheetQRepository sheetQRepository;
        public SheetQBLL()
        {
            this.sheetQRepository = new SheetQRepository();
        }
        public SheetQBLL(ISheetQRepository sheetQRepository)
        {
            this.sheetQRepository = sheetQRepository;
        }
        public IEnumerable<SheetQ> GetSheetQsForExam(int assId)
        {
            return sheetQRepository.GetSheetQs().Where(x => x.Sheet.Assignment.AssignmentId == assId).OrderBy(x => x.QTemplate.qType).ThenBy(x => x.qOrder).ToList();
        }
        public void UpdateSheetQForExam(SheetQ item, SheetQ origItem)
        {
            sheetQRepository.UpdateSheetQ(item, origItem);
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
                    sheetQRepository.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~SheetQBLL()
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