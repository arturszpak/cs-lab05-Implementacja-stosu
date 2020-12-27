using System;
using System.Collections;
using System.Collections.Generic;

namespace Stos
{
    public class StosWTablicy<T> : IStos<T>
    {
        private T[] tab;
        private int szczyt = -1;

        public StosWTablicy(int size = 10)
        {
            tab = new T[size];
            szczyt = -1;
        }

        public T Peek => IsEmpty ? throw new StosEmptyException() : tab[szczyt];

        public int Count => szczyt + 1;

        public bool IsEmpty => szczyt == -1;

        public void Clear() => szczyt = -1;

        public T Pop()
        {
            if (IsEmpty)
                throw new StosEmptyException();

            szczyt--;
            return tab[szczyt + 1];
        }

        public void Push(T value)
        {
            if (szczyt == tab.Length - 1)
            {
                //Array.Resize(ref tab, tab.Length * 2);
                TrimExcess();
            }

            szczyt++;
            tab[szczyt] = value;
        }

        public T[] ToArray()
        {
            if (IsEmpty) throw new StosEmptyException();
            //return tab;  //bardzo źle - reguły hermetyzacji
            //poprawnie:
            T[] temp = new T[szczyt + 1];
            Array.Copy(tab, temp, Count);
            return temp;
        }

        //TrimExcess
        public void TrimExcess()
        {
            int newSize = tab.Length + (int)Math.Ceiling(szczyt * 0.9);
            Array.Resize(ref tab, newSize);
        }

        //indexer
        public T this[int index] =>
            (index > Count - 1) ?
            throw new IndexOutOfRangeException() : tab[index];

        //IEnumerator
        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeratorStosu(this);
        }


        public IEnumerable<T> TopToBottom
        {
            get
            {
                for (int i = Count - 1; i >= 0; i--)
                {
                    yield return this[i];
                }
            }
        }

        private class EnumeratorStosu : IEnumerator<T>
        {
            private StosWTablicy<T> stos;
            private int position = -1;

            internal EnumeratorStosu(StosWTablicy<T> stos) =>
                this.stos = stos;

            //Zwraca bieżący element
            public T Current => stos.tab[position];
            object IEnumerator.Current => Current;

            public void Dispose() { } 
            public bool MoveNext()
            {
                if (position < stos.Count - 1)
                {
                    position++;
                    return true;
                }
                else return false;
            }

            public void Reset() => position = -1;
        }
        //ToArray
        public System.Collections.ObjectModel.ReadOnlyCollection<T> ToArrayReadOnly()
        {
            return Array.AsReadOnly(tab);
        }
    }
}