using System;
using System.Runtime.InteropServices;

namespace Test {

    public class Values {

        protected enum EnumU8 : byte { Value = 0x88 }
        protected enum EnumU16 : ushort { Value = 0x8899 }
        protected enum EnumU32 : uint { Value = 0x8899aabb }
        protected enum EnumU64 : ulong { Value = 0x8899aabbccddeeff }
        protected enum EnumS8 : sbyte { Value = unchecked((sbyte)0x88) }
        protected enum EnumS16 : short { Value = unchecked((short)0x8899) }
        protected enum EnumS32 : int { Value = unchecked((int)0x8899aabb) }
        protected enum EnumS64 : long { Value = unchecked((long)0x8899aabbccddeeff) }

        // 10 8 8 1
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        protected class AnsiType {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "012345678";
            const int array_size = 2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            public double[] array = new double[array_size] { 1.1, 1.2 }; 
            public char c16 = '@';
        }

        // 20 8 8 2
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        protected class UnicodeType {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "012345678";
            const int array_size = 2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            public double[] array = new double[array_size] { 1.1, 1.2 };
            public char c16 = '@';
        }

        protected static byte[] values = { 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
        protected static byte[] chars = { 0x00, 0x40 };
        protected static byte[] bools = { 0x00, 0x01 };
        protected static sbyte s8 = unchecked((sbyte)0x88);
        protected static Int16 s16 = unchecked((Int16)0x8899);
        protected static Int32 s32 = unchecked((Int32)0x8899aabb);
        protected static Int64 s64 = unchecked((Int64)0x8899aabbccddeeff);
        protected static byte u8 = unchecked((byte)0x88);
        protected static UInt16 u16 = (UInt16)0x8899;
        protected static UInt32 u32 = (UInt32)0x8899aabb;
        protected static UInt64 u64 = (UInt64)0x8899aabbccddeeff;
        protected static char c16 = '\x40';
        //
        protected static sbyte s8r = unchecked((sbyte)0x88);
        protected static Int16 s16r = unchecked((Int16)0x9988);
        protected static Int32 s32r = unchecked((Int32)0xbbaa9988);
        protected static Int64 s64r = unchecked((Int64)0xffeeddccbbaa9988);
        protected static UInt16 u16r = (UInt16)0x9988;
        protected static UInt32 u32r = (UInt32)0xbbaa9988;
        protected static UInt64 u64r = (UInt64)0xffeeddccbbaa9988;
        protected static char c16r = '\x4000';
        //
        protected static float f32 = 1.1f;
        protected static double f64 = 1.2;
    }
}
