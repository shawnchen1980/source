﻿using System;
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
            //要选择已经分配好试卷的考试
            using (OnlineExamContext context=new OnlineExamContext())
            {
                var validExamArr = context.Assignments.Include("Exam").Include("Sheet").Where(x => x.Sheet != null).Select(x => x.Exam.ExamId).ToArray();
                return context.Exams.Where(x => validExamArr.Contains(x.ExamId)).ToArray();
            }
            //return examRepository.GetExams();
        }
        public IEnumerable<Exam> GetAllExams()
        {
            return examRepository.GetExams().OrderBy(x=>x.ExamId).ToList();
        }
        public IEnumerable<Exam> GetOpenedExams()
        {
            return examRepository.GetExams().Where(x => x.open).ToList();
        }
        public IEnumerable<SheetForExportDTO> GetSheetExportByExam(int exId, string sId, string sName, int status, int sheetId, string classId)
        {
            return GetAssignmentsByExam(exId, sId, sName, status, sheetId, classId).Select(x => new SheetForExportDTO {
                ExamId = x.Exam.ExamId,
                ExamName = x.Exam.name,
                SheetId = x.Sheet.SheetId,
                StuClass = x.Student.classId,
                StuId = x.Student.StudentId,
                StuName = x.Student.name,
                score1 = Utilities.GradeHelper.CalScoreRange(x.Sheet.answers, x.Sheet.qAns, x.Sheet.qScores, 0, 20),
                score3 = Utilities.GradeHelper.CalScoreRange(x.Sheet.answers, x.Sheet.qAns, x.Sheet.qScores, 20, 10),
                score4 = Utilities.GradeHelper.CalScoreRange(x.Sheet.answers, x.Sheet.qAns, x.Sheet.qScores, 30, 15),
                score2 = x.Sheet.score2,
                scoreSum=Utilities.GradeHelper.CalScore(x.Sheet.answers,x.Sheet.qAns,x.Sheet.qScores)+x.Sheet.score2,
                answer1=x.Sheet.answer1,
                answer2=x.Sheet.answer2,
                answer3=x.Sheet.answer3,
                marker=x.Sheet.marker});
        }
        public IEnumerable<SheetForExportDTO> GetSheetExportByExamAndReviewer(int exId, string reviewer)
        {
            return GetAssignmentsByExamAndReviewer(exId, reviewer).Select(x => new SheetForExportDTO
            {
                ExamId = x.Exam.ExamId,
                ExamName = x.Exam.name,
                SheetId = x.Sheet.SheetId,
                StuClass = x.Student.classId,
                StuId = x.Student.StudentId,
                StuName = x.Student.name,
                score1 = Utilities.GradeHelper.CalScoreRange(x.Sheet.answers, x.Sheet.qAns, x.Sheet.qScores, 0, 20),
                score3 = Utilities.GradeHelper.CalScoreRange(x.Sheet.answers, x.Sheet.qAns, x.Sheet.qScores, 20, 10),
                score4 = Utilities.GradeHelper.CalScoreRange(x.Sheet.answers, x.Sheet.qAns, x.Sheet.qScores, 30, 15),
                score2 = x.Sheet.score2,
                scoreSum = Utilities.GradeHelper.CalScore(x.Sheet.answers, x.Sheet.qAns, x.Sheet.qScores) + x.Sheet.score2,
                answer1 = x.Sheet.answer1,
                answer2 = x.Sheet.answer2,
                answer3 = x.Sheet.answer3,
                marker = x.Sheet.marker
            });
        }
        public IEnumerable<Assignment> GetAssignmentsByExam(int exId, string sId, string sName, int status, int sheetId,string classId)
        {
            var query = examRepository.GetExams().SingleOrDefault(x => x.ExamId == exId);
            if (query == null) return null;
            IEnumerable<Assignment> res = query.Assignments;
            if (!string.IsNullOrEmpty(sId))
            {
                res = res.Where(x => x.Student.StudentId == sId);
            }
            if (!string.IsNullOrEmpty(sName))
            {
                res = res.Where(x => x.Student.name == sName);
            }
            if (!string.IsNullOrEmpty(classId))
            {
                res = res.Where(x => x.Student.classId == classId);
            }
            if (sheetId > 0)
            {
                res = res.Where(x => x.SheetSchema.SheetSchemaId == sheetId);
            }
            switch (status)
            {
                case 1://查待考
                    return res.Where(x => x.firstLogin == null);
                    
                case 2://查考试中
                    return res.Where(x => x.firstLogin != null && !x.sheetSubmited);
                    
                case 3://查完成
                    return res.Where(x => x.sheetSubmited);
                    
                default:
                    return res;
                    
            }
        }

        public IEnumerable<Assignment> GetAssignmentsByExamAndReviewer(int exId, string reviewer)
        {
            var query = examRepository.GetExams().SingleOrDefault(x => x.ExamId == exId );
            if (query == null) return null;
            IEnumerable<Assignment> res = query.Assignments;
            if (!string.IsNullOrEmpty(reviewer))
            {
                res = res.Where(x => x.Sheet.marker== reviewer);
            }
            return res;
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