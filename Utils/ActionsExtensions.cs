﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class ActionsExtensions
    {
        public static void ExecuteAsParallel(this IEnumerable<Action> actions)
        {
            var _exceptions = new ConcurrentQueue<Exception>();

            System.Threading.CancellationToken cts = default(System.Threading.CancellationToken);
            try
            {
                Parallel.ForEach<Action>(actions, new ParallelOptions() { CancellationToken = cts }, a =>
                                a.Invoke());
            }
            catch (AggregateException agex)
            {
                agex.InnerExceptions.ToList().ForEach(_exceptions.Enqueue);
            }

            if (_exceptions.Any())
                throw new ApplicationException(string.Format("Error: {0}", string.Join("\r\nError: ", _exceptions.Select(e => e.Message))));
        }

        public static void ExecuteAsSerial(this IEnumerable<Action> actions)
        {
            foreach (var action in actions)
            {
                action.Invoke();
            }
        }
    }
}
