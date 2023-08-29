namespace Save
{
    public interface ISaveDataWriteService
    {
        SaveData SaveDataWrirtable { get; }
        void Save();
    }
}