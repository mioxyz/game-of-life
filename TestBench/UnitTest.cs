/*
This file is part of "mio's Game Of Life".

"mio's Game Of Life" is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

"mio's Game Of Life" is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with "mio's Game Of Life".  If not, see<https://www.gnu.org/licenses/>
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;
using Moq;

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

    public class DatabaseEngineTests
    {
        private Mock<DatabaseInterface> buildMockDatabase()
        {
           
            // build response based on the mockedDatabase call, get data locally
            var response = new CouchResponse<Board>(); 
            response.docs.Add(new Board(10, 10, WorkingDirectory.ReadFile("data/board/main.mock.board")));

            // create a Mock instance of Database
            var mockDatabase = new Mock<DatabaseInterface>();

            // setup mock function "get" such that it always returns a board loaded with main.mock.board
            mockDatabase.Setup( db => db.get(It.IsAny<Board>())).Returns(response);

            // setup mock function get
            mockDatabase.Setup(db => db.getDatabaseInfo()).Returns("mock database info");


            return mockDatabase;
        }


        [TestMethod] public void Blinker() {

            var mockDatabase = buildMockDatabase();

            // normally we pass mocked object to another object, it makes little sense
            // building a mock object and then testing the return value of the mock whatever.

            Assert.IsTrue(mockDatabase.Object.getDatabaseInfo() == "mock database info");


        }

    }

}
