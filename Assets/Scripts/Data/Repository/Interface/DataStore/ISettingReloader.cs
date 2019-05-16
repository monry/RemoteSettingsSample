using System;
using UniRx;

namespace Data.Repository.Interface.DataStore
{
    public interface ISettingReloader
    {
        void Reload();
        IObservable<Unit> OnReloadAsObservable();
    }
}