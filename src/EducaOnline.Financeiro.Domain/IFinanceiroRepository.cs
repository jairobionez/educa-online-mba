using EducaOnline.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Financeiro.Domain
{
    public interface IFinanceiroRepository :  IRepository<Pagamento>
    {
        void AdicionarPagamento(Pagamento pagamento);
    }
}
