using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiarios
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiarios">Objeto de beneficiario</param>
        public long Incluir(DML.Beneficiarios beneficiarios)
        {
            /// Hélio Barbosa
            /// 20/11/2020
            /// 
            if( !VerificarExistencia(beneficiarios.CPF))
            {
                return 0;
            }
            else
            {
                DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
                return ben.Incluir(beneficiarios);
            }

        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="beneficiarios">Objeto de beneficiario</param>
        public void Alterar(DML.Beneficiarios beneficiarios)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            ben.Alterar(beneficiarios);
        }

        /// <summary>
        /// Consulta o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public DML.Beneficiarios Consultar(long id)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            return ben.Consultar(id);
        }

        /// <summary>
        /// Excluir o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            ben.Excluir(id);
        }

        /// <summary>
        /// Lista os beneficiarios
        /// </summary>
        public List<DML.Beneficiarios> Listar()
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            return ben.Listar();
        }

        /// <summary>
        /// Lista os beneficiarios
        /// </summary>
        public List<DML.Beneficiarios> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            return ben.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();

            if( !ValidaDigitoCPF.ValidaCPF(CPF) )
            {
                return false;
            }
            else
            {
                return ben.VerificarExistencia(CPF);
            }
            
        }
    }
}
