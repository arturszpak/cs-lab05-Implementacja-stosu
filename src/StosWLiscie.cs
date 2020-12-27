using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stos
{
    public class StosWLiscie<T> : IStos<T>
    {
        private class Wezel
        {
            public T dane;
            public Wezel nastepnik;
            internal Wezel(T e, Wezel nastepnik)
            {
                this.dane = e;
                this.nastepnik = nastepnik;
            }
        }

        private Wezel szczyt;

        public StosWLiscie()
        {
            szczyt = null;
        }

        public void Push(T e)
        {
            szczyt = new Wezel(e, szczyt);
        }

        public bool IsEmpty => szczyt == null;
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new StosEmptyException();
            }
            szczyt = szczyt.nastepnik;

            return default;
        }

        public T Peek => IsEmpty ? throw new StosEmptyException() : szczyt.dane;

        public int Size()
        {
            int size = 0;
            Wezel currentWezel = szczyt;
            while (currentWezel != null)
            {
                size++;
                currentWezel = currentWezel.nastepnik;
            }

            return size;
        }

        public int Count => Size();
        public void Clear() => szczyt = null;

        private T[] tab;

        public void TrimExcess()
        {
            int trim = (int)(Count * 0.1);
            Array.Resize(ref tab, trim);
        }

        public T this[int index]
        {
            get
            {
                Wezel currentWezel = szczyt;
                int i = index;
                while (i > 0)
                {
                    currentWezel = currentWezel.nastepnik;
                    i--;
                }
                return currentWezel.dane;
            }
        }

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
            private StosWLiscie<T> stos;
            private int position = -1;

            internal EnumeratorStosu(StosWLiscie<T> stos) =>
                this.stos = stos;

            //Zwraca bieżący element
            public T Current => stos[position];
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


        public T[] ToArray()
        {
            if (IsEmpty) throw new StosEmptyException();
            int i = 0;
            T[] temp = new T[Size()];
            foreach (var x in this)
            {
                temp[i++] = x;
            }
            return temp;
        }
    }
}