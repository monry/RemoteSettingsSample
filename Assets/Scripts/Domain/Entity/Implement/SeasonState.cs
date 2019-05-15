using RemoteSettingsSample.Application.Enum;
using RemoteSettingsSample.Domain.UseCase.Interface.Entity;
using UniRx;

namespace RemoteSettingsSample.Domain.Entity.Implement
{
    public class SeasonState : ReactiveProperty<Season>, ISeasonState
    {
    }
}