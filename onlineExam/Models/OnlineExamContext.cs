using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace onlineExam.Models
{
    public class OnlineExamContext:DbContext
    {
        public OnlineExamContext():base("DefaultConnection")
        {
            Database.SetInitializer<OnlineExamContext>(new DBInitializer());
        }
        public DbSet<QTemplate> QTemplates { get; set; }
        public DbSet<SheetSchemaQ> SheetSchemaQs { get; set; }
        public DbSet<SheetSchema> SheetSchemas { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<SheetQ> SheetQs { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}