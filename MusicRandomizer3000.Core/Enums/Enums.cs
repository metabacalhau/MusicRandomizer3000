namespace MusicRandomizer3000.Core.Enums
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
        FilesTotalSize = 0,
        FilesNumber = 1,
        FilesNumberPerFolder = 2
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }
}