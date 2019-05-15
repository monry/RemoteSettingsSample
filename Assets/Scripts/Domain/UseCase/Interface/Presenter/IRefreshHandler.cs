using System;
using UniRx;

namespace RemoteSettingsSample.Domain.UseCase.Interface.Presenter
{
    public interface IRefreshHandler
    {
        IObservable<Unit> OnRefreshAsObservable();
    }
}