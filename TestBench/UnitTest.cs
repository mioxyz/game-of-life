﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;

namespace TestBench
{
    [TestClass]
    public class UnitTest
    {

        void test(string name)
        {
            TestFrame tf = new TestFrame(WorkingDirectory.ReadFile("data/test/" + name + ".json"));

            Engine seed        = new Engine(tf.getDimX(), tf.getDimY(), tf.getSeed());
            Engine expectation = new Engine(tf.getDimX(), tf.getDimY(), tf.getExpectation());

            // we need to swap buffers since Board.getCell() method reads from adjacent buffer and not
            // the buffer we are currently in. Might have to change that.
            expectation.getBoard().swapBuffers();

            seed.step(tf.getDistance());

            //analogous reason to expect.swapB
            seed.getBoard().swapBuffers();


            Assert.IsTrue(seed.getBoard() == expectation.getBoard());
        }

        /// <summary> We test the "oscillator" Blinker pattern (period 2).</summary>
        [TestMethod] public void Blinker() => test("blinker");
        /// <summary> We test the "oscillator" Pulsar pattern (period 2).</summary>
        [TestMethod] public void Pulsar() => test("pulsar");

        /// <summary> We test the "oscillator" Toad pattern (period 2).</summary>
        [TestMethod] public void Toad() => test("toad");

        /// <summary> We test the Block "still life" pattern.</summary>        
        [TestMethod] public void Block() => test("block");

        /// <summary> We test the Beehive "still life" pattern.</summary>
        [TestMethod] public void Beehive() => test("beehive");

        /// <summary> We test the Boat"still life" pattern.</summary>
        [TestMethod] public void Boat() => test("boat");

        /// <summary> We test the Loaf "still life" pattern.</summary>
        [TestMethod] public void Loaf() => test("loaf");

        /// <summary> We test the Tub "still life" pattern.</summary>
        [TestMethod] public void Tub() => test("tub");

    }
}
