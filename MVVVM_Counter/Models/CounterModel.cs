using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using MVVVM_Counter.Service;

namespace MVVVM_Counter.Models
{
    public class CounterModel
    {
        public CounterModel(int initialValue = 0)
        {
            Value = initialValue;
        }
        
        // private set: クラス外からは読み取り専用。値の変更はIncrement/Decrementのみで行う
        public int Value { get; private set; }
        public event Action? ValueChanged;

        private void Notify() => ValueChanged?.Invoke();

        public void SetValue(int value)
        {
            Value = value;
            Notify();
        }

        public void Increment()
        {
            Value++;
            Notify();
        }

        public bool CanDecrement()
        {
            return Value > 0;
        }

        public void Decrement()
        {
            if(!CanDecrement()) return;
            Value--;
            Notify();
        }
    }
}
