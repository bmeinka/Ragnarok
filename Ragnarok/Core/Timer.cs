using System;
using OpenTK;

namespace Ragnarok.Core
{
    // creating a custom timer allows the game to have total control over time
    class Timer
    {
        private bool started;
        private float elapsed;
        public float Interval { get; set; }
        public EventHandler Event;

        public Timer(float interval = 10f)
        {
            started = false;
            elapsed = 0f;
            Interval = interval;
            Game.Window.UpdateFrame += Update;
        }

        public void Start(float interval)
        {
            Interval = interval;
            started = true;
        }
        public void Start() => started = true;
        public void Stop() => started = false;
        public void Reset() => elapsed = 0f;
        public void Restart() { Reset(); Start(); }

        private void Update(object sender, FrameEventArgs e)
        {
            if (started)
                elapsed += (float)e.Time;
            if (elapsed >= Interval)
            {
                Event(this, EventArgs.Empty);
                elapsed -= Interval;
            }
        }
    }
}
