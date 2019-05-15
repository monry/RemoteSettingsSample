using Zenject;

namespace RemoteSettingsSample.Application.Installer
{
    public class SignalBusInstaller : MonoInstaller<SignalBusInstaller>
    {
        public override void InstallBindings()
        {
            Zenject.SignalBusInstaller.Install(Container);
        }
    }
}