using System.Collections.Generic;

namespace Ragnarok.Gameplay
{
    abstract class Controller
    {
        protected readonly IControlState default_state;
        protected readonly Stack<IControlState> stack = new Stack<IControlState>();
        public IControlState State
        {
            get
            {
                if (stack.Count > 0)
                    return stack.Peek();
                return default_state;
            }
        }
        public Controller(IControlState state) => default_state = state;

        public void Push(IControlState state) => stack.Push(state);
        public void Pop() => stack.Pop();
        public void Replace(IControlState state)
        {
            Pop();
            Push(state);
        }
        public void Update() => State.Update(this);
    }
}
