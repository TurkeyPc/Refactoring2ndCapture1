namespace Refactoring2ndCapture1 {
    public class play {
        public string name { get; set; }
        public string type { get; set; }

        public override string ToString() {
            return this.name + ":" + this.type;
        }
    }
}
