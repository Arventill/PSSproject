namespace PSSCMS
{
    public enum SessionTypeEnum 
    {
        Other = 0,
        Wedding = 1,
        Street = 2,
        Landscape = 4,
        Portrait = 8
    }

    public enum SessionStatusEnum
    {
        None = 0,
        Pending = 1,
        Approved = 2,
        Canceled = 4,
        Postponed = 8
    }
}