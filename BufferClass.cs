using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stichingFirstOne
{
    class BufferClass
    {
    }
}


//#region Comment
//private void ImageStiching(string[] images)
//{

//    Mat[] imagesMat = new Mat[images.Length];

//    for (int i = 0; i < images.Length; i++)
//        imagesMat[i] = new Mat(images[i], LoadImageType.Color);

//    Image<Bgr, Byte> one = new Image<Bgr, Byte>(images[0]);
//    Image<Bgr, Byte> two = new Image<Bgr, Byte>(images[1]);
//    FindMatch(one, two);


//    VectorOfMat vmsrc = new VectorOfMat();

//    //for (int i = 0; i < images.Length; i++)
//    //    vmsrc.Push(imagesMat[i]);

//    vmsrc.Push(imagesMat[0]);
//    vmsrc.Push(imagesMat[1]);


//    Console.WriteLine("Obrada slika - prva iteracija");


//    //Image<Bgr, byte> res = new Image<Bgr, byte>(2000, 2000);
//    Mat result = new Mat();
//    Stitcher stitcher = new Stitcher(true);

//    try
//    {
//        stitcher.Stitch(vmsrc, result);

//        Console.WriteLine("Prva obrada zavrsena.");
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine("Dogodila se pogreska.");
//        throw e;
//    }


//    vmsrc.Dispose();

//    VectorOfMat vmsrc2 = new VectorOfMat();
//    Console.WriteLine("Obrada slika - druga iteracija");
//    Mat result2 = new Mat();

//    try
//    {
//        vmsrc2.Push(result);
//        vmsrc2.Push(imagesMat[2]);
//        vmsrc2.Push(imagesMat[3]);

//        stitcher.Stitch(vmsrc2, result2);
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine("Dogodila se pogreska2.");
//        throw e;
//    }


//    Console.WriteLine("Druga obrada zavrsena.");

//    vmsrc2.Dispose();


//    if (result2 == null)
//    {
//        Console.WriteLine("Necemo sprmeati.");

//    }
//    else
//    {
//        saveFileDialog1.ShowDialog();
//        string path = saveFileDialog1.FileName;
//        result2.Save(path);

//    }



//    //Image<Bgr, Byte> one = new Image<Bgr, Byte>("D:\\Samples\\sample1.jpg");
//    //Image<Bgr, Byte> two = new Image<Bgr, Byte>("D:\\Samples\\sample2.jpg");
//    //Image<Bgr, Byte> third = new Image<Bgr, Byte>("D:\\Samples\\sample3.jpg");
//    //Image<Bgr, Byte> fourth = new Image<Bgr, Byte>("D:\\Samples\\sample4.jpg");

//    ////Image<Bgr, Byte>[] sourceImages = new Image<Bgr, Byte>[4];

//    ////CvArray<Image<Bgr, Byte>> test = new CvArray<Image<Bgr, byte>>();


//    //Image<Bgr, Byte> result = new Image<Bgr, byte>(one.Size.Width * 4, one.Size.Height);

//    ////Image<Bgr, byte> test = new Image<Bgr, byte>(one.Width*2, one.Height);

//    //Stitcher sticher = new Stitcher(true);
//    //sticher.Stitch(one, result);

//    //result.Save("D:\\Samples\\result1.jpg");

//    //InputArray i = new InputArray();


//    ////Image<Bgr, Byte> result = FindMatch(one, two);
//    ////result = convert(result);
//    ////Image<Bgr, Byte> onePlusTwo = result.Convert<Bgr, Byte>();

//    ////Image<Bgr, Byte> result2 = FindMatch(third, fourth);
//    ////result2 = convert(result2);
//    ////Image<Bgr, Byte> threePlusFour = result.Convert<Bgr, Byte>();

//    ////Image<Bgr, Byte> finalResult = FindMatch(onePlusTwo, threePlusFour);
//    ////finalResult  = convert(finalResult);
//    ////Image<Bgr, Byte> final = finalResult.Convert<Bgr, Byte>();

//    //result.Save("D:\\Samples\\result1.jpg");
//    //result2.Save("D:\\Samples\\result2.jpg");
//    //finalResult.Save("D:\\Samples\\resultFinal.jpg");
//    //Image<Bgr, Byte>[] sourceImages = new Image<Bgr, Byte>[4];
//    //Image<Bgr, Byte>[] resultImages = new Image<Bgr, Byte>[4];


