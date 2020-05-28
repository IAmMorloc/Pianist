using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Pianist
{
    public static class PitchHelper
    {
        public static string GetPitchSoundsFileName(int soundCode)
        {
            return GetPitchSoundsFileName(DecodeTone(soundCode), DecodeOctave(soundCode), DecodePitchName(soundCode));
        }

        public static string GetPitchSoundsFileName(int tone, int octave, int pitchName)
        {
            if (!octave.Equals(EPitchType.Octave4))
            {
                return "Sounds/Custom/" + tone + "_" + pitchName + "-4";
            }
            return tone + "_" + pitchName;
        }

        public static void PlaySound(Mod mod, int soundCode)
        {
            PlaySound(mod, soundCode, -1, -1);
        }

        public static void PlaySound(Mod mod, int soundCode, int x, int y)
        {
            float offset = 0f;
            switch (DecodeOctave(soundCode))
            {
                case 3:
                    offset = -1f; break;
                case 4:
                    offset = 0f; break;
                case 5:
                    offset = 1f; break;
            }
            Main.PlaySound(SoundLoader.customSoundType, x, y, mod.GetSoundSlot(SoundType.Custom, GetPitchSoundsFileName(soundCode)), 0.4f, offset);
        }

        public static int EncodeSoundCode(int tone, int octave, int pitchName)
        {
            return (tone << 8) | (octave << 4) | pitchName;
        }
        public static int DecodePitchName(int soundCode)
        {
            return soundCode & 15;
        }
        public static int DecodeOctave(int soundCode)
        {
            return (soundCode >> 4) & 15;
        }
        public static int DecodeTone(int soundCode)
        {
            return (soundCode >> 8) & 15;
        }


    }

    enum EPitchType
    {
        C = 1,
        C_ = 2,
        D = 3,
        D_ = 4,
        E = 5,
        F = 6,
        F_ = 7,
        G = 8,
        G_ = 9,
        A = 10,
        A_ = 11,
        B = 12,

        Octave3 = 3,
        Octave4 = 4,
        Octave5 = 5,

        Piano = 1,
    }
}
