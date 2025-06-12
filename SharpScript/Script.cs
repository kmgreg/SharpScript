using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpScript
{
    internal class Script
    {
        private List<Step> Steps { get; set; }
        private Dictionary<string, float> DataVars { get; set; }

        public Script(List<string> lines)
        {
            this.DataVars = new Dictionary<string, float>();
            this.Steps = new List<Step>();

            foreach (string line in lines)
            {

            }
        }

        public void RunSteps()
        {

        }
    }
}
