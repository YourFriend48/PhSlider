using System;

namespace YandexSDK
{
    public interface IGameSpeedGhangable
    {
        public event Action<float> GameSpeedChanged;
    }
}
