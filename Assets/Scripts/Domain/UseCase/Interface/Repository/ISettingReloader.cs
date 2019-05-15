using System;
using UniRx;

namespace RemoteSettingsSample.Domain.UseCase.Interface.Repository
{
    public interface ISettingReloader
    {
        void Reload();
        IObservable<Unit> OnReloadAsObservable();
    }
}