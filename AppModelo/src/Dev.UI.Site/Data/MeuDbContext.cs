using Dev.UI.Site.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.UI.Site.Data
{
    public class MeuDbContext: DbContext
    {

        public DbSet<Aluno> Alunos { get; set; }


        public MeuDbContext(DbContextOptions<MeuDbContext> options)
        :base(options)
        {

        }

    }
}
