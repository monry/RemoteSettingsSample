using RemoteSettingsSample.Domain.UseCase.Interface.Entity;
using RemoteSettingsSample.Domain.UseCase.Interface.Presenter;
using RemoteSettingsSample.Domain.UseCase.Interface.Repository;
using UniRx;
using Zenject;

namespace RemoteSettingsSample.Domain.UseCase.Implement
{
    public class HandleRemoteSetting : IInitializable
    {
        public HandleRemoteSetting(ISeasonState seasonState, IRefreshHandler refreshHandler, ISettingReloader settingReloader, ISettingReader settingReader)
        {
            SeasonState = seasonState;
            RefreshHandler = refreshHandler;
            SettingReloader = settingReloader;
            SettingReader = settingReader;
        }

        // Entities
        private ISeasonState SeasonState { get; }

        // Presenters
        private IRefreshHandler RefreshHandler { get; }

        // Repositories
        private ISettingReloader SettingReloader { get; }
        private ISettingReader SettingReader { get; }

        void IInitializable.Initialize()
        {
            RefreshHandler
                .OnRefreshAsObservable()
                .Subscribe(_ => SettingReloader.Reload());
            SettingReloader
                .OnReloadAsObservable()
                .Subscribe(_ => SeasonState.Value = SettingReader.ReadSeason());
        }
    }
}