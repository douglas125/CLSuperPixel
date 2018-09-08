using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCLTemplate;
using AForge.Video.DirectShow;
using System.IO;

namespace CLSuperPixel
{
    /// <summary>
    /// Initial form
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void FromPicture()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.jpg;*.png;*.bmp";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in ofd.FileNames)
                {
                    Bitmap bmp = new Bitmap(s);
                    CLSuperPixel sp = new CLSuperPixel(bmp);

                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    sp.SetBmp(bmp);


                    Bitmap bmp2 = bmp;
                    bmp2 = sp.GetColorRegionIdentification(chkDrawUnique.Checked, chkDrawMeanDist.Checked);
                    //bmp2 = sp.LoG(bmp);

                    List<CLSuperPixel.ConcentricRegionInfo> concentricRegions = sp.FindTargets(3);
                    Graphics g = Graphics.FromImage(bmp2);

                    Font f = new Font("Arial", 10, FontStyle.Regular);
                    foreach (CLSuperPixel.ConcentricRegionInfo cri in concentricRegions)
                    {
                        g.FillEllipse(Brushes.Yellow, cri.CenterX - 3, cri.CenterY - 3, 6, 6);
                        g.DrawString(cri.ID, f, Brushes.Black, cri.CenterX, cri.CenterY);
                        g.DrawString(cri.ID, f, Brushes.White, cri.CenterX + 1, cri.CenterY + 1);
                    }

                    try
                    {
                        List<List<CLSuperPixel.RegionData>> regions = sp.FindNeighborhood();
                        List<CLSuperPixel.RegionData[,]> checkerboards = sp.FindCheckerboards(regions);


                        #region Old region display
                        Random rnd = new Random();
                        for (int i = 0; i < regions.Count; i++)
                        {
                            Color c = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                            Color c2 = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));

                            if (regions[i].Count > 10)
                            {
                                List<PointF> centers = new List<PointF>();

                                foreach (CLSuperPixel.RegionData r in regions[i])
                                {
                                    centers.Add(new PointF(r.Center[0], r.Center[1]));

                                    g.FillEllipse(new SolidBrush(c2), r.Center[0] - 7, r.Center[1] - 7, 14, 14);
                                    g.FillEllipse(new SolidBrush(c), r.Center[0] - 5, r.Center[1] - 5, 10, 10);
                                }

                                if (regions[i].Count >= 4)
                                {
                                    List<PointF> quad = PolygonFinder.ApproximatePolygon(centers, 4);
                                    quad.Add(quad[0]);
                                    g.DrawLines(new Pen(c, 3), quad.ToArray());
                                }


                            }
                        }
                        #endregion


                        #region Display checkerboard data
                        Random rnd2 = new Random();
                        for (int i = 0; i < checkerboards.Count; i++)
                        {
                            Color c = Color.FromArgb(rnd2.Next(255), rnd2.Next(255), rnd2.Next(255));
                            Color c2 = Color.FromArgb(rnd2.Next(255), rnd2.Next(255), rnd2.Next(255));

                            List<PointF> centers = new List<PointF>();

                            CLSuperPixel.RegionData[,] curChecker = checkerboards[i];

                            int N = curChecker.GetLength(0); int M = curChecker.GetLength(1);

                            g.DrawLine(new Pen(c, 3), curChecker[0, 0].Center[0], curChecker[0, 0].Center[1], curChecker[N - 1, 0].Center[0], curChecker[N - 1, 0].Center[1]);
                            g.DrawLine(new Pen(c, 3), curChecker[0, 0].Center[0], curChecker[0, 0].Center[1], curChecker[0, M - 1].Center[0], curChecker[0, M - 1].Center[1]);

                            g.DrawLine(new Pen(c, 3), curChecker[N - 1, M - 1].Center[0], curChecker[N - 1, M - 1].Center[1], curChecker[N - 1, 0].Center[0], curChecker[N - 1, 0].Center[1]);
                            g.DrawLine(new Pen(c, 3), curChecker[N - 1, M - 1].Center[0], curChecker[N - 1, M - 1].Center[1], curChecker[0, M - 1].Center[0], curChecker[0, M - 1].Center[1]);

                            for (int x = 0; x < N; x++)
                                for (int y = 0; y < M; y++)
                                {
                                    if (curChecker[x, y] != null)
                                    {
                                        g.FillEllipse(new SolidBrush(c2), curChecker[x, y].Center[0] - 7, curChecker[x, y].Center[1] - 7, 14, 14);
                                        g.FillEllipse(new SolidBrush(c), curChecker[x, y].Center[0] - 5, curChecker[x, y].Center[1] - 5, 10, 10);

                                        string txt = "[" + x.ToString() + " " + y.ToString() + "]";
                                        g.DrawString(txt, f, new SolidBrush(c2), curChecker[x, y].Center[0] + 1, curChecker[x, y].Center[1] + 1);
                                        g.DrawString(txt, f, new SolidBrush(c), curChecker[x, y].Center[0], curChecker[x, y].Center[1]);
                                    }
                                }


                        }
                        #endregion

