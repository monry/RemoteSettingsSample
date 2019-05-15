using System;
using UniRx;

namespace RemoteSettingsSample.Presentation.Presenter.Interface.View
{
    public interface IRefreshTrigger
    {
        IObservable<Unit> OnTriggerAsObservable();
    }
}