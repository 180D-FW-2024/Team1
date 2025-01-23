using OpenCvSharp;
using OpenCvSharp.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleFinder : WebCamera
{
    [SerializeField] private float MinArea = 1000;
    [SerializeField] private bool ShowProcessingImage = true;
    [SerializeField] private float CurveAccuracy = 0.02f;
    [SerializeField] private FlipMode flipMode;
    //add the ability to flip the camera horiztonally
    public bool flipHorizontal = false;


    public float xPos;
    public float yPos;

    private Mat processImage = new Mat();
    private Mat image;
    private Point[][] contours;
    private HierarchyIndex[] hierarchy;

    // Red color thresholds (in HSV space)
    private readonly Scalar lowerRed1 = new Scalar(0, 120, 70);
    private readonly Scalar upperRed1 = new Scalar(10, 255, 255);
    private readonly Scalar lowerRed2 = new Scalar(170, 120, 70);
    private readonly Scalar upperRed2 = new Scalar(180, 255, 255);

    private readonly Scalar lowerGreen = new Scalar(45, 30, 40);  // Adjust based on green center
    private readonly Scalar upperGreen = new Scalar(85, 255, 220);

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        
        image = OpenCvSharp.Unity.TextureToMat(input);
        //add the ability to flip the camera horiztonally
        Cv2.Flip(image, image, flipMode);
        
        // Convert to HSV color space
        Cv2.CvtColor(image, processImage, ColorConversionCodes.BGR2HSV);

        // Create masks for red color
        Mat mask1 = new Mat();
        Mat mask2 = new Mat();
        Cv2.InRange(processImage, lowerGreen, upperGreen, mask1);
        //Cv2.InRange(processImage, lowerRed2, upperRed2, mask2);
        Mat redMask = mask1 | mask2;

        // Find contours on the red mask
        Cv2.FindContours(redMask, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

        // Iterate through contours
        foreach (Point[] contour in contours)
        {
            // Approximate the contour for better shape representation
            Point[] approx = Cv2.ApproxPolyDP(contour, CurveAccuracy * Cv2.ArcLength(contour, true), true);

            // Check if the contour is a rectangle and meets area requirements
            if (Cv2.IsContourConvex(approx) && Cv2.ContourArea(approx) > MinArea)
            {
                // Draw the rectangle on the original image
                Cv2.Polylines(image, new[] { approx }, true, new Scalar(0, 255, 0), 3);
                

                //set xPos and yPos to the center of the rectangle
                float xSum = 0;
                foreach (Point p in approx)
                {
                    xSum += p.X;
                }
                xPos = xSum / approx.Length;

                float ySum = 0;
                foreach (Point p in approx)
                {
                    ySum += p.Y;
                }
                yPos = ySum / approx.Length;
                Cv2.Circle(image, new Point(xPos, yPos), 5, new Scalar(0, 0, 255), -1);
            }
        }

        // Convert the processed or original image to texture
        if (output == null)
            output = OpenCvSharp.Unity.MatToTexture(ShowProcessingImage ? redMask : image);
        else
            OpenCvSharp.Unity.MatToTexture(ShowProcessingImage ? redMask : image, output);

        // Cleanup temporary matrices
        mask1.Dispose();
        mask2.Dispose();
        redMask.Dispose();

        return true;
    }
}