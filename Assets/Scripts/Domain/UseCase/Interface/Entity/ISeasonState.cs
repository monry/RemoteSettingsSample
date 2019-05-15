using RemoteSettingsSample.Application.Enum;
using UniRx;

namespace RemoteSettingsSample.Domain.UseCase.Interface.Entity
{
    public interface ISeasonState : IReactiveProperty<Season>
    {
    }
}