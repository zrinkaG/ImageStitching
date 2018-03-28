using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace stichingFirstOne
{
    public static class Helper
    {
        public static void SetValue(this Mat mat, int row, int col, dynamic value)
        {
            var target = CreateElement(mat.Depth, value);
            Marshal.Copy(target, 0, mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, 1);
        }
        private static dynamic CreateElement(DepthType depthType, dynamic value)
        {
            var element = CreateElement(depthType);
            element[0] = value;
            return element;
        }

        private static dynamic CreateElement(DepthType depthType)
        {
            if (depthType == DepthType.Cv8S)
            {
                return new sbyte[1];
            }
            if (depthType == DepthType.Cv8U)
            {
                return new byte[1];
            }
            if (depthType == DepthType.Cv16S)
            {
                return new short[1];
            }
            if (depthType == DepthType.Cv16U)
            {
                return new ushort[1];
            }
            if (depthType == DepthType.Cv32S)
            {
                return new int[1];
            }
            if (depthType == DepthType.Cv32F)
            {
                return new float[1];
            }
            if (depthType == DepthType.Cv64F)
            {
                return new double[1];
            }
            return new float[1];
        }

        public static string GetFileName()
        {
            return GetFileName(String.Empty);
        }

        public static string GetFileName(string apendix)
        {
            string path = @"D:\Samples2\Results";

            string[] images = Directory.GetFiles(path, "*.jpg", SearchOption.TopDirectoryOnly);
            int maxValue = 0;

            foreach (string tempFile in images)
            {
                string[] tempTempFile = Path.GetFileNameWithoutExtension(tempFile).Split('_');
                
                if (maxValue < Int32.Parse(tempTempFile[1]) + 1)
                    maxValue = Int32.Parse(tempTempFile[1]) + 1;
            }

            return "D:\\Samples2\\Results\\" + apendix + "_" +  maxValue.ToString() + ".jpg";
        }
    }
}
