namespace Summary.WASenderApi
{
    using System.Collections.Generic;

    public class BaseResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class StatusResponseModel
    {
        public RecentActivity? Status { get; set; }
        public bool? Success { get; set; }
        public string Message { get; set; }
    }

    public enum RecentActivity
    {
        Connected,
        Connecting,
        NeedsQRScan,
        LoggedOut,
        Expired,
        NotFound,
        BadRequest
    }


    public class GetGroupsResponseModel : BaseResponseModel
    {
        public List<GroupInfo> Data { get; set; }
    }

    public class GroupInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}