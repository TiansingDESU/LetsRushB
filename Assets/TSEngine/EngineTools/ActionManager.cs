using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class ActionManager
    {
        Dictionary<Object, Object> m_Actions = new Dictionary<object, object>();

        public EZAction RegAction(EZAction newAct, Action act)
        {
            newAct += act;
            m_Actions[newAct] = act;
            return newAct;
        }

        public EZAction<T1> RegAction<T1>(EZAction<T1> newAct, Action<T1> act)
        {
            newAct += act;
            m_Actions[newAct] = act;
            return newAct;
        }

        public EZAction<T1, T2> RegAction<T1, T2>(EZAction<T1, T2> newAct, Action<T1, T2> act)
        {
            newAct += act;
            m_Actions[newAct] = act;
            return newAct;
        }

        public EZAction<T1, T2, T3> RegAction<T1, T2, T3>(EZAction<T1, T2, T3> newAct, Action<T1, T2, T3> act)
        {
            newAct += act;
            m_Actions[newAct] = act;
            return newAct;
        }

        public EZAction<T1, T2, T3, T4> RegAction<T1, T2, T3, T4>(EZAction<T1, T2, T3, T4> newAct, Action<T1, T2, T3, T4> act)
        {
            newAct += act;
            m_Actions[newAct] = act;
            return newAct;
        }

        public EZAction<T1, T2, T3, T4, T5> RegAction<T1, T2, T3, T4, T5>(EZAction<T1, T2, T3, T4, T5> newAct, Action<T1, T2, T3, T4, T5> act)
        {
            newAct += act;
            m_Actions[newAct] = act;
            return newAct;
        }

        public EZFunc<T1> RegFunc<T1>(EZFunc<T1> newAct, Func<T1> act)
        {
            newAct += act;
            m_Actions[newAct] = act;
            return newAct;
        }

        public EZFunc<T1, T2> RegFunc<T1, T2>(EZFunc<T1, T2> newAct, Func<T1, T2> act)
        {
            newAct += act;
            m_Actions[newAct] = act;
            return newAct;
        }

        public EZFunc<T1, T2, T3> RegFunc<T1, T2, T3>(EZFunc<T1, T2, T3> newAct, Func<T1, T2, T3> act)
        {
            newAct += act;
            m_Actions[newAct] = act;
            return newAct;
        }

        public EZFunc<T1, T2, T3, T4> RegFunc<T1, T2, T3, T4>(EZFunc<T1, T2, T3, T4> newAct, Func<T1, T2, T3, T4> act)
        {
            newAct += act;
            m_Actions[newAct] = act;
            return newAct;
        }

        public EZFunc<T1, T2, T3, T4, T5> RegFunc<T1, T2, T3, T4, T5>(EZFunc<T1, T2, T3, T4, T5> newAct, Func<T1, T2, T3, T4, T5> act)
        {
            newAct += act;
            m_Actions[newAct] = act;
            return newAct;
        }

        public void Clear()
        {
            foreach (var act in m_Actions)
            {
                ((IEZAction)act.Key).Dispose(act.Value);
            }
        }
    }

    public interface IEZAction
    {
        void Dispose(Object obj);
    }

    public class EZAction : IEZAction
    {
        Action action;
        public void Dispose(Object obj)
        {
            if (obj is Action act)
                action -= act;
        }

        public void Invoke()
        {
            action?.Invoke();
        }

        public static EZAction operator +(EZAction a, Action b)
        {
            a.action -= b;
            a.action += b;
            return a;
        }

        public static EZAction operator -(EZAction a, Action b)
        {
            a.action -= b;
            return a;
        }
    }

    public class EZAction<T1> : IEZAction
    {
        Action<T1> action;
        public void Dispose(Object obj)
        {
            if (obj is Action<T1> act)
                action -= act;
        }

        public void Invoke(T1 t1)
        {
            action?.Invoke(t1);
        }

        public static EZAction<T1> operator +(EZAction<T1> a, Action<T1> b)
        {
            a.action -= b;
            a.action += b;
            return a;
        }

        public static EZAction<T1> operator -(EZAction<T1> a, Action<T1> b)
        {
            a.action -= b;
            return a;
        }
    }

    public class EZAction<T1, T2> : IEZAction
    {
        Action<T1, T2> action;
        public void Dispose(Object obj)
        {
            if (obj is Action<T1, T2> act)
                action -= act;
        }

        public void Invoke(T1 t1, T2 t2)
        {
            action?.Invoke(t1, t2);
        }

        public static EZAction<T1, T2> operator +(EZAction<T1, T2> a, Action<T1, T2> b)
        {
            a.action -= b;
            a.action += b;
            return a;
        }

        public static EZAction<T1, T2> operator -(EZAction<T1, T2> a, Action<T1, T2> b)
        {
            a.action -= b;
            return a;
        }
    }

    public class EZAction<T1, T2, T3> : IEZAction
    {
        Action<T1, T2, T3> action;
        public void Dispose(Object obj)
        {
            if (obj is Action<T1, T2, T3> act)
                action -= act;
        }

        public void Invoke(T1 t1, T2 t2, T3 t3)
        {
            action?.Invoke(t1, t2, t3);
        }

        public static EZAction<T1, T2, T3> operator +(EZAction<T1, T2, T3> a, Action<T1, T2, T3> b)
        {
            a.action -= b;
            a.action += b;
            return a;
        }

        public static EZAction<T1, T2, T3> operator -(EZAction<T1, T2, T3> a, Action<T1, T2, T3> b)
        {
            a.action -= b;
            return a;
        }
    }

    public class EZAction<T1, T2, T3, T4> : IEZAction
    {
        Action<T1, T2, T3, T4> action;
        public void Dispose(Object obj)
        {
            if (obj is Action<T1, T2, T3, T4> act)
                action -= act;
        }

        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4)
        {
            action?.Invoke(t1, t2, t3, t4);
        }

        public static EZAction<T1, T2, T3, T4> operator +(EZAction<T1, T2, T3, T4> a, Action<T1, T2, T3, T4> b)
        {
            a.action -= b;
            a.action += b;
            return a;
        }

        public static EZAction<T1, T2, T3, T4> operator -(EZAction<T1, T2, T3, T4> a, Action<T1, T2, T3, T4> b)
        {
            a.action -= b;
            return a;
        }
    }

    public class EZAction<T1, T2, T3, T4, T5> : IEZAction
    {
        Action<T1, T2, T3, T4, T5> action;
        public void Dispose(Object obj)
        {
            if (obj is Action<T1, T2, T3, T4, T5> act)
                action -= act;
        }

        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            action?.Invoke(t1, t2, t3, t4, t5);
        }

        public static EZAction<T1, T2, T3, T4, T5> operator +(EZAction<T1, T2, T3, T4, T5> a, Action<T1, T2, T3, T4, T5> b)
        {
            a.action -= b;
            a.action += b;
            return a;
        }

        public static EZAction<T1, T2, T3, T4, T5> operator -(EZAction<T1, T2, T3, T4, T5> a, Action<T1, T2, T3, T4, T5> b)
        {
            a.action -= b;
            return a;
        }
    }

    public class EZFunc<T1>
    {
        Func<T1> func;

        public void Dispose(object obj)
        {
            if (obj is Func<T1> act)
                func -= act;
        }

        public T1 Invoke()
        {
            return func == null ? default(T1) : func.Invoke();
        }

        public static EZFunc<T1> operator +(EZFunc<T1> a, Func<T1> b)
        {
            a.func -= b;
            a.func += b;
            return a;
        }

        public static EZFunc<T1> operator -(EZFunc<T1> a, Func<T1> b)
        {
            a.func -= b;
            return a;
        }
    }

    public class EZFunc<T1, T2>
    {
        Func<T1, T2> func;

        public void Dispose(object obj)
        {
            if (obj is Func<T1, T2> act)
                func -= act;
        }

        public T2 Invoke(T1 t1)
        {
            return func == null ? default(T2) : func.Invoke(t1);
        }

        public static EZFunc<T1, T2> operator +(EZFunc<T1, T2> a, Func<T1, T2> b)
        {
            a.func -= b;
            a.func += b;
            return a;
        }

        public static EZFunc<T1, T2> operator -(EZFunc<T1, T2> a, Func<T1, T2> b)
        {
            a.func -= b;
            return a;
        }
    }

    public class EZFunc<T1, T2, T3>
    {
        Func<T1, T2, T3> func;

        public void Dispose(object obj)
        {
            if (obj is Func<T1, T2, T3> act)
                func -= act;
        }

        public T3 Invoke(T1 t1, T2 t2)
        {
            return func == null ? default(T3) : func.Invoke(t1, t2);
        }

        public static EZFunc<T1, T2, T3> operator +(EZFunc<T1, T2, T3> a, Func<T1, T2, T3> b)
        {
            a.func -= b;
            a.func += b;
            return a;
        }

        public static EZFunc<T1, T2, T3> operator -(EZFunc<T1, T2, T3> a, Func<T1, T2, T3> b)
        {
            a.func -= b;
            return a;
        }
    }

    public class EZFunc<T1, T2, T3, T4>
    {
        Func<T1, T2, T3, T4> func;

        public void Dispose(object obj)
        {
            if (obj is Func<T1, T2, T3, T4> act)
                func -= act;
        }

        public T4 Invoke(T1 t1, T2 t2, T3 t3)
        {
            return func == null ? default(T4) : func.Invoke(t1, t2, t3);
        }

        public static EZFunc<T1, T2, T3, T4> operator +(EZFunc<T1, T2, T3, T4> a, Func<T1, T2, T3, T4> b)
        {
            a.func -= b;
            a.func += b;
            return a;
        }

        public static EZFunc<T1, T2, T3, T4> operator -(EZFunc<T1, T2, T3, T4> a, Func<T1, T2, T3, T4> b)
        {
            a.func -= b;
            return a;
        }
    }

    public class EZFunc<T1, T2, T3, T4, T5>
    {
        Func<T1, T2, T3, T4, T5> func;

        public void Dispose(object obj)
        {
            if (obj is Func<T1, T2, T3, T4, T5> act)
                func -= act;
        }

        public T5 Invoke(T1 t1, T2 t2, T3 t3, T4 t4)
        {
            return func == null ? default(T5) : func.Invoke(t1, t2, t3, t4);
        }

        public static EZFunc<T1, T2, T3, T4, T5> operator +(EZFunc<T1, T2, T3, T4, T5> a, Func<T1, T2, T3, T4, T5> b)
        {
            a.func -= b;
            a.func += b;
            return a;
        }

        public static EZFunc<T1, T2, T3, T4, T5> operator -(EZFunc<T1, T2, T3, T4, T5> a, Func<T1, T2, T3, T4, T5> b)
        {
            a.func -= b;
            return a;
        }
    }
}
