using spaghettoOS.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaghettoOS.Applications {
    public interface IApp {
        public abstract string Name { get; }

        public virtual void OnStart(params object[] args) {

        } 

        public virtual void OnExit() {

        }

        public virtual void OnKeyDown(Form focusedForm, int key) { }
        public virtual void OnKeyUp(Form focusedForm, int key) { }
        public virtual void OnKeyPressed(Form focusedForm, int key) { }


        public abstract void Update();
    }
}
