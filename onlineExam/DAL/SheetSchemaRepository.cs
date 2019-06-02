using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using onlineExam.Models;
namespace onlineExam.DAL
{
    public interface ISheetSchemaRepository : IDisposable
    {
        IEnumerable<SheetSchema> GetSheetSchemas();
        void InsertSheetSchema(SheetSchema item);
        void DeleteSheetSchema(SheetSchema item);
        void UpdateSheetSchema(SheetSchema item, SheetSchema origItem);
        void SaveOrUpdate(SheetSchema item);
    }
    public class SheetSchemaRepository : IDisposable, ISheetSchemaRepository
    {
        private OnlineExamContext context = new OnlineExamContext();
        public void SaveOrUpdate
    (SheetSchema entity)

        {
            try
            {

                SheetSchema origYqsbb = context.SheetSchemas.FirstOrDefault(x => x.SheetSchemaId == entity.SheetSchemaId);
                if (origYqsbb == null)
                {
                    InsertSheetSchema(entity);
                }
                else
                {
                    UpdateSheetSchema(entity, origYqsbb);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<SheetSchema> GetSheetSchemas()
        {
            return context.SheetSchemas.Include("SheetSchemaQs.QTemplate").ToList();
        }
        public void InsertSheetSchema(SheetSchema yqsbb)
        {
            try
            {

                context.SheetSchemas.Add(yqsbb);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //Include catch blocks for specific exceptions first,
                //and handle or log the error as appropriate in each.
                //Include a generic catch block like this one last.
                throw ex;
            }
        }

        public void DeleteSheetSchema(SheetSchema yqsbb)
        {
            try
            {
                context.SheetSchemas.Attach(yqsbb);
                context.SheetSchemas.Remove(yqsbb);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //Include catch blocks for specific exceptions first,
                //and handle or log the error as appropriate in each.
                //Include a generic catch block like this one last.
                throw ex;
            }
        }
        public void UpdateSheetSchema(SheetSchema yqsbb, SheetSchema origYqsbb)
        {
            try
            {

                context.SheetSchemas.Attach(origYqsbb);
                //context.ApplyCurrentValues("Departments", department);
                ((IObjectContextAdapter)context).ObjectContext.ApplyCurrentValues("SheetSchemas", yqsbb);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //Include catch blocks for specific exceptions first,
                //and handle or log the error as appropriate in each.
                //Include a generic catch block like this one last.
                throw ex;
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
                    context.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~YqsbRepository()
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
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}