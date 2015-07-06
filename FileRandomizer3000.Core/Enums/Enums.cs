namespace FileRandomizer3000.Core.Enums
{
    public enum FileSizeUnit
    {
        None,
        Megabyte,
        Gigabyte
    }

    public enum UniqueCharsPosition
    {
        None,
        Prefix,
        Suffix
    }

    public enum BackgroundWorkerType
    {
        Sync,
        Async
    }

    public enum LimitType
    {
        FilesTotalSize,
        FilesNumber,
        FilesNumberPerFolder
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }
}