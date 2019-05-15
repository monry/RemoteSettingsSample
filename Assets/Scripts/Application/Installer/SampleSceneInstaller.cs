using System.Collections.Generic;
using RemoteSettingsSample.Application.ValueObject;
using RemoteSettingsSample.Data.Repository.Implement;
using RemoteSettingsSample.Domain.Entity.Implement;
using RemoteSettingsSample.Domain.UseCase.Implement;
using RemoteSettingsSample.Presentation.Presenter.Implement;
using UnityEngine;
using Zenject;

namespace RemoteSettingsSample.Application.Installer
{
    public class SampleSceneInstaller : MonoInstaller<SampleSceneInstaller>
    {
        [SerializeField] private List<SeasonInformation> seasonTexts = new List<SeasonInformation>();
        private IEnumerable<SeasonInformation> SeasonTexts => seasonTexts;

        public override void InstallBindings()
        {
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
            Container.BindInstance(SeasonTexts);
        }
    }
}