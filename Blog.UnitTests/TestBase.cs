using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.UnitTests
{
    public class TestBase
    {
        public static class Assert
        {
            public static void AreEqual(object exp, object act)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(exp, act);
            }

            public static void AreEqual<T>(T exp, T act)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual<T>(exp, act);
            }

            public static void AreEqual<T>(T[] exp, T[] act)
            {
                if (exp.Length != act.Length)
                {
                    throw new Exception("Length of exp and act are not Equal");
                }
                for (int i = 0; i < exp.Length; i++)
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(exp[i], act[i]);
                }
            }
        }

        public void ExpectException<T>(Action callback) where T : Exception
        {
            ExpectException<T>(null, callback);
        }

        public void ExpectException<T>(string exceptionMessage, Action callback) where T : Exception
        {
            try
            {
                callback();
            }
            catch (T e)
            {
                if (exceptionMessage == null || e.Message == exceptionMessage)
                {
                    return;
                }
                throw new Exception("Expect FeatherException message : " + exceptionMessage + " , but was : " + e.Message);
            }
            throw new Exception("Expect " + typeof(T).Name);
        }
    }
}
