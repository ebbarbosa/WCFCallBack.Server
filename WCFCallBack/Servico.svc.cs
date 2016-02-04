using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using Utils;

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


                var actions = new List<Action>
                {
                    () =>
                    {
                        RetornaValores(inteiro/2, inteiro/3);
                    },
                    () =>
                    {
                        RetornaValores(inteiro/4, inteiro/5);
                    },
                    () =>
                    {
                        RetornaValores(inteiro/7, inteiro/13);
                    }
                };

                actions.ExecuteAsParallel();

                Thread.Sleep(50);

                inteiro++;
            }

            _retornoServico.RetornouServico(new Retorno { Mensagem = "Fim da Execucao" });
        }

        private void RetornaValores(decimal var1, decimal var2)
        {
            _retorno = new Retorno
            {
                Mensagem = string.Format("Chamada numero {0} - thr{1} - var {2} var {3}", inteiro, Thread.CurrentThread.ManagedThreadId, var1, var2)
            };
            //retorna o callback
            _retornoServico.RetornouServico(_retorno);
        }
    }
}
