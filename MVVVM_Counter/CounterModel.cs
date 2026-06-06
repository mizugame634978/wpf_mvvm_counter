using System;
using System.Collections.Generic;
using System.Text;

namespace MVVVM_Counter
{
    internal class CounterModel
    {
        // private set: クラス外からは読み取り専用。値の変更はIncrement/Decrementのみで行う
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
