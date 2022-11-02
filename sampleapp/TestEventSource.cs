
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftAntimalwareEngine;
using System.Diagnostics.Tracing;

namespace SampleApp
{
    [EventSource(Name = "TestEventSource")]
    public class TestEventSource : EventSource
    {
        public static class Keywords
        {
            public const EventKeywords m_1 = (EventKeywords)(1L << 1);
            public const EventKeywords m_2 = (EventKeywords)(1L << 2);
            public const EventKeywords m_3 = (EventKeywords)(1L << 3);
            public const EventKeywords m_4 = (EventKeywords)(1L << 4);
            public const EventKeywords m_5 = (EventKeywords)(1L << 5);
            public const EventKeywords m_6 = (EventKeywords)(1L << 6);
            public const EventKeywords m_7 = (EventKeywords)(1L << 7);
            public const EventKeywords m_8 = (EventKeywords)(1L << 8);
            public const EventKeywords m_9 = (EventKeywords)(1L << 9);
            public const EventKeywords m_10 = (EventKeywords)(1L << 10);
            public const EventKeywords m_11 = (EventKeywords)(1L << 11);
            public const EventKeywords m_12 = (EventKeywords)(1L << 12);
            public const EventKeywords m_13 = (EventKeywords)(1L << 13);
            public const EventKeywords m_14 = (EventKeywords)(1L << 14);
            public const EventKeywords m_15 = (EventKeywords)(1L << 15);
            public const EventKeywords m_16 = (EventKeywords)(1L << 16);
            public const EventKeywords m_17 = (EventKeywords)(1L << 17);
            public const EventKeywords m_18 = (EventKeywords)(1L << 18);
            public const EventKeywords m_19 = (EventKeywords)(1L << 19);
            public const EventKeywords m_20 = (EventKeywords)(1L << 20);
            public const EventKeywords m_21 = (EventKeywords)(1L << 21);
            public const EventKeywords m_22 = (EventKeywords)(1L << 22);
            public const EventKeywords m_23 = (EventKeywords)(1L << 23);
            public const EventKeywords m_24 = (EventKeywords)(1L << 24);
            public const EventKeywords m_25 = (EventKeywords)(1L << 25);
            public const EventKeywords m_26 = (EventKeywords)(1L << 26);
            public const EventKeywords m_27 = (EventKeywords)(1L << 27);
            public const EventKeywords m_28 = (EventKeywords)(1L << 28);
            public const EventKeywords m_29 = (EventKeywords)(1L << 29);
            public const EventKeywords m_30 = (EventKeywords)(1L << 30);
            public const EventKeywords m_31 = (EventKeywords)(1L << 31);
            public const EventKeywords m_32 = (EventKeywords)(1L << 32);
            public const EventKeywords m_33 = (EventKeywords)(1L << 33);
            public const EventKeywords m_34 = (EventKeywords)(1L << 34);
            public const EventKeywords m_35 = (EventKeywords)(1L << 35);
            public const EventKeywords m_36 = (EventKeywords)(1L << 36);
            public const EventKeywords m_37 = (EventKeywords)(1L << 37);
            public const EventKeywords m_38 = (EventKeywords)(1L << 38);
            public const EventKeywords m_39 = (EventKeywords)(1L << 39);
            public const EventKeywords m_40 = (EventKeywords)(1L << 40);
            public const EventKeywords m_41 = (EventKeywords)(1L << 41);
            public const EventKeywords m_42 = (EventKeywords)(1L << 42);
            public const EventKeywords m_43 = (EventKeywords)(1L << 43);
            public const EventKeywords m_44 = (EventKeywords)(1L << 44);
            public const EventKeywords m_45 = (EventKeywords)(1L << 45);
            public const EventKeywords m_46 = (EventKeywords)(1L << 46);
            public const EventKeywords m_47 = (EventKeywords)(1L << 47);
            public const EventKeywords m_48 = (EventKeywords)(1L << 48);
            public const EventKeywords m_49 = (EventKeywords)(1L << 49);
            public const EventKeywords m_50 = (EventKeywords)(1L << 50);
            public const EventKeywords m_51 = (EventKeywords)(1L << 51);
            public const EventKeywords m_52 = (EventKeywords)(1L << 52);
            public const EventKeywords m_53 = (EventKeywords)(1L << 53);
            public const EventKeywords m_54 = (EventKeywords)(1L << 54);
            public const EventKeywords m_55 = (EventKeywords)(1L << 55);
            public const EventKeywords m_56 = (EventKeywords)(1L << 56);
            public const EventKeywords m_57 = (EventKeywords)(1L << 57);
            public const EventKeywords m_58 = (EventKeywords)(1L << 58);
            public const EventKeywords m_59 = (EventKeywords)(1L << 59);
            public const EventKeywords m_60 = (EventKeywords)(1L << 60);
            public const EventKeywords m_61 = (EventKeywords)(1L << 61);
            public const EventKeywords m_62 = (EventKeywords)(1L << 62);
            public const EventKeywords m_63 = (EventKeywords)(1L << 63);
            public const EventKeywords m_64 = (EventKeywords)(1L << 64);
        }

