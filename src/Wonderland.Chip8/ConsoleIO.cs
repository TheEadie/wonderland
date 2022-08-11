﻿namespace Wonderland.Chip8;

public class ConsoleIO
{
    public bool[] Keys { get; } = new bool[0xF];
    public bool Pause { get; private set; }
    public bool StepForward { get; private set; }

    private readonly Dictionary<ConsoleKey, byte> _lookup;

    public ConsoleIO()
    {
        _lookup = new Dictionary<ConsoleKey, byte> {
            {ConsoleKey.D1, 0x1},
            {ConsoleKey.D2, 0x2},
            {ConsoleKey.D3, 0x3},
            {ConsoleKey.D4, 0xC},
            {ConsoleKey.Q, 0x4},
            {ConsoleKey.W, 0x5},
            {ConsoleKey.E, 0x6},
            {ConsoleKey.R, 0xD},
            {ConsoleKey.A, 0x7},
            {ConsoleKey.S, 0x8},
            {ConsoleKey.D, 0x9},
            {ConsoleKey.F, 0xE},
            {ConsoleKey.Z, 0xA},
            {ConsoleKey.X, 0x0},
            {ConsoleKey.C, 0xB},
            {ConsoleKey.V, 0xF}
        };
    }

    public void Step()
    {
        if (!Console.KeyAvailable)
        {
            Pause = false;
            StepForward = false;

            for (int i = 0; i < 0xF; i++)
            {
                Keys[i] = false;
            }

            return;
        }

        var key = Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.P:
                Pause = true;
                break;
            case ConsoleKey.N:
                StepForward = true;
                break;
            default:
                if (_lookup.ContainsKey(key))
                {
                    Keys[_lookup[key]] = true;
                }
                break;
        }
    }
}