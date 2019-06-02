using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using OfficeOpenXml;

using onlineExam.Models;
namespace onlineExam.DAL
{
    public interface IExamRepository : IDisposable
    {
        IEnumerable<Exam> GetExams();
        void InsertExam(Exam qt);
        void DeleteExam(Exam qt);
        void UpdateExam(Exam qt, Exam origqt);
        void SaveOrUpdate(Exam qt);
    }
    public class ExamRepository : IDisposable, IExamRepository
    {
        private OnlineExamContext context = new OnlineExamContext();
        public void SaveOrUpdate
    (Exam entity)

        {
            try
            {

                Exam origYqsbb = context.Exams.FirstOrDefault(x => x.ExamId == entity.ExamId);
                if (origYqsbb == null)
                {
                    InsertExam(entity);
                }
                else
                {
                    UpdateExam(entity, origYqsbb);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Exam> GetExams()
        {
            return context.Exams.ToList();
        }
        public void InsertExam(Exam yqsbb)
        {
            try
            {

                context.Exams.Add(yqsbb);
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

        public void DeleteExam(Exam yqsbb)
        {
            try
            {
                context.Exams.Attach(yqsbb);
                context.Exams.Remove(yqsbb);
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
        public void UpdateExam(Exam yqsbb, Exam origYqsbb)
        {
            try
            {

                context.Exams.Attach(origYqsbb);
                //context.ApplyCurrentValues("Departments", department);
                ((IObjectContextAdapter)context).ObjectContext.ApplyCurrentValues("Exams", yqsbb);
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