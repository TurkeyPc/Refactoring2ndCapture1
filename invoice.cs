using System.Collections.Generic;
using System.Linq;

namespace Refactoring2ndCapture1 {
    public class invoice {
        public string customer { get; set; }
        public IEnumerable<performance> performances { get; set; }
        public override string ToString() {
            return this.customer + "::" + string.Join(":", this.performances.Select(_ => _.ToString()));
        }

        public class performance {
            public string playID { get; set; }
            public int audience { get; set; }

            public override string ToString() {
                return this.playID + ":" + this.audience.ToString();
            }
        }
    }
}
