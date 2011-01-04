using System;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace dnughh.SilverlightErfahrungen.Helpers
{
    public class FaultHandlerOperationInvoker : IOperationInvoker
    {
        public FaultHandlerOperationInvoker(IOperationInvoker operationInvoker)
        {
            InnerOperationInvoker = operationInvoker;
        }

        private IOperationInvoker InnerOperationInvoker { get; set; }

        #region IOperationInvoker Members

        public Object[] AllocateInputs()
        {
            return InnerOperationInvoker.AllocateInputs();
        }

        public Object Invoke(Object instance, Object[] inputs, out Object[] outputs)
        {
            outputs = new Object[] { };
            var messageHeadersElement = OperationContext.Current.IncomingMessageHeaders;
            Object returnObject = null;
            try
            {
                return InnerOperationInvoker.Invoke(instance, inputs, out outputs);
            }
            catch (Exception ex)
            {
                if (messageHeadersElement.FindHeader("DoesNotHandleFault", "") > -1)
                {
                    var invokerType = InnerOperationInvoker.GetType();
                    var pi = invokerType.GetProperty("Method");
                    var mi = (MethodInfo)pi.GetValue(InnerOperationInvoker, null);
                    var returnType = mi.ReturnType;
                    var interfaceType = returnType.GetInterface("dnughh.SilverlightErfahrungen.Helpers.IContainsFaultDetail");
                    if (interfaceType != null)
                    {
                        returnObject = Activator.CreateInstance(returnType);
                    }
                    if (returnObject != null && returnObject is IContainsFaultDetail)
                    {
                        ((IContainsFaultDetail)returnObject).FaultDetail = FaultDetail.CreateFaultDetail(ex.Message);
                    }
                }
                else
                {
                    throw;
                }
            }

            return returnObject;
        }

        public IAsyncResult InvokeBegin(Object instance, Object[] inputs, AsyncCallback callback, Object state)
        {
            return InnerOperationInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        public Object InvokeEnd(Object instance, out Object[] outputs, IAsyncResult result)
        {
            return InnerOperationInvoker.InvokeEnd(instance, out outputs, result);
        }

        public Boolean IsSynchronous
        {
            get { return InnerOperationInvoker.IsSynchronous; }
        }

        #endregion
    }
}