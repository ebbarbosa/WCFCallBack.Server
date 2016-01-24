using System;
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
            System.Threading.CancellationToken cts = default(System.Threading.CancellationToken);
            Parallel.ForEach<Action>(actions, new ParallelOptions() { CancellationToken = cts }, a =>
            {
                a.Invoke();
            });
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
