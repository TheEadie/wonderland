using Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Add;
using Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Dec;
using Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Inc;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Add;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.And;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Compare;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Dec;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Inc;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Or;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Sub;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.SubWithCarry;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Xor;
using Wonderland.GameBoy.OpCodes.CpuControl;
using Wonderland.GameBoy.OpCodes.JumpAndCalls;
using Wonderland.GameBoy.OpCodes.Load16Bit;
using Wonderland.GameBoy.OpCodes.Load8Bit;
using Wonderland.GameBoy.OpCodes.RotateShift;
using Wonderland.GameBoy.OpCodes.SingleBit;
using u8 = byte;

namespace Wonderland.GameBoy.OpCodes;

public class OpCodeHandler
{
    private readonly Dictionary<u8, OpCode> _opCodes;
    private readonly Dictionary<u8, OpCode> _cbOpCodes;

    public OpCodeHandler()
    {
        _cbOpCodes = new Dictionary<u8, OpCode>
        {
            #region RLC
            { 0x00, new RotateShift_r(0x00, "RLC", "B", r => r.B, (r, v) => r.B = v, Rlc) },
            { 0x01, new RotateShift_r(0x01, "RLC", "C", r => r.C, (r, v) => r.C = v, Rlc) },
            { 0x02, new RotateShift_r(0x02, "RLC", "D", r => r.D, (r, v) => r.D = v, Rlc) },
            { 0x03, new RotateShift_r(0x03, "RLC", "E", r => r.E, (r, v) => r.E = v, Rlc) },
            { 0x04, new RotateShift_r(0x04, "RLC", "H", r => r.H, (r, v) => r.H = v, Rlc) },
            { 0x05, new RotateShift_r(0x05, "RLC", "L", r => r.L, (r, v) => r.L = v, Rlc) },
            { 0x06, new RotateShift_HL(0x06, "RLC", Rlc) },
            { 0x07, new RotateShift_r(0x07, "RLC", "A", r => r.A, (r, v) => r.A = v, Rlc) },
            #endregion

            #region RRC
            { 0x08, new RotateShift_r(0x08, "RRC", "B", r => r.B, (r, v) => r.B = v, Rrc) },
            { 0x09, new RotateShift_r(0x09, "RRC", "C", r => r.C, (r, v) => r.C = v, Rrc) },
            { 0x0A, new RotateShift_r(0x0A, "RRC", "D", r => r.D, (r, v) => r.D = v, Rrc) },
            { 0x0B, new RotateShift_r(0x0B, "RRC", "E", r => r.E, (r, v) => r.E = v, Rrc) },
            { 0x0C, new RotateShift_r(0x0C, "RRC", "H", r => r.H, (r, v) => r.H = v, Rrc) },
            { 0x0D, new RotateShift_r(0x0D, "RRC", "L", r => r.L, (r, v) => r.L = v, Rrc) },
            { 0x0E, new RotateShift_HL(0x0E, "RRC", Rrc) },
            { 0x0F, new RotateShift_r(0x0F, "RRC", "A", r => r.A, (r, v) => r.A = v, Rrc) },
            #endregion

            #region RL
            { 0x10, new RotateShift_r(0x10, "RL", "B", r => r.B, (r, v) => r.B = v, Rl) },
            { 0x11, new RotateShift_r(0x11, "RL", "C", r => r.C, (r, v) => r.C = v, Rl) },
            { 0x12, new RotateShift_r(0x12, "RL", "D", r => r.D, (r, v) => r.D = v, Rl) },
            { 0x13, new RotateShift_r(0x13, "RL", "E", r => r.E, (r, v) => r.E = v, Rl) },
            { 0x14, new RotateShift_r(0x14, "RL", "H", r => r.H, (r, v) => r.H = v, Rl) },
            { 0x15, new RotateShift_r(0x15, "RL", "L", r => r.L, (r, v) => r.L = v, Rl) },
            { 0x16, new RotateShift_HL(0x16, "RL", Rl) },
            { 0x17, new RotateShift_r(0x17, "RL", "A", r => r.A, (r, v) => r.A = v, Rl) },
            #endregion

            #region RR
            { 0x18, new RotateShift_r(0x18, "RR", "B", r => r.B, (r, v) => r.B = v, Rr) },
            { 0x19, new RotateShift_r(0x19, "RR", "C", r => r.C, (r, v) => r.C = v, Rr) },
            { 0x1A, new RotateShift_r(0x1A, "RR", "D", r => r.D, (r, v) => r.D = v, Rr) },
            { 0x1B, new RotateShift_r(0x1B, "RR", "E", r => r.E, (r, v) => r.E = v, Rr) },
            { 0x1C, new RotateShift_r(0x1C, "RR", "H", r => r.H, (r, v) => r.H = v, Rr) },
            { 0x1D, new RotateShift_r(0x1D, "RR", "L", r => r.L, (r, v) => r.L = v, Rr) },
            { 0x1E, new RotateShift_HL(0x1E, "RR", Rr) },
            { 0x1F, new RotateShift_r(0x1F, "RR", "A", r => r.A, (r, v) => r.A = v, Rr) },
            #endregion

            #region SLA
            { 0x20, new RotateShift_r(0x20, "SLA", "B", r => r.B, (r, v) => r.B = v, Sla) },
            { 0x21, new RotateShift_r(0x21, "SLA", "C", r => r.C, (r, v) => r.C = v, Sla) },
            { 0x22, new RotateShift_r(0x22, "SLA", "D", r => r.D, (r, v) => r.D = v, Sla) },
            { 0x23, new RotateShift_r(0x23, "SLA", "E", r => r.E, (r, v) => r.E = v, Sla) },
            { 0x24, new RotateShift_r(0x24, "SLA", "H", r => r.H, (r, v) => r.H = v, Sla) },
            { 0x25, new RotateShift_r(0x25, "SLA", "L", r => r.L, (r, v) => r.L = v, Sla) },
            { 0x26, new RotateShift_HL(0x26, "SLA", Sla) },
            { 0x27, new RotateShift_r(0x27, "SLA", "A", r => r.A, (r, v) => r.A = v, Sla) },
            #endregion

            #region SRA
            { 0x28, new RotateShift_r(0x28, "SRA", "B", r => r.B, (r, v) => r.B = v, Sra) },
            { 0x29, new RotateShift_r(0x29, "SRA", "C", r => r.C, (r, v) => r.C = v, Sra) },
            { 0x2A, new RotateShift_r(0x2A, "SRA", "D", r => r.D, (r, v) => r.D = v, Sra) },
            { 0x2B, new RotateShift_r(0x2B, "SRA", "E", r => r.E, (r, v) => r.E = v, Sra) },
            { 0x2C, new RotateShift_r(0x2C, "SRA", "H", r => r.H, (r, v) => r.H = v, Sra) },
            { 0x2D, new RotateShift_r(0x2D, "SRA", "L", r => r.L, (r, v) => r.L = v, Sra) },
            { 0x2E, new RotateShift_HL(0x2E, "SRA", Sra) },
            { 0x2F, new RotateShift_r(0x2F, "SRA", "A", r => r.A, (r, v) => r.A = v, Sra) },
            #endregion

            #region SWAP
            { 0x30, new RotateShift_r(0x30, "SWAP", "B", r => r.B, (r, v) => r.B = v, Swap) },
            { 0x31, new RotateShift_r(0x31, "SWAP", "C", r => r.C, (r, v) => r.C = v, Swap) },
            { 0x32, new RotateShift_r(0x32, "SWAP", "D", r => r.D, (r, v) => r.D = v, Swap) },
            { 0x33, new RotateShift_r(0x33, "SWAP", "E", r => r.E, (r, v) => r.E = v, Swap) },
            { 0x34, new RotateShift_r(0x34, "SWAP", "H", r => r.H, (r, v) => r.H = v, Swap) },
            { 0x35, new RotateShift_r(0x35, "SWAP", "L", r => r.L, (r, v) => r.L = v, Swap) },
            { 0x36, new RotateShift_HL(0x36, "SWAP", Swap) },
            { 0x37, new RotateShift_r(0x37, "SWAP", "A", r => r.A, (r, v) => r.A = v, Swap) },
            #endregion

            #region SRL
            { 0x38, new RotateShift_r(0x38, "SRL", "B", r => r.B, (r, v) => r.B = v, Srl) },
            { 0x39, new RotateShift_r(0x39, "SRL", "C", r => r.C, (r, v) => r.C = v, Srl) },
            { 0x3A, new RotateShift_r(0x3A, "SRL", "D", r => r.D, (r, v) => r.D = v, Srl) },
            { 0x3B, new RotateShift_r(0x3B, "SRL", "E", r => r.E, (r, v) => r.E = v, Srl) },
            { 0x3C, new RotateShift_r(0x3C, "SRL", "H", r => r.H, (r, v) => r.H = v, Srl) },
            { 0x3D, new RotateShift_r(0x3D, "SRL", "L", r => r.L, (r, v) => r.L = v, Srl) },
            { 0x3E, new RotateShift_HL(0x3E, "SRL", Srl) },
            { 0x3F, new RotateShift_r(0x3F, "SRL", "A", r => r.A, (r, v) => r.A = v, Srl) },
            #endregion

            #region BIT
            // bit 0
            { 0x40, new BIT(0x40, 0, "B", r => r.B) },
            { 0x41, new BIT(0x41, 0, "C", r => r.C) },
            { 0x42, new BIT(0x42, 0, "D", r => r.D) },
            { 0x43, new BIT(0x43, 0, "E", r => r.E) },
            { 0x44, new BIT(0x44, 0, "H", r => r.H) },
            { 0x45, new BIT(0x45, 0, "L", r => r.L) },
            { 0x46, new BIT_HL(0x46, 0) },
            { 0x47, new BIT(0x47, 0, "A", r => r.A) },
            // bit 1
            { 0x48, new BIT(0x48, 1, "B", r => r.B) },
            { 0x49, new BIT(0x49, 1, "C", r => r.C) },
            { 0x4A, new BIT(0x4A, 1, "D", r => r.D) },
            { 0x4B, new BIT(0x4B, 1, "E", r => r.E) },
            { 0x4C, new BIT(0x4C, 1, "H", r => r.H) },
            { 0x4D, new BIT(0x4D, 1, "L", r => r.L) },
            { 0x4E, new BIT_HL(0x4E, 1) },
            { 0x4F, new BIT(0x4F, 1, "A", r => r.A) },
            // bit 2
            { 0x50, new BIT(0x50, 2, "B", r => r.B) },
            { 0x51, new BIT(0x51, 2, "C", r => r.C) },
            { 0x52, new BIT(0x52, 2, "D", r => r.D) },
            { 0x53, new BIT(0x53, 2, "E", r => r.E) },
            { 0x54, new BIT(0x54, 2, "H", r => r.H) },
            { 0x55, new BIT(0x55, 2, "L", r => r.L) },
            { 0x56, new BIT_HL(0x56, 2) },
            { 0x57, new BIT(0x57, 2, "A", r => r.A) },
            // bit 3
            { 0x58, new BIT(0x58, 3, "B", r => r.B) },
            { 0x59, new BIT(0x59, 3, "C", r => r.C) },
            { 0x5A, new BIT(0x5A, 3, "D", r => r.D) },
            { 0x5B, new BIT(0x5B, 3, "E", r => r.E) },
            { 0x5C, new BIT(0x5C, 3, "H", r => r.H) },
            { 0x5D, new BIT(0x5D, 3, "L", r => r.L) },
            { 0x5E, new BIT_HL(0x5E, 3) },
            { 0x5F, new BIT(0x5F, 3, "A", r => r.A) },
            // bit 4
            { 0x60, new BIT(0x60, 4, "B", r => r.B) },
            { 0x61, new BIT(0x61, 4, "C", r => r.C) },
            { 0x62, new BIT(0x62, 4, "D", r => r.D) },
            { 0x63, new BIT(0x63, 4, "E", r => r.E) },
            { 0x64, new BIT(0x64, 4, "H", r => r.H) },
            { 0x65, new BIT(0x65, 4, "L", r => r.L) },
            { 0x66, new BIT_HL(0x66, 4) },
            { 0x67, new BIT(0x67, 4, "A", r => r.A) },
            // bit 5
            { 0x68, new BIT(0x68, 5, "B", r => r.B) },
            { 0x69, new BIT(0x69, 5, "C", r => r.C) },
            { 0x6A, new BIT(0x6A, 5, "D", r => r.D) },
            { 0x6B, new BIT(0x6B, 5, "E", r => r.E) },
            { 0x6C, new BIT(0x6C, 5, "H", r => r.H) },
            { 0x6D, new BIT(0x6D, 5, "L", r => r.L) },
            { 0x6E, new BIT_HL(0x6E, 5) },
            { 0x6F, new BIT(0x6F, 5, "A", r => r.A) },
            // bit 6
            { 0x70, new BIT(0x70, 6, "B", r => r.B) },
            { 0x71, new BIT(0x71, 6, "C", r => r.C) },
            { 0x72, new BIT(0x72, 6, "D", r => r.D) },
            { 0x73, new BIT(0x73, 6, "E", r => r.E) },
            { 0x74, new BIT(0x74, 6, "H", r => r.H) },
            { 0x75, new BIT(0x75, 6, "L", r => r.L) },
            { 0x76, new BIT_HL(0x76, 6) },
            { 0x77, new BIT(0x77, 6, "A", r => r.A) },
            // bit 7
            { 0x78, new BIT(0x78, 7, "B", r => r.B) },
            { 0x79, new BIT(0x79, 7, "C", r => r.C) },
            { 0x7A, new BIT(0x7A, 7, "D", r => r.D) },
            { 0x7B, new BIT(0x7B, 7, "E", r => r.E) },
            { 0x7C, new BIT(0x7C, 7, "H", r => r.H) },
            { 0x7D, new BIT(0x7D, 7, "L", r => r.L) },
            { 0x7E, new BIT_HL(0x7E, 7) },
            { 0x7F, new BIT(0x7F, 7, "A", r => r.A) },
            #endregion

            #region RES
            // bit 0
            { 0x80, new RES(0x80, 0, "B", r => r.B, (r, v) => r.B = v) },
            { 0x81, new RES(0x81, 0, "C", r => r.C, (r, v) => r.C = v) },
            { 0x82, new RES(0x82, 0, "D", r => r.D, (r, v) => r.D = v) },
            { 0x83, new RES(0x83, 0, "E", r => r.E, (r, v) => r.E = v) },
            { 0x84, new RES(0x84, 0, "H", r => r.H, (r, v) => r.H = v) },
            { 0x85, new RES(0x85, 0, "L", r => r.L, (r, v) => r.L = v) },
            { 0x86, new RES_HL(0x86, 0) },
            { 0x87, new RES(0x87, 0, "A", r => r.A, (r, v) => r.A = v) },
            // bit 1
            { 0x88, new RES(0x88, 1, "B", r => r.B, (r, v) => r.B = v) },
            { 0x89, new RES(0x89, 1, "C", r => r.C, (r, v) => r.C = v) },
            { 0x8A, new RES(0x8A, 1, "D", r => r.D, (r, v) => r.D = v) },
            { 0x8B, new RES(0x8B, 1, "E", r => r.E, (r, v) => r.E = v) },
            { 0x8C, new RES(0x8C, 1, "H", r => r.H, (r, v) => r.H = v) },
            { 0x8D, new RES(0x8D, 1, "L", r => r.L, (r, v) => r.L = v) },
            { 0x8E, new RES_HL(0x8E, 1) },
            { 0x8F, new RES(0x8F, 1, "A", r => r.A, (r, v) => r.A = v) },
            // bit 2
            { 0x90, new RES(0x90, 2, "B", r => r.B, (r, v) => r.B = v) },
            { 0x91, new RES(0x91, 2, "C", r => r.C, (r, v) => r.C = v) },
            { 0x92, new RES(0x92, 2, "D", r => r.D, (r, v) => r.D = v) },
            { 0x93, new RES(0x93, 2, "E", r => r.E, (r, v) => r.E = v) },
            { 0x94, new RES(0x94, 2, "H", r => r.H, (r, v) => r.H = v) },
            { 0x95, new RES(0x95, 2, "L", r => r.L, (r, v) => r.L = v) },
            { 0x96, new RES_HL(0x96, 2) },
            { 0x97, new RES(0x97, 2, "A", r => r.A, (r, v) => r.A = v) },
            // bit 3
            { 0x98, new RES(0x98, 3, "B", r => r.B, (r, v) => r.B = v) },
            { 0x99, new RES(0x99, 3, "C", r => r.C, (r, v) => r.C = v) },
            { 0x9A, new RES(0x9A, 3, "D", r => r.D, (r, v) => r.D = v) },
            { 0x9B, new RES(0x9B, 3, "E", r => r.E, (r, v) => r.E = v) },
            { 0x9C, new RES(0x9C, 3, "H", r => r.H, (r, v) => r.H = v) },
            { 0x9D, new RES(0x9D, 3, "L", r => r.L, (r, v) => r.L = v) },
            { 0x9E, new RES_HL(0x9E, 3) },
            { 0x9F, new RES(0x9F, 3, "A", r => r.A, (r, v) => r.A = v) },
            // bit 4
            { 0xA0, new RES(0xA0, 4, "B", r => r.B, (r, v) => r.B = v) },
            { 0xA1, new RES(0xA1, 4, "C", r => r.C, (r, v) => r.C = v) },
            { 0xA2, new RES(0xA2, 4, "D", r => r.D, (r, v) => r.D = v) },
            { 0xA3, new RES(0xA3, 4, "E", r => r.E, (r, v) => r.E = v) },
            { 0xA4, new RES(0xA4, 4, "H", r => r.H, (r, v) => r.H = v) },
            { 0xA5, new RES(0xA5, 4, "L", r => r.L, (r, v) => r.L = v) },
            { 0xA6, new RES_HL(0xA6, 4) },
            { 0xA7, new RES(0xA7, 4, "A", r => r.A, (r, v) => r.A = v) },
            // bit 5
            { 0xA8, new RES(0xA8, 5, "B", r => r.B, (r, v) => r.B = v) },
            { 0xA9, new RES(0xA9, 5, "C", r => r.C, (r, v) => r.C = v) },
            { 0xAA, new RES(0xAA, 5, "D", r => r.D, (r, v) => r.D = v) },
            { 0xAB, new RES(0xAB, 5, "E", r => r.E, (r, v) => r.E = v) },
            { 0xAC, new RES(0xAC, 5, "H", r => r.H, (r, v) => r.H = v) },
            { 0xAD, new RES(0xAD, 5, "L", r => r.L, (r, v) => r.L = v) },
            { 0xAE, new RES_HL(0xAE, 5) },
            { 0xAF, new RES(0xAF, 5, "A", r => r.A, (r, v) => r.A = v) },
            // bit 6
            { 0xB0, new RES(0xB0, 6, "B", r => r.B, (r, v) => r.B = v) },
            { 0xB1, new RES(0xB1, 6, "C", r => r.C, (r, v) => r.C = v) },
            { 0xB2, new RES(0xB2, 6, "D", r => r.D, (r, v) => r.D = v) },
            { 0xB3, new RES(0xB3, 6, "E", r => r.E, (r, v) => r.E = v) },
            { 0xB4, new RES(0xB4, 6, "H", r => r.H, (r, v) => r.H = v) },
            { 0xB5, new RES(0xB5, 6, "L", r => r.L, (r, v) => r.L = v) },
            { 0xB6, new RES_HL(0xB6, 6) },
            { 0xB7, new RES(0xB7, 6, "A", r => r.A, (r, v) => r.A = v) },
            // bit 7
            { 0xB8, new RES(0xB8, 7, "B", r => r.B, (r, v) => r.B = v) },
            { 0xB9, new RES(0xB9, 7, "C", r => r.C, (r, v) => r.C = v) },
            { 0xBA, new RES(0xBA, 7, "D", r => r.D, (r, v) => r.D = v) },
            { 0xBB, new RES(0xBB, 7, "E", r => r.E, (r, v) => r.E = v) },
            { 0xBC, new RES(0xBC, 7, "H", r => r.H, (r, v) => r.H = v) },
            { 0xBD, new RES(0xBD, 7, "L", r => r.L, (r, v) => r.L = v) },
            { 0xBE, new RES_HL(0xBE, 7) },
            { 0xBF, new RES(0xBF, 7, "A", r => r.A, (r, v) => r.A = v) },
            #endregion

            #region SET
            // bit 0
            { 0xC0, new SET(0xC0, 0, "B", r => r.B, (r, v) => r.B = v) },
            { 0xC1, new SET(0xC1, 0, "C", r => r.C, (r, v) => r.C = v) },
            { 0xC2, new SET(0xC2, 0, "D", r => r.D, (r, v) => r.D = v) },
            { 0xC3, new SET(0xC3, 0, "E", r => r.E, (r, v) => r.E = v) },
            { 0xC4, new SET(0xC4, 0, "H", r => r.H, (r, v) => r.H = v) },
            { 0xC5, new SET(0xC5, 0, "L", r => r.L, (r, v) => r.L = v) },
            { 0xC6, new SET_HL(0xC6, 0) },
            { 0xC7, new SET(0xC7, 0, "A", r => r.A, (r, v) => r.A = v) },
            // bit 1
            { 0xC8, new SET(0xC8, 1, "B", r => r.B, (r, v) => r.B = v) },
            { 0xC9, new SET(0xC9, 1, "C", r => r.C, (r, v) => r.C = v) },
            { 0xCA, new SET(0xCA, 1, "D", r => r.D, (r, v) => r.D = v) },
            { 0xCB, new SET(0xCB, 1, "E", r => r.E, (r, v) => r.E = v) },
            { 0xCC, new SET(0xCC, 1, "H", r => r.H, (r, v) => r.H = v) },
            { 0xCD, new SET(0xCD, 1, "L", r => r.L, (r, v) => r.L = v) },
            { 0xCE, new SET_HL(0xCE, 1) },
            { 0xCF, new SET(0xCF, 1, "A", r => r.A, (r, v) => r.A = v) },
            // bit 2
            { 0xD0, new SET(0xD0, 2, "B", r => r.B, (r, v) => r.B = v) },
            { 0xD1, new SET(0xD1, 2, "C", r => r.C, (r, v) => r.C = v) },
            { 0xD2, new SET(0xD2, 2, "D", r => r.D, (r, v) => r.D = v) },
            { 0xD3, new SET(0xD3, 2, "E", r => r.E, (r, v) => r.E = v) },
            { 0xD4, new SET(0xD4, 2, "H", r => r.H, (r, v) => r.H = v) },
            { 0xD5, new SET(0xD5, 2, "L", r => r.L, (r, v) => r.L = v) },
            { 0xD6, new SET_HL(0xD6, 2) },
            { 0xD7, new SET(0xD7, 2, "A", r => r.A, (r, v) => r.A = v) },
            // bit 3
            { 0xD8, new SET(0xD8, 3, "B", r => r.B, (r, v) => r.B = v) },
            { 0xD9, new SET(0xD9, 3, "C", r => r.C, (r, v) => r.C = v) },
            { 0xDA, new SET(0xDA, 3, "D", r => r.D, (r, v) => r.D = v) },
            { 0xDB, new SET(0xDB, 3, "E", r => r.E, (r, v) => r.E = v) },
            { 0xDC, new SET(0xDC, 3, "H", r => r.H, (r, v) => r.H = v) },
            { 0xDD, new SET(0xDD, 3, "L", r => r.L, (r, v) => r.L = v) },
            { 0xDE, new SET_HL(0xDE, 3) },
            { 0xDF, new SET(0xDF, 3, "A", r => r.A, (r, v) => r.A = v) },
            // bit 4
            { 0xE0, new SET(0xE0, 4, "B", r => r.B, (r, v) => r.B = v) },
            { 0xE1, new SET(0xE1, 4, "C", r => r.C, (r, v) => r.C = v) },
            { 0xE2, new SET(0xE2, 4, "D", r => r.D, (r, v) => r.D = v) },
            { 0xE3, new SET(0xE3, 4, "E", r => r.E, (r, v) => r.E = v) },
            { 0xE4, new SET(0xE4, 4, "H", r => r.H, (r, v) => r.H = v) },
            { 0xE5, new SET(0xE5, 4, "L", r => r.L, (r, v) => r.L = v) },
            { 0xE6, new SET_HL(0xE6, 4) },
            { 0xE7, new SET(0xE7, 4, "A", r => r.A, (r, v) => r.A = v) },
            // bit 5
            { 0xE8, new SET(0xE8, 5, "B", r => r.B, (r, v) => r.B = v) },
            { 0xE9, new SET(0xE9, 5, "C", r => r.C, (r, v) => r.C = v) },
            { 0xEA, new SET(0xEA, 5, "D", r => r.D, (r, v) => r.D = v) },
            { 0xEB, new SET(0xEB, 5, "E", r => r.E, (r, v) => r.E = v) },
            { 0xEC, new SET(0xEC, 5, "H", r => r.H, (r, v) => r.H = v) },
            { 0xED, new SET(0xED, 5, "L", r => r.L, (r, v) => r.L = v) },
            { 0xEE, new SET_HL(0xEE, 5) },
            { 0xEF, new SET(0xEF, 5, "A", r => r.A, (r, v) => r.A = v) },
            // bit 6
            { 0xF0, new SET(0xF0, 6, "B", r => r.B, (r, v) => r.B = v) },
            { 0xF1, new SET(0xF1, 6, "C", r => r.C, (r, v) => r.C = v) },
            { 0xF2, new SET(0xF2, 6, "D", r => r.D, (r, v) => r.D = v) },
            { 0xF3, new SET(0xF3, 6, "E", r => r.E, (r, v) => r.E = v) },
            { 0xF4, new SET(0xF4, 6, "H", r => r.H, (r, v) => r.H = v) },
            { 0xF5, new SET(0xF5, 6, "L", r => r.L, (r, v) => r.L = v) },
            { 0xF6, new SET_HL(0xF6, 6) },
            { 0xF7, new SET(0xF7, 6, "A", r => r.A, (r, v) => r.A = v) },
            // bit 7
            { 0xF8, new SET(0xF8, 7, "B", r => r.B, (r, v) => r.B = v) },
            { 0xF9, new SET(0xF9, 7, "C", r => r.C, (r, v) => r.C = v) },
            { 0xFA, new SET(0xFA, 7, "D", r => r.D, (r, v) => r.D = v) },
            { 0xFB, new SET(0xFB, 7, "E", r => r.E, (r, v) => r.E = v) },
            { 0xFC, new SET(0xFC, 7, "H", r => r.H, (r, v) => r.H = v) },
            { 0xFD, new SET(0xFD, 7, "L", r => r.L, (r, v) => r.L = v) },
            { 0xFE, new SET_HL(0xFE, 7) },
            { 0xFF, new SET(0xFF, 7, "A", r => r.A, (r, v) => r.A = v) },
            #endregion
        };

        _opCodes = new Dictionary<u8, OpCode>
        {
            // 8-bit Load Instructions

            #region 8-bit load
            #region r=r
            { 0x7F, new Load_A_A() },
            { 0x78, new Load_A_B() },
            { 0x79, new Load_A_C() },
            { 0x7A, new Load_A_D() },
            { 0x7B, new Load_A_E() },
            { 0x7C, new Load_A_H() },
            { 0x7D, new Load_A_L() },
            { 0x47, new Load_B_A() },
            { 0x40, new Load_B_B() },
            { 0x41, new Load_B_C() },
            { 0x42, new Load_B_D() },
            { 0x43, new Load_B_E() },
            { 0x44, new Load_B_H() },
            { 0x45, new Load_B_L() },
            { 0x4F, new Load_C_A() },
            { 0x48, new Load_C_B() },
            { 0x49, new Load_C_C() },
            { 0x4A, new Load_C_D() },
            { 0x4B, new Load_C_E() },
            { 0x4C, new Load_C_H() },
            { 0x4D, new Load_C_L() },
            { 0x57, new Load_D_A() },
            { 0x50, new Load_D_B() },
            { 0x51, new Load_D_C() },
            { 0x52, new Load_D_D() },
            { 0x53, new Load_D_E() },
            { 0x54, new Load_D_H() },
            { 0x55, new Load_D_L() },
            { 0x5F, new Load_E_A() },
            { 0x58, new Load_E_B() },
            { 0x59, new Load_E_C() },
            { 0x5A, new Load_E_D() },
            { 0x5B, new Load_E_E() },
            { 0x5C, new Load_E_H() },
            { 0x5D, new Load_E_L() },
            { 0x67, new Load_H_A() },
            { 0x60, new Load_H_B() },
            { 0x61, new Load_H_C() },
            { 0x62, new Load_H_D() },
            { 0x63, new Load_H_E() },
            { 0x64, new Load_H_H() },
            { 0x65, new Load_H_L() },
            { 0x6F, new Load_L_A() },
            { 0x68, new Load_L_B() },
            { 0x69, new Load_L_C() },
            { 0x6A, new Load_L_D() },
            { 0x6B, new Load_L_E() },
            { 0x6C, new Load_L_H() },
            { 0x6D, new Load_L_L() },
            #endregion

            #region r=n
            { 0x3E, new Load_A_u8() },
            { 0x06, new Load_B_u8() },
            { 0x0E, new Load_C_u8() },
            { 0x16, new Load_D_u8() },
            { 0x1E, new Load_E_u8() },
            { 0x26, new Load_H_u8() },
            { 0x2E, new Load_L_u8() },
            #endregion

            #region r=(HL)
            { 0x7E, new Load_A_HL() },
            { 0x46, new Load_B_HL() },
            { 0x4E, new Load_C_HL() },
            { 0x56, new Load_D_HL() },
            { 0x5E, new Load_E_HL() },
            { 0x66, new Load_H_HL() },
            { 0x6E, new Load_L_HL() },
            #endregion

            #region (HL)=r
            { 0x77, new Load_HL_A() },
            { 0x70, new Load_HL_B() },
            { 0x71, new Load_HL_C() },
            { 0x72, new Load_HL_D() },
            { 0x73, new Load_HL_E() },
            { 0x74, new Load_HL_H() },
            { 0x75, new Load_HL_L() },
            #endregion

            #region other
            { 0x36, new Load_HL_u8() },
            { 0x0A, new Load_A_BC() },
            { 0x1A, new Load_A_DE() },
            { 0xFA, new Load_A_u16() },
            { 0x02, new Load_BC_A() },
            { 0x12, new Load_DE_A() },
            { 0xEA, new Load_u16_A() },
            { 0xF0, new Load_A_FF00_u8() },
            { 0xE0, new Load_FF00_u8_A() },
            { 0xF2, new Load_A_FF00_C() },
            { 0xE2, new Load_FF00_C_A() },
            { 0x22, new Load_HL_INC_A() },
            { 0x2A, new Load_A_HL_INC() },
            { 0x32, new Load_HL_DEC_A() },
            { 0x3A, new Load_A_HL_DEC() },
            #endregion
            #endregion

            // 16-bit Load Instructions

            #region 16-bit load
            { 0x01, new Load_BC_u16() },
            { 0x11, new Load_DE_u16() },
            { 0x21, new Load_HL_u16() },
            { 0x31, new Load_SP_u16() },
            { 0x08, new Load_u16_SP() },
            { 0xF9, new Load_SP_HL() },
            { 0xF8, new Load_HL_SP_s8() },
            { 0xC5, new Push_BC() },
            { 0xD5, new Push_DE() },
            { 0xE5, new Push_HL() },
            { 0xF5, new Push_AF() },
            { 0xC1, new Pop_BC() },
            { 0xD1, new Pop_DE() },
            { 0xE1, new Pop_HL() },
            { 0xF1, new Pop_AF() },
            #endregion

            // CPU Control Instructions

            #region CPU Control
            { 0x3F, new CCF() },
            { 0x37, new SCF() },
            { 0x00, new NOP() },
            { 0x76, new Halt() },
            { 0x10, new Stop() },
            { 0xF3, new DisableInterrupts() },
            { 0xFB, new EnableInterrupts() },
            #endregion

            // 8-bit Arithmetic / Logic Instructions

            #region 8-bit Arithmetic/Logic
            #region ADD
            { 0x87, new Add_A_A() },
            { 0x80, new Add_A_B() },
            { 0x81, new Add_A_C() },
            { 0x82, new Add_A_D() },
            { 0x83, new Add_A_E() },
            { 0x84, new Add_A_H() },
            { 0x85, new Add_A_L() },
            { 0x86, new Add_A_HL() },
            { 0xC6, new Add_A_u8() },
            #endregion

            #region ADD with Carry
            { 0x8F, new AddWithCarry_A_A() },
            { 0x88, new AddWithCarry_A_B() },
            { 0x89, new AddWithCarry_A_C() },
            { 0x8A, new AddWithCarry_A_D() },
            { 0x8B, new AddWithCarry_A_E() },
            { 0x8C, new AddWithCarry_A_H() },
            { 0x8D, new AddWithCarry_A_L() },
            { 0x8E, new AddWithCarry_A_HL() },
            { 0xCE, new AddWithCarry_A_u8() },
            #endregion

            #region SUB
            { 0x97, new Sub_A_A() },
            { 0x90, new Sub_A_B() },
            { 0x91, new Sub_A_C() },
            { 0x92, new Sub_A_D() },
            { 0x93, new Sub_A_E() },
            { 0x94, new Sub_A_H() },
            { 0x95, new Sub_A_L() },
            { 0x96, new Sub_A_HL() },
            { 0xD6, new Sub_A_u8() },
            #endregion

            #region SUB with Carry
            { 0x9F, new SubWithCarry_A_A() },
            { 0x98, new SubWithCarry_A_B() },
            { 0x99, new SubWithCarry_A_C() },
            { 0x9A, new SubWithCarry_A_D() },
            { 0x9B, new SubWithCarry_A_E() },
            { 0x9C, new SubWithCarry_A_H() },
            { 0x9D, new SubWithCarry_A_L() },
            { 0x9E, new SubWithCarry_A_HL() },
            { 0xDE, new SubWithCarry_A_u8() },
            #endregion

            #region AND
            { 0xA7, new And_A_A() },
            { 0xA0, new And_A_B() },
            { 0xA1, new And_A_C() },
            { 0xA2, new And_A_D() },
            { 0xA3, new And_A_E() },
            { 0xA4, new And_A_H() },
            { 0xA5, new And_A_L() },
            { 0xA6, new And_A_HL() },
            { 0xE6, new And_A_u8() },
            #endregion

            #region XOR
            { 0xAF, new Xor_A_A() },
            { 0xA8, new Xor_A_B() },
            { 0xA9, new Xor_A_C() },
            { 0xAA, new Xor_A_D() },
            { 0xAB, new Xor_A_E() },
            { 0xAC, new Xor_A_H() },
            { 0xAD, new Xor_A_L() },
            { 0xAE, new Xor_A_HL() },
            { 0xEE, new Xor_A_u8() },
            #endregion

            #region OR
            { 0xB7, new Or_A_A() },
            { 0xB0, new Or_A_B() },
            { 0xB1, new Or_A_C() },
            { 0xB2, new Or_A_D() },
            { 0xB3, new Or_A_E() },
            { 0xB4, new Or_A_H() },
            { 0xB5, new Or_A_L() },
            { 0xB6, new Or_A_HL() },
            { 0xF6, new Or_A_u8() },
            #endregion

            #region CP
            { 0xBF, new Compare_A_A() },
            { 0xB8, new Compare_A_B() },
            { 0xB9, new Compare_A_C() },
            { 0xBA, new Compare_A_D() },
            { 0xBB, new Compare_A_E() },
            { 0xBC, new Compare_A_H() },
            { 0xBD, new Compare_A_L() },
            { 0xBE, new Compare_A_HL() },
            { 0xFE, new Compare_A_u8() },
            #endregion

            #region INC
            { 0x3C, new Inc_A() },
            { 0x04, new Inc_B() },
            { 0x0C, new Inc_C() },
            { 0x14, new Inc_D() },
            { 0x1C, new Inc_E() },
            { 0x24, new Inc_H() },
            { 0x2C, new Inc_L() },
            { 0x34, new Arithmetic8Bit.Inc.Inc_HL() },
            #endregion

            #region DEC
            { 0x3D, new Dec_A() },
            { 0x05, new Dec_B() },
            { 0x0D, new Dec_C() },
            { 0x15, new Dec_D() },
            { 0x1D, new Dec_E() },
            { 0x25, new Dec_H() },
            { 0x2D, new Dec_L() },
            { 0x35, new Arithmetic8Bit.Dec.Dec_HL() },
            #endregion

            { 0x27, new DAA() },
            { 0x2F, new CPL() },
            #endregion

            // Rotate and Shift Instructions

            #region Rotate/Shift
            { 0x07, new RLCA() },
            { 0x17, new RLA() },
            { 0x0F, new RRCA() },
            { 0x1F, new RRA() },
            #endregion

            // 16-bit Arithmetic Instructions

            #region 16-bit Arithmetic
            { 0x03, new Inc_BC() },
            { 0x13, new Inc_DE() },
            { 0x23, new Arithmetic16Bit.Inc.Inc_HL() },
            { 0x33, new Inc_SP() },
            { 0x0B, new Dec_BC() },
            { 0x1B, new Dec_DE() },
            { 0x2B, new Arithmetic16Bit.Dec.Dec_HL() },
            { 0x3B, new Dec_SP() },
            { 0x09, new Add_HL_BC() },
            { 0x19, new Add_HL_DE() },
            { 0x29, new Add_HL_HL() },
            { 0x39, new Add_HL_SP() },
            { 0xE8, new Add_SP_s8() },
            #endregion

            { 0xC3, new JP_u16() },
            { 0xC2, new JP_NZ_u16() },
            { 0xCA, new JP_Z_u16() },
            { 0xD2, new JP_NC_u16() },
            { 0xDA, new JP_C_u16() },
            { 0xE9, new JP_HL() },
            { 0x18, new JR_s8() },
            { 0x20, new JR_NZ_s8() },
            { 0x30, new JR_NC_s8() },
            { 0x28, new JR_Z_s8() },
            { 0x38, new JR_C_s8() },
            { 0xCD, new CALL_u16() },
            { 0xC4, new CALL_NZ_u16() },
            { 0xCC, new CALL_Z_u16() },
            { 0xD4, new CALL_NC_u16() },
            { 0xDC, new CALL_C_u16() },
            { 0xC9, new RET() },
            { 0xD9, new RETI() },
            { 0xC0, new RET_NZ() },
            { 0xC8, new RET_Z() },
            { 0xD0, new RET_NC() },
            { 0xD8, new RET_C() },
            { 0xC7, new RST(0xC7, 0x00) },
            { 0xCF, new RST(0xCF, 0x08) },
            { 0xD7, new RST(0xD7, 0x10) },
            { 0xDF, new RST(0xDF, 0x18) },
            { 0xE7, new RST(0xE7, 0x20) },
            { 0xEF, new RST(0xEF, 0x28) },
            { 0xF7, new RST(0xF7, 0x30) },
            { 0xFF, new RST(0xFF, 0x38) }
        };
    }

