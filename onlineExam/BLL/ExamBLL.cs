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
    public class ExamBLL:IDisposable, IExamRepository
    {
        
        private IExamRepository examRepository;

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用
        public ExamBLL()
        {
            this.examRepository = new ExamRepository();
        }
        public ExamBLL(IExamRepository exam)
        {
            this.examRepository = exam;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    examRepository.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~ExamBLL()
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

        public IEnumerable<Exam> GetExams()
        {
            return examRepository.GetExams();
        }

        public void InsertExam(Exam qt)
        {
            examRepository.InsertExam(qt);
        }

        public void DeleteExam(Exam qt)
        {
            examRepository.DeleteExam(qt);
        }

        public void UpdateExam(Exam qt, Exam origqt)
        {
            examRepository.UpdateExam(qt, origqt);
        }

        public void SaveOrUpdate(Exam qt)
        {
            examRepository.SaveOrUpdate(qt);
        }
        #endregion
    }
}