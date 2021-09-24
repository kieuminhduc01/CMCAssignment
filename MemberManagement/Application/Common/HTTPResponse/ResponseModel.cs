using System.Collections.Generic;

namespace Application.Common.HTTPResponse
{
    public class ResponseModel<T>
    {
        public string Message { get; set; }
        public ResponseCode ResponseCode { get; set; }
        public int TotalRecordsInDb { get; set; }
        public int TotalResults { get; set; }
        public ICollection<T> Results { get; set; }
    }
}
