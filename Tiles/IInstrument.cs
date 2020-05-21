using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pianist.Tiles
{
    interface IInstrument
    {
        //Also implemented for modulation.
        bool PitchOffset(int i, int j);

        int GetSoundCode(int i, int j);
    }

}
