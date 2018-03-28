using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu Cv imports
using Emgu.CV.Structure;        //
using Emgu.CV.UI;               //
using Emgu.CV.Features2D;
using Emgu.CV.Util;

using Emgu.CV.XFeatures2D;
using Emgu.CV.Stitching;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace stichingFirstOne
{
   
    public partial class Form1 : Form
    {
        #region Fields
        string[] _images;
        string _resultPath;
        private const bool _surfExtendedFlag = true;
        private const bool _saveDebugImages = true;
        private double _SurfHessianThresh = 2200;

        #endregion

        #region Properties
        public string[] ChosenImages
        {
            get
            {
                return _images;
            }

            set
            {
                _images = value;
            }
        }

        public string ResultPath
        {
            get
            {
                return _resultPath;
            }

            set
            {
                _resultPath = value;
            }
        }
        #endregion

        #region Constructors
        public Form1()
        {
            InitializeComponent();
        }
        #endregion
   
        #region Events
        private void btnProcess_Click(object sender, EventArgs e)
        {

            //ChosenImages[0] = "D:\\Samples2\\DJI_0074.jpg";
            //ChosenImages[1] = "D:\\Samples2\\DJI_0073.jpg";

            Cursor = Cursors.WaitCursor;

            long elapsedTime = ImageStiching();
            txtMessages.Text += "Result: " + ResultPath + Environment.NewLine;
            txtMessages.Text += "Time elapsed: " + (elapsedTime / 1000F).ToString() + "s with SurfHessianThresh: " + _SurfHessianThresh + Environment.NewLine;

            Cursor = Cursors.Default;
        }

        private void btnOpenResult_Click(object sender, EventArgs e)
        {
            var resultForm = new DisplayImage();
            resultForm.SetImage(ResultPath);
            resultForm.ShowDialog();
        }

        private void btnChooseImages_MouseClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.ShowDialog();

            txtChosenImages.Text = String.Empty;
            txtMessages.Text = String.Empty;

            ChosenImages = ofd.FileNames;

            foreach (string image in ChosenImages)
                txtChosenImages.Text += image + Environment.NewLine;

            ResultPath = String.Empty;

        }
              

        #endregion

        #region Methods

        private long ImageStiching()
        {
            //TODO:
            //1. Ubaciti progress bar
            //2. Dodati logiku za vise slika
            //3.Blend slika
            //4. Spremanje slika - refaktoring
            //5. Omoguciti debug slike 
            //6. Adekvatne poruke ako se slike ne mogu spojiti


            Mat homography;

            Mat modelImage = new Mat(ChosenImages[0], LoadImageType.Color);
            Mat observedImage = new Mat(ChosenImages[1], LoadImageType.Color);
            long matchTime;


            Mat result = Draw(modelImage, observedImage, out matchTime, _SurfHessianThresh, out homography);

            // MatProucavanje(homography);


            string files = Helper.GetFileName("Rezultat mapiranja" + _SurfHessianThresh.ToString() + "-" + ((int)matchTime / 1000).ToString());
            result.Save(files);

            //Image<Bgr, byte> mImage = new Image<Bgr, byte>(ChosenImages[0]);
            //Image<Bgr, byte> oImage = new Image<Bgr, byte>(ChosenImages[1]);
            //MakeMosaicImages(homography, mImage, oImage);


            MakeMosaicMat(homography, modelImage, observedImage);
            return matchTime;
        }

        private string RemoveBlackSections(Image<Bgr, byte> mosaic, string path)
        {
            Mat mask = new Mat();

            Image<Gray, byte> grayImage = mosaic.Convert<Gray, byte>();

            CvInvoke.Threshold(grayImage, mask, 1.0, 255.0, ThresholdType.Binary);

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(mask, contours, null, RetrType.Ccomp, ChainApproxMethod.ChainApproxSimple);
                Rectangle roi = CvInvoke.BoundingRectangle(FindLargestRoi(contours));
                mosaic.ROI = roi;
                string tempPath = Helper.GetFileName("NewMosaic");
                mosaic.Save(tempPath);
                return tempPath;
            }
        }

        private IInputArray FindLargestRoi(VectorOfVectorOfPoint contours)
        {
            int biggestContourIndex = 0;
            double size = -1;
            double tempSize;

            for (int i = 0; i < contours.Size; i++)
            {
                tempSize = CvInvoke.ContourArea(contours[i]);
                if (tempSize > size)
                {
                    size = tempSize;
                    biggestContourIndex = i;
                }
            }
            return contours[biggestContourIndex];
        }

        private void MakeMosaicMat(Mat homography, Mat modelImage, Mat observedImage)
        {
            Mat origin = homography.Clone();

            for (int i = 0; i < origin.Rows; i++)
                for (int j = 0; j < origin.Cols; j++)
                {
                    if (i != j)
                        origin.SetValue(i, j, 0);
                    else
                        origin.SetValue(i, j, 1);
                }

            int resultWidth = (int)(modelImage.Width + observedImage.Width+1000);
            int resultHeight = (int)(modelImage.Height + observedImage.Height+1000);

            Image<Bgr, Byte> mosaic = new Image<Bgr, byte>(resultWidth, resultHeight);
            Image<Bgr, byte> warp_image = mosaic.Clone(); //---ovo je radilo

            string tempPath;

            CvInvoke.WarpPerspective(observedImage, mosaic, origin, mosaic.Size, Inter.Linear);
            origin.Dispose();


            CvInvoke.WarpPerspective(modelImage, warp_image, homography, warp_image.Size, Inter.Linear); //radi

            Image<Gray, byte> warp_image_mask = new Image<Gray, byte>(observedImage.Width, observedImage.Height);
            warp_image_mask.SetValue(new Gray(255));


            Image<Gray, byte> warp_mosaic_mask = mosaic.Convert<Gray, byte>();
            warp_mosaic_mask.SetZero();

            CvInvoke.WarpPerspective(warp_image_mask, warp_mosaic_mask, homography, mosaic.Size, Inter.Linear);


            //tempPath = Helper.GetFileName("WarpMosaikMASK");
            //warp_mosaic_mask.Save(tempPath);

            //tempPath = Helper.GetFileName("WarpImageMASK");
            //warp_image_mask.Save(tempPath);

            //tempPath = Helper.GetFileName("WarpImage-PrijeKopiranja");
            //warp_image.Save(tempPath);

            //tempPath = Helper.GetFileName("Mosaic-PrijeKopiranja");
            //mosaic.Save(tempPath);


            warp_image.Copy(mosaic, warp_mosaic_mask);


            tempPath = Helper.GetFileName("REZULTAT");
            mosaic.Save(tempPath);

            string lastImageSaved = RemoveBlackSections(mosaic, tempPath);
            ResultPath = lastImageSaved;

            #region Disposing

            mosaic.Dispose();
            warp_image.Dispose();
            warp_mosaic_mask.Dispose();
            warp_image_mask.Dispose();

            #endregion
        }

        public static void FindMatch(Mat modelImage, Mat observedImage, out long matchTime, out VectorOfKeyPoint modelKeyPoints, out VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches, out Mat mask, out Mat homography, double surfHessianThresh)
        {
            int k = 2;
            double uniquenessThreshold = 0.8;
            //double hessianThresh = 2300;

            Stopwatch watch;
            homography = null;

            modelKeyPoints = new VectorOfKeyPoint();
            observedKeyPoints = new VectorOfKeyPoint();

            using (UMat uModelImage = modelImage.ToUMat(AccessType.Read))
            using (UMat uObservedImage = observedImage.ToUMat(AccessType.Read))
            {
                SURF surfCPU = new SURF(surfHessianThresh);
                //extract features from the object image
                UMat modelDescriptors = new UMat();
                surfCPU.DetectAndCompute(uModelImage, null, modelKeyPoints, modelDescriptors, false);

                watch = Stopwatch.StartNew();

                // extract features from the observed image
                UMat observedDescriptors = new UMat();
                surfCPU.DetectAndCompute(uObservedImage, null, observedKeyPoints, observedDescriptors, false);
                BFMatcher matcher = new BFMatcher(DistanceType.L2);
                matcher.Add(modelDescriptors);

                matcher.KnnMatch(observedDescriptors, matches, k, null);
                mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                mask.SetTo(new MCvScalar(255));
                Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                int nonZeroCount = CvInvoke.CountNonZero(mask);
                if (nonZeroCount >= 4)
                {
                    nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints,
                       matches, mask, 1.5, 20);
                    if (nonZeroCount >= 4)
                        homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints,
                           observedKeyPoints, matches, mask, 2);
                }

                watch.Stop();
            }

            matchTime = watch.ElapsedMilliseconds;
        }

        public Mat Draw(Mat modelImage, Mat observedImage, out long matchTime, double surfHessianTresh, out Mat homography)
        {
            VectorOfKeyPoint modelKeyPoints;
            VectorOfKeyPoint observedKeyPoints;
            using (VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
            {
                Mat mask;
                FindMatch(modelImage, observedImage, out matchTime, out modelKeyPoints, out observedKeyPoints, matches,
                   out mask, out homography, surfHessianTresh);

                string temp = Helper.GetFileName("Trazenje deskriptora");
                modelImage.Save(temp);


                //Draw the matched keypoints
                Mat result = new Mat();

                Features2DToolbox.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints,
                   matches, result, new MCvScalar(255, 255, 255), new MCvScalar(255, 255, 255), mask);


                #region draw the projected region on the image

                if (homography != null)
                {
                    //draw a rectangle along the projected model
                    Rectangle rect = new Rectangle(Point.Empty, modelImage.Size);
                    PointF[] pts = new PointF[]
                    {
                      new PointF(rect.Left, rect.Bottom),
                      new PointF(rect.Right, rect.Bottom),
                      new PointF(rect.Right, rect.Top),
                      new PointF(rect.Left, rect.Top)
                    };
                    pts = CvInvoke.PerspectiveTransform(pts, homography);

                    Point[] points = Array.ConvertAll<PointF, Point>(pts, Point.Round);
                    using (VectorOfPoint vp = new VectorOfPoint(points))
                    {
                        CvInvoke.Polylines(result, vp, true, new MCvScalar(255, 0, 0, 255), 5);
                    }

                }
                #endregion

                //kad je ovo zakomentirano algoritam radi!
                //Pokusaj panorame - 15.1.2017

                //MakeMosaic(homography, mmodelImage, observedImage);

                modelKeyPoints.Dispose();
                observedKeyPoints.Dispose();

                return result;

            }


     

    }

        #endregion

        #region NotInUse
        private void BlendPokusaj()
        {
            Image<Bgr, Byte> image = new Image<Bgr, byte>("D:\\Samples2\\DJI_0064.jpg");
            Image<Gray, Byte> mask = new Image<Bgr, byte>("D:\\Samples2\\DJI_0065.jpg").Convert<Gray, Byte>();

            Image<Bgr, float> maskF = mask.Convert<Bgr, float>();
            //Image<Bgr, float> maskF2 = maskF.Mul(1 / 255);
            Image<Bgr, float> imageF = image.Convert<Bgr, float>();
            string filePath = Helper.GetFileName("BlendPokusaj");
            imageF.Mul(maskF).Convert<Bgr, byte>().Save(filePath);

        }
        private void MatProucavanje(Mat homography)
        {
            Mat modelImage = new Mat("D:\\Samples2\\LOL2DJI_0067.jpg", LoadImageType.Color);

            Image<Bgr, byte> mosaicHOMOG = new Image<Bgr, byte>(modelImage.Width * 2, modelImage.Height * 2);

            CvInvoke.WarpPerspective(modelImage, mosaicHOMOG, homography, mosaicHOMOG.Size, Inter.Linear);
            mosaicHOMOG.Save(Helper.GetFileName("MatricaHOMOG"));


            Mat origin = homography.Clone();
            Image<Bgr, byte> mosaic = new Image<Bgr, byte>(modelImage.Width * 3, modelImage.Height * 3);

            for (int i = 0; i < origin.Rows; i++)
                for (int j = 0; j < origin.Cols; j++)
                {
                    if (i != j)
                        origin.SetValue(i, j, 0);
                    else
                        origin.SetValue(i, j, 1);
                }

            CvInvoke.WarpPerspective(modelImage, mosaic, origin, mosaic.Size, Inter.Lanczos4);
            mosaic.Save(Helper.GetFileName("MatricaTEST"));
        }

        private void MakeMosaicImages(Mat homography, Image<Bgr, byte> modelImage, Image<Bgr, byte> observedImage)
        {
            Mat origin = homography.Clone();

            for (int i = 0; i < origin.Rows; i++)
                for (int j = 0; j < origin.Cols; j++)
                {
                    if (i != j)
                        origin.SetValue(i, j, 0);
                    else
                        origin.SetValue(i, j, 1);
                }

            int resultWidth = (int)(modelImage.Width + observedImage.Width);
            int resultHeight = (int)(modelImage.Height);

            Image<Bgr, Byte> mosaic = new Image<Bgr, byte>(resultWidth, resultHeight);
            Image<Bgr, byte> warp_image = mosaic.Clone(); //---ovo je radilo

            string tempPath;

            CvInvoke.WarpPerspective(modelImage, mosaic, homography, mosaic.Size, Inter.Linear);
            CvInvoke.WarpPerspective(observedImage, warp_image, origin, warp_image.Size, Inter.Linear); //radi


            //CvInvoke.WarpPerspective(observedImage, warp_image, origin, observedImage.Size, Inter.Linear, Warp.Default, BorderType.Transparent);

            Image<Gray, byte> warp_image_mask = observedImage.Convert<Gray, Byte>();//new Image<Gray, byte>(observedImage.Width, observedImage.Height);
            warp_image_mask.SetValue(new Gray(255));


            Image<Gray, byte> warp_mosaic_mask = mosaic.Convert<Gray, byte>();
            warp_mosaic_mask.SetZero();

            CvInvoke.WarpPerspective(warp_image_mask, warp_mosaic_mask, homography, mosaic.Size, Inter.Linear);



            tempPath = Helper.GetFileName("WarpImage-PrijeKopiranja");
            warp_image.Save(tempPath);

            tempPath = Helper.GetFileName("Mosaic-PrijeKopiranja");
            mosaic.Save(tempPath);


            warp_image.Copy(mosaic, warp_mosaic_mask);

            //tempPath = Helper.GetFileName("WarpImage-NakonKopiranja");
            //warp_image.Save(tempPath);

            tempPath = Helper.GetFileName("REZULTAT");
            mosaic.Save(tempPath);



        }



        
        #endregion
        
    }
}


