using System.Collections;
using System.Collections.Generic;
using Data.Repository.Interface.DataStore;
using Moq;
using NUnit.Framework;
using RemoteSettingsSample.Application.Enum;
using RemoteSettingsSample.Application.Message;
using RemoteSettingsSample.Application.ValueObject;
using RemoteSettingsSample.Data.Repository.Implement;
using RemoteSettingsSample.Domain.Entity.Implement;
using RemoteSettingsSample.Domain.UseCase.Implement;
using RemoteSettingsSample.Presentation.Presenter.Implement;
using RemoteSettingsSample.Presentation.Presenter.Interface.View;
using UniRx;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace RemoteSettingsSample.Presentation.View
{
    public class ColorAnimatorTest : ZenjectIntegrationTestFixture
    {
        [SetUp]
        public void Install()
        {
            PreInstall();

            // UseCases
            Container.BindInterfacesTo<ChangeSeason>().AsCached();
            Container.BindInterfacesTo<HandleRemoteSetting>().AsCached();

            // Entities
            Container.BindInterfacesTo<SeasonMaster>().AsCached();
            Container.BindInterfacesTo<SeasonState>().AsCached();

            // Presenters
            Container.BindInterfacesTo<SampleScene>().AsCached();

            // Repositories
            Container.BindInterfacesTo<SettingHandler>().AsCached();

            // ValueObjects
            Container
                .BindInstance(
                    new List<SeasonInformation>
                    {
                        new SeasonInformation(Season.Spring, "春", "春 is spring", Color.red),
                    } as IEnumerable<SeasonInformation>
                );

            // Messages
            Container.BindFactory<SeasonInformation, SeasonText, SeasonText.Factory.FromSeasonInformation>().AsCached();
            Container.BindFactory<SeasonInformation, SeasonColor, SeasonColor.Factory.FromSeasonInformation>().AsCached();

            // Signals
            Container.DeclareSignal<SeasonText>();
            Container.DeclareSignal<SeasonColor>();
        }

        [UnityTest]
        public IEnumerator 正しい季節が取得出来た()
        {
            var settingReloaderMock = new Mock<ISettingReloader>();
            var settingReaderMock = new Mock<ISettingReader>();
            var refreshTriggerMock = new Mock<IRefreshTrigger>();

            var subject = new Subject<Unit>();

            settingReloaderMock.Setup(x => x.Reload()).Callback(() => subject.OnNext(Unit.Default));
            settingReloaderMock.Setup(x => x.OnReloadAsObservable()).Returns(subject);
            Container.BindInstance(settingReloaderMock.Object);

            // enum Season の範囲内の値を返す
            settingReaderMock.Setup(x => x.ReadInt(It.IsAny<string>())).Returns((int) Season.Spring);
            Container.BindInstance(settingReaderMock.Object);

            refreshTriggerMock.Setup(x => x.OnTriggerAsObservable()).Returns(Observable.ReturnUnit());
            Container.BindInstance(refreshTriggerMock.Object);

            var hasStreamed = false;

            Container
                .Resolve<SignalBus>()
                .GetStream<SeasonColor>()
                .Subscribe(
                    x =>
                    {
                        hasStreamed = true;
                        Assert.AreEqual(Color.red, x.Color);
                    }
                );

            PostInstall();

            Assert.True(hasStreamed);

            yield break;
        }
    }
}