using Wonderland.Chip8;

var emulator = new Emulator();
emulator.Load(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/games/chip-8/IBM Logo.ch8");
emulator.Run();
