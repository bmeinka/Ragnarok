using System.Collections.Generic;

namespace Ragnarok.Gameplay.Control
{
    abstract class Controller
    {
        protected readonly Stack<IControlState> stack = new Stack<IControlState>();
        public IControlState State
        {
            get
            {
                // push a new state if there isn't one
                if (stack.Count <= 0)
                    stack.Push(GetDefaultState());
                return stack.Peek();
            }
        }

        public void Push(IControlState state) => stack.Push(state);
        public void Pop()
        {
            if (stack.Count > 0)
                stack.Pop();
        }
        public void Replace(IControlState state)
        {
            Pop();
            Push(state);
        }
        public virtual void Update() => State.Update(this);
        public abstract IControlState GetDefaultState();
    }
}
