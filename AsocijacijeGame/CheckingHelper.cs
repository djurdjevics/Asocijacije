using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsocijacijeGame
{
    public static class CheckingHelper
    {
        public static bool CanSubmitSolve(List<Button> buttons)
        {
            bool result = false;
            foreach(Button btn in buttons)
            {
                if (btn.Enabled == false) result = true;
            }
            return result;
        }
    }
}
