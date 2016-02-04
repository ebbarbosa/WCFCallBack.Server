using System.ServiceModel;
using System.Threading;

namespace WCFCallBack
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.PerSession)]
    public class Servico : IServico
    {
        IRetornoServico _retornoServico;
        Retorno _retorno;
        int inteiro = 1;

        public Servico()
        {
            _retornoServico = OperationContext.Current.GetCallbackChannel<IRetornoServico>();
        }

        public void IniciaServico()
        {
            while (inteiro <= 100)
            {
                //faz alguma coisa demorada
                _retorno = new Retorno { Mensagem = string.Format("Chamada numero {0}", inteiro) };

                Thread.Sleep(20);

                //retorna o callback
                _retornoServico.RetornouServico(_retorno);

                inteiro++;
            }

            _retornoServico.RetornouServico(new Retorno { Mensagem = "Fim da Execucao" });
        }
    }
}
