using System.Net;

namespace Application.Common.HTTPResponse
{
    public record CQRSResponse
    {
        public HttpStatusCode StatusCode { get; init; }=HttpStatusCode.OK;

    }
}
