namespace NuveiPayBlazorDemo.Shared.RespStructure
{
    public class NuvGeneralTransResponse
    {

        public long internalRequestId { get; set; }
        public string status { get; set; }
        public int errCode { get; set; }
        public string reason { get; set; }
        public string version { get; set; }
        public string sessionToken { get; set; }
    }
}
