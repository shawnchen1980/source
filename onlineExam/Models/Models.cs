using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace onlineExam.Models
{
    //public class DBInitializer : CreateDatabaseIfNotExists<OnlineExamContext>
     public class DBInitializer : DropCreateDatabaseIfModelChanges<OnlineExamContext>
    {
        protected override void Seed(OnlineExamContext context)
        {
            //IList<Sb> defaultStandards = new List<Sb>();

            //defaultStandards.Add(new Sb() { SbId = 1 });
            //defaultStandards.Add(new Sb() { SbId = 2 });
            //defaultStandards.Add(new Sb() { SbId = 3 });

            //context.Sbs.AddRange(defaultStandards);

            IList<QTemplate> defaultYqsbbs = new List<QTemplate>();
            defaultYqsbbs.Add(new QTemplate() { qid = 1, qType = 1,qtext1 = "选择题", qtext2 = "苹果什么颜色的", opLength = 4, op1 = "红", op2 = "黄", op3 = "蓝", op4 = "绿", op5 = "白" });
            defaultYqsbbs.Add(new QTemplate() { qid = 2, qType = 2, qtext1 = "多选题", qtext2 = "苹果可能是什么颜色的", opLength = 4, op1 = "红", op2 = "黄", op3 = "蓝", op4 = "绿", op5 = "白" });
            defaultYqsbbs.Add(new QTemplate() { qid = 3, qType = 3, qtext1 = "是否题", qtext2 = "苹果可能是什么颜色的", opLength = 2, op1 = "红", op2 = "黄", op3 = "蓝", op4 = "绿", op5 = "白" });
            defaultYqsbbs.Add(new QTemplate() { qid = 4, qType = 4, qtext1 = "简答题",
                qtext2 = "苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的" +
                "苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的" +
                "苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的" +
                "苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的" +
                "苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的" +
                "苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的" +
                "苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的" +
                "苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的" +
                "苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的苹果可能是什么颜色的",
                opLength = 0, op1 = "红", op2 = "黄", op3 = "蓝", op4 = "绿",
                op5 = "白"
            });
            //defaultYqsbbs.Add(new QTemplate() { yqbh = "abc125", yqmc = "服务器", dj = 13000, dwbh = "123", dwmc = "信息学院" });

            context.QTemplates.AddRange(defaultYqsbbs);
            base.Seed(context);
        }
    }
    public class QTemplate //问题模板
    {
        [Key]
        public int qid { get; set; }//问题编号

        
        public string qtext1 { get; set; }//题面
        public string qtext2 { get; set; }//题面
        [Index]
        public int qType { get; set; }// 1-single choice, 2-multiple choice,3 yes-no,4 long question
        public int opLength { get; set; }//选项数
        public string op1 { get; set; }//选项1
        public string op2 { get; set; }//选项2
        public string op3 { get; set; }//选项3
        public string op4 { get; set; }//选项4
        public string op5 { get; set; }//选项5
        public string answer { get; set; }//答案
        public string answer2 { get; set; }//答案
        public string answer3 { get; set; }//答案


    }
    public class SheetSchemaQ
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SheetSchemaQId { get; set; } //主键
        //public int SheetSchemaId { get; set; } //模板id
        [Index]
        public int qOrder { get; set; }//题目顺序
        public QTemplate QTemplate { get; set; }//题目
        //public int QTemplateId { get; set; }//题目
        public SheetSchema SheetSchema { get; set; }
        public int score { get; set; }
    }
    public class SheetSchema
    {
        public int SheetSchemaId { get; set; }
        public string name { get; set; }
        public ICollection<SheetSchemaQ> SheetSchemaQs { get; set; }
    }
    public class SheetQ
    {
        public int SheetQId { get; set; }
        public QTemplate QTemplate { get; set; }

        [Index]
        public int qOrder { get; set; }
        public int optionOffset { set; get; }
        public string correctAnswer { get; set; }
        public string answer { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public int score { get; set; }//分值
        public int scored { get; set; }//得分
        public DateTime? timestamp { get; set; }//时间戳
        //public DateTime? t2 { get; set; }
        public string marker { get; set; }//阅卷人姓名
        
        public Sheet Sheet { get; set; }
    }
    public class Sheet
    {
        [ForeignKey("Assignment")]
        public int SheetId { get; set; }
        public DateTime timestamp { get; set; }
        public string answers { get; set; }
        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public string qOrders { get; set; }//题目打乱的顺序列表字符串
        public string qOffs { get; set; }//题目选项偏移量列表字符串
        public string qAns { get; set; }//打乱后的答案列表
        public string qScores { get; set; }//题目分值列表
        public int score1 { get; set; }//自动评卷分
        public int score2 { get; set; }//人工阅卷分
        public string marker { get; set; }
        
        public ICollection<SheetQ> SheetQs { get; set; }
        public Assignment Assignment { get; set; }
       
    }
    public class Student
    {
        public string StudentId { get; set; }
        //public string xhId { get; set; }
        public string name { get; set; }
        public string classId { get; set; }
        
        
    }
    public class Assignment
    {
        public int AssignmentId { get; set; }
        //public string name { get; set; }
        public Student Student { get; set; }
        public Sheet Sheet { get; set; }
        public string ipAddress { get; set; }
        public bool sheetSubmited { get; set; }
        public DateTime? firstLogin { get; set; }
        public DateTime? lastLogin { get; set; }
        public Exam Exam { get; set; }
        public SheetSchema SheetSchema {get;set;}
        public Assignment ShallowCopy()
        {
            return (Assignment)this.MemberwiseClone();
        }

    }
    public class Exam
    {
        public int ExamId { get; set; }
        public string name { get; set; }
        public bool open { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
    public class SheetForExportDTO
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public int SheetId { get; set; }
        public string StuId { get; set; }
        public string StuName { get; set; }
        public string StuClass { get; set; }
        public int score1 { get; set; }
        public int score2 { get; set; }
        public int scoreSum { get; set; }
        public string marker { get; set; }
    }

}