        [Event(1, Keywords = Keywords.m_1)]
        public void Event1()
        {
            WriteEvent(1);
        }

        [Event(2, Keywords = Keywords.m_2)]
        public void Event2()
        {
            WriteEvent(2);
        }

        [Event(3, Keywords = Keywords.m_3)]
        public void Event3()
        {
            WriteEvent(3);
        }

        [Event(4, Keywords = Keywords.m_4)]
        public void Event4()
        {
            WriteEvent(4);
        }

        [Event(5, Keywords = Keywords.m_5)]
        public void Event5()
        {
            WriteEvent(5);
        }

        [Event(6, Keywords = Keywords.m_6)]
        public void Event6()
        {
            WriteEvent(6);
        }

        [Event(7, Keywords = Keywords.m_7)]
        public void Event7()
        {
            WriteEvent(7);
        }

        [Event(8, Keywords = Keywords.m_8)]
        public void Event8()
        {
            WriteEvent(8);
        }

        [Event(9, Keywords = Keywords.m_9)]
        public void Event9()
        {
            WriteEvent(9);
        }

        [Event(10, Keywords = Keywords.m_10)]
        public void Event10()
        {
            WriteEvent(10);
        }

        [Event(11, Keywords = Keywords.m_11)]
        public void Event11()
        {
            WriteEvent(11);
        }

        [Event(12, Keywords = Keywords.m_12)]
        public void Event12()
        {
            WriteEvent(12);
        }

        [Event(13, Keywords = Keywords.m_13)]
        public void Event13()
        {
            WriteEvent(13);
        }

        [Event(14, Keywords = Keywords.m_14)]
        public void Event14()
        {
            WriteEvent(14);
        }

        [Event(15, Keywords = Keywords.m_15)]
        public void Event15()
        {
            WriteEvent(15);
        }

        [Event(16, Keywords = Keywords.m_16)]
        public void Event16()
        {
            WriteEvent(16);
        }

        [Event(17, Keywords = Keywords.m_17)]
        public void Event17()
        {
            WriteEvent(17);
        }

        [Event(18, Keywords = Keywords.m_18)]
        public void Event18()
        {
            WriteEvent(18);
        }

        [Event(19, Keywords = Keywords.m_19)]
        public void Event19()
        {
            WriteEvent(19);
        }

        [Event(20, Keywords = Keywords.m_20)]
        public void Event20()
        {
            WriteEvent(20);
        }

        [Event(21, Keywords = Keywords.m_21)]
        public void Event21()
        {
            WriteEvent(21);
        }

        [Event(22, Keywords = Keywords.m_22)]
        public void Event22()
        {
            WriteEvent(22);
        }

        [Event(23, Keywords = Keywords.m_23)]
        public void Event23()
        {
            WriteEvent(23);
        }

        [Event(24, Keywords = Keywords.m_24)]
        public void Event24()
        {
            WriteEvent(24);
        }

        [Event(25, Keywords = Keywords.m_25)]
        public void Event25()
        {
            WriteEvent(25);
        }

        [Event(26, Keywords = Keywords.m_26)]
        public void Event26()
        {
            WriteEvent(26);
        }

        [Event(27, Keywords = Keywords.m_27)]
        public void Event27()
        {
            WriteEvent(27);
        }

        [Event(28, Keywords = Keywords.m_28)]
        public void Event28()
        {
            WriteEvent(28);
        }

        [Event(29, Keywords = Keywords.m_29)]
        public void Event29()
        {
            WriteEvent(29);
        }

