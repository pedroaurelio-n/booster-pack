using NSubstitute;
using NUnit.Framework;

namespace GameTests.Cards.Card
{
    public class CardModelTests
    {
        class BaseCardModelTests
        {
            protected ICardSettings Settings { get; private set; }
            
            protected CardModel Model { get; private set; }

            [SetUp]
            public void Setup()
            {
                Settings = Substitute.For<ICardSettings>();
                
                Model = new CardModel(Settings);
            }
        }

        class PublicProperties : BaseCardModelTests
        {
            [Test]
            public void Uid ()
            {
                const int UID = 1;
                Settings.Uid.Returns(UID);
                Assert.AreEqual(UID, Model.Uid);
            }
            
            [Test]
            public void Type ()
            {
                const CardType CARD_TYPE = CardType.Magic;
                Settings.Type.Returns(CARD_TYPE);
                Assert.AreEqual(CARD_TYPE, Model.Type);
            }
            
            [Test]
            public void Name ()
            {
                const string NAME = "name";
                Settings.Name.Returns(NAME);
                Assert.AreEqual(NAME, Model.Name);
            }
            
            [Test]
            public void Description ()
            {
                const string DESCRIPTION = "description";
                Settings.Description.Returns(DESCRIPTION);
                Assert.AreEqual(DESCRIPTION, Model.Description);
            }

            [Test]
            public void Level ()
            {
                const int LEVEL = 6;
                Settings.Level.Returns(LEVEL);
                Assert.AreEqual(LEVEL, Model.Level);
            }
            
            [Test]
            public void Attack ()
            {
                const int ATTACK = 100;
                Settings.Attack.Returns(ATTACK);
                Assert.AreEqual(ATTACK, Model.Attack);
            }
            
            [Test]
            public void Defense ()
            {
                const int DEFENSE = 200;
                Settings.Defense.Returns(DEFENSE);
                Assert.AreEqual(DEFENSE, Model.Defense);
            }
        }
    }
}
