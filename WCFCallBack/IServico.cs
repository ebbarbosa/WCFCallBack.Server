using System.ServiceModel;

namespace WCFCallBack
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IRetornoServico), SessionMode =SessionMode.Required)]
    public interface IServico
    {
        [OperationContract(IsOneWay = true)]
        void IniciaServico();
    }
}
