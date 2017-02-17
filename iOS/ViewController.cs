using System;
using System.Drawing;

using UIKit;

namespace MajanScoreList.iOS
{
    public partial class ViewController : UIViewController
    {
        UIButton ParentButton   = new UIButton();
        UIButton ChildButton    = new UIButton();
        Score[,] ParentScores   = Score.GetScores(ScoreType.Parent);
        Score[,] ChildScores    = Score.GetScores(ScoreType.Child);
        UILabel[,] ScoreLabels  = new UILabel[11, 4];
        UILabel[] HanHeaders    = new UILabel[4];
        UILabel[] HuHeaders     = new UILabel[11];

        public ViewController(IntPtr handle) : base(handle)
        {
            this.View.AddSubview(this.ChildButton);
            this.View.AddSubview(this.ParentButton);
            this.ConfigureHeaders();
        }

        private void ConfigureHeaders()
        {
            for (int i = 0; i < 4; i++) 
            {
                UILabel label = new UILabel();
                this.View.AddSubview(label);

                int offset = 8 + 44;
                int w = ((int)UIScreen.MainScreen.Bounds.Width - (offset + 8)) / 4;
                int h = 32;
                int y = 28;
                int x = (8 + 44) + (i * w);
                label.Frame = new RectangleF(x, y, w, h);
                label.TextAlignment = UITextAlignment.Center;
                label.TextColor = UIColor.Black;
                label.BackgroundColor = UIColor.Orange;
                label.Text = (i + 1) + "ハン";
                label.Layer.BorderWidth = (nfloat)1.0;

                this.HanHeaders[i] = label;
            }

            for (int i = 0; i < 11; i++)
            {
                UILabel label = new UILabel();
                this.View.AddSubview(label);

                int offset = 60;
                int w = 45;
                int h = 32;
                int y = offset + (i * h);
                int x = 8;
                String text = (i + 1) * 10 + "符";
                if (i == 0) { text = "20符"; }
                if (i == 1) { text = "25符"; }

                label.Frame = new RectangleF(x, y, w, h);
                label.TextAlignment = UITextAlignment.Center;
                label.TextColor = UIColor.Black;
                label.BackgroundColor = UIColor.Orange;
                label.Layer.BorderWidth = (nfloat)1.0;
                label.Text = text;
                label.AdjustsFontSizeToFitWidth = true;

                this.HuHeaders[i] = label;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ConfigureView();
            this.ConfigureTableLabels();
            this.SetScores(ScoreType.Child);
        }

        protected void SetScores(ScoreType type)
        {
            Score[,] scores = type.Equals(ScoreType.Parent) ? this.ParentScores : this.ChildScores;

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    UILabel label = this.ScoreLabels[i,j];
                    Score score = scores[i, j];
                    label.Text = score.ToString();
                }
            }


        }

        private void ConfigureView()
        {
            int y = (int)UIScreen.MainScreen.Bounds.Height - 40;
            int px = (int)UIScreen.MainScreen.Bounds.Width - 108;
            int cx = px - 108;
            this.ChildButton.Frame = new RectangleF(cx, y, 100, 32);
            this.ChildButton.SetTitle("子の点数", UIControlState.Normal);
            this.ChildButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            this.ChildButton.BackgroundColor = UIColor.Orange;
            this.ChildButton.Layer.BorderWidth = (nfloat)1.0;
            this.ChildButton.Layer.BorderColor = UIColor.LightGray.CGColor;

            this.ParentButton.Frame = new RectangleF(px, y, 100, 32);
            this.ParentButton.SetTitle("親の点数", UIControlState.Normal);
            this.ParentButton.SetTitleColor(UIColor.LightGray, UIControlState.Normal);
            this.ParentButton.Layer.BorderWidth = (nfloat)1.0;
            this.ParentButton.Layer.BorderColor = UIColor.LightGray.CGColor;

            this.ChildButton.TouchUpInside += delegate {
                this.SetScores(ScoreType.Child);
                this.ChildButton.BackgroundColor = UIColor.Orange;
                this.ChildButton.SetTitleColor(UIColor.White, UIControlState.Normal);
                this.ParentButton.BackgroundColor = UIColor.White;
                this.ParentButton.SetTitleColor(UIColor.LightGray, UIControlState.Normal);
            };  
        
            this.ParentButton.TouchUpInside += delegate {
                this.SetScores(ScoreType.Parent);
                this.ChildButton.BackgroundColor = UIColor.White;
                this.ChildButton.SetTitleColor(UIColor.LightGray, UIControlState.Normal);
                this.ParentButton.BackgroundColor = UIColor.Orange;
                this.ParentButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            };
        }

        private void ConfigureTableLabels()
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int x = (int)this.HanHeaders[j].Frame.X;
                    int y = (int)this.HuHeaders[i].Frame.Y;
                    int w = (int)this.HanHeaders[j].Frame.Width;
                    int h = (int)this.HuHeaders[i].Frame.Height;

                    UILabel label = new UILabel();
                    this.View.AddSubview(label);

                    label.Frame = new RectangleF(x, y, w, h);
                    label.Text = "-";
                    label.TextAlignment = UITextAlignment.Center;
                    label.TextColor = UIColor.Black;
                    label.Layer.BorderWidth = (nfloat)1.0;
                    label.AdjustsFontSizeToFitWidth = true;
                    label.Lines = 2;

                    this.ScoreLabels[i, j] = label;
                }
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
