using Ragnarok.Core;

namespace Ragnarok.Gameplay.Control.State
{
    abstract class TimeoutState : IControlState
    {
        private readonly float timeout;
        private readonly float start_time;
        /// <summary>
        /// if the state actually has a timer and the amount of time has elapsed
        /// </summary>
        protected bool Elapsed => timeout > 0 && Game.Time - start_time >= timeout;
        public TimeoutState(float timeout = 0f)
        {
            this.timeout = timeout;
            this.start_time = Game.Time;
        }
        /// <summary>
        /// by default, the update will simply pop the state if the timer has elapsed
        /// </summary>
        /// <param name="parent"></param>
        public virtual void Update(Controller parent)
        {
            if (Elapsed)
                parent.Pop();
        }
    }
}
