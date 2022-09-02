namespace Wonderland.GameBoy;

public static class Bits
{
    public static ushort CreateU16(byte msb, byte lsb) => (ushort) ((msb << 8) | lsb);
}