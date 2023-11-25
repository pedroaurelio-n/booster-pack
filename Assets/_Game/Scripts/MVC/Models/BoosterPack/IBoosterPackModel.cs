using System.Collections.Generic;

public interface IBoosterPackModel
{
    IReadOnlyList<IPackSettings> Packs { get; }
    string CurrentPackName { get; }
    int CurrentCardQuantity { get; }

    void UpdateCurrentPack (int uid);
    string GetPackNameByUid (int uid);
    ICardModel GetCardFromPool ();
}