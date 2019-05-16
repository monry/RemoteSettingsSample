using JetBrains.Annotations;
using RemoteSettingsSample.Application.Message;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RemoteSettingsSample.Presentation.View.Implement
{
    [RequireComponent(typeof(Image))]
    public class ColorAnimator : MonoBehaviour
    {
        [Inject]
        [UsedImplicitly]
        private void Initialize(SignalBus signalBus)
        {
            signalBus.GetStream<SeasonColor>().Select(x => x.Color).Subscribe().AddTo(gameObject);
        }
    }
}