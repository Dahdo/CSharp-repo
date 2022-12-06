using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace EN_Lab_09 {
    #region step1
    public interface ISequence : IEnumerable {
        public string Description();
    }

    public class Repeat : ISequence {
        private int number { get; set; }
        private int frequency { get; set; }

        public Repeat(int num, int freq) {
            number = num;
            frequency = freq;
}
        public string Description() {
            return $"Repeat({number}, {frequency})";
        }

        public IEnumerator GetEnumerator() {
            for (int i = 0; i < frequency; i++)
                yield return number;
        }
    }

    public class ArithmeticProgression : ISequence {
        private int initial { get; set; }
        private int step { get; set; }
        public ArithmeticProgression(int initial, int step = 1) {
            this.initial = initial;
            this.step = step;
        }
        public string Description() {
            return $"ArithmeticProgression({initial}, {step})";
        }

     public IEnumerator GetEnumerator() {
            yield return initial;
            while (true)
                yield return initial += step;
        }
    }

    public class GeometricProgression : ISequence {
        protected int initial { get; set; }
        protected int step { get; set; }
        public GeometricProgression(int initial, int step) {
            this.initial = initial;
            this.step = step;
        }
        public virtual string Description() {
            return $"GeometricProgression({initial}, {step})";
        }

        public virtual IEnumerator GetEnumerator() {
            yield return initial;
            while (true)
                yield return initial *= step;
        }
    }
    
    public class PowersOf : GeometricProgression {
        public PowersOf(int value) : base(value, 1){}
        public override string Description() {
            return $"PowersOf({initial})";
        }

        public override IEnumerator GetEnumerator() {
            for (int i = 0; ; i++)
                yield return Math.Pow(initial, i);
        }
    }
    #endregion step1

    #region step2

    #endregion step2
}