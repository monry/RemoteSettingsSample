using RemoteSettingsSample.Domain.UseCase.Interface.Entity;
using RemoteSettingsSample.Domain.UseCase.Interface.Presenter;
using RemoteSettingsSample.Domain.UseCase.Interface.Repository;
using UniRx;
using Zenject;

namespace RemoteSettingsSample.Domain.UseCase.Implement
{
    public class HandleRemoteSetting : IInitializable
    {
        public HandleRemoteSetting(ISeasonState seasonState, IRefreshHandler refreshHandler, ISettingReloadable settingReloadable, ISettingReadable settingReadable)
        {
            SeasonState = seasonState;
            RefreshHandler = refreshHandler;
            SettingReloadable = settingReloadable;
            SettingReadable = settingReadable;
        }

        // Entities
        private ISeasonState SeasonState { get; }

        // Presenters
        private IRefreshHandler RefreshHandler { get; }

        // Repositories
        private ISettingReloadable SettingReloadable { get; }
        private ISettingReadable SettingReadable { get; }

        void IInitializable.Initialize()
        {
            SettingReloadable
                .OnReloadAsObservable()
                .Subscribe(_ => SeasonState.Value = SettingReadable.ReadSeason());
            RefreshHandler
                .OnRefreshAsObservable()
                .Subscribe(_ => SettingReloadable.Reload());
        }
    }
}