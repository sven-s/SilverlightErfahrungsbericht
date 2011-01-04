using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace dnughh.SilverlightErfahrungen.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FaultHandlerOperationBehavior : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Invoker = new FaultHandlerOperationInvoker(dispatchOperation.Invoker);
        }

        public void Validate(OperationDescription operationDescription)
        {
        }
    }
}