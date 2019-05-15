using JetBrains.Annotations;
using RemoteSettingsSample.Application.Message;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace RemoteSettingsSample.Presentation.View.Implement
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TitleRenderer : MonoBehaviour
    {
        private TextMeshProUGUI textMeshProUGUI;
        private TextMeshProUGUI TextMeshProUGUI => textMeshProUGUI ? textMeshProUGUI : textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        [Inject]
        [UsedImplicitly]
        private void Initialize(SignalBus signalBus)
        {
            signalBus.GetStream<SeasonText>().Subscribe(x => TextMeshProUGUI.SetText(x.Title));
        }
    }
}