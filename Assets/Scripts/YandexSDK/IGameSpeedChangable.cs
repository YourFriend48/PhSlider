using System;

namespace YandexSDK
{
    public interface IGameSpeedChangable
    {
        public event Action<float> GameSpeedChanged;
    }
}
