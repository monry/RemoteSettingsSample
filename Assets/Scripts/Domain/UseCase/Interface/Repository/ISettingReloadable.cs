using System;
using UniRx;

namespace RemoteSettingsSample.Domain.UseCase.Interface.Repository
{
    public interface ISettingReloadable
    {
        void Reload();
        IObservable<Unit> OnReloadAsObservable();
    }
}