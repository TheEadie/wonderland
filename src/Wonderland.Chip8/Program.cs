using Wonderland.Chip8;

var emulator = new Emulator();
emulator.Load(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/games/chip-8/BC_test.ch8");
emulator.Run();
