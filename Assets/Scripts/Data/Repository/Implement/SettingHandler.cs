using System;
using Data.Repository.Interface.DataStore;
using RemoteSettingsSample.Application;
using RemoteSettingsSample.Application.Enum;
using RemoteSettingsSample.Domain.UseCase.Interface.Repository;
using UniRx;

namespace RemoteSettingsSample.Data.Repository.Implement
{
    public class SettingHandler : ISettingReloadable, ISettingReadable
    {
        public SettingHandler(ISettingReloader settingReloader, ISettingReader settingReader)
        {
            SettingReloader = settingReloader;
            SettingReader = settingReader;
        }

        private ISettingReloader SettingReloader { get; }
        private ISettingReader SettingReader { get; }

        void ISettingReloadable.Reload() =>
            SettingReloader.Reload();

        IObservable<Unit> ISettingReloadable.OnReloadAsObservable() =>
            SettingReloader.OnReloadAsObservable();

        Season ISettingReadable.ReadSeason() =>
            (Season) SettingReader.ReadInt(Const.RemoteSettingKey.Season);
    }
}