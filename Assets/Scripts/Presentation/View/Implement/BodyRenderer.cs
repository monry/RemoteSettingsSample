using System;
using JetBrains.Annotations;
using RemoteSettingsSample.Application.Message;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace RemoteSettingsSample.Presentation.View.Implement
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class BodyRenderer : MonoBehaviour
    {
        private TextMeshProUGUI textMeshProUGUI;
        private TextMeshProUGUI TextMeshProUGUI => textMeshProUGUI ? textMeshProUGUI : textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        private IDisposable DisposableTypeWriter { get; set; }

        [Inject]
        [UsedImplicitly]
        private void Initialize(SignalBus signalBus)
        {
            signalBus.GetStream<SeasonText>().Select(x => x.Body).Subscribe(RenderTextAsTypeWriter);
        }

        private void RenderTextAsTypeWriter(string text)
        {
            DisposableTypeWriter?.Dispose();
            DisposableTypeWriter = text
                .ToObservable()
                .Delay(TimeSpan.FromSeconds(0.1f))
                .Subscribe(x => TextMeshProUGUI.text += x);
        }
    }
}