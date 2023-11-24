using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace GameTests.BoosterPack
{
    public class BoosterPackModelTests
    {
        class BaseBoosterPackModelTests
        {
            protected IBoosterPackSettings Settings { get; private set; }
            protected ICardManagerModel CardManagerModel { get; private set; }
            protected IRandomProvider RandomProvider { get; private set; }
            
            protected BoosterPackModel Model { get; private set; }

            [SetUp]
            public void Setup ()
            {
                Settings = Substitute.For<IBoosterPackSettings>();
                CardManagerModel = Substitute.For<ICardManagerModel>();
                RandomProvider = Substitute.For<IRandomProvider>();

                Model = new BoosterPackModel(
                    Settings,
                    CardManagerModel,
                    RandomProvider
                );
            }

            protected void SetupBoosterPackSettings (List<IPackSettings> packSettings)
            {
                Settings.Packs.Returns(packSettings);
            }

            protected IPackSettings SetupPackSettings (
                int uid,
                string name,
                int cardQuantity,
                List<ICardPoolSettings> cardPools)
            {
                IPackSettings packSettings = Substitute.For<IPackSettings>();
                packSettings.Uid.Returns(uid);
                packSettings.Name.Returns(name);
                packSettings.CardQuantity.Returns(cardQuantity);
                packSettings.CardPools.Returns(cardPools);
                return packSettings;
            }
            
            protected ICardPoolSettings SetupCardPoolSettings (CardRarity rarity, float chance, List<int> cardIds)
            {
                ICardPoolSettings cardPoolSettings = Substitute.For<ICardPoolSettings>();
                cardPoolSettings.Rarity.Returns(rarity);
                cardPoolSettings.Chance.Returns(chance);
                cardPoolSettings.Ids.Returns(cardIds);
                return cardPoolSettings;
            }
            
            protected IPackSettings CreateSettings (
                List<int> cardIds,
                CardRarity rarity,
                float chance,
                int uid,
                string name,
                int cardQuantity)
            {
                ICardPoolSettings cardPoolSettings = SetupCardPoolSettings(rarity, chance, cardIds);
                return SetupPackSettings(uid, name, cardQuantity, new List<ICardPoolSettings> { cardPoolSettings });
            }
        }

        class PublicProperties : BaseBoosterPackModelTests
        {
            [Test]
            public void CurrentPackName ()
            {
                const int UID = 2;
                const string PACK_NAME = "Pack2";

                IPackSettings pack1 = CreateSettings(new List<int> { 1, 2, 3 }, CardRarity.Normal, 1, 1, "Pack1", 1);
                IPackSettings pack2 = CreateSettings(new List<int> { 4, 5, 6 }, CardRarity.Normal, 1, UID, PACK_NAME, 2);
                SetupBoosterPackSettings(new List<IPackSettings> { pack1, pack2 });
                
                Model.UpdateCurrentPack(UID);
                
                Assert.AreEqual(PACK_NAME, Model.CurrentPackName);
            }

            [Test]
            public void CurrentCardQuantity ()
            {
                const int UID = 1;
                const int QUANTITY = 5;

                IPackSettings pack1 = CreateSettings(new List<int> { 1, 2, 3 }, CardRarity.Normal, 1, UID, "Pack1", QUANTITY);
                IPackSettings pack2 = CreateSettings(new List<int> { 4, 5, 6 }, CardRarity.Normal, 1, 2, "Pack2", 2);
                SetupBoosterPackSettings(new List<IPackSettings> { pack1, pack2 });
                
                Model.UpdateCurrentPack(UID);
                
                Assert.AreEqual(QUANTITY, Model.CurrentCardQuantity);
            }
        }

        class GetPackNameByUid : BaseBoosterPackModelTests
        {
            [Test]
            public void GetPackNameByUid_Returns_Correct_Name ()
            {
                const int UID = 1;
                const string PACK_NAME = "Pack1";

                IPackSettings pack = CreateSettings(new List<int> { 1, 2, 3 }, CardRarity.Normal, 1, UID, PACK_NAME, 1);
                SetupBoosterPackSettings(new List<IPackSettings> { pack });
                
                Assert.AreEqual(PACK_NAME, Model.GetPackNameByUid(UID));
            }
            
            [Test]
            public void GetPackNameByUid_Independent_From_Current_Pack ()
            {
                const int CURRENT_UID = 1;
                const int UID = 2;
                const string PACK_NAME = "Pack2";

                IPackSettings pack1 = CreateSettings(new List<int> { 1, 2, 3 }, CardRarity.Normal, 1, CURRENT_UID, "Pack1", 1);
                IPackSettings pack2 = CreateSettings(new List<int> { 4, 5, 6 }, CardRarity.Normal, 1, UID, PACK_NAME, 2);
                SetupBoosterPackSettings(new List<IPackSettings> { pack1, pack2 });
                
                Model.UpdateCurrentPack(CURRENT_UID);

                string packName = Model.GetPackNameByUid(UID);
                Assert.AreEqual(PACK_NAME, packName);
                Assert.AreNotEqual(Model.CurrentPackName, packName);
            }
        }

        class GetCardFromPool : BaseBoosterPackModelTests
        {
            void SetupSettings ()
            {
                List<int> cardIds1 = new() { 1, 2, 3 };
                ICardPoolSettings pool1 = SetupCardPoolSettings(CardRarity.Normal, 0.6f, cardIds1);
                List<int> cardIds2 = new() { 4, 5, 6 };
                ICardPoolSettings pool2 = SetupCardPoolSettings(CardRarity.Rare, 0.4f, cardIds2);
                List<ICardPoolSettings> poolSettings1 = new() { pool1, pool2 };
                IPackSettings pack1 = SetupPackSettings(1, "Pack1", 3, poolSettings1);

                List<int> cardIds3 = new() { 7, 8, 9 };
                ICardPoolSettings pool3 = SetupCardPoolSettings(CardRarity.Normal, 0.7f, cardIds3);
                List<int> cardIds4 = new() { 10, 11, 12 };
                ICardPoolSettings pool4 = SetupCardPoolSettings(CardRarity.Rare, 0.2f, cardIds4);
                List<int> cardIds5 = new() { 13, 14, 15, 16 };
                ICardPoolSettings pool5 = SetupCardPoolSettings(CardRarity.SuperRare, 0.1f, cardIds5);
                List<ICardPoolSettings> poolSettings2 = new() { pool3, pool4, pool5 };
                IPackSettings pack2 = SetupPackSettings(2, "Pack2", 5, poolSettings2);

                Settings.Packs.Returns(new List<IPackSettings> { pack1, pack2 });
            }
            
            [Test]
            public void GetCardFromPool_Returns_Correct_Rarity ()
            {
                SetupSettings();

                List<WeightedObject<CardRarity>> weightedObjects = new()
                {
                    new WeightedObject<CardRarity>(CardRarity.Normal, 0.7f),
                    new WeightedObject<CardRarity>(CardRarity.Rare, 0.2f),
                    new WeightedObject<CardRarity>(CardRarity.SuperRare, 0.1f)
                };
                
                Model.UpdateCurrentPack(2);

                RandomProvider.WeightedRandom(Arg.Any<List<WeightedObject<CardRarity>>>())
                    .Returns(CardRarity.SuperRare);
                RandomProvider.Range(0, 4).Returns(3);

                ICardModel expectedCard = Substitute.For<ICardModel>();
                expectedCard.Uid.Returns(16);
                expectedCard.Type.Returns(CardType.Magic);
                expectedCard.Name.Returns("Test");
                expectedCard.Description.Returns("Description");

                CardManagerModel.GetCardByUid(16).Returns(expectedCard);

                ICardModel selectedCard = Model.GetCardFromPool();
                Assert.AreEqual(expectedCard, selectedCard);
            }
        }
    }
}