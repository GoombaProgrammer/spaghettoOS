using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaghettoOS.Forms {
    public interface IFormElement {
        public abstract string ID { get; set; }
        public abstract Form Form { get; set; }
        public abstract void Render(Canvas cv, Form form);
    }
}
