using System;
using System.Net;
using Data.Repository.Interface.DataStore;
using UniRx;
using UnityEngine;
using Zenject;

namespace RemoteSettingsSample.Data.DataStore.Implement
{
    public class UnityRemoteSetting : IInitializable, ISettingReloader, ISettingReader
    {
        private ISubject<Unit> OnCompletedSubject { get; } = new Subject<Unit>();

        // Annotate [Inject] to workaround for the issue of Zenject
        // that IInitializable.Initlaize() is not called
        // even if I use Bind().FromSubContainerResolve().ByMethod().WithKernel()
        [Inject]
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

        void ISettingReloader.Reload() =>
            RemoteSettings.ForceUpdate();

        IObservable<Unit> ISettingReloader.OnReloadAsObservable() =>
            OnCompletedSubject;

        int ISettingReader.ReadInt(string key) =>
            RemoteSettings.GetInt(key);
    }
}