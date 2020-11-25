using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(DML.Cliente cliente)
        {
            /// Hélio Barbosa
            /// 20/11/2020
            /// 
            if( !VerificarExistencia(cliente.CPF))
            {
                return 0;
            }
            else
            {
                DAL.DaoCliente cli = new DAL.DaoCliente();
                return cli.Incluir(cliente);
            }

        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Alterar(cliente);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();

            if( !ValidaDigitoCPF.ValidaCPF(CPF) )
            {
                return false;
            }
            else
            {
                return cli.VerificarExistencia(CPF);
            }
            
        }
    }



    /// Hélio Barbosa
    /// 20/11/2020
    public class ValidaDigitoCPF
    {
        /// <summary>
        /// Informar um CPF completo para validação do digito verificador
        /// </summary>
        /// <param name="cpf">Int64 com o numero CPF completo com Digito</param>
        /// <returns>Boolean True/False onde True=Digito CPF Valido</returns>
        //public static Boolean ValidaCPF(string cpf)
        //{
        //    return ValidaCPF(cpf);
        //}

        /// <summary>
        /// Informar um CPF completo para validação do digito verificador
        /// </summary>
        /// <param name="cpf">string com o numero CPF completo com Digito</param>
        /// <returns>Boolean True/False onde True=Digito CPF Valido</returns>
        public static Boolean ValidaCPF(string cpf)
        {
            // Declara variaveis para uso
            string new_cpf = "";

            // Retira carcteres invalidos não numericos da string
            for (int i = 0; i < cpf.Length; i++)
            {
                if (ValidaDigitoCPF.IsDigito(cpf.Substring(i, 1)))
                {
                    new_cpf = new_cpf + cpf.Substring(i, 1);
                }
            }

            // Ajusta o Tamanho do CPF para 11 digitos considerando o digito verificador e completando com zeros a esquerda
            new_cpf = Convert.ToInt64(new_cpf).ToString(new_cpf);

            // Verifica se o cpf informado tem os 11 digitos 
            if (new_cpf.Length > 11)
            {
                return false;
            }

            // Calcula o digito do CPF e compara com o digito informado
            if (CalculaDigCPF(new_cpf.Substring(0, 9)) == cpf.Substring(9, 2))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Calcula o Digito verificador de um CPF informado  
        /// </summary>
        /// <param name="cpf">int64 com o CPF contendo 9 digitos e sem o digito verificador</param>
        /// <returns>string com o digito calculado do CPF ou null caso o cpf informado for maior que 9 digitos</returns>
        //public static string CalculaDigCPF(string cpf)
        //{
        //    return CalculaDigCPF(cpf);
        //}

        /// <summary>
        /// Calcula o Digito verificador de um CPF informado  
        /// </summary>
        /// <param name="cpf">string com o CPF contendo 9 digitos e sem o digito verificador</param>
        /// <returns>string com o digito calculado do CPF ou null caso o cpf informado for maior que 9 digitos</returns>
        public static string CalculaDigCPF(string cpf)
        {
            // Declara variaveis para uso
            string new_cpf = "";
            string digito = "";
            Int32 Aux1 = 0;
            Int32 Aux2 = 0;

            // Retira carcteres invalidos não numericos da string
            for (int i = 0; i < cpf.Length; i++)
            {
                if (IsDigito(cpf.Substring(i, 1)))
                {
                    new_cpf += cpf.Substring(i, 1);
                }
            }

            // Ajusta o Tamanho do CPF para 9 digitos completando com zeros a esquerda
            new_cpf = Convert.ToInt64(new_cpf).ToString(new_cpf);

            // Caso o tamanho do CPF informado for maior que 9 digitos retorna nulo
            if (new_cpf.Length > 9)
            {
                return null;
            }

            // Calcula o primeiro digito do CPF
            Aux1 = 0;

            for (int i = 0; i < new_cpf.Length; i++)
            {
                Aux1 += Convert.ToInt32(new_cpf.Substring(i, 1)) * (10 - i);
            }

            Aux2 = 11 - (Aux1 % 11);

            // Carrega o primeiro digito na variavel digito
            if (Aux2 > 9)
            {
                digito += "0";
            }
            else
            {
                digito += Aux2.ToString();
            }

            // Adiciona o primeiro digito ao final do CPF para calculo do segundo digito
            new_cpf += digito;

            // Calcula o segundo digito do CPF
            Aux1 = 0;

            for (int i = 0; i < new_cpf.Length; i++)
            {
                Aux1 += Convert.ToInt32(new_cpf.Substring(i, 1)) * (11 - i);
            }

            Aux2 = 11 - (Aux1 % 11);

            // Carrega o segundo digito na variavel digito
            if (Aux2 > 9)
            {
                digito += "0";
            }
            else
            {
                digito += Aux2.ToString();
            }

            return digito;
        }

        /// <summary>
        /// Verifica se um digito informado é um numero
        /// </summary>
        /// <param name="digito">string com um caracter para verificar se é um numero</param>
        /// <returns>Boolean True/False</returns>
        static bool IsDigito(string digito)
        {
            int n;
            return Int32.TryParse(digito, out n);
        }
    }


}
