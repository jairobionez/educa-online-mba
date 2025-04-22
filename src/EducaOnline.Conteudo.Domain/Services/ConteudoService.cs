using EducaOnline.Conteudo.Domain.ValueObjects;
using EducaOnline.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Conteudo.Domain.Services
{
    public class ConteudoService : IConteudoService
    {
        private readonly IConteudoRepository _conteudoRepository;

        public ConteudoService(IConteudoRepository conteudoRepository)
        {
            _conteudoRepository = conteudoRepository;
        }

        public async Task<List<Curso>> BuscarCursos()
        {
            return await _conteudoRepository.BuscarCursos();
        }

        public async Task<Curso> BuscarCurso(Guid id)
        {
            return await _conteudoRepository.BuscarCurso(id);
        }

        public async Task AdicionarCurso(Curso curso)
        {
            curso.Validar();
            _conteudoRepository.AdicionarCurso(curso);

            await _conteudoRepository.UnitOfWork.Commit();
        }

        public async Task AlterarNomeCurso(Guid id, string nome)
        {
            var curso = await _conteudoRepository.BuscarCurso(id);
            if (curso == null)
                throw new DomainException("Curso não encontrado");

            curso.Atualizar(nome);

            _conteudoRepository.AlterarCurso(curso);

            await _conteudoRepository.UnitOfWork.Commit();
        }

        public async Task AlterarConteudoProgramaticoCurso(Guid id, ConteudoProgramatico conteudoProgramatico)
        {
            var curso = await _conteudoRepository.BuscarCurso(id);
            if (curso == null)
                throw new DomainException("Curso não encontrado");

            curso.AtualizarConteudoProgramatico(conteudoProgramatico);

            _conteudoRepository.AlterarCurso(curso);

            await _conteudoRepository.UnitOfWork.Commit();
        }

        public async Task DesativarCurso(Guid id)
        {
            // TODO: Regra
        }

        public async Task<bool> AdicionarAula(Guid cursoId, Aula aula)
        {
            var curso = await _conteudoRepository.BuscarCurso(cursoId);
            if (curso == null)
                throw new DomainException("Curso não encontrado");

            curso.AdicionarAula(aula);

            _conteudoRepository.AlterarCurso(curso);

            return await _conteudoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> AlterarAula(Guid cursoId, Guid aulaId, Aula aula)
        {
            var curso = await _conteudoRepository.BuscarCurso(cursoId);
            if (curso == null)
                throw new DomainException("Curso não encontrado");

            curso.AlterarAula(aulaId, aula);

            _conteudoRepository.AlterarCurso(curso);

            return await _conteudoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> RemoverAula(Guid cursoId, Guid aulaId)
        {
            var curso = await _conteudoRepository.BuscarCurso(cursoId);
            if (curso == null)
                throw new DomainException("Curso não encontrado");

            curso.RemoverAula(aulaId);

            _conteudoRepository.AlterarCurso(curso);

            return await _conteudoRepository.UnitOfWork.Commit();
        }
    }
}
