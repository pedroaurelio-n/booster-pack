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
                int? cardQuantity,
                List<ICardPoolSettings> cardPools
            )
            {
                IPackSettings packSettings = Substitute.For<IPackSettings>();
                packSettings.Uid.Returns(uid);
                packSettings.Name.Returns(name);
                packSettings.RandomQuantity.Returns(cardQuantity);
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
            
            protected IPackSettings CreatePackSettings (
                List<int> cardIds,
                CardRarity rarity,
                float chance,
                int uid,
                string name,
                int? cardQuantity
            )
            {
                ICardPoolSettings cardPoolSettings = SetupCardPoolSettings(rarity, chance, cardIds);
                return SetupPackSettings(uid, name, cardQuantity, new List<ICardPoolSettings> { cardPoolSettings });
            }
        }

        class PublicProperties : BaseBoosterPackModelTests
        {
            [Test]
            public void Packs ()
            {
                const string NAME = "Pack1";
                IPackSettings pack1 = CreatePackSettings(new List<int> { 1, 2, 3 }, CardRarity.Normal, 1, 1, NAME, 1);
                IPackSettings pack2 = CreatePackSettings(new List<int> { 4, 5, 6 }, CardRarity.Normal, 1, 2, "Pack2", 2);
                SetupBoosterPackSettings(new List<IPackSettings> { pack1, pack2 });
                
                Assert.AreEqual(2, Model.Packs.Count);
                Assert.AreEqual(NAME, Model.Packs[0].Name);
            }
            
            [Test]
            public void CurrentPackName ()
            {
                const int UID = 2;
                const string PACK_NAME = "Pack2";

                IPackSettings pack1 = CreatePackSettings(new List<int> { 1, 2, 3 }, CardRarity.Normal, 1, 1, "Pack1", 1);
                IPackSettings pack2 = CreatePackSettings(new List<int> { 4, 5, 6 }, CardRarity.Normal, 1, UID, PACK_NAME, 2);
                SetupBoosterPackSettings(new List<IPackSettings> { pack1, pack2 });
                
                Model.UpdateCurrentPack(UID);
                
                Assert.AreEqual(PACK_NAME, Model.CurrentPackName);
            }

            [Test]
            public void CurrentCardQuantity_By_RandomQuantity ()
            {
                const int QUANTITY = 5;

                IPackSettings pack = CreatePackSettings(new List<int> { 1, 2, 3 }, CardRarity.Normal, 1, 1, "Pack1", QUANTITY);
                SetupBoosterPackSettings(new List<IPackSettings> { pack });
                
                Model.UpdateCurrentPack(1);
                
                Assert.AreEqual(QUANTITY, Model.CurrentCardQuantity);
            }
            
            [Test]
            public void CurrentCardQuantity_By_CardSpots ()
            {
                List<ICardSelectionSettings> cardSpots = new()
                {
                    new CardSelectionSettings(CardSelectionType.Fixed, CardRarity.Normal),
                    new CardSelectionSettings(CardSelectionType.GreaterOrEqual, CardRarity.Normal),
                    new CardSelectionSettings(CardSelectionType.GreaterOrEqual, CardRarity.Rare)
                };
                
                IPackSettings pack = CreatePackSettings(new List<int> { 1, 2, 3 }, CardRarity.Normal, 1, 1, "Pack1", null);
                pack.CardSpots.Returns(cardSpots);
                SetupBoosterPackSettings(new List<IPackSettings> { pack });
                
                Model.UpdateCurrentPack(1);
                
                Assert.AreEqual(cardSpots.Count, Model.CurrentCardQuantity);
            }
        }

        class GetPackNameByUid : BaseBoosterPackModelTests
        {
            [Test]
            public void GetPackNameByUid_Returns_Correct_Name ()
            {
                const int UID = 1;
                const string PACK_NAME = "Pack1";

                IPackSettings pack = CreatePackSettings(new List<int> { 1, 2, 3 }, CardRarity.Normal, 1, UID, PACK_NAME, 1);
                SetupBoosterPackSettings(new List<IPackSettings> { pack });
                
                Assert.AreEqual(PACK_NAME, Model.GetPackNameByUid(UID));
            }
            
            [Test]
            public void GetPackNameByUid_Independent_From_Current_Pack ()
            {
                const int CURRENT_UID = 1;
                const int UID = 2;
                const string PACK_NAME = "Pack2";

                IPackSettings pack1 = CreatePackSettings(new List<int> { 1, 2, 3 }, CardRarity.Normal, 1, CURRENT_UID, "Pack1", 1);
                IPackSettings pack2 = CreatePackSettings(new List<int> { 4, 5, 6 }, CardRarity.Normal, 1, UID, PACK_NAME, 2);
                SetupBoosterPackSettings(new List<IPackSettings> { pack1, pack2 });
                
                Model.UpdateCurrentPack(CURRENT_UID);

                string packName = Model.GetPackNameByUid(UID);
                Assert.AreEqual(PACK_NAME, packName);
                Assert.AreNotEqual(Model.CurrentPackName, packName);
            }
        }

        class GenerateCard : BaseBoosterPackModelTests
        {
            void SetupSettings ()
            {
                List<ICardSelectionSettings> cardSpots = new()
                {
                    new CardSelectionSettings(CardSelectionType.Fixed, CardRarity.Normal),
                    new CardSelectionSettings(CardSelectionType.GreaterOrEqual, CardRarity.Rare)
                };
                
                List<int> cardIds1 = new() { 1, 2, 3 };
                ICardPoolSettings pool1 = SetupCardPoolSettings(CardRarity.Normal, 0.6f, cardIds1);
                List<int> cardIds2 = new() { 4, 5, 6 };
                ICardPoolSettings pool2 = SetupCardPoolSettings(CardRarity.Rare, 0.4f, cardIds2);
                List<ICardPoolSettings> poolSettings1 = new() { pool1, pool2 };
                IPackSettings guaranteedPack = SetupPackSettings(1, "Pack1", null, poolSettings1);
                guaranteedPack.CardSpots.Returns(cardSpots);

                List<int> cardIds3 = new() { 7, 8, 9 };
                ICardPoolSettings pool3 = SetupCardPoolSettings(CardRarity.Normal, 0.7f, cardIds3);
                List<int> cardIds4 = new() { 10, 11, 12 };
                ICardPoolSettings pool4 = SetupCardPoolSettings(CardRarity.Rare, 0.2f, cardIds4);
                List<int> cardIds5 = new() { 13, 14, 15, 16 };
                ICardPoolSettings pool5 = SetupCardPoolSettings(CardRarity.SuperRare, 0.1f, cardIds5);
                List<ICardPoolSettings> poolSettings2 = new() { pool3, pool4, pool5 };
                IPackSettings randomPack = SetupPackSettings(2, "Pack2", 5, poolSettings2);
                randomPack.CardSpots.Returns((IReadOnlyList<CardSelectionSettings>)null);

                Settings.Packs.Returns(new List<IPackSettings> { guaranteedPack, randomPack });
            }
            
            [Test]
            public void GenerateCard_By_Random_Returns_Correct_Rarity ()
            {
                const CardRarity RARITY = CardRarity.SuperRare;
                
                SetupSettings();

                Model.UpdateCurrentPack(2);

                RandomProvider.WeightedRandom(Arg.Any<List<WeightedObject<CardRarity>>>()).Returns(RARITY);
                RandomProvider.Range(0, 4).Returns(3);

                ICardModel expectedCard = Substitute.For<ICardModel>();
                expectedCard.Uid.Returns(16);

                CardManagerModel.GetCardByUid(16).Returns(expectedCard);

                ICardModel selectedCard = Model.GenerateCard(0);
                
                Assert.AreEqual(expectedCard, selectedCard);
                selectedCard.Received().AssignRarity(RARITY);
            }
            
            [Test]
            public void GenerateCard_By_Random_Throws_Exception_If_Card_Not_Found ()
            {
                SetupSettings();

                Model.UpdateCurrentPack(2);

                RandomProvider.WeightedRandom(Arg.Any<List<WeightedObject<CardRarity>>>())
                    .Returns(CardRarity.SuperRare);
                RandomProvider.Range(0, 4).Returns(3);
                
                CardManagerModel.GetCardByUid(16).Returns((ICardModel)null);

                Assert.Throws<InvalidOperationException>(() => Model.GenerateCard(0));
            }
            
            [Test]
            public void GenerateCard_By_Fixed_CardSpot_Returns_Correct_Rarity ()
            {
                const CardRarity RARITY = CardRarity.Normal;
                
                SetupSettings();

                Model.UpdateCurrentPack(1);
                
                RandomProvider.Range(0, 3).Returns(1);

                ICardModel expectedCard = Substitute.For<ICardModel>();
                expectedCard.Uid.Returns(2);

                CardManagerModel.GetCardByUid(2).Returns(expectedCard);

                ICardModel selectedCard = Model.GenerateCard(0);
                
                Assert.AreEqual(expectedCard, selectedCard);
                selectedCard.Received().AssignRarity(RARITY);
            }
            
            [Test]
            public void GenerateCard_By_GreaterOrEqual_CardSpot_Returns_Correct_Rarity ()
            {
                const CardRarity RARITY = CardRarity.Rare;
                
                SetupSettings();

                Model.UpdateCurrentPack(1);
                
                //TODO pedro test: Figure out a more solid way to check this call
                RandomProvider.WeightedRandom(Arg.Any<List<WeightedObject<CardRarity>>>()).Returns(RARITY);
                RandomProvider.Range(0, 3).Returns(0);

                ICardModel expectedCard = Substitute.For<ICardModel>();
                expectedCard.Uid.Returns(4);

                CardManagerModel.GetCardByUid(4).Returns(expectedCard);

                ICardModel selectedCard = Model.GenerateCard(1);
                
                Assert.AreEqual(expectedCard, selectedCard);
                selectedCard.Received().AssignRarity(RARITY);
            }
            
            [Test]
            public void GenerateCard_By_Fixed_CardSpot_Throws_Exception_If_Card_Not_Found ()
            {
                SetupSettings();

                Model.UpdateCurrentPack(1);
                
                RandomProvider.Range(0, 3).Returns(1);

                CardManagerModel.GetCardByUid(2).Returns((ICardModel)null);

                Assert.Throws<InvalidOperationException>(() => Model.GenerateCard(0));
            }
            
            [Test]
            public void GenerateCard_By_GreaterOrEqual_CardSpot_Throws_Exception_If_Card_Not_Found ()
            {
                SetupSettings();

                Model.UpdateCurrentPack(1);
                
                //TODO pedro test: Figure out a more solid way to check this call
                RandomProvider.WeightedRandom(Arg.Any<List<WeightedObject<CardRarity>>>()).Returns(CardRarity.Rare);
                RandomProvider.Range(0, 3).Returns(0);

                CardManagerModel.GetCardByUid(4).Returns((ICardModel)null);

                Assert.Throws<InvalidOperationException>(() => Model.GenerateCard(1));
            }
        }
    }
}