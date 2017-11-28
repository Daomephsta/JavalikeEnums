using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JavalikeEnums;

namespace JavalikeEnums.Tests
{
    [TestClass]
    public class JavalikeEnumTest
    {
        [TestMethod]
        public void TestNames()
        {
            Assert.AreEqual("UPPERCASE_TEST", NameTestEnum.UPPERCASE_TEST.Name);
            Assert.AreEqual("lowercase_test", NameTestEnum.lowercase_test.Name);
            Assert.AreEqual("camelCaseTest", NameTestEnum.camelCaseTest.Name);
            Assert.AreEqual("mIxEdCaSeTeSt", NameTestEnum.mIxEdCaSeTeSt.Name);
        }

        [TestMethod]
        public void TestOrdinals()
        {
            Assert.AreEqual(0, OrdinalTestEnum.TEST0.Ordinal);
            Assert.AreEqual(1, OrdinalTestEnum.TEST1.Ordinal);
            Assert.AreEqual(0, OrdinalTestEnum2.TEST0.Ordinal);
            Assert.AreEqual(1, OrdinalTestEnum2.TEST1.Ordinal);
        }
    }

    public class NameTestEnum : JavalikeEnum<NameTestEnum>
    {
        public static readonly NameTestEnum UPPERCASE_TEST = newConstant().create();
        public static readonly NameTestEnum lowercase_test = newConstant().create();
        public static readonly NameTestEnum camelCaseTest = newConstant().create();
        public static readonly NameTestEnum mIxEdCaSeTeSt = newConstant().create();
    }

    public class OrdinalTestEnum : JavalikeEnum<OrdinalTestEnum>
    {
        public static readonly OrdinalTestEnum TEST0 = newConstant().create();
        public static readonly OrdinalTestEnum TEST1 = newConstant().create();
    }

    public class OrdinalTestEnum2 : JavalikeEnum<OrdinalTestEnum2>
    {
        public static readonly OrdinalTestEnum2 TEST0 = newConstant().create();
        public static readonly OrdinalTestEnum2 TEST1 = newConstant().create();
    }
}
