using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaghettoOS.Resources {
    public static class ResourceManager {
        [ManifestResourceStream(ResourceName = "spaghettoOS.Resources.inconsolata.regular.ttf")] 
        public static byte[] defaultFont;

        [ManifestResourceStream(ResourceName = "spaghettoOS.Resources.Background.bmp")]
        private static byte[] backgroundImage;
        public static Image BackgroundImage { get; set; }

        [ManifestResourceStream(ResourceName = "spaghettoOS.Resources.Cursor.bmp")]
        private static byte[] cursorImage;
        public static Image CursorImage { get; set; }

        [ManifestResourceStream(ResourceName = "spaghettoOS.Resources.GenericIcon.bmp")]
        private static byte[] genericIcon;
        public static Image GenericIcon { get; set; }

        public static void Init() {
            BackgroundImage = new Bitmap(backgroundImage);
            CursorImage = new Bitmap(cursorImage);
            GenericIcon = new Bitmap(genericIcon);
        }
    }
}
