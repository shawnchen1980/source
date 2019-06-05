using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using onlineExam.DAL;
using onlineExam.Models;
namespace onlineExam.BLL
{
    public class StudentBLL : IDisposable, IStudentRepository
    {
        private IStudentRepository studentRepository;

        public IEnumerable<Student> ImportStudent(HttpPostedFile file)
        {
            if (file != null && file.ContentLength > 0)
            {
                using (ExcelPackage package = new ExcelPackage(file.InputStream))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                    List<Student> list = new List<Student>();
                    int length = sheet.Cells["A:A"].Count();
                    for (int i = 2; i <= length; i++)
                    {
                        string str = "A" + i + ":D" + i;
                        if (sheet.Cells[str].Count() < 4)
                        {
                            continue;
                        }
                        var arr = sheet.Cells[str].ToArray();
                        list.Add(new Student()
                        {
                            StudentId = Convert.ToString(arr[1].Value),
                            //xhId = Convert.ToString(arr[1].Value),
                            name = Convert.ToString(arr[2].Value),
                            classId = Convert.ToString(arr[3].Value)
                        });
                    }
                    return list;
                }
                //string fname = Path.GetFileName(file.FileName);
                //file.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));
            }
            return null;
        }
        public StudentBLL()
        {
            this.studentRepository = new StudentRepository();
        }
        public StudentBLL(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
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
                    studentRepository.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~StudentBLL()
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

        public IEnumerable<Student> GetStudents()
        {
            return studentRepository.GetStudents();
        }

        public void InsertStudent(Student qt)
        {
            studentRepository.InsertStudent(qt);
        }

        public void DeleteStudent(Student qt)
        {
            studentRepository.DeleteStudent(qt);
        }

        public void UpdateStudent(Student qt, Student origqt)
        {
            studentRepository.UpdateStudent(qt, origqt);
        }

        public void SaveOrUpdate(Student qt)
        {
            studentRepository.SaveOrUpdate(qt);
        }
        #endregion
    }
}