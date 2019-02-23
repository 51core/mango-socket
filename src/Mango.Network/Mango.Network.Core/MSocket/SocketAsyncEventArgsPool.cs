using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mango.Network.Core.MSocket
{
    /// <summary>
    /// AsyncUserToken对象池（固定缓存设计）
    /// </summary>
    public class SocketAsyncEventArgsPool
    {
        private Stack<AsyncUserToken> m_pool;

        public SocketAsyncEventArgsPool(int capacity)
        {
            m_pool = new Stack<AsyncUserToken>(capacity);
        }

        public void Push(AsyncUserToken item)
        {
            if (item == null) { throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null"); }
            lock (m_pool)
            {
                m_pool.Push(item);
            }
        }

        // Removes a AsyncUserToken instance from the pool
        // and returns the object removed from the pool
        public AsyncUserToken Pop()
        {
            lock (m_pool)
            {
                return m_pool.Pop();
            }
        }

        // The number of AsyncUserToken instances in the pool
        public int Count
        {
            get { return m_pool.Count; }
        }
    }
}
