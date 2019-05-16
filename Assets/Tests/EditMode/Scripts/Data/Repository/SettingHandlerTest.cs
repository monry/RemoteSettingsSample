using Data.Repository.Interface.DataStore;
using Moq;
using NUnit.Framework;
using RemoteSettingsSample.Application.Enum;
using RemoteSettingsSample.Data.Repository.Implement;
using RemoteSettingsSample.Domain.UseCase.Interface.Repository;
using UniRx;
using Zenject;

namespace RemoteSettingsSample.Data.Repository
{
    public class SettingHandlerTest : ZenjectUnitTestFixture
    {
        [SetUp]
        public void Install()
        {
            Container.BindInterfacesTo<SettingHandler>().AsCached();
        }

        [Test]
        public void SettingReloadable()
        {
            var subject = new Subject<Unit>();
            var settingReloader = new Mock<ISettingReloader>();
            settingReloader.Setup(x => x.Reload()).Callback(() => subject.OnNext(Unit.Default));
            settingReloader.Setup(x => x.OnReloadAsObservable()).Returns(subject);
            Container.BindInstance(new Mock<ISettingReader>().Object);
            Container.BindInstance(settingReloader.Object);

            var settingReloadable = Container.Resolve<ISettingReloadable>();
            var reloadedInvoked = false;
            var onReloadedAsObservableInvoked = false;
            subject.Subscribe(_ => reloadedInvoked = true);
            settingReloadable.OnReloadAsObservable().Subscribe(_ => onReloadedAsObservableInvoked = true);

            settingReloadable.Reload();

            Assert.True(reloadedInvoked);
            Assert.True(onReloadedAsObservableInvoked);
        }

        [Test]
        public void SettingReadable()
        {
            var settingReader = new Mock<ISettingReader>();
            settingReader.Setup(x => x.ReadInt(It.IsAny<string>())).Returns(1);
            Container.Unbind<ISettingReader>();
            Container.BindInstance(new Mock<ISettingReloader>().Object);
            Container.BindInstance(settingReader.Object);

            var settingReadable = Container.Resolve<ISettingReadable>();

            Assert.AreEqual(Season.Spring, settingReadable.ReadSeason());

            settingReader.Setup(x => x.ReadInt(It.IsAny<string>())).Returns(10);
            Assert.AreEqual(default(Season), settingReadable.ReadSeason());
        }
    }
}