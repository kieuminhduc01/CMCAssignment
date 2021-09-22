using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpResponse
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
