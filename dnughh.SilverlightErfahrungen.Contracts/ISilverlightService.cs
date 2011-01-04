
namespace dnughh.SilverlightErfahrungen.Contract
{
    public interface ISilverlightService
    {

        void MethodToCall();


#if NETCLR
        void OnlyNetClr();
#endif

#if SILVERLIGHT
        void  OnlySilverlight();
#endif

    }
}
