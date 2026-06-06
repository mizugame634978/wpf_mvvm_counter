using System;
using System.Collections.Generic;
using System.Text;

namespace MVVVM_Counter
{
    internal class CounterModel
    {
        public int Value { get; private set; }

        public void Increment()
        {
            Value++;
        }

        public bool CanDecrement()
        {
            return Value > 0;
        }

        public void Decrement()
        {
            Value--;
        }
    }
}
