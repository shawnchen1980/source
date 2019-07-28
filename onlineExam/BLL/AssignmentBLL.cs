using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using onlineExam.DAL;
using onlineExam.Models;
using System.ComponentModel;

namespace onlineExam.BLL
{
    [DataObjectAttribute]
    public class AssignmentBLL : IDisposable
    {
        private IAssignmentRepository assignmentRepository;
        public AssignmentBLL()
        {
            this.assignmentRepository = new AssignmentRepository();
        }
        public AssignmentBLL(IAssignmentRepository assignmentRepository)
        {
            this.assignmentRepository = assignmentRepository;
        }
        public IEnumerable<Assignment> GetAssignmentsForLogin(string id,string name)
        {
            var res= assignmentRepository.GetAssignments().Where(x => x.Student.StudentId == id && x.Student.name == name && x.Exam.open).ToList();
            return res;
            //return null;
        }
        public IEnumerable<Assignment> GetAssignmentsForCheckAnswers(string id, string name)
        {
            var res = assignmentRepository.GetAssignments().Where(x => x.Student.StudentId == id && x.Student.name == name ).ToList();
            return res;
            //return null;
        }
        public IEnumerable<SheetForExportDTO> GetSheetExportByReviewer(string reviewer)
        {
            return GetAssignmentsByReviewer(reviewer).Select(x => new SheetForExportDTO
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
        public IEnumerable<Assignment> GetAssignmentsByReviewer(string reviewer)
        {
            var res = assignmentRepository.GetAssignments().Where(x => x.Sheet.marker == reviewer );
            return res;
        }
        public IEnumerable<SheetForExportDTO> GetSheetExportByReviewerAndExam(string reviewer,int ExId, bool withLaterExam = false)
        {
            return GetAssignmentsByReviewerAndExam(reviewer,ExId,withLaterExam).Select(x => new SheetForExportDTO
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
        public IEnumerable<Assignment> GetAssignmentsByReviewerAndExam(string reviewer,int ExId,bool withLaterExam=false)
        {
            var res = assignmentRepository.GetAssignments().Where(x => x.Sheet.marker == reviewer && (x.Exam.ExamId==ExId || (x.Exam.ExamId>= ExId && withLaterExam)));
            return res;
        }
        public Assignment GetAssignment(int id)
        {
            return assignmentRepository.GetAssignments().FirstOrDefault(x => x.AssignmentId == id);
        }
        public void UpdateAssignment(Assignment item, Assignment origItem)
        {
            try
            {
                assignmentRepository.UpdateAssignment(item, origItem);
                if (item.sheetSubmited)
                {
                    using (OnlineExamContext context=new OnlineExamContext())
                    {
                        Sheet sheet = context.Sheets.FirstOrDefault(x => x.SheetId == item.AssignmentId);
                        if (sheet != null)
                        {
                            sheet.score1 = Utilities.GradeHelper.CalScore(sheet.answers, sheet.qAns, sheet.qScores);
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("更新失败，可能由于设备不存在或数据不符合要求");
            }
        }
        public void UpdateToUnsubmitted(int id)
        {
            using (OnlineExamContext context=new OnlineExamContext())
            {
                Assignment ass = context.Assignments.FirstOrDefault(x => x.AssignmentId == id);
                if (ass != null)
                {
                    ass.sheetSubmited = false;
                    context.SaveChanges();
                }
            }
        }
        public void Dispose()
        {
            assignmentRepository.Dispose();
        }
    }
}