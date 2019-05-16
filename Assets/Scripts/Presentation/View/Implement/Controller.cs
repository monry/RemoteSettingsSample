using System;
using RemoteSettingsSample.Application;
using RemoteSettingsSample.Presentation.Presenter.Interface.View;
using UniRx;
using UnityEngine;

namespace RemoteSettingsSample.Presentation.View.Implement
{
    public class Controller : MonoBehaviour, IRefreshTrigger
    {
        [SerializeField] private float refreshRatio = Const.Time.IntervalRefresh;
        private float RefreshRatio => refreshRatio;

        IObservable<Unit> IRefreshTrigger.OnTriggerAsObservable() =>
            Observable.Interval(TimeSpan.FromSeconds(RefreshRatio)).AsUnitObservable();
    }
}