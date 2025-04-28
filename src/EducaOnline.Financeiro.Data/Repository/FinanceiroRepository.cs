using EducaOnline.Core.Data;
using EducaOnline.Financeiro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Financeiro.Data.Repository
{
    public class FinanceiroRepository : IFinanceiroRepository
    {
        private readonly PagamentoDbContext _context;

        public FinanceiroRepository(PagamentoDbContext context)
        {
            _context = context;
        }

        public void AdicionarPagamento(Pagamento pagamento)
        {
            _context.Add(pagamento);
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
