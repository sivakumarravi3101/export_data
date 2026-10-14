namespace ExportStatistics.Data.Response
{
    public class BaseResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public BaseResponse(string responseCode, string responseMessage)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
        }

    }
}