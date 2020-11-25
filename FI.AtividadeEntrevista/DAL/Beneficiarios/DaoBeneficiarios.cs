using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de beneficiario
    /// </summary>
    internal class DaoBeneficiarios : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiarios">Objeto de beneficiario</param>
        internal long Incluir(DML.Beneficiarios beneficiarios)
        {

            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiarios.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiarios.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiarios.IdCliente));

            DataSet ds = base.Consultar("FI_SP_IncBeneficiariosV2", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="Beneficiarios">Objeto de beneficiario</param>
        internal DML.Beneficiarios Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiarios", parametros);
            List<DML.Beneficiarios> ben = Converter(ds);

            return ben.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiarios", parametros);

            return ds.Tables[0].Rows.Count > 0;

        }

        internal List<Beneficiarios> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqBeneficiarios", parametros);
            List<DML.Beneficiarios> ben = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return ben;
        }

        /// <summary>
        /// Lista todos os beneficiarios
        /// </summary>
        internal List<DML.Beneficiarios> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_ConsBSeneficiarios", parametros);
            List<DML.Beneficiarios> ben = Converter(ds);

            return ben;
        }

        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiarios">Objeto de beneficiario</param>
        internal void Alterar(DML.Beneficiarios beneficiarios)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", beneficiarios.Id));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiarios.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiarios.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiarios.IdCliente));

            base.Executar("FI_SP_AltBeneficiarios", parametros);
        }


        /// <summary>
        /// Excluir beneficiario
        /// </summary>
        /// <param name="beneficiarios">Objeto de beneficiario</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelBeneficiarios", parametros);
        }

        private List<DML.Beneficiarios> Converter(DataSet ds)
        {
            List<DML.Beneficiarios> lista = new List<DML.Beneficiarios>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiarios ben = new DML.Beneficiarios();
                    ben.Id = row.Field<long>("Id");
                    ben.CPF = row.Field<string>("CPF");
                    ben.Nome = row.Field<string>("Nome");
                    ben.IdCliente = row.Field<long>("idCliente");
                    lista.Add(ben);
                }
            }

            return lista;
        }
    }
}
