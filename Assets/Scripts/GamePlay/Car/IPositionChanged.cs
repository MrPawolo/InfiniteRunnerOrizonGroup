using System;

namespace ML.GamePlay
{
    public interface IPositionChanged
    {
        public Action OnPositionChanged { get; set; }
    }
}
