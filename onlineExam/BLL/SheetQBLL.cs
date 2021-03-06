﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using onlineExam.DAL;
using onlineExam.Models;
using onlineExam.Utilities;
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
        public List<SheetQ> GetSheetQsForExam(int assId)
        {
            //return sheetQRepository.GetSheetQs().Where(x => x.Sheet.Assignment.AssignmentId == assId).OrderBy(x => x.QTemplate.qType).ThenBy(x => x.qOrder).ToList();
            string assid = Convert.ToString(assId);
            if (CacheHelper.Exists(assid))
            {
                List<SheetQ> res;
                CacheHelper.Get<List<SheetQ>>(assid,out res);
                if (res != null)
                {
                    return res;
                }
                
            }
            using (OnlineExamContext context=new OnlineExamContext())
            {
                var sheet = context.Sheets.Include("Assignment.SheetSchema.SheetSchemaQs.QTemplate").FirstOrDefault(x => x.Assignment.AssignmentId == assId);
                if (sheet != null)
                {
                    var qArray = sheet.Assignment.SheetSchema.SheetSchemaQs.OrderBy(x => x.qOrder).Select(x => x.QTemplate).ToArray();
                    Char dl = '|';
                    var offArray = sheet.qOffs.Split(dl).ToArray().Select(x=>Convert.ToInt32(x)).ToArray();

                    var res= sheet.qOrders.Split(dl).ToArray().Select(x=>Convert.ToInt32(x)).Select((x, i) => new SheetQ { QTemplate = qArray[x], qOrder = i, Sheet = sheet, optionOffset = offArray[i] }).ToList();
                    CacheHelper.Add<List<SheetQ>>(res, assid, 1);
                    return res;
                }
            }
            return null;
        }
        public void UpdateSheetQForExam(SheetQ item, SheetQ origItem)
        {
            using (OnlineExamContext context=new OnlineExamContext())
            {
                var sheet = context.SheetQs.Include("Sheet.Assignment").Include("QTemplate").FirstOrDefault(x => x.SheetQId == item.SheetQId);
                
                if (sheet == null) return;
                if (sheet.Sheet.Assignment.sheetSubmited) return;
                sheet.answer = item.answer;
                sheet.answer2 = item.answer2;
                sheet.answer3 = item.answer3;
                sheet.scored = sheet.answer == sheet.correctAnswer&&sheet.QTemplate.qType!=4 ? sheet.score : 0;
                context.SaveChanges();

            }
           // sheetQRepository.UpdateSheetQ(item, origItem);
        }
        public void UpdateSheetForExam(SheetQ item,string answers,string answer1,string answer2,string answer3)
        {

        }
        public void UpdateSheetForExam(SheetQ item)
        {

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