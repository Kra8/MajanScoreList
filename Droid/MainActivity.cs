using Android.App;
using Android.Widget;
using Android.OS;

namespace MajanScoreList.Droid
{
    [Activity(Label = "MajanScoreList", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        Score[,] ParentScores = Score.GetScores(ScoreType.Parent);
        Score[,] ChildScores = Score.GetScores(ScoreType.Child);
        TextView[,] ScoreCells = new TextView[11, 4];

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            this.ConfigureGrid();
            this.SetScores(ScoreType.Child);

            // add event
            FindViewById<Button>(Resource.Id.childButton).Click += (s, e) =>
            {
                this.SetScores(ScoreType.Child);
            };

            FindViewById<Button>(Resource.Id.parentButton).Click += (s, e) =>
            {
                this.SetScores(ScoreType.Parent);
            };

        }

        protected void SetScores(ScoreType type)
        {
            var scores = (type.Equals(ScoreType.Parent)) ? this.ParentScores : this.ChildScores;
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.ScoreCells[i, j].Text = scores[i, j].ToString();
                }
            }
        }

        private void ConfigureGrid()
        {
            var grid = FindViewById<GridLayout>(Resource.Id.gridLayout);

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var cell = new TextView(this);
                    var param = new GridLayout.LayoutParams();
                    param.SetGravity(Android.Views.GravityFlags.Fill);
                    param.ColumnSpec = GridLayout.InvokeSpec(j, GridLayout.Fill, 1);
                    param.MarginStart = 1;
                    param.RowSpec = GridLayout.InvokeSpec(i, GridLayout.Fill, 1);

                    cell.Gravity = Android.Views.GravityFlags.Center;
                    cell.TextSize = (float)10.0;
                    cell.SetHeight((int)(32 * 1.5));
                    cell.SetBackgroundColor(Android.Graphics.Color.Black);

                    grid.AddView(cell, param);
                    this.ScoreCells[i, j] = cell;
                }
            }


        }
    }
}

