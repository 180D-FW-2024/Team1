using OpenCvSharp;
using OpenCvSharp.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleFinder : WebCamera
{

    // [SerializeField] private FlipMode ImageFlip;
    [SerializeField] private float Threshold = 96.4f;
    [SerializeField] private bool ShowProcessingImage = true;
    [SerializeField] private float CurveAccuracy = 10f;
    [SerializeField] private float MinArea = 5000f;

    private Mat image;
    private Mat processImage = new Mat();
    private Point[][] contours;
    private HierarchyIndex[] hierarchy;

    // red color thresholds
    private Scalar lowerRed1 = new Scalar(0, 120, 70);  // Lower range of red
    private Scalar upperRed1 = new Scalar(10, 255, 255);
    private Scalar lowerRed2 = new Scalar(170, 120, 70); // Upper range of red
    private Scalar upperRed2 = new Scalar(180, 255, 255);
    
    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        image = OpenCvSharp.Unity.TextureToMat(input);

        // do processing

        // convert to grayscale
        // Cv2.Flip(image, image, ImageFlip);
        // Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2GRAY);
        Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2HSV);
        // Cv2.Threshold(processImage, processImage, Threshold, 255, ThresholdTypes.BinaryInv);


        // generate red mask
        Mat mask1 = new Mat();
        Mat mask2 = new Mat();
        Cv2.InRange(processImage, lowerRed1, upperRed1, mask1);
        Cv2.InRange(processImage, lowerRed2, upperRed2, mask2);
        Mat redMask = mask1 | mask2;

        // find (complex) contours
        // Cv2.FindContours(processImage, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, null);
        Cv2.FindContours(redMask, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple, null);

        // simplify contours
        foreach(Point[] contour in contours)
        {
            // Point[] points = Cv2.ApproxPolyDP(contour, CurveAccuracy, true);
            // var area = Cv2.ContourArea(contour);
            

            // if (area > MinArea)
            // {
            //     drawContour(image, new Scalar(0, 255, 255), 2, points);
            // }

            Point[] approx = Cv2.ApproxPolyDP(contour, 0.02*Cv2.ArcLength(contour, true), true);

            if (approx.Length == 4 && Cv2.ContourArea(approx) > 500)
            {
                if (Cv2.IsContourConvex(approx))
                {
                    Cv2.Polylines(image, new[] {approx}, true, new Scalar(0, 255, 0), 3);
                }
            }
        }

        if (output == null)
            output = OpenCvSharp.Unity.MatToTexture(ShowProcessingImage ? processImage : image);
        else
            OpenCvSharp.Unity.MatToTexture(ShowProcessingImage ? processImage : image, output);
        
        return true;
    }

    private void drawContour(Mat Image, Scalar Color, int Thickness, Point[] Points)
    {
        for (int i = 1; i < Points.Length; i++)
        {
            Cv2.Line(Image, Points[i-1], Points[i], Color, Thickness);
        }
        Cv2.Line(Image, Points[Points.Length-1], Points[0], Color, Thickness);
    }
}



