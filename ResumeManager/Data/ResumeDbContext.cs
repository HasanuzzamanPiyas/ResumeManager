using Microsoft.EntityFrameworkCore;
using ResumeManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeManager.Dal
{
    public class ResumeDbContext:DbContext
    {
        public ResumeDbContext(DbContextOptions <ResumeDbContext> options): base(options)
        {

        }
        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Exprience> Expriences { get; set; }
    }
}
