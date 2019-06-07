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
    public interface IAssignmentRepository : IDisposable
    {
        IEnumerable<Assignment> GetAssignments();
        void InsertAssignment(Assignment qt);
        void DeleteAssignment(Assignment qt);
        void UpdateAssignment(Assignment qt, Assignment origqt);
        void SaveOrUpdate(Assignment qt);
    }
    public class AssignmentRepository : IDisposable, IAssignmentRepository
    {
        private OnlineExamContext context = new OnlineExamContext();
        public void SaveOrUpdate
    (Assignment entity)

        {
            try
            {

                Assignment origYqsbb = context.Assignments.FirstOrDefault(x => x.AssignmentId == entity.AssignmentId);
                if (origYqsbb == null)
                {
                    InsertAssignment(entity);
                }
                else
                {
                    UpdateAssignment(entity, origYqsbb);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public IEnumerable<Assignment> GetAssignments()
        {
            return context.Assignments.Include("Student").Include("Exam").Include("SheetSchema").ToList();
        }
        public void InsertAssignment(Assignment yqsbb)
        {
            try
            {

                context.Assignments.Add(yqsbb);
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

        public void DeleteAssignment(Assignment yqsbb)
        {
            try
            {
                context.Assignments.Attach(yqsbb);
                context.Assignments.Remove(yqsbb);
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
        public void UpdateAssignment(Assignment yqsbb, Assignment origYqsbb)
        {
            try
            {

                context.Assignments.Attach(origYqsbb);
                //context.ApplyCurrentValues("Departments", department);
                ((IObjectContextAdapter)context).ObjectContext.ApplyCurrentValues("Assignments", yqsbb);
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