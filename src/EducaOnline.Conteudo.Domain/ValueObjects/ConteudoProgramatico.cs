﻿using EducaOnline.Core.DomainObjects;

namespace EducaOnline.Conteudo.Domain.ValueObjects
{
    public class ConteudoProgramatico
    {
        protected ConteudoProgramatico()
        {
            
        }

        public ConteudoProgramatico(string titulo, string descricao, int cargaHoraria, string objetivos)
        {
            Titulo = titulo;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            Objetivos = objetivos;
        }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }
        public string Objetivos { get; private set; }
    }
}