//    //Stitcher sticher = new Stitcher(false);
//    //sticher.Stitch(one, result);



//}

//private Image<Bgr, Byte> convert(Image<Bgr, Byte> img)
//{
//    Image<Gray, byte> imgGray = img.Convert<Gray, byte>();
//    Image<Gray, byte> mask = imgGray.CopyBlank();

//    VectorOfPoint largestContour = null;
//    double largestarea = 0;

//    VectorOfVectorOfPoint contoursDetected = new VectorOfVectorOfPoint();


//    CvInvoke.FindContours(imgGray, contoursDetected, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
//    int size = contoursDetected.Size;

//    List<VectorOfPoint> arrayContours = new List<VectorOfPoint>();

//    for (int i = 0; i < size; i++)
//    {
//        using (VectorOfPoint currContour = contoursDetected[i])
//        {
//            arrayContours.Add(currContour);
//        }
//    }

//    foreach (VectorOfPoint currContour in arrayContours)
//    {
//        double contArea = CvInvoke.ContourArea(currContour, false);
//        if (contArea > largestarea)
//        {
//            largestarea = contArea;
//            largestContour = currContour;
//        }

//    }

//    CvInvoke.cvSetImageROI(img, CvInvoke.BoundingRectangle(largestContour));
//    return img;
//}

//private Image<Bgr, byte> FindMatch(Image<Bgr, Byte> fImage, Image<Bgr, Byte> lImage)
//{
//    //HomographyMatrix homography = null;
//    SURF surfCPU = new SURF(500);

//    int k = 2;
//    double uniquenessThreshold = 0.8;

//    Matrix<int> indices;


//    VectorOfKeyPoint modelKeyPoints;
//    VectorOfKeyPoint observedKeyPoints;
//    Image<Gray, Byte> fImageG = fImage.Convert<Gray, Byte>();
//    Image<Gray, Byte> lImageG = lImage.Convert<Gray, Byte>();

//    //Matrix<float> modelDescriptors = new Matrix<float>();
//    Mat modelDescriptor = new Mat();

//    //extract features from the object image
//    modelKeyPoints = new VectorOfKeyPoint();
//    surfCPU.DetectAndCompute(fImageG, null, modelKeyPoints, modelDescriptor, false);

//    // extract features from the observed image
//    observedKeyPoints = new VectorOfKeyPoint();
//    Mat observedDescriptor = new Mat();

//    //Matrix<float> observedDescriptors = surfCPU.DetectAndCompute(lImageG, null, observedKeyPoints);
//    surfCPU.DetectAndCompute(lImageG, null, observedKeyPoints, observedDescriptor, false);

//    BFMatcher matcher = new BFMatcher(DistanceType.L2);

//    matcher.Add(modelDescriptor);

//    indices = new Matrix<int>(observedDescriptor.Rows, k);
//    VectorOfVectorOfDMatch ind = null;
//    try
//    {
//        ind = new VectorOfVectorOfDMatch();

//        observedDescriptor.ConvertTo(ind, DepthType.Default);

//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.InnerException);
//    }

//    Mat mask = new Mat();

//    //Matrix<byte> mask;


//    using (Matrix<float> dist = new Matrix<float>(observedDescriptor.Rows, k))
//    {
//        //matcher.KnnMatch(observedDescriptor, indices, dist, k, null);
//        matcher.KnnMatch(observedDescriptor, ind, k, null);
//        mask = new Mat(dist.Rows, 1, DepthType.Default, 1);
//        //set na 1???
//        Features2DToolbox.VoteForUniqueness(ind, uniquenessThreshold, mask);
//    }

//    Mat homography = new Mat();
//    int nonZeroCount = CvInvoke.CountNonZero(mask);
//    if (nonZeroCount >= 4)
//    {
//        nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, ind, mask, 1.5, 20);
//        if (nonZeroCount >= 4)
//            homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, ind, mask, 2);
//    }
//    Image<Bgr, Byte> mImage = fImage.Convert<Bgr, Byte>();
//    Image<Bgr, Byte> oImage = lImage.Convert<Bgr, Byte>();
//    Image<Bgr, Byte> result = new Image<Bgr, byte>(mImage.Width + oImage.Width, mImage.Height);

