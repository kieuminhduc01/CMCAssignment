namespace Common.HttpResponse
{
    public enum ResponseCode
    {
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        NotAcceptable = 406,
        OK = 200,
        Created = 201,
        Accepted = 202
    }
}
