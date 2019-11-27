using System.Collections.Generic;

namespace Ragnarok.Gameplay.Control
{
    abstract class Controller
    {

        private readonly bool debug;
        protected readonly Stack<IControlState> stack = new Stack<IControlState>();

        private bool dirty = false;

        public bool Enabled { get; set; } = true;

        public IControlState CurrentState
        {
            get
            {
                // push a new state if there isn't one
                if (stack.Count <= 0)
                    stack.Push(GetDefaultState());
                return stack.Peek();
            }
        }

        public Controller(bool debug = false) => this.debug = debug;
        private void ShowStack()
        {
            if (debug)
            {
                var line = "";
                foreach (var control in stack)
                    line += control.ToString() + ", ";
                System.Console.WriteLine(line);
            }
        }

        //public void Push(IControlState state) => stack.Push(state);
        public void Push(IControlState state)
        {
            stack.Push(state);
            dirty = true;
        }
        public void Pop()
        {
            if (stack.Count > 0)
                stack.Pop();
            dirty = true;
        }
        public void Replace(IControlState state)
        {
            Pop();
            Push(state);
            dirty = true;
        }

        /// <summary>
        /// collapse the stack down to only the given state
        /// </summary>
        /// <param name="state">the state to be in</param>
        public void Collapse(IControlState state)
        {
            stack.Clear();
            stack.Push(state);
            dirty = true;
        }

        //public virtual void Update() => CurrentState.Update(this);
        public virtual void Update()
        {
            if (debug && dirty)
            {
                ShowStack();
                dirty = false;
            }
            if (Enabled)
                CurrentState.Update(this);
        }
        public abstract IControlState GetDefaultState();
    }
}
