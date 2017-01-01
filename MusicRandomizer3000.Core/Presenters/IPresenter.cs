namespace MusicRandomizer3000.Core.Presenters
{
    public interface IPresenter
    {
        void Run();
    }

    public interface IPresenter<in TArgument>
    {
        void Run(TArgument argument);
    }
}