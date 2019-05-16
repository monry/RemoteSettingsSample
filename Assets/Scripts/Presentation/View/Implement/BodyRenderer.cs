using System;
using JetBrains.Annotations;
using RemoteSettingsSample.Application;
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
            signalBus
                .GetStream<SeasonText>()
                .Select(x => x.Body)
                .Subscribe(RenderTextAsTypeWriter)
                .AddTo(gameObject);
        }

        private void RenderTextAsTypeWriter(string text)
        {
            TextMeshProUGUI.text = string.Empty;
            DisposableTypeWriter?.Dispose();
            DisposableTypeWriter = text
                .ToObservable()
                .Delay(TimeSpan.FromSeconds(Const.Time.IntervalTextTypeWriter))
                .Subscribe(x => TextMeshProUGUI.text += x)
                .AddTo(gameObject);
        }
    }
}