using EducaOnline.Conteudo.Domain.ValueObjects;
using EducaOnline.Conteudo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Conteudo.Application.ViewModels
{
    public class CursoNomeViewModel
    {
        public string? Nome { get; set; }
        public int LimiteAlunos { get; set; }
    }

    public class CursoViewModel
    {
        public string? Nome { get; set; }
        public int LimiteAlunos { get; set; }

        public ConteudoProgramaticoViewModel? ConteudoProgramatico { get;  set; }
        public ICollection<AulaViewModel>? Aulas { get; set; }
    }

    public class CursoResponseViewModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public int LimiteAlunos { get; set; }
        public ConteudoProgramaticoViewModel? ConteudoProgramatico { get; set; }
        public ICollection<AulaResponseViewModel>? Aulas { get; set; }
    }
}
