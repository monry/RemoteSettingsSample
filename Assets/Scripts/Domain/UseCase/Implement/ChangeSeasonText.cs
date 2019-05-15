using RemoteSettingsSample.Domain.UseCase.Interface.Entity;
using UniRx;
using Zenject;

namespace RemoteSettingsSample.Domain.UseCase.Implement
{
    public class ChangeSeasonText : IInitializable
    {
        // Entities
        private ISeasonTextMaster SeasonTextMaster { get; }
        private ISeasonState SeasonState { get; }

        // Others
        private SignalBus SignalBus { get; }

        void IInitializable.Initialize()
        {
            SeasonState
                .Where(SeasonTextMaster.Exists)
                .Select(SeasonTextMaster.Find)
                .Subscribe(SignalBus.Fire);
        }
    }
}