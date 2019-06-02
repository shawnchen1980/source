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
            return assignmentRepository.GetAssignments().Where(x => x.Student.xhId == id && x.Student.name == name && x.Exam.open).ToList();

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
            }
            catch (Exception)
            {

                throw new Exception("更新失败，可能由于设备不存在或数据不符合要求");
            }
        }
        public void Dispose()
        {
            assignmentRepository.Dispose();
        }
    }
}