using System;
using RemoteSettingsSample.Domain.UseCase.Interface.Presenter;
using RemoteSettingsSample.Presentation.Presenter.Interface.View;
using UniRx;

namespace RemoteSettingsSample.Presentation.Presenter.Implement
{
    public class SampleScene : IRefreshHandler
    {
        public SampleScene(IRefreshTrigger refreshTrigger)
        {
            RefreshTrigger = refreshTrigger;
        }

        private IRefreshTrigger RefreshTrigger { get; }

        IObservable<Unit> IRefreshHandler.OnRefreshAsObservable() =>
            RefreshTrigger.OnTriggerAsObservable();
    }
}