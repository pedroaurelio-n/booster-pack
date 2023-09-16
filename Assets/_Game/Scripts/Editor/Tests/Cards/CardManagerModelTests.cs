using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace GameTests.Cards.CardManager
{
    public class CardManagerModelTests
    {
        class BaseCardManagerModelTests
        {
            protected List<ICardSettings> Cards { get; private set; }
            protected ICardListSettings Settings { get; private set; }
            
            protected CardManagerModel Model { get; private set; }

            [SetUp]
            public void Setup ()
            {
                Settings = Substitute.For<ICardListSettings>();

                Cards = new List<ICardSettings>();

                Model = new CardManagerModel(Settings);
            }

            protected void SetupCardList ()
            {
                Settings.Cards.Returns(Cards);
            }

            protected ICardSettings CreateCard (
                int uid,
                CardType type = CardType.Monster,
                string name = "card",
                string description = "description",
                int attack = 1,
                int defense = 1
            )
            {
                ICardSettings card = Substitute.For<ICardSettings>();
                card.Uid.Returns(uid);
                card.Type.Returns(type);
                card.Name.Returns(name);
                card.Description.Returns(description);
                card.Attack.Returns(attack);
                card.Defense.Returns(defense);
                
                Cards.Add(card);
                return card;
            }
        }

        class GetCardByUid : BaseCardManagerModelTests
        {
            [Test]
            public void GetCardByUid_Returns_Correct_Card ()
            {
                const int UID = 1;
                
                ICardSettings expectedCard = CreateCard(UID, CardType.Monster, "two");
                CreateCard(0, CardType.Monster, "one");
                CreateCard(2, CardType.Monster, "three");
                SetupCardList();
                
                Assert.AreEqual(expectedCard, Model.GetCardByUid(UID));
            }
            
            [Test]
            public void GetCardByUid_Throws_Exception_If_Uid_Not_Found ()
            {
                const int UID = 5;
                
                CreateCard(0, CardType.Monster, "one");
                ICardSettings expectedCard = CreateCard(1, CardType.Monster, "two");
                CreateCard(2, CardType.Monster, "three");
                SetupCardList();

                Assert.Throws<InvalidOperationException>(() => Model.GetCardByUid(UID));
            }
        }
    }
}