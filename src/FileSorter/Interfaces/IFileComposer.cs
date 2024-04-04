namespace FileSorter.Interfaces
{
    public interface IFileComposer
    {
        void ComposeFilesByExtension();
        void ComposeFilesByLastWriteTime();
        void ComposeFilesByType();
    }
}