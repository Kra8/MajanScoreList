using NUnit.Framework;
using System;
using MajanScoreList;

namespace MajanScoreListTests
{
    [TestFixture()]
    public class Test
    {
        [TestCase(30, 1, 1500)]
        [TestCase(40, 4, 12000)]
        public void TestScore(int hu, int han, int answer)
        {
            var score = new Score(ScoreType.Parent, hu, han);

            Assert.True(score.Ron == answer);
        }
    }
}
