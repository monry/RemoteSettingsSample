using System.Collections.Generic;
using NUnit.Framework;
using RemoteSettingsSample.Application.Enum;
using RemoteSettingsSample.Application.Message;
using RemoteSettingsSample.Application.ValueObject;
using RemoteSettingsSample.Domain.Entity.Implement;
using RemoteSettingsSample.Domain.UseCase.Interface.Entity;
using UnityEngine;
using Zenject;

namespace RemoteSettingsSample.Domain.Entity
{
    public class SeasonTextMasterTest : ZenjectUnitTestFixture
    {
        [SetUp]
        public void Install()
        {
            Container
                .BindInstance(
                    new List<SeasonInformation>
                    {
                        new SeasonInformation(Season.Spring, "春", "春 is spring", Color.white),
                    } as IEnumerable<SeasonInformation>
                );
            Container.BindInterfacesTo<SeasonTextMaster>().AsCached();
            Container.BindFactory<SeasonInformation, SeasonText, SeasonText.Factory.FromSeasonInformation>().AsCached();
        }

        [Test]
        public void Exists()
        {
            var seasonTextMaster = Container.Resolve<ISeasonTextMaster>();
            Assert.True(seasonTextMaster.Exists(Season.Spring));
            Assert.False(seasonTextMaster.Exists(Season.Summer));
        }

        [Test]
        public void Find()
        {
            var seasonTextMaster = Container.Resolve<ISeasonTextMaster>();
            Assert.IsInstanceOf<SeasonText>(seasonTextMaster.Find(Season.Spring));
            Assert.IsInstanceOf<SeasonText>(seasonTextMaster.Find(Season.Autumn));
            Assert.AreNotEqual(default(SeasonText), seasonTextMaster.Find(Season.Spring));
            Assert.AreEqual(default(SeasonText), seasonTextMaster.Find(Season.Autumn));
            Assert.AreEqual("春", seasonTextMaster.Find(Season.Spring).Title);
            Assert.AreEqual("春 is spring", seasonTextMaster.Find(Season.Spring).Body);
            Assert.AreEqual(null, seasonTextMaster.Find(Season.Autumn).Title);
            Assert.AreEqual(null, seasonTextMaster.Find(Season.Autumn).Body);
        }
    }
}