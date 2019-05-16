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
using RemoteSettingsSample.Domain.UseCase.Interface.Repository;
using RemoteSettingsSample.Presentation.Presenter.Implement;
using RemoteSettingsSample.Presentation.Presenter.Interface.View;
using UniRx;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace RemoteSettingsSample.Presentation.View
{
    public class TextRendererTest : ZenjectIntegrationTestFixture
    {
        [SetUp]
        public void Install()
        {
            PreInstall();

            // UseCases
            Container.BindInterfacesTo<ChangeSeasonText>().AsCached();
            Container.BindInterfacesTo<HandleRemoteSetting>().AsCached();

            // Entities
            Container.BindInterfacesTo<SeasonTextMaster>().AsCached();
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
                        new SeasonInformation(Season.Spring, "春", "春 is spring", Color.white),
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
                .GetStream<SeasonText>()
                .Subscribe(
                    x =>
                    {
                        hasStreamed = true;
                        Assert.AreEqual("春", x.Title);
                        Assert.AreEqual("春 is spring", x.Body);
                    }
                );

            PostInstall();

            Assert.True(hasStreamed);

            yield break;
        }

        [UnityTest]
        public IEnumerator 正しい季節が取得出来なかった()
        {
            var settingReloaderMock = new Mock<ISettingReloader>();
            var settingReaderMock = new Mock<ISettingReader>();
            var refreshTriggerMock = new Mock<IRefreshTrigger>();

            var subject = new Subject<Unit>();

            settingReloaderMock.Setup(x => x.Reload()).Callback(() => subject.OnNext(Unit.Default));
            settingReloaderMock.Setup(x => x.OnReloadAsObservable()).Returns(subject);
            Container.BindInstance(settingReloaderMock.Object);

            // enum Season の範囲外の値を返す
            settingReaderMock.Setup(x => x.ReadInt(It.IsAny<string>())).Returns(100);
            Container.BindInstance(settingReaderMock.Object);

            refreshTriggerMock.Setup(x => x.OnTriggerAsObservable()).Returns(Observable.ReturnUnit());
            Container.BindInstance(refreshTriggerMock.Object);

            // Avoid warning
            Container.Resolve<SignalBus>().GetStream<SeasonText>().Subscribe();

            PostInstall();

            Assert.AreEqual(default(Season), Container.Resolve<ISettingReadable>().ReadSeason());

            yield break;
        }
    }

}
