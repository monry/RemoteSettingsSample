using System;
using System.Net;
using RemoteSettingsSample.Application;
using RemoteSettingsSample.Application.Enum;
using RemoteSettingsSample.Domain.UseCase.Interface.Repository;
using UniRx;
using UnityEngine;
using Zenject;

namespace RemoteSettingsSample.Data.Repository.Implement
{
    // DataStore として UnityEngine.RemoteSettings の Wrapper を作る手もあるが、冗長なのでやらない
    public class SettingHandler : IInitializable, ISettingReloadable, ISettingReadable
    {
        private ISubject<Unit> OnCompletedSubject { get; } = new Subject<Unit>();

        void IInitializable.Initialize()
        {
            RemoteSettings.Completed += (wasUpdatedFromServer, settingsChanged, serverResponse) =>
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch ((HttpStatusCode) serverResponse)
                {
                    case HttpStatusCode.OK:
                        OnCompletedSubject.OnNext(Unit.Default);
                        break;
                }
            };
        }

        IObservable<Unit> ISettingReloadable.OnReloadAsObservable() =>
            OnCompletedSubject;

        void ISettingReloadable.Reload() =>
            RemoteSettings.ForceUpdate();

        Season ISettingReadable.ReadSeason() =>
            (Season) RemoteSettings.GetInt(Const.RemoteSettingKey.Season);
    }
}