        [Event(30, Keywords = Keywords.m_30)]
        public void Event30()
        {
            WriteEvent(30);
        }

        [Event(31, Keywords = Keywords.m_31)]
        public void Event31()
        {
            WriteEvent(31);
        }

        [Event(32, Keywords = Keywords.m_32)]
        public void Event32()
        {
            WriteEvent(32);
        }

        [Event(33, Keywords = Keywords.m_33)]
        public void Event33()
        {
            WriteEvent(33);
        }

        [Event(34, Keywords = Keywords.m_34)]
        public void Event34()
        {
            WriteEvent(34);
        }

        [Event(35, Keywords = Keywords.m_35)]
        public void Event35()
        {
            WriteEvent(35);
        }

        [Event(36, Keywords = Keywords.m_36)]
        public void Event36()
        {
            WriteEvent(36);
        }

        [Event(37, Keywords = Keywords.m_37)]
        public void Event37()
        {
            WriteEvent(37);
        }

        [Event(38, Keywords = Keywords.m_38)]
        public void Event38()
        {
            WriteEvent(38);
        }

        [Event(39, Keywords = Keywords.m_39)]
        public void Event39()
        {
            WriteEvent(39);
        }

        [Event(40, Keywords = Keywords.m_40)]
        public void Event40()
        {
            WriteEvent(40);
        }

        [Event(41, Keywords = Keywords.m_41)]
        public void Event41()
        {
            WriteEvent(41);
        }

        [Event(42, Keywords = Keywords.m_42)]
        public void Event42()
        {
            WriteEvent(42);
        }

        [Event(43, Keywords = Keywords.m_43)]
        public void Event43()
        {
            WriteEvent(43);
        }

        [Event(44, Keywords = Keywords.m_44)]
        public void Event44()
        {
            WriteEvent(44);
        }

        [Event(45, Keywords = Keywords.m_45)]
        public void Event45()
        {
            WriteEvent(45);
        }

        [Event(46, Keywords = Keywords.m_46)]
        public void Event46()
        {
            WriteEvent(46);
        }

        [Event(47, Keywords = Keywords.m_47)]
        public void Event47()
        {
            WriteEvent(47);
        }

        [Event(48, Keywords = Keywords.m_48)]
        public void Event48()
        {
            WriteEvent(48);
        }

        [Event(49, Keywords = Keywords.m_49)]
        public void Event49()
        {
            WriteEvent(49);
        }

        [Event(50, Keywords = Keywords.m_50)]
        public void Event50()
        {
            WriteEvent(50);
        }

        [Event(51, Keywords = Keywords.m_51)]
        public void Event51()
        {
            WriteEvent(51);
        }

        [Event(52, Keywords = Keywords.m_52)]
        public void Event52()
        {
            WriteEvent(52);
        }

        [Event(53, Keywords = Keywords.m_53)]
        public void Event53()
        {
            WriteEvent(53);
        }

        [Event(54, Keywords = Keywords.m_54)]
        public void Event54()
        {
            WriteEvent(54);
        }

        [Event(55, Keywords = Keywords.m_55)]
        public void Event55()
        {
            WriteEvent(55);
        }

        [Event(56, Keywords = Keywords.m_56)]
        public void Event56()
        {
            WriteEvent(56);
        }

        [Event(57, Keywords = Keywords.m_57)]
        public void Event57()
        {
            WriteEvent(57);
        }

        [Event(58, Keywords = Keywords.m_58)]
        public void Event58()
        {
            WriteEvent(58);
        }

        [Event(59, Keywords = Keywords.m_59)]
        public void Event59()
        {
            WriteEvent(59);
        }

        [Event(60, Keywords = Keywords.m_60)]
        public void Event60()
        {
            WriteEvent(60);
        }

        [Event(61, Keywords = Keywords.m_61)]
        public void Event61()
        {
            WriteEvent(61);
        }

        [Event(62, Keywords = Keywords.m_62)]
        public void Event62()
        {
            WriteEvent(62);
        }

        [Event(63, Keywords = Keywords.m_63)]
        public void Event63()
        {
            WriteEvent(63);
        }

        [Event(64, Keywords = Keywords.m_64)]
        public void Event64()
        {
            WriteEvent(64);
        }

        [Event(65)]
        public void Event65()
        {
            WriteEvent(65);
        }
    }
}