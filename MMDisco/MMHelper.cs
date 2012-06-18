using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMDisco
{
    class MMTrack 
    {
        SongsDB.SDBSongData songRef;

        public MMTrack(SongsDB.SDBSongData s)
        {
            songRef = s;
        }

        public override string ToString()
        {
            return songRef.Title;
        }
    }

    class MMHelper
    {
    }
}
