using System.Collections.Generic;

namespace Ragnarok.Gameplay.Control
{
    abstract class Controller
    {
        private readonly bool debug;
        protected readonly Stack<IControlState> stack = new Stack<IControlState>();
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
            ShowStack();
        }
        public void Pop()
        {
            if (stack.Count > 0)
                stack.Pop();
            ShowStack();
        }
        public void Replace(IControlState state)
        {
            Pop();
            Push(state);
        }

        /// <summary>
        /// collapse the stack down to only the given state
        /// </summary>
        /// <param name="state">the state to be in</param>
        public void Collapse(IControlState state)
        {
            stack.Clear();
            stack.Push(state);
        }

        public virtual void Update() => CurrentState.Update(this);
        public abstract IControlState GetDefaultState();
    }
}