                        Font f2 = new Font("Arial", 20, FontStyle.Bold);

                        g.DrawString("Elapsed time (s): " + sw.Elapsed.ToString(), f2, Brushes.Red, 10, 10);
                        //g.DrawString("Checkerboard count: " + checkerboards.Count, f2, Brushes.Red, 10, 50);
                        //g.DrawString("Concentric region count (3+ rings): " + concentricRegions.Count, f2, Brushes.Red, 10, 100);

                    }
                    catch
                    {
                    }

                    picClustered.Image = bmp2;
                    sw.Stop();
                    this.Text = sw.Elapsed.ToString();
                    picClustered.Refresh();
                    Application.DoEvents();


                    string procFileName = s + "proc.png";
                    bmp2.Save(procFileName, System.Drawing.Imaging.ImageFormat.Png);

                    //picClustered.Image = sp.PixelVariance(bmp);
                    //picClustered.Image = sp.Sobel(bmp);
                }
            }
        }


        #region Display and process webcam image

        bool fechando = false;
        VideoCaptureDevice vcd;
        private void StartCam()
        {
            delegUpdtPic = UpdtPic;
            FilterInfoCollection devs = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //MJPEGStream stream = new MJPEGStream();
            //stream.NewFrame += new NewFrameEventHandler(video_NewFrame);
            //stream.VideoSourceError += new VideoSourceErrorEventHandler(stream_VideoSourceError);
            //stream.Login = "login";
            //stream.Password = "pass";
            //stream.RequestTimeout = 10000;
            //stream.Source = "http://192.168.0.33/nphMotionJpeg?Resolution=320x240&Quality=Standard";
            //stream.Start();

            if (devs.Count > 0)
            {
                vcd = new VideoCaptureDevice(devs[0].MonikerString);
                //vcd = new VideoCaptureDevice(devs[devs.Count - 1].MonikerString);
                vcd.NewFrame += str_NewFrame;
                vcd.Start();
                //vcd.DesiredFrameRate = 10;
                //vcd.DesiredFrameSize = new Size(800, 600);
            }
            else MessageBox.Show("Unable to find webcam", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        Bitmap bmp;
        int bmpWidth, bmpHeight;

        bool terminou = true;

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        int nframes = 0;
        void str_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            if (!terminou) return;
            terminou = false;

            if (!sw.IsRunning) sw.Start();
            nframes++;

            bmp = (Bitmap)eventArgs.Frame;//.Clone();
            //bmp = CLHoughTransg.CLHoughTransform.TestFuncs.MedianFilter(bmp);

            //bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);

            if (bmpWidth == 0)
            {
                bmpWidth = bmp.Width;
                bmpHeight = bmp.Height;
            }


            //bmp = CLHoughTransg.CLHoughTransform.TestFuncs.MedianFilter(bmp);
            //List<float[]> interestPts;
            //bmp = BlockDetector.GetInterestPtsVisualization(bmp, out interestPts);

            //Tries to form interest points
            ProcessBMP(ref bmp);
            try
            {
                if (!fechando) this.Invoke(delegUpdtPic);
            }
            catch { }
            //pic.Image = bmp; // imgdt.WriteData(bmp);
            //pic.Refresh();
            bmp.Dispose();

            terminou = true;
        }

        private void CloseVIdeo()
        {
            if (vcd != null)
            {
                if (vcd.IsRunning)
                {
                    vcd.SignalToStop();
                    vcd = null;
                }
            }
        }

        delegate void voidFunc();
        voidFunc delegUpdtPic;
        private void UpdtPic()
        {
            picClustered.Image = bmp;
            picClustered.Refresh();

            this.Text = ((double)nframes / (double)sw.Elapsed.TotalSeconds).ToString() + " fps";
        }

        #endregion

        CLSuperPixel spCam;
        bool processando = false;
        List<Color> lstColor = new List<Color>();
        private void ProcessBMP(ref Bitmap bmp)
        {
            if (lstColor.Count == 0)
            {
                Random rnd = new Random();
                for (int i = 0; i < 100000; i++)
                {
                    lstColor.Add(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)));
                }
            }

            if (processando) return;
            processando = true;

            if (spCam == null)
            {
                spCam = new CLSuperPixel(bmp);
                spCam.USEMEDIANFILTER = false;
            }
            else spCam.SetBmp(bmp);

            
            List<CLSuperPixel.ConcentricRegionInfo> concentricRegions = spCam.FindTargets(3);

            //bmp = spCam.GetColorRegionIdentification(true);
            //bmp = spCam.Sobel(bmp);

            Graphics g = Graphics.FromImage(bmp);

            Font f = new Font("Arial", 12, FontStyle.Regular);
            foreach (CLSuperPixel.ConcentricRegionInfo cri in concentricRegions)
            {
                g.FillEllipse(Brushes.Yellow, cri.CenterX - 3, cri.CenterY - 3, 6, 6);
                g.DrawString(cri.ID, f, Brushes.Red, cri.CenterX, cri.CenterY);
            }

            List<List<CLSuperPixel.RegionData>> regions = spCam.FindNeighborhood();
            int idxcor = 0;
            for (int i = 0; i < regions.Count; i++)
            {

                if (regions[i].Count > 5)
                {
                    Color c = lstColor[idxcor];
                    Color c2 = lstColor[idxcor + 1];
                    idxcor += 2;

                    List<PointF> centers = new List<PointF>();

                    foreach (CLSuperPixel.RegionData r in regions[i])
                    {
                        centers.Add(new PointF(r.Center[0], r.Center[1]));

                        g.FillEllipse(new SolidBrush(c2), r.Center[0] - 7, r.Center[1] - 7, 14, 14);
                        g.FillEllipse(new SolidBrush(c), r.Center[0] - 5, r.Center[1] - 5, 10, 10);
                        //g.DrawLine(Pens.LightBlue, r.Center[0], r.Center[1], neighbor.Center[0], neighbor.Center[1]);
                        //g.DrawLine(Pens.DarkBlue, r.Center[0] + 1, r.Center[1] + 1, neighbor.Center[0] + 1, neighbor.Center[1] + 1);
                    }

                    if (regions[i].Count >= 4)
                    {
                        List<PointF> quad = PolygonFinder.ApproximatePolygon(centers, 4);
                        quad.Add(quad[0]);
                        g.DrawLines(new Pen(c, 3), quad.ToArray());
                    }
                }
            }


            #region Display checkerboard data
            idxcor = 0;
            List<CLSuperPixel.RegionData[,]> checkerboards = spCam.FindCheckerboards(regions);
            Random rnd2 = new Random();
            for (int i = 0; i < checkerboards.Count; i++)
            {
                Color c = lstColor[idxcor];
                Color c2 = lstColor[idxcor + 1];
                idxcor += 2;

                List<PointF> centers = new List<PointF>();

                CLSuperPixel.RegionData[,] curChecker = checkerboards[i];

                int N = curChecker.GetLength(0); int M = curChecker.GetLength(1);

                g.DrawLine(new Pen(c, 3), curChecker[0, 0].Center[0], curChecker[0, 0].Center[1], curChecker[N - 1, 0].Center[0], curChecker[N - 1, 0].Center[1]);
                g.DrawLine(new Pen(c, 3), curChecker[0, 0].Center[0], curChecker[0, 0].Center[1], curChecker[0, M - 1].Center[0], curChecker[0, M - 1].Center[1]);

                g.DrawLine(new Pen(c, 3), curChecker[N - 1, M - 1].Center[0], curChecker[N - 1, M - 1].Center[1], curChecker[N - 1, 0].Center[0], curChecker[N - 1, 0].Center[1]);
                g.DrawLine(new Pen(c, 3), curChecker[N - 1, M - 1].Center[0], curChecker[N - 1, M - 1].Center[1], curChecker[0, M - 1].Center[0], curChecker[0, M - 1].Center[1]);

                for (int x = 0; x < N; x++)
                    for (int y = 0; y < M; y++)
                    {
                        if (curChecker[x, y] != null)
                        {
                            g.FillEllipse(new SolidBrush(c2), curChecker[x, y].Center[0] - 7, curChecker[x, y].Center[1] - 7, 14, 14);
                            g.FillEllipse(new SolidBrush(c), curChecker[x, y].Center[0] - 5, curChecker[x, y].Center[1] - 5, 10, 10);

                            string txt = "[" + x.ToString() + " " + y.ToString() + "]";
                            g.DrawString(txt, f, new SolidBrush(c2), curChecker[x, y].Center[0] + 1, curChecker[x, y].Center[1] + 1);
                            g.DrawString(txt, f, new SolidBrush(c), curChecker[x, y].Center[0], curChecker[x, y].Center[1]);
                        }
                    }


            }
            #endregion


            processando = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            fechando = true;
            CloseVIdeo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartCam();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Bitmap bmpSave = (Bitmap)picClustered.Image;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bmpSave.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void batchShrink_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Img|*.jpg;*.bmp";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in ofd.FileNames)
                {
                    using (Bitmap bmp = new Bitmap(s))
                    {
                        using (Bitmap bmp2 = new Bitmap(bmp.Width / 2, bmp.Height / 2))
                        {
                            Graphics g = Graphics.FromImage(bmp2);
                            g.DrawImage(bmp, 0, 0, bmp.Width / 2, bmp.Height / 2);
                            bmp2.Save(s + ".shrink.png", System.Drawing.Imaging.ImageFormat.Png);
                            picClustered.Image = bmp2;
                            picClustered.Refresh();
                        }
                    }
                }
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            FromPicture();
        }

    }
}
