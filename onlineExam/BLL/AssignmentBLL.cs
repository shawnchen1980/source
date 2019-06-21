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