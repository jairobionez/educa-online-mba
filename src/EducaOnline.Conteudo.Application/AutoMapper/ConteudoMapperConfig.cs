using AutoMapper;
using EducaOnline.Conteudo.Application.ViewModels;
using EducaOnline.Conteudo.Domain;
using EducaOnline.Conteudo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Conteudo.Application.AutoMapper
{
    public class ConteudoMapperConfig : Profile
    {
        public ConteudoMapperConfig()
        {
            CreateMap<Curso, CursoResponseViewModel>();
            CreateMap<CursoViewModel, Curso>();

            CreateMap<Aula, AulaResponseViewModel>();
            CreateMap<AulaViewModel, Aula>();

            CreateMap<ConteudoProgramaticoViewModel, ConteudoProgramatico>()
                .ReverseMap();
        }
    }
}
