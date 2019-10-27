namespace CreateKnightSquireXml
{
    public interface IJob
    {
        void DoWork();
        void DoWork(string csvFilename);
    }
}