//    if (homography != null)
//    {  //draw a rectangle along the projected model
//        Rectangle rect = fImage.ROI;
//        PointF[] pts = new PointF[] {
//                 new PointF(rect.Left, rect.Bottom),
//               new PointF(rect.Right, rect.Bottom),
//               new PointF(rect.Right, rect.Top),
//               new PointF(rect.Left, rect.Top)
//             };
//        //homography.ProjectPoints(pts);


//        Mat origin = new Mat();
//        //HomographyMatrix origin = new HomographyMatrix();                //I perform a copy of the left image with a not real shift operation on the origin
//        //origin.SetIdentity();
//        //origin.Data[0, 2] = 0;
//        //origin.Data[1, 2] = 0;

//        Image<Bgr, Byte> mosaic = new Image<Bgr, byte>(mImage.Width + oImage.Width + 2000, mImage.Height * 2);

//        //Image<Bgr, byte> warp_image = mosaic.Clone();

//        //mosaic = mImage.WarpPerspective(origin, mosaic.Width, mosaic.Height, Inter.Linear, Warp.Default, new Bgr());


//        //warp_image = oImage.WarpPerspective(homography, warp_image.Width, warp_image.Height, Inter.Linear, Warp.InverseMap, new Bgr(200, 0, 0));
//        //Image<Gray, byte> warp_image_mask = oImage.Convert<Gray, byte>();
//        //warp_image_mask.SetValue(new Gray(255));
//        //Image<Gray, byte> warp_mosaic_mask = mosaic.Convert<Gray, byte>();
//        //warp_mosaic_mask.SetZero();

//        //warp_mosaic_mask = warp_image_mask.WarpPerspective(homography, warp_mosaic_mask.Width, warp_mosaic_mask.Height, Inter.Linear,Warp.InverseMap, new Gray(0));
//        //warp_image.Copy(mosaic, warp_mosaic_mask);

//        return mosaic;
//    }
//    return null;

//}

//#endregion


#region blizu rada
//private void MakeMosaicImages(Mat homography, Image<Bgr, byte> modelImage, Image<Bgr, byte> observedImage)
//{
//    Mat origin = homography.Clone();

//    for (int i = 0; i < origin.Rows; i++)
//        for (int j = 0; j < origin.Cols; j++)
//        {
//            if (i != j)
//                origin.SetValue(i, j, 0);
//            else
//                origin.SetValue(i, j, 1);
//        }

//    int resultWidth = (int)(modelImage.Width + observedImage.Width);
//    int resultHeight = (int)(modelImage.Height);

//    Image<Bgr, Byte> mosaic = new Image<Bgr, byte>(resultWidth, resultHeight);
//    Image<Bgr, byte> warp_image = mosaic.Clone(); //---ovo je radilo

//    string tempPath;

//    CvInvoke.WarpPerspective(modelImage, mosaic, homography, mosaic.Size, Inter.Linear);
//    CvInvoke.WarpPerspective(observedImage, warp_image, origin, warp_image.Size, Inter.Linear); //radi


//    //CvInvoke.WarpPerspective(observedImage, warp_image, origin, observedImage.Size, Inter.Linear, Warp.Default, BorderType.Transparent);

//    Image<Gray, byte> warp_image_mask = observedImage.Convert<Gray, Byte>();//new Image<Gray, byte>(observedImage.Width, observedImage.Height);
//    warp_image_mask.SetValue(new Gray(255));


//    Image<Gray, byte> warp_mosaic_mask = mosaic.Convert<Gray, byte>();
//    warp_mosaic_mask.SetZero();

//    CvInvoke.WarpPerspective(warp_image_mask, warp_mosaic_mask, homography, mosaic.Size, Inter.Linear);



//    tempPath = Helper.GetFileName("WarpImage-PrijeKopiranja");
//    warp_image.Save(tempPath);

//    tempPath = Helper.GetFileName("Mosaic-PrijeKopiranja");
//    mosaic.Save(tempPath);


//    warp_image.Copy(mosaic, warp_mosaic_mask);

//    //tempPath = Helper.GetFileName("WarpImage-NakonKopiranja");
//    //warp_image.Save(tempPath);

//    tempPath = Helper.GetFileName("REZULTAT");
//    mosaic.Save(tempPath);



//}
#endregion