using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;

namespace TestBench
{
    [TestClass]
    public class UnitTest
    {

        void assertStillLife(string name)
        {
            TestFrame tf = new TestFrame(WorkingDirectory.ReadFile("data/test/" + name + ".json"));

            Engine seed = new Engine(tf.getDimX(), tf.getDimY(), tf.getSeed(), "seed");

            Engine expectation = new Engine(tf.getDimX(), tf.getDimY(), tf.getExpectation(), "expectation");
            expectation.getBoard().swapBuffers();

            seed.step(tf.getDistance());
            seed.getBoard().swapBuffers();

            string asd = seed.getBoard().toString();
            asd = seed.getBoard().toString();
            string asd32 = expectation.getBoard().toString();

            Assert.IsTrue(seed.getBoard() == expectation.getBoard());
        }

        /// <summary> We test the "oscillator" Blinker pattern (period 2).</summary>
        [TestMethod] public void Blinker() => assertStillLife("blinker");

        /// <summary> We test the Block "still life" pattern.</summary>        
        [TestMethod] public void Block() => assertStillLife("block");

        /// <summary> We test the Beehive "still life" pattern.</summary>
        [TestMethod] public void Beehive() => assertStillLife("beehive");

        /// <summary> We test the Boat"still life" pattern.</summary>
        [TestMethod] public void Boat() => assertStillLife("boat");

        /// <summary> We test the Loaf "still life" pattern.</summary>
        [TestMethod] public void Loaf() => assertStillLife("loaf");

        /// <summary> We test the Tub "still life" pattern.</summary>
        [TestMethod] public void Tub() => assertStillLife("tub");

    }
}
