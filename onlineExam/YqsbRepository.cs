using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WebApplication4.Models;
namespace WebApplication4.DAL
{
    public interface IYqsbRepository : IDisposable
    {
        IEnumerable<Yqsbb> GetYqsbbs();
        void InsertYqsbb(Yqsbb yqsbb);
        void DeleteYqsbb(Yqsbb yqsbb);
        void UpdateYqsbb(Yqsbb yqsbb, Yqsbb origYqsbb);
        void SaveOrUpdate(Yqsbb yqsbb);
    }
    public class YqsbRepository:IDisposable, IYqsbRepository
    {
       
        private ApplicationDbContext context = new ApplicationDbContext();
        public  void SaveOrUpdate
    ( Yqsbb entity)
    
        {
            try
            {
                
                Yqsbb origYqsbb = context.Yqsbbs.FirstOrDefault(x => x.yqbh == entity.yqbh);
                if (origYqsbb == null)
                {
                    InsertYqsbb(entity);
                }
                else
                {
                    UpdateYqsbb(entity, origYqsbb);
                }
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

      
        public IEnumerable<Yqsbb> GetYqsbbs()
        {
            return context.Yqsbbs.ToList();
        }
        public void InsertYqsbb(Yqsbb yqsbb)
        {
            try
            {

                context.Yqsbbs.Add(yqsbb);
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

        public void DeleteYqsbb(Yqsbb yqsbb)
        {
            try
            {
                context.Yqsbbs.Attach(yqsbb);
                context.Yqsbbs.Remove(yqsbb);
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
        public void UpdateYqsbb(Yqsbb yqsbb, Yqsbb origYqsbb)
        {
            try
            {
                
                context.Yqsbbs.Attach(origYqsbb);
                //context.ApplyCurrentValues("Departments", department);
                ((IObjectContextAdapter)context).ObjectContext.ApplyCurrentValues("Yqsbbs", yqsbb);
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