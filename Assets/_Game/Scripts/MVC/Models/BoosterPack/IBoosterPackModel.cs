public interface IBoosterPackModel
{
    string CurrentPackName { get; }
    int CurrentCardQuantity { get; }

    void UpdateCurrentPack (int uid);
    string GetPackNameByUid (int uid);
    ICardModel GetCardFromPool ();
}