    internal static void And(Registers r, u8 value)
    {
        var result = r.A & value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = true;
        r.FlagC = false;
        r.A = (u8)result;
    }

    internal static void Xor(Registers r, u8 value)
    {
        var result = r.A ^ value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = false;
        r.FlagC = false;
        r.A = (u8)result;
    }

    internal static void Or(Registers r, u8 value)
    {
        var result = r.A | value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = false;
        r.FlagC = false;
        r.A = (u8)result;
    }

    internal static void Compare(Registers r, u8 value)
    {
        var result = r.A - value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
    }

    internal static void Sub(Registers r, u8 value)
    {
        var result = r.A - value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
        r.A = (u8)result;
    }

    internal static void SubWithCarry(Registers r, u8 value)
    {
        var c = r.FlagC ? 1 : 0;
        var result = r.A - value - c;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) + c > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
        r.A = (u8)result;
    }

    internal static void Add(Registers r, u8 value)
    {
        var result = r.A + value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = false;
        r.FlagH = (r.A & 0b_0000_1111) + (value & 0b_0000_1111) > 0b_0000_1111;
        r.FlagC = result > 0b_1111_1111;
        r.A = (u8)result;
    }

    internal static u8 Rlc(Registers r, u8 value)
    {
        var carry = (value & 0b_1000_0000) != 0;
        var result = (u8)((value << 1) | (carry ? 1 : 0));
        SetRotateShiftFlags(r, result, carry);
        return result;
    }

    internal static u8 Rrc(Registers r, u8 value)
    {
        var carry = (value & 0b_0000_0001) != 0;
        var result = (u8)((value >> 1) | (carry ? 0b_1000_0000 : 0));
        SetRotateShiftFlags(r, result, carry);
        return result;
    }

    internal static u8 Rl(Registers r, u8 value)
    {
        var carry = (value & 0b_1000_0000) != 0;
        var result = (u8)((value << 1) | (r.FlagC ? 1 : 0));
        SetRotateShiftFlags(r, result, carry);
        return result;
    }

    internal static u8 Rr(Registers r, u8 value)
    {
        var carry = (value & 0b_0000_0001) != 0;
        var result = (u8)((value >> 1) | (r.FlagC ? 0b_1000_0000 : 0));
        SetRotateShiftFlags(r, result, carry);
        return result;
    }

    internal static u8 Sla(Registers r, u8 value)
    {
        var carry = (value & 0b_1000_0000) != 0;
        var result = (u8)(value << 1);
        SetRotateShiftFlags(r, result, carry);
        return result;
    }

    internal static u8 Sra(Registers r, u8 value)
    {
        var carry = (value & 0b_0000_0001) != 0;
        var result = (u8)((value >> 1) | (value & 0b_1000_0000));
        SetRotateShiftFlags(r, result, carry);
        return result;
    }

    internal static u8 Swap(Registers r, u8 value)
    {
        var result = (u8)((value << 4) | (value >> 4));
        SetRotateShiftFlags(r, result, false);
        return result;
    }

    internal static u8 Srl(Registers r, u8 value)
    {
        var carry = (value & 0b_0000_0001) != 0;
        var result = (u8)(value >> 1);
        SetRotateShiftFlags(r, result, carry);
        return result;
    }

    private static void SetRotateShiftFlags(Registers r, u8 result, bool carry)
    {
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = false;
        r.FlagC = carry;
    }

    internal static void TestBit(Registers r, int bit, u8 value)
    {
        r.FlagZ = (value & (1 << bit)) == 0;
        r.FlagN = false;
        r.FlagH = true;
    }

    internal static void AddToHl(Registers r, ushort value)
    {
        var result = r.HL + value;
        r.FlagN = false;
        r.FlagH = (r.HL & 0b_0000_1111_1111_1111) + (value & 0b_0000_1111_1111_1111) > 0b_0000_1111_1111_1111;
        r.FlagC = result > 0b_1111_1111_1111_1111;
        r.HL = (ushort)result;
    }

    internal static ushort AddSignedByteToSp(Registers r)
    {
        var value = (u8)r.SubOp_SignedByte;
        r.FlagZ = false;
        r.FlagN = false;
        r.FlagH = (r.SP & 0b_0000_1111) + (value & 0b_0000_1111) > 0b_0000_1111;
        r.FlagC = (r.SP & 0b_1111_1111) + value > 0b_1111_1111;
        return (ushort)(r.SP + r.SubOp_SignedByte);
    }

    internal static void AddWithCarry(Registers r, u8 value)
    {
        var c = r.FlagC ? 1 : 0;
        var result = r.A + value + c;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = false;
        r.FlagH = (r.A & 0b_0000_1111) + (value & 0b_0000_1111) + c > 0b_0000_1111;
        r.FlagC = result > 0b_1111_1111;
        r.A = (u8)result;
    }

    public OpCode Lookup(u8 value)
    {
        if (!_opCodes.TryGetValue(value, out var lookup))
        {
            throw new NotImplementedException($"Unknown Opcode: 0x{value:X2}");
        }

        return lookup;
    }

    public OpCode LookupCb(u8 value)
    {
        if (!_cbOpCodes.TryGetValue(value, out var lookup))
        {
            throw new NotImplementedException($"Unknown CB-prefixed Opcode: 0xCB 0x{value:X2}");
        }

        return lookup;
    }
}
