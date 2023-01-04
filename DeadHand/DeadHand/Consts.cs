using System;
using System.Threading;

namespace DeadHand
{
    public static class Consts
    {
        public static Note[] NewEmailSound =
        {
            new Note(Tone.B, Duration.QUARTER),
            new Note(Tone.A, Duration.QUARTER),
            new Note(Tone.GbelowC, Duration.QUARTER),
            new Note(Tone.A, Duration.QUARTER),
            new Note(Tone.B, Duration.QUARTER),
            new Note(Tone.B, Duration.QUARTER),
            new Note(Tone.B, Duration.HALF)
        };

        public static Note[] OSStartSound =
{
            new Note(Tone.F, Duration.EIGHTH),
            new Note(Tone.C, Duration.EIGHTH),
            new Note(Tone.G, Duration.HALF)
        };

        public static int TimerMinute = 60 * 1000;
        public static void PlayMelody(Note[] tune)
        {
            foreach (Note n in tune)
            {
                if (n.NoteTone == Tone.REST)
                    Thread.Sleep((int)n.NoteDuration);
                else
                    Console.Beep((int)n.NoteTone, (int)n.NoteDuration);
            }
        }

        public enum Tone
        {
            REST = 0,
            GbelowC = 196,
            A = 220,
            Asharp = 233,
            B = 247,
            C = 262,
            Csharp = 277,
            D = 294,
            Dsharp = 311,
            E = 330,
            F = 349,
            Fsharp = 370,
            G = 392,
            Gsharp = 415,
        }

        public enum Duration
        {
            WHOLE = 1600,
            HALF = WHOLE / 2,
            QUARTER = HALF / 2,
            EIGHTH = QUARTER / 2,
            SIXTEENTH = EIGHTH / 2,
        }

        public struct Note
        {
            Tone toneVal;
            Duration durVal;

            public Note(Tone frequency, Duration time)
            {
                toneVal = frequency;
                durVal = time;
            }

            public Tone NoteTone { get { return toneVal; } }
            public Duration NoteDuration { get { return durVal; } }
        }
    }

}

