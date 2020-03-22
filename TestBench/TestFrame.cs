//using System;
//using System.Collections.Generic;
//using System.Text;
using Newtonsoft.Json;

namespace TestBench
{

    class TestFrameJsonWrapper
    {
        public string[] seed;
        public string[] expectation;
        public int distance;
        public int period;
        public int dim_x;
        public int dim_y;
    }


    /// <summary>
    ///  loads tests from disc in json format to our TestFrame objects. 
    /// </summary>
    public class TestFrame
    {
        public readonly string seed;
        public readonly string expectation;
        public readonly int distance;
        public readonly int period;
        public readonly int dim_x;
        public readonly int dim_y;

        //enum Type : byte { Equals = 0, Contains = 1, StillLife = 2}
        //Type type = Type.Equals;

        public TestFrame(string data)
        {
            TestFrameJsonWrapper tfjw = JsonConvert.DeserializeObject<TestFrameJsonWrapper>(data);
            foreach (string s in tfjw.seed) seed += s + "\n";
            foreach (string s in tfjw.expectation) expectation += s + "\n";
            distance = tfjw.distance;
            period = tfjw.period;
            dim_x = tfjw.dim_x;
            dim_y = tfjw.dim_y;
        }

        public string getSeed() => seed;
        public string getExpectation() => expectation;
        public int getDistance() => distance;
        public int getDimX() => dim_x;
        public int getDimY() => dim_y;
    }
}
