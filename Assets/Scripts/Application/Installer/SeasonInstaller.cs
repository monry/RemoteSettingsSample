using RemoteSettingsSample.Application.Message;
using RemoteSettingsSample.Application.ValueObject;
using RemoteSettingsSample.Data.DataStore.Implement;
using RemoteSettingsSample.Data.Repository.Implement;
using RemoteSettingsSample.Domain.Entity.Implement;
using RemoteSettingsSample.Domain.UseCase.Implement;
using RemoteSettingsSample.Presentation.Presenter.Implement;
using Zenject;

namespace Application.Installer
{
    public class SeasonInstaller : Installer<SeasonInstaller>
    {
        public override void InstallBindings()
        {
            // UseCases
            Container.BindInterfacesTo<ChangeSeason>().AsCached();
            Container.BindInterfacesTo<HandleRemoteSetting>().AsCached();

            // Entities
            Container.BindInterfacesTo<SeasonMaster>().AsCached();
            Container.BindInterfacesTo<SeasonState>().AsCached();

            // Presenters
            Container.BindInterfacesTo<SampleScene>().AsCached();

            // Repositories
            Container
                .BindInterfaces<SettingHandler>()
                // 無理に SubContainer にする必要も無いが、安全のために隔離する
                .FromSubContainerResolve()
                .ByMethod(
                    container =>
                    {
                        container.BindInterfacesTo<SettingHandler>().AsCached();
                        container.BindInterfacesTo<UnityRemoteSetting>().AsCached();
                    }
                )
                .WithKernel()
                .AsCached();

            // Messages
            Container.BindFactory<SeasonInformation, SeasonText, SeasonText.Factory.FromSeasonInformation>().AsCached();
            Container.BindFactory<SeasonInformation, SeasonColor, SeasonColor.Factory.FromSeasonInformation>().AsCached();

            // Signals
            Container.DeclareSignal<SeasonText>();
            Container.DeclareSignal<SeasonColor>();
        }
    }
}