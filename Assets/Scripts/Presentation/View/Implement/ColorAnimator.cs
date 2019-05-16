using System;
using JetBrains.Annotations;
using RemoteSettingsSample.Application;
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
        private Image image;
        private Image Image => image ? image : image = GetComponent<Image>();

        private IDisposable DisposableColorTransition { get; set; }

        [Inject]
        [UsedImplicitly]
        private void Initialize(SignalBus signalBus)
        {
            signalBus
                .GetStream<SeasonColor>()
                .Select(x => x.Color)
                .Subscribe(TransitColor)
                .AddTo(gameObject);
        }

        private void TransitColor(Color color)
        {
            var originalColor = Image.color;
            DisposableColorTransition?.Dispose();
            DisposableColorTransition = Observable
                .IntervalFrame(1)
                .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(Const.Time.TweenDurationColorTransition)))
                .Subscribe(time => Image.color = Color.Lerp(originalColor, color, (float)(time / (Const.Time.TweenDurationColorTransition * 60))))
                .AddTo(gameObject);
        }
    }
}