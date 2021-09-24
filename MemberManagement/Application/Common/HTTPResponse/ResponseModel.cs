using System.Collections.Generic;

namespace Application.Common.HTTPResponse
{
    public class ResponseModel<T>
    {
        public ICollection<T> Results { get; set; }
        public ResponseCode ResponseCode { get; set; }
        public string Message { get; set; }
        public int TotalResults { get; set; }
        public int TotalRecordsInDb { get; set; }
        public static ResponseModel<T> Success(ICollection<T> results, ResponseCode responseCode)
        {
            return new ResponseModel<T>
            {
                Results = results,
                ResponseCode = responseCode,
            };
        }
        public static ResponseModel<T> Success(int totalResults, ResponseCode responseCode,string message)
        {
            return new ResponseModel<T>
            {
                TotalResults = totalResults,
                ResponseCode = responseCode,
                Message = message,
            };
        }
        public static ResponseModel<T> Success(ICollection<T> results, ResponseCode responseCode,string message)
        {
            return new ResponseModel<T>
            {
                Results = results,
                ResponseCode = responseCode,
                Message = message,
            };
        }
        public static ResponseModel<T> Success(ICollection<T> results, ResponseCode responseCode,string message, int totalResults)
        {
            return new ResponseModel<T>
            {
                Results = results,
                ResponseCode = responseCode,
                Message = message,
                TotalResults = totalResults,
            };
        }
        public static ResponseModel<T> Success(ICollection<T> results, ResponseCode responseCode,string message, int totalResults, int totalRecordsInDb)
        {
            return new ResponseModel<T>
            {
                Results = results,
                ResponseCode = responseCode,
                Message = message,
                TotalResults = totalResults,
                TotalRecordsInDb = totalRecordsInDb,
            };
        }
        public static ResponseModel<T> Fail(ResponseCode responseCode, string message)
        {
            return new ResponseModel<T>
            {
                Results = null,
                ResponseCode = responseCode,
                Message = message,
                TotalResults=0,
                TotalRecordsInDb=0,
            };
        }
    }
}
