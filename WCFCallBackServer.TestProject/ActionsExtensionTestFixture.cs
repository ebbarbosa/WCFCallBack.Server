using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using Utils;
using SharpTestsEx;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WCFCallBackServer.TestProject
{
    [TestClass]
    public class ActionsExtensionTestFixture
    {
        private IEnumerable<Action> actions;
        private int _expected = 1;
        private int _var1actual;

        [TestInitialize]
        public void Init()
        {
            actions = new List<Action>()
            {
                ()=> { _var1actual= GetSomeValueToVar1(); },
                ()=> NewMethod(2000),
                ()=> NewMethod(5500),
            };
        }

        private static void NewMethod(int milli)
        {
            Thread.Sleep(milli);
        }


        [TestMethod]
        public void Execute_as_parallel()
        {
            _var1actual = 0;
            var timer = new Stopwatch();
            timer.Start();

            actions.ExecuteAsParallel();

            timer.Elapsed.TotalMilliseconds.Should().Be.LessThanOrEqualTo(5509);

            _var1actual.Should().Be.EqualTo(_expected);
        }

        [TestMethod]
        public void Execute_as_serial()
        {
            _var1actual = 0;
            var timer = new Stopwatch();
            timer.Start();

            actions.ExecuteAsSerial();

            timer.Elapsed.TotalMilliseconds.Should().Be.GreaterThanOrEqualTo(5500);

            _var1actual.Should().Be.EqualTo(_expected);
        }

        private int GetSomeValueToVar1()
        {
            return _expected;
        }
    }
}
