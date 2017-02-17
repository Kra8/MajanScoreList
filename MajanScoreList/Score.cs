using System;

namespace MajanScoreList
{
    /// <summary>
    /// 麻雀の点数計算を行うクラス
    /// </summary>
    public class Score
    {
        /// <summary>
        /// 点数のタイプ(親/子).
        /// </summary>
        public readonly ScoreType Type;

        /// <summary>
        /// 符数.
        /// </summary>
        public readonly int Hu;

        /// <summary>
        /// ハン数.
        /// </summary>
        public readonly int Han;

        /// <summary>
        /// ロンしたときの点数.
        /// </summary>
        public readonly int Ron;

        /// <summary>
        /// ツモしたときの子供が支払う点数.
        /// </summary>
        public readonly int TumoChild;

        /// <summary>
        /// ツモしたときの親が支払う点数.
        /// Typeが親の場合Nullとなる.
        /// </summary>
        public readonly int? TumoParent;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MajanScoreList.Score"/> class.
        /// </summary>
        /// <param name="type">点数のタイプ(親/子).</param>
        /// <param name="hu">符数.</param>
        /// <param name="han">ハン数</param>
        public Score(ScoreType type, int hu, int han)
        {
            this.Type = type;
            this.Hu = hu;
            this.Han = han;

            this.Ron = Score.Calc(type, hu, han);

            if (Type.Equals(ScoreType.Child))
            {
                this.TumoParent = Score.Ceiling(this.Ron / 2);
                this.TumoChild = Score.Ceiling(this.Ron / 4);
            }
            else
            {
                this.TumoChild = Score.Ceiling(this.Ron / 3);
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:MajanScoreList.Score"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:MajanScoreList.Score"/>.</returns>
        public override String ToString()
        {
            String ron  = "-";
            String tumo = "";

            // 通常時
            ron = "" + this.Ron;
            tumo = "(" + this.TumoChild + "/" + this.TumoParent + ")";

            // 親の場合のツモ表記
            if (this.Type.Equals(ScoreType.Parent))
            {
                tumo = "(" + this.TumoChild + ")";
            }

            // 20符の特殊表記
            if (this.Hu == 20)
            {
                ron = "-";

                if (this.Han == 1) 
                {
                    tumo = ""; 
                }
                if (this.Han != 1 && this.Type.Equals(ScoreType.Child))
                {
                    tumo = "(" + this.TumoChild + "/" + this.TumoParent + ")";
                }
                if (this.Han != 1 && this.Type.Equals(ScoreType.Parent))
                {
                    tumo = "(" + this.TumoChild + ")";
                }
            }

            // 25符の子の特殊表記
            if (this.Hu == 25 && this.Type.Equals(ScoreType.Child))
            {
                if (this.Han == 1)
                {
                    ron = "-";
                    tumo = "";
                }
                else if (this.Han == 2)
                {
                    ron = "" + this.Ron;
                    tumo = "-/-";
                }
                else
                {
                    ron = "" + this.Ron;
                    tumo = "(" + this.TumoChild + "/" + this.TumoParent + ")";
                }
            }

            // 25符の親の特殊表記
            if (this.Hu == 25 && this.Type.Equals(ScoreType.Parent))
            {
                if (this.Han == 1)
                {
                    ron = "-";
                    tumo = "";
                }
                else if (this.Han == 2)
                {
                    ron = "" + this.Ron;
                    tumo = "-";
                }
                else
                {
                    ron = "" + this.Ron;
                    tumo = "(" + this.TumoChild + ")";
                }
            }

            // normarize
            String text = (tumo == "") ? ron : ron + "\n" + tumo;

            return text;
        }

        /// <summary>
        /// Gets the scores.
        /// </summary>
        /// <returns>The scores.</returns>
        /// <param name="type">Type.</param>
        public static Score[,] GetScores(ScoreType type)
        {
            Score[,] scores = new Score[11, 4];

            // 20符
            for (int i = 0; i < 4; i++)
            {
                scores[0, i] = new Score(type, 20, i + 1);
            }

            // 25符
            for (int i = 0; i < 4; i++)
            {
                scores[1, i] = new Score(type, 25, i + 1);
            }

            // 30符以上
            for (int i = 2; i < 11; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    scores[i, j] = new Score(type, (i + 1) * 10, j + 1);
                }
            }

            return scores;
        }

        /// <summary>
        /// ロンした時の点数を返す
        /// </summary>
        /// <returns>The calculate.</returns>
        /// <param name="type">点数のタイプ(親/子).</param>
        /// <param name="hu">符数.</param>
        /// <param name="han">ハン数</param>
        static int Calc(ScoreType type, int hu, int han)
        {
            int ready = 0;

            if (han > 0 || han < 5)
            {
                ready = hu * (int)Math.Pow(2, han + 2);
                ready = (ready < 2000) ? ready : 2000;
            }
            // 満貫
            else if (han == 5)
            {
                ready = 2000;
            }
            // 跳満
            else if (han == 6 || han == 7)
            {
                ready = 3000;
            }
            // 倍満
            else if (han == 8 || han == 9 || han == 10)
            {
                ready = 4000;
            }
            // 三倍満
            else if (han == 11 || han == 12)
            {
                ready = 6000; 
            }
            // 役満
            else if (han > 12)
            {
                ready = 8000;   
            }

            return Score.Ceiling((int)type * ready);
        }

        /// <summary>
        /// Ceiling the specified number.
        /// </summary>
        /// <returns>The ceiling.</returns>
        /// <param name="number">Number.</param>
        static int Ceiling(int number)
        {
            return (int)(Math.Ceiling(number / 100.0) * 100);
        }
    }
}
