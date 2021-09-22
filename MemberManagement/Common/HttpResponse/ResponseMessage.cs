namespace Common.HttpResponse
{
    public static class ResponseMessage
    {
        public static readonly string LoginFail = " Login failed, please try again ";
        public static readonly string TokenHasBeenRevoked = " Token has been revoked ";
        public static readonly string RefreshTokenNotValid = " Refresh token is not valid ";
        public static readonly string TokenNotExpired = " We cannot refresh this token because it has not expired ";
        public static readonly string TokenExpired = " Token has expired, user needs to relogin ";
        public static readonly string TokenUsed = " Token used ";
        public static readonly string CreateSuccessfully = " Create successfully ";
        public static readonly string CreateFailed = " Create failed ";
        public static readonly string UpdateSuccessfully = " Update successfully ";
        public static readonly string UpdateNothing = " Nothing was updated ";
        public static readonly string UpdateFailed = " Update failed ";
        public static readonly string DeleteSuccessfully = " Delete successfully ";
        public static readonly string DeleteFailed = " Delete failed ";
        public static readonly string GetSuccessfully = " Get successfully ";
        public static readonly string CouldNotFound = " Could not found ";
        public static readonly string ArgumentCanNotBeNull = " Argument can not be null ";
    }
}
