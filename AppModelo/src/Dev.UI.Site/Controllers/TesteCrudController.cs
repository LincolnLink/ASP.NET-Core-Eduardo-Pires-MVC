using Dev.UI.Site.Data;
using Dev.UI.Site.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.UI.Site.Controllers
{
    public class TesteCrudController : Controller
    {

        private readonly MeuDbContext _contexto;

        public TesteCrudController(MeuDbContext contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            // Adicionando Alunos
            var aluno = new Aluno
            {
                Nome = "Lincoln",
                DataNascimento = DateTime.Now,
                Email = "link@email.com.br"
            };
            _contexto.Alunos.Add(aluno);
            _contexto.SaveChanges();

            // Consultando Aluno por id ou email, busca apenas 1.
            var aluno2 = _contexto.Alunos.Find(aluno.Id);
            var aluno3 = _contexto.Alunos.FirstOrDefault(a => a.Email == "link@email.com.br");
            
            // Buscando uma coleção de aluno, busca quantos existir.
            var aluno4 = _contexto.Alunos.Where(a => a.Nome == "Lincoln");

            // Editando Aluno
            aluno.Nome = "João";
            _contexto.Alunos.Update(aluno);
            _contexto.SaveChanges();

            // Removendo Aluno
            _contexto.Alunos.Remove(aluno);
            _contexto.SaveChanges();

            return View();
        }
    }
}
