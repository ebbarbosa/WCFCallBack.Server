using System.ServiceModel;

namespace WCFCallBack
{

    [ServiceContract()]
    public interface IRetornoServico
    {
        [OperationContract(IsOneWay = true)]
        void RetornouServico(Retorno _retorno);
